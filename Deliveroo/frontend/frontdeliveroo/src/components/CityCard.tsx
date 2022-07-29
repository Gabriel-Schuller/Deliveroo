import React from 'react';
import {Box, Paper, Typography, Stack} from "@mui/material";

interface Props {
    cityName: string;
    cityPicture: string;
}

const CityCard = ({cityName, cityPicture}: Props) => {
    return (
        <Stack spacing={0} direction={"column"} alignItems={"center"}>
            <Paper
                sx={{
                    display: "flex",
                    alignItems: "center",
                    justifyContent: "center",
                    height: 50,
                    width: "400px",
                    minWidth: "100px",
                    pl: 2,
                    bgcolor: "gray",
                    opacity: "90%",
                    border: "1px solid",

                }}
            >
                <Typography color={"white"} sx={{fontStyle: 'oblique'}}>
                    {cityName}
                </Typography>
            </Paper>
            <Box sx={{

                height: "250px",
                width: "400px",
                backgroundImage: `url(${cityPicture})`,
                backgroundSize: "100% 100%"

            }}>

            </Box>
        </Stack>
    );
};

export default CityCard;
