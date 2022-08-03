import React from 'react';
import {Formik, Form, Field, ErrorMessage} from "formik";
import * as Yup from "yup"
import {IUserAccount, IUserProfile} from "../../interfaces";

interface Props {
    initialValues: IUserAccount;
    onSubmit(values: IUserAccount): void;

}

const validationSchema= Yup.object({
    userEmail: Yup.string().email("Invalid email format").required("Required!")
})

const AccountForm = ({initialValues, onSubmit}: Props) => {

    console.log(initialValues)

    return (
        <Formik initialValues={initialValues} onSubmit={onSubmit} validationSchema={validationSchema}>

            <Form>
                <div className={"form-control"}>
                    <label htmlFor={"userName"}>UserName</label>
                    <Field type={"text"} id={"name"} name={"userName"}/>
                    <ErrorMessage name={"name"}/>
                </div>

                <div className={"form-control"}>

                    <label htmlFor={"userEmail"}>E-mail</label>
                    <Field type={"email"} id={"email"} name={"userEmail"}/>
                    <ErrorMessage name={"userEmail"}/>
                </div>

                <div className={"form-control"}>
                    <div>Address</div>

                    <label htmlFor={"city"}>City</label>
                    <Field type={"text"} id={"city"} name={"city"}/>
                    <label htmlFor={"streetName"}>Street</label>
                    <Field type={"text"} id={"street"} name={"streetName"}/>
                    <label htmlFor={"postalCode"}>PostalCode</label>
                    <Field type={"text"} id={"postalCode"} name={"postalCode"}/>

                </div>

                <button type={"submit"}>Change</button>
            </Form>
        </Formik>
    )
};

export default AccountForm;
