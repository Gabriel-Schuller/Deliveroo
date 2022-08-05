import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import {useNavigate, Link} from "react-router-dom";
import axios from "../../helpers/axios/AxiosHelper";
import {useState} from "react";
import {CustomSnackbar} from "./CustomSnackbar";


const theme = createTheme();

export default function SignUp() {
    let navigate = useNavigate();
    const [registerFail, setRegisterFail] = useState(false);
    const [registrationError, setRegistrationError] = useState("");

    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();

        let data = new FormData(event.currentTarget);
        let emailAddress: string = String(data.get("email"));
        let password: string = String(data.get("password"));
        let name: string = String(data.get("name"))
        let user = {EmailAddress: emailAddress, Password: password, UserName: name};
        try {
            const response = await axios.post("api/Users/register", user, {withCredentials: true});
            setRegisterFail(false);
            const loginResponse = await axios.post("api/Users/login", user, {withCredentials: true});
            sessionStorage.setItem("jwtBearer", loginResponse.data);
            sessionStorage.setItem("userEmail", emailAddress);
            navigate("/home");

        } catch (err: any) {
            setRegisterFail(true)
            setRegistrationError(err.response.data)
        }
    };

    return (
        <ThemeProvider theme={theme}>
            <Container component="main" maxWidth="xs">
                <CssBaseline />
                <Box
                    sx={{
                        marginTop: 8,
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                    }}
                >
                    <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                        <LockOutlinedIcon />
                    </Avatar>
                    <Typography component="h1" variant="h5">
                        Sign up
                    </Typography>
                    <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 3 }}>
                        <Grid container spacing={2}>

                            <Grid item xs={12}>
                                <TextField
                                    fullWidth
                                    id="name"
                                    label="Name"
                                    name="name"
                                    autoComplete="name"
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextField
                                    required
                                    fullWidth
                                    id="email"
                                    label="Email Address"
                                    name="email"
                                    autoComplete="email"
                                />
                            </Grid>
                            <Grid item xs={12}>
                                <TextField
                                    required
                                    fullWidth
                                    name="password"
                                    label="Password"
                                    type="password"
                                    id="password"
                                    autoComplete="new-password"
                                />
                            </Grid>

                        </Grid>
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                        >
                            Sign Up
                        </Button>
                        <Grid container justifyContent="flex-end">
                            <Grid item>
                                <Link to={"/signin"}>
                                    Already have an account? Sign in
                                </Link>
                            </Grid>
                        </Grid>
                    </Box>
                </Box>
            </Container>

            <CustomSnackbar open={registerFail} message={registrationError} color={"error"}/>


        </ThemeProvider>
    );
}