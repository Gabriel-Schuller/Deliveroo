import React from 'react';
import './App.css';
import {TestNavigationBar} from './testcomponents/TestNavigationBar';
import NavigationBar from "./components/navigation/NavigationBar";
import HomePage from "./components/pages/HomePage";
import {Box} from "@mui/material";
import {Routes, Route, useParams} from "react-router-dom";
import SignIn from "./components/navigation/SigngIn";
import SignUp from "./components/navigation/SignUp";
import Account from "./components/pages/Account";
import ErrorPage from "./components/pages/ErrorPage";
import TestComponentFormik from "./testcomponents/TestComponentFormik";
import Logout from "./components/navigation/Logout";
import Ship from "./components/pages/Ship";
import OrderComplete from "./components/pages/OrderComplete";

function App() {

    let {orderId} = useParams()
    return (
        <Box>
            <NavigationBar/>
            <Routes>

                <Route path={"/"} element={<HomePage/>}></Route>
                <Route path={"/home"} element={<HomePage/>}></Route>
                <Route path={"/signin"} element={<SignIn/>}></Route>
                <Route path={"/signup"} element={<SignUp/>}></Route>
                <Route path={"/account"} element={<Account/>}></Route>
                <Route path={"/logout"} element={<Logout/>}></Route>
                <Route path={"/ship"} element={<Ship/>}></Route>
                <Route path={"/order-complete/:orderId"} element={<OrderComplete/>}></Route>


                <Route path={"*"} element={<HomePage/>}></Route>

            </Routes>
        </Box>
    );
}

export default App;
