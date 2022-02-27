import React, { useState } from "react";
import axios from "axios";
import { useHistory } from 'react-router';
import { Link } from "react-router-dom";
import $ from 'jquery';
const Register = () => {
    let history = useHistory();
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [ConfirmPassword, setConfirmPassword] = useState('');
    const [EmailErr, setEmailErr] = useState({})
    const [PasswordErr, setPasswordErr] = useState({})
    const [ConfirmPasswordErr, setConfirmPasswordErr] = useState('');
    const [SuccessfullMessage, setSuccessfullMessage] = useState('');
    const [Loading, setLoading] = useState(false)
    const submit = (e) => {
        e.preventDefault()
        const isValid = formValidation();
        if (isValid) {
            axios.post("https://localhost:44352/api/auth/register", {
                email, password,

            }).then(setLoading(true)).then((response) => {setSuccessfullMessage(response.data.message)
                setLoading(false)
            }).then((response) => setTimeout(function () {
                setSuccessfullMessage('')
                
            }, 3000)).catch(error => { setSuccessfullMessage(error.response.data.message);setLoading(false) })

          
        }
    }

    const formValidation = () => {
        const emailErr = {};
        const passwordErr = {};
        const confirmPasswordErr = {};
        let isValid = true;
        if (email.length <= 0) {
            emailErr.errLenghtMessage = "Email is required.";
            isValid = false;
        }
        const reg = /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;

        if (!reg.test(email)) {
            emailErr.errLenghtMessage = "Email is invalid.";
            isValid = false;
        }
        if (email.trim().length < 5) {
            emailErr.errLenghtMessage = "Email is too short.";
            isValid = false;
        }
        if (email.trim().length > 50) {
            emailErr.errLenghtMessage = "Email is too long.";
            isValid = false;
        }
        if (password.trim().length < 6) {
            passwordErr.errLenghtMessage = "Password is too short.";
            isValid = false;
        }
        if (password.trim().length > 16) {
            passwordErr.errLenghtMessage = "Password is too long.";
            isValid = false;
        }
        if (ConfirmPassword.trim().length < 6) {
            confirmPasswordErr.errLenghtMessage = "Confirm password is too short.";
            isValid = false;
        }
        if (ConfirmPassword.trim().length > 16) {
            confirmPasswordErr.errLenghtMessage = "Confirm password is too long.";
            isValid = false;
        }
        if (password != ConfirmPassword) {
            passwordErr.errLenghtMessage = "Password and confirm password are not equal.";
        }

        setEmailErr(emailErr);
        setPasswordErr(passwordErr);
        setConfirmPasswordErr(confirmPasswordErr);
        return isValid;
    }
    return (
        <div className="container">
            <div className="row text-center  pt-5">
                <div className="col-md-6 d-none d-sm-block d-sm-none d-md-block">
                    <h1 className="display-1">Gamehub</h1>
                    <hr />
                    <h2>Sign up to write your own review.</h2>
                    <h3 className="lh-lg">You can write your own review and post it on Gamehub. Your reviews help us to have a great definition from a game.</h3>

                </div>
                <div className="col-lg-5 offset-lg-1 col-md-6 pt-5 shadow">
                    <h1>Sign Up</h1>
                    <div className="display-1">
                        <i className="bi bi-people-fill text-info"></i>
                    </div>
                    <div>
                        {Loading ? <div className="row">
                            <div className="container">
                                <div className="d-flex justify-content-center pt-3">
                                    <div className="spinner-border text-info" role="status">
                                        <span className="visually-hidden">Loading...</span>
                                    </div>
                                </div>
                            </div>
                        </div> : <div>{ SuccessfullMessage }</div>}
                    </div>
                    <form onSubmit={submit}>
                        <div className="p-5 m-5 justify-content-center">
                            <div className="form-group pb-3">
                                <input type="email" onChange={e => setEmail(e.target.value)} className="form-control" placeholder="Email" required />
                                {Object.keys(EmailErr).map((key) => {
                                    return <div className="text-danger fw-bold">{EmailErr[key]}</div>
                                })}
                            </div>
                            <div className="form-group pb-3">
                                <input type="password" onChange={e => setPassword(e.target.value)} className="form-control" placeholder="Password" required />
                                {Object.keys(PasswordErr).map((key) => {
                                    return <div className="text-danger fw-bold">{PasswordErr[key]}</div>
                                })}
                            </div>
                            <div className="form-group pb-3">
                                <input type="password" onChange={e => setConfirmPassword(e.target.value)} className="form-control" placeholder="Confirm Password" required />
                                {Object.keys(ConfirmPasswordErr).map((key) => {
                                    return <div className="text-danger fw-bold">{ConfirmPasswordErr[key]}</div>
                                })}
                            </div>
                            <div className="form-group">
                                <div className="row">
                                    <div className="col-md-6 col-sm-6">
                                        <input type="submit" className="btn btn-primary" value="Register" />
                                    </div>
                                    <div className="col-md-6 col-sm-6">
                                        <Link to="./Login" className="btn btn-success">Login</Link>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>

    )
}
export default Register;