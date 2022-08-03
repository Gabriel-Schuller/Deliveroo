import React, {useEffect, useState} from 'react';
import {Formik, Form, Field, ErrorMessage} from "formik";
import * as Yup from "yup"
import axios from "axios";
import {IAdress, IUserAccount, IUserInfo, IUserProfile} from "../../interfaces";
import AccountForm from "../formcomponents/AccountForm";
import ErrorPage from "./ErrorPage";
import CircularProgress from "@mui/material/CircularProgress";


const Account = () => {

    const [profile, setProfile] = useState<IUserAccount | null>(null);

    // @ts-ignore
    useEffect(() => {
        return async () => {
            let userEmail = sessionStorage.getItem("userEmail");
            if (userEmail !== null) {
                let response = await axios.get(`https://localhost:44338/email/${userEmail}`);
                let user = response.data;
                let userInfo: IUserInfo = {
                    userName: user.userName,
                    userEmail: user.emailAddress,
                    userPassword: user.password
                };
                let addressId = user.addressID;
                let addressResponse = await axios.get(`https://localhost:44338/api/Address/${addressId}`);
                let userAddress: IAdress = addressResponse.data;
                let userAccount: IUserAccount = {...userAddress, ...userInfo}
                setProfile(userAccount);

            }
        };
    }, []);

    const onSubmit = (values: IUserAccount) => {
        console.log(values)
    }


    return (
        <div>
            {profile ? <AccountForm initialValues={profile} onSubmit={onSubmit}/> :
                <CircularProgress color={"secondary"}/>
            }
        </div>
    );
};

export default Account;
