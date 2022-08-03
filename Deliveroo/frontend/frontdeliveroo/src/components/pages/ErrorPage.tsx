import React from 'react';
import CircularProgress from '@mui/material/CircularProgress';

const ErrorPage = () => {
    return (
        <div>
            <CircularProgress color={"secondary"} sx={{position: "relative" ,top: 90}}/>
        </div>
    );
};

export default ErrorPage;
