import {Grid, Box, Paper} from '@mui/material';
import React from 'react';
import CityCard from "./CityCard";
import {ICity} from "../interfaces";
import {stringify} from "querystring";
import london from "../images/london.jpg"
import edinburgh from "../images/edinburgh.jpg"
import liverpool from "../images/liverpool.jpg"
import manchester from "../images/manchester.jpg"
import perth from "../images/perth.jpg"


const cities: ICity[] = [
    {name: "London", image: london}, {name: "Edinburgh", image: edinburgh}, {name: "Liverpool", image: liverpool},
    {name: "Manchester", image:manchester}, {name: "Perth", image:perth},
]


const CityGrid = () => {
    return (
        <Paper sx={{padding: '15px', bgcolor: "#DCDCDC"}} elevation={2}>
            <Grid container spacing={4} justifyContent={"center"}>

                {cities.map((city: ICity, key: number) => {
                    return (
                        <Grid key={key} item xs={6} sm={2} md={4} lg={4} xl={3}>
                            <CityCard cityName={city.name} cityPicture={city.image}/>
                        </Grid>
                    )
                })}

            </Grid>
        </Paper>
    );
};

export default CityGrid;
