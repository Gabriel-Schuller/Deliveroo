import React from 'react';
import {Box, Stack, Typography} from "@mui/material";
import {Field} from "formik";
import {TextField} from "formik-mui";

interface Props {
    title:string;
    required:boolean;
}

const AddressForm = ({title, required}:Props) => {
    return (
        <Box sx={{width: "100%"}}>
            <Typography variant={"caption"} gutterBottom>{title}</Typography>
            <Stack spacing={2} direction={"column"} alignItems={"center"}>

                <Field component={TextField} label={"City"} type={"text"} id={"city"}
                       variant={"filled"} name={"city"} required={required}
                       fullWidth sx={{maxWidth: "300px"}}/>
                <Field component={TextField} label={"Street"} type={"text"} id={"street"}
                       variant={"filled"} name={"streetName"} required={required}
                       fullWidth sx={{maxWidth: "300px"}}/>
                <Field component={TextField} label={"Postal Code"} type={"text"}
                       variant={"filled"} id={"postalCode"} name={"postalCode"} required={required}
                       fullWidth sx={{maxWidth: "150px"}}/>
            </Stack>
        </Box>
    );
};

export default AddressForm;
