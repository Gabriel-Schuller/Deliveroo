import {Snackbar, Button, AlertColor} from '@mui/material'
import Alert from '@mui/material/Alert';
import MuiAlert from '@mui/material/Alert';
import { useState, forwardRef } from 'react'

interface Props {
    open: boolean;
    message: string;
    color: AlertColor;
}

export const CustomSnackbar = ({open, message, color} : Props) => {

    return (
        <>
            <Snackbar
                open={open}
                autoHideDuration={200}
                message= {message}
                anchorOrigin={{ vertical: 'bottom', horizontal: 'center' }}
            >
                <Alert severity={color}>
                    {message}
                </Alert>
            </Snackbar>
        </>
    )
}
