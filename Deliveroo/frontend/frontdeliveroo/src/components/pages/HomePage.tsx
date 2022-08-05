import React from 'react';
import {Box, Button, Grid, Paper, Stack, Typography} from "@mui/material";
import LocalPostOfficeIcon from '@mui/icons-material/LocalPostOffice';
import RocketLaunchIcon from '@mui/icons-material/RocketLaunch';
import CityGrid from "../CityGrid";
import {useNavigate} from "react-router-dom";

const picture1 = "https://www.ups.com/assets/resources/webcontent/images/business-shipping-B-1187659-Q421.jpg"


const HomePage = () => {
    const navigate=useNavigate();
    return (
        <Stack spacing={4} direction={"column"} alignItems={"center"}>
            <Paper sx={{padding: '32px'}} elevation={2}>

                <Box position={"relative"} height="650px" sx={{
                    backgroundImage: `url("${picture1}")`,
                    backgroundSize: "100% 100%",
                    opacity: "95%",
                }}>
                    <Stack spacing={8} direction={"column"} alignItems={"center"}>
                        <Box></Box>
                        <Box sx={{
                            backgroundColor: "black",
                            opacity: "50%"
                        }}>
                            <Typography variant={"h2"}
                                        sx={{
                                            // background: "-webkit-linear-gradient(45deg, #00FF00 0%, #00FFFF 100%)",
                                            background: "-webkit-linear-gradient(45deg, #D1E0FF 10%, #BD7A00 100%)",
                                            WebkitBackgroundClip: "text",
                                            WebkitTextFillColor: "transparent",
                                            fontWeight: "bold"
                                        }}>
                                Romania - England - Scotland
                            </Typography>
                        </Box>
                        <Box sx={{
                            width: "70%",
                            backgroundColor: "black",
                            opacity: "80%",
                            color: "#F0F8FF",
                            fontFamily: 'Monospace',
                        }}>
                            <Typography variant={"h5"}
                                        sx={{
                                            fontFamily: 'Monospace',
                                        }}>
                                Shippy is a startup that aims to make it possible to send packages through our couriers.
                                The
                                difference between Shippy and a recognized site like DHL is that Shippy charges a
                                reduced
                                fee
                                compared to them, using a system similar to Uber or Bla Bla Car.
                            </Typography>
                            <br/>
                            <Typography variant={"h6"} sx={{
                                fontFamily: 'Monospace',
                            }}>
                                The packages are left at the collection point and then delivered to one of the big
                                cities on
                                our list. For a small additional cost, they can be delivered directly to the address, as
                                long as it is within a 30 mile radius.
                            </Typography>
                        </Box>
                        <Button color={"warning"} size={"large"} variant="contained" endIcon={<RocketLaunchIcon/>}
                        onClick={()=> navigate("/ship")}>
                            Ship now
                        </Button>
                    </Stack>
                </Box>
            </Paper>
            <Box sx={{paddingTop: "1%", paddingBottom: "1%"}}>
                <Typography fontWeight={"bold"} variant={"h3"} color={"cadetblue"}>
                    Cities
                </Typography>
            </Box>
            <CityGrid/>

        </Stack>
    );
};

export default HomePage;
