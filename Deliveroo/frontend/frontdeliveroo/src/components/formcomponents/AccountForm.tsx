import React from 'react';
import {Formik, Form, Field} from "formik";
import * as Yup from "yup"
import {IUserAccount} from "../../interfaces";
import {Box, Button, Paper, Stack, Typography} from "@mui/material";
import {TextField} from "formik-mui";
import ChangeCircleIcon from '@mui/icons-material/ChangeCircle';
import AddressForm from "./AddressForm";

interface Props {
    initialValues: IUserAccount;

    onSubmit(values: IUserAccount): void;

}

const validationSchema = Yup.object({
    userEmail: Yup.string().email("Invalid email format!").required("E-mail required!"),
})

const AccountForm = ({initialValues, onSubmit}: Props) => {

    return (

        <Box sx={{paddingTop: "3%", paddingBottom: "1%"}}>
            <Formik initialValues={initialValues} onSubmit={onSubmit} validationSchema={validationSchema}>

                <Form>
                    <Stack alignItems={"center"}>
                        <Paper sx={{padding: '22px', width: "450px"}} elevation={12}>
                            <Stack spacing={2} direction={"column"} alignItems={"center"}>
                                <Typography variant={"subtitle1"} fontWeight={"bold"} color={"cadetblue"}>
                                    User Info
                                </Typography>
                                <Field component={TextField} label={"Username"} type={"text"} id={"name"}
                                       variant={"filled"} name={"userName"}
                                       fullWidth sx={{maxWidth: "300px"}}/>


                                <Field required component={TextField} label={"E-mail"} type={"email"} id={"email"}
                                       variant={"filled"} name={"userEmail"}
                                       fullWidth sx={{maxWidth: "300px"}}/>

                                <Field component={TextField} label={"Phone"} type={"text"} id={"userPhoneNumber"}
                                       variant={"filled"} name={"userPhoneNumber"}
                                       fullWidth sx={{maxWidth: "300px"}}/>

                                <AddressForm title={"Main Address"} required={false}/>

                                <Button type={"submit"} color={"secondary"} size='large'
                                        variant={"contained"} endIcon={<ChangeCircleIcon/>}
                                >
                                    Change
                                </Button>

                            </Stack>
                        </Paper>
                    </Stack>
                </Form>
            </Formik>
        </Box>
    )
};

export default AccountForm;
