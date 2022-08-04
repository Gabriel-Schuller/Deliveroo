import {
    Button,
    Dialog,
    DialogTitle,
    DialogContent,
    DialogContentText,
    DialogActions
} from '@mui/material'
import { useState } from 'react'
import {useNavigate} from "react-router-dom";

const Logout = () => {
    const [open, setOpen] = useState(true)
    const navigate= useNavigate();

    const logout = () => {
    sessionStorage.removeItem("jwtBearer");
    sessionStorage.removeItem("userEmail");
        setOpen(false);
        navigate("/");
    }

    return (
        <>
            <Dialog
                open={open}
                onClose={() => {setOpen(false); navigate(-1)}}
                aria-labelledby='dialog-title'
                aria-describedby='dialog-description'>
                <DialogTitle id='dialog-title'>LOG-OUT</DialogTitle>
                <DialogContent>
                    <DialogContentText id='dialog-description'>
                        Are you sure you want to Logout?
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={() => {setOpen(false); navigate(-1)}}>Cancel</Button>
                    <Button onClick={logout} autoFocus>
                        Logout
                    </Button>
                </DialogActions>
            </Dialog>
        </>)
};

export default Logout;
