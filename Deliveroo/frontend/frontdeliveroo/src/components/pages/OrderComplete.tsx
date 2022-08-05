import React, {useEffect} from 'react';
import {useParams} from "react-router-dom";
import {Paper, Stack, Box, Typography, TextField, InputAdornment} from "@mui/material";
import axios from "../../helpers/axios/AxiosHelper";

const OrderComplete = () => {
    let params = useParams();

    useEffect((): any => {
        return async () => {
            let orderId = params.orderId;
            let response = await axios.get(`/api/Order/${orderId}`)
            console.log(response.data)
        };
    }, []);

    return (
        <Stack alignItems={"center"}>
            <Paper sx={{padding: '22px', borderRadius: "30px", width: "800px", position: "relative", top: "55px"}}
                   elevation={12}>
                <Stack spacing={6} alignItems={"center"} direction={"column"}>
                    <Box sx={{width: "600px"}}>
                        <Typography variant={"h3"} sx={{fontWeight: 800}}>
                            Thank you for your order
                        </Typography>
                        <Typography variant={"subtitle1"}>
                            You will be contacted soon by one of our representatives
                        </Typography>
                    </Box>
                    <Stack spacing={5} direction={"column"}>
                        <Typography variant={"overline"}>Order Details</Typography>
                        <Stack spacing={4} direction={"row"} justifyContent={"center"}>
                            <TextField disabled color={"secondary"} variant={"filled"} value={"2"}
                                       label={"Packages to ship:"}/>
                            <TextField disabled color={"secondary"} variant={"filled"} value={"2"}
                                       label={"Total weight:"}/>
                            <TextField disabled color={"secondary"} value={"200"}
                                       label={"COST"} sx={{maxWidth: "100px"}} required
                                       InputProps={{
                                           endAdornment: <InputAdornment position='start'>£</InputAdornment>
                                       }}
                            />
                        </Stack>
                        <Stack spacing={4}>
                            <Typography variant={"caption"}>Address Details</Typography>
                            <Stack spacing={4} direction={"row"}>
                                <TextField disabled color={"secondary"} variant={"filled"} value={"Edinburgh"}
                                           label={"City:"}/>
                                <TextField disabled color={"secondary"} variant={"filled"} value={"Dumbrava nr 18"}
                                           label={"Street:"}/>
                                <TextField disabled color={"secondary"} variant={"filled"} value={"235400"}
                                           label={"Postal Code:"} sx={{maxWidth: "170px"}}/>
                            </Stack>
                            <Stack spacing={8} direction={"row"} alignItems={"center"} justifyContent={"center"}>
                                <TextField disabled color={"secondary"} variant={"filled"} value={"2022-08-05, 15:57"}
                                           label={"Order date:"}/>
                                <TextField disabled color={"secondary"} variant={"filled"} value={"0748957450"}
                                           label={"Phone Number:"}/>

                            </Stack>
                        </Stack>
                    </Stack>
                </Stack>
            </Paper>
        </Stack>
    );
};

export default OrderComplete;
