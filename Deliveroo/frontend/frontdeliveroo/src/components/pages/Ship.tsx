import {Formik, Form, Field} from "formik";
import * as Yup from "yup"
import {IOrder, IAdress, IOrderForm, IUserAccount} from "../../interfaces";
import {Box, Button, Paper, Stack, Typography} from "@mui/material";
import {TextField, CheckboxWithLabel} from "formik-mui";
import ChangeCircleIcon from '@mui/icons-material/ChangeCircle';
import React, {useState} from "react";
import MailOutlineIcon from '@mui/icons-material/MailOutline';
import AddressForm from "../formcomponents/AddressForm";
import axios from "../../helpers/axios/AxiosHelper";
import {useNavigate} from "react-router-dom";


const validationSchema = Yup.object({
    contactPhoneNumber: Yup.string().min(10, "Phone Number must be 10(RO) or 11(UK) digits!")
        .max(11, "Phone Number must be 10(RO) or 11(UK) digits!").required("Required"),
    numberOfBaggages: Yup.number().positive().integer().min(1, "Must be between 1-10")
        .max(10, "Must be between 1-10").required("Required"),
    totalWeight: Yup.number().positive().integer().min(5, "Must be between 5-200")
        .max(200, "Must be between 5-200").required("Required"),
    city: Yup.string().min(2, "Quite the short city.. Check please").required("Required"),
    postalCode: Yup.string().min(2, "Too short!").required("Required"),
    streetName: Yup.string().min(2, "Quite a short Street name.. Check please").required("Required")
})


const Ship = () => {
    const navigate= useNavigate();
    const [checked, setChecked] = useState(false);

    const initialValues: IOrderForm = {
        numberOfBaggages: 1,
        totalWeight: 5,
        contactPhoneNumber: "",
        city: "RO",
        postalCode: "00",
        streetName: "00",
    }


    const submit = async (values: IOrderForm) => {
        let userEmail = sessionStorage.getItem("userEmail");
        let addressId="";
        if (checked) {
            let response = await axios.get(`email/${userEmail}`);
            addressId=response.data.addressID;
        } else {
            let address: IAdress = {city: values.city, postalCode: values.postalCode, streetName: values.streetName};
            let response = await axios.post("api/Address", address, {withCredentials: true});
            addressId=response.data.addressID;
        }

        let order:IOrder = {numberOfBaggages: values.numberOfBaggages, totalWeight: values.totalWeight,
        contactPhoneNumber: values.contactPhoneNumber, addressID: addressId};

        let orderResponse= await axios.post(`api/Order/${userEmail}`, order, {withCredentials: true});

    }

    return (
        <Box sx={{paddingTop: "3%", paddingBottom: "1%"}}>
            <Stack alignItems={"center"}>
                <Formik initialValues={initialValues} onSubmit={submit} validationSchema={validationSchema}>
                    <Form>
                        <Paper sx={{padding: '22px', width: "450px"}} elevation={12}>
                            <Stack spacing={2} direction={"column"} alignItems={"center"}>
                                <Typography variant={"subtitle1"} fontWeight={"bold"} color={"cadetblue"}>
                                    Baggage Info
                                </Typography>
                                <Field component={TextField} label={"Number of Baggages"} type={"number"}
                                       id={"numberOfBaggages"} required
                                       variant={"filled"} name={"numberOfBaggages"} helperText={"Between 1-10"}
                                       fullWidth sx={{maxWidth: "300px"}}/>


                                <Field component={TextField} label={"Total Weight"} type={"number"}
                                       id={"totalWeight"} required
                                       variant={"filled"} name={"totalWeight"} helperText={"Between 5-200kg"}
                                       fullWidth sx={{maxWidth: "300px"}}/>

                                <Field component={TextField} label={"Phone Number"} type={"text"}
                                       id={"contactPhoneNumber"} required placeholder={"Without country code"}
                                       variant={"filled"} name={"contactPhoneNumber"}
                                       fullWidth sx={{maxWidth: "300px"}}/>

                                <Field component={CheckboxWithLabel} type="checkbox" name="checked"
                                       Label={{label: 'Use account main Address?'}}
                                       checked={checked} onClick={() => setChecked(prevState => !prevState)}
                                />

                                {!checked && <AddressForm title={"Shipping Address"} required={true}/>}


                                <Button type={"submit"} color={"secondary"} size='large'
                                        variant={"contained"} endIcon={<MailOutlineIcon/>}
                                >
                                    Ship
                                </Button>


                            </Stack>

                        </Paper>
                    </Form>

                </Formik>
            </Stack>
        </Box>
    );
};

export default Ship;
