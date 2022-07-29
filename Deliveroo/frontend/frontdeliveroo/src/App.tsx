import React from 'react';
import './App.css';
import {TestNavigationBar} from './testcomponents/TestNavigationBar';
import NavigationBar from "./components/navigation/NavigationBar";
import HomePage from "./components/pages/HomePage";
import {Box} from "@mui/material";

function App() {
    return (
        <Box>
            <NavigationBar/>
            <HomePage/>
        </Box>
    );
}

export default App;
