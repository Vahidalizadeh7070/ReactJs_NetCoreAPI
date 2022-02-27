import React, { useState, useEffect } from "react";
import { useLocation } from "react-router-dom";
import axios from "axios";
const ConfirmEmail = () => {
    const search = useLocation().search;
    const invalidtoken = search.split("&")[0];
    const token = invalidtoken.split("?")[1];
    const invaliduser = search.split("&")[1];
    const user = invaliduser.split("?")[1];
    const [Message, setMessage] = useState('')
    useEffect(() => {
        (
            async () => {
                axios.get("https://localhost:44352/api/auth/ConfirmEmail?token=" + invalidtoken.split("?")[1] + "&userId=" + invaliduser.split("?")[1],).then(
                    (response) => {
                        setMessage(response.data.message)
                    }
                ).catch((error)=>setMessage(error.response.data.message))

            }
        )();

    })

    return (
        <div className="container text-center">
            <div className="col-md-9 offset-md-2 mt-5 mb-5 p-5 shadow">
                <h1 className="text-success">Email Confirmation</h1>
                <hr/>
                <h2 className="fw-bold display-2 text-secondary"> {Message}</h2>
            </div>
        </div>
    )
};
export default ConfirmEmail;