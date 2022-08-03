import React, {useEffect, useState} from 'react';
import {Formik, Form, Field, ErrorMessage} from "formik";
import * as Yup from "yup"
import axios from "axios";
import {IAdress, IUserAccount, IUserInfo, IUserModel, IUserProfile} from "../../interfaces";
import AccountForm from "../formcomponents/AccountForm";
import ErrorPage from "./ErrorPage";
import CircularProgress from "@mui/material/CircularProgress";
import {useNavigate} from "react-router-dom";


const Account = () => {

    const [profile, setProfile] = useState<IUserAccount | null>(null);
    const [redirect, setRedirect] = useState<boolean>(false);
    const [addressId, setAddressId] = useState<string>("");
    const [userId, setUserId] = useState("");
    const [submit, setSubmit] = useState(true);
    const navigate = useNavigate();

    // @ts-ignore
    useEffect(() => {
        return async () => {
            let userEmail = sessionStorage.getItem("userEmail");
            if (userEmail !== null) {
                let response = await axios.get(`https://localhost:44338/email/${userEmail}`);
                let user = response.data;
                setUserId(user.userID);
                setAddressId(user.addressID);
                let userInfo: IUserInfo = {
                    userName: user.userName || "",
                    userEmail: user.emailAddress,
                    userPassword: user.password,
                    userPhoneNumber: user.userPhoneNumber || ""
                };
                let neededId = user.addressID;
                let addressResponse = await axios.get(`https://localhost:44338/api/Address/${neededId}`);
                let userAddress: IAdress = addressResponse.data;
                let userAccount: IUserAccount = {...userAddress, ...userInfo}
                setProfile(userAccount);
            } else {
                setRedirect(true)
                navigate("/signin")
            }
        };
    }, [redirect, submit]);

    const onSubmit = async (values: IUserAccount) => {
        let address: IAdress = {city: values.city, postalCode: values.postalCode, streetName: values.streetName}
        if (profile) {
            if (address.city !== profile.city || address.streetName !== profile.streetName
                || address.postalCode !== profile.postalCode) {
                let addressResponse = axios.put(`https://localhost:44338/api/Address/${addressId}`, address, {withCredentials: true})
            }

            let userInfo: IUserModel = {
                userName: values.userName,
                emailAddress: values.userEmail,
                password: values.userPassword,
                userPhoneNumber: values.userPhoneNumber
            }
            if (userInfo.userName !== profile.userName || userInfo.emailAddress !== profile.userEmail
                || userInfo.userPhoneNumber !== profile.userPhoneNumber) {
                let userResponse = axios.put(`https://localhost:44338/api/Users/${userId}`, userInfo, {withCredentials: true})
            }
            setSubmit(!submit)
        }
    }


    return (
        <div>
            {profile ? <AccountForm initialValues={profile} onSubmit={onSubmit}/> :
                <CircularProgress color={"secondary"} sx={{position: "relative", top: 90}}/>
            }
        </div>
    );
};

export default Account;
