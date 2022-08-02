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
import {createTheme, ThemeProvider} from '@mui/material/styles';
import {useNavigate, Link} from "react-router-dom";
import axios from "axios";
import {useState} from "react";
import {CustomSnackbar} from "./CustomSnackbar";


const theme = createTheme();

export default function SignIn() {
    let navigate = useNavigate();
    const [loginFail, setLoginFail] = useState(false);

    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        let data = new FormData(event.currentTarget);
        let emailAddress: string = String(data.get("email"));
        let password: string = String(data.get("password"));
        let user = {EmailAddress: emailAddress, Password: password};
        try {
            const response = await axios.post("https://localhost:44338/api/Users/login", user, {withCredentials: true});
            setLoginFail(false);
            sessionStorage.setItem("jwtBearer", response.data);
            sessionStorage.setItem("userEmail", emailAddress);
            navigate("/home");

        } catch (err: any) {
            setLoginFail(true)
        }

    };


    return (
        <ThemeProvider theme={theme}>
            <Container component="main" maxWidth="xs">
                <CssBaseline/>
                <Box
                    sx={{
                        marginTop: 8,
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center',
                    }}
                >
                    <Avatar sx={{m: 1, bgcolor: 'secondary.main'}}>
                        <LockOutlinedIcon/>
                    </Avatar>
                    <Typography component="h1" variant="h5">
                        Sign in
                    </Typography>
                    <Box component="form" onSubmit={handleSubmit} noValidate sx={{mt: 1}}>
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            id="email"
                            label="Email Address"
                            name="email"
                            autoComplete="email"
                            autoFocus
                        />
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            name="password"
                            label="Password"
                            type="password"
                            id="password"
                            autoComplete="current-password"
                        />
                        <FormControlLabel
                            control={<Checkbox value="remember" color="primary"/>}
                            label="Remember me"
                        />
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            sx={{mt: 3, mb: 2}}
                        >
                            Sign In
                        </Button>
                        <Grid container>
                            <Grid item xs>
                                <Link to={"/forgotpassword"}>
                                    Forgot password?
                                </Link>
                            </Grid>
                            <Grid item>
                                <Link to={"/signup"}>
                                    {"Don't have an account? Sign Up"}
                                </Link>
                            </Grid>
                        </Grid>
                    </Box>
                </Box>

            </Container>

            <CustomSnackbar open={loginFail} message={"Wrong Username or Passowrd!!"} color={"warning"}/>

        </ThemeProvider>
    );
}