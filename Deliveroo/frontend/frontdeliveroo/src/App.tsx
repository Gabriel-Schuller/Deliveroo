import React from 'react';
import './App.css';
import {TestNavigationBar} from './testcomponents/TestNavigationBar';
import NavigationBar from "./components/navigation/NavigationBar";
import HomePage from "./components/pages/HomePage";
import {Box} from "@mui/material";
import {Routes, Route} from "react-router-dom";
import SignIn from "./components/navigation/SigngIn";
import SignUp from "./components/navigation/SignUp";

function App() {
    return (
        <Box>
            <NavigationBar/>
            <Routes>

                <Route path={"/"} element={<HomePage/>}></Route>
                <Route path={"/home"} element={<HomePage/>}></Route>
                <Route path={"/signin"} element={<SignIn/>}></Route>
                <Route path={"/signup"} element={<SignUp/>}></Route>

                <Route path={"*"} element={<HomePage/>}></Route>

            </Routes>
        </Box>
    );
}

export default App;
