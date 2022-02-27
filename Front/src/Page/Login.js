import React from "react";
import { Link, useHistory } from 'react-router-dom';
import { useState } from "react";
import axios from "axios";
import { Redirect } from "react-router";
import Cookies from 'universal-cookie';

const Login = (props) => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [redirect, setRedirect] = useState(false);
    const cookies = new Cookies();
    const [SuccessfullMessage, setSuccessfullMessage] = useState('');
    const submit = async (e) => {
        e.preventDefault()
        const response = await fetch("https://localhost:44352/api/auth/login", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            credentials: "include",
            body: JSON.stringify({
                email,
                password
            })
        }).catch(error => { setSuccessfullMessage(error.response.data.message) });

        const content = await response.json();
        if (content.status == "Error") {
            setSuccessfullMessage(content.message);
            
            setTimeout(function () {
                setSuccessfullMessage('')
            }, 4000);
        }
        else {
            setRedirect(true)
            content.email = email;

            props.setName(content.email);
            cookies.set('Token', content.token, { path: '/' });
        }
    }
    if (redirect) {
        return <Redirect to="/Writing" />
    }
    return (
        <div className="container">
            <div className="row text-center pt-5">
                <div className="col-md-6 d-none d-sm-block d-sm-none d-md-block">
                    <h1 className="display-1">Gamehub</h1>
                    <hr />
                    <h2>Sign In to write your own review.</h2>
                    <h3 className="lh-lg">You can write your own review and post it on Gamehub. Your reviews help us to have a great definition from a game.</h3>

                </div>
                <div className="col-lg-5 offset-lg-1 col-md-6 pt-4 shadow">
                    <h1>Sign In</h1>
                    <div className="display-1">
                        <i className="bi bi-people-fill text-info"></i>
                    </div>
                    <div>{SuccessfullMessage}</div>
                    <form onSubmit={submit}>
                        <div className="p-4 m-5 justify-content-center">
                            <div className="form-group pb-3">
                                <input type="email" onChange={e => setEmail(e.target.value)} className="form-control" placeholder="Email" required />
                            </div>
                            <div className="form-group pb-3">
                                <input type="password" onChange={e => setPassword(e.target.value)} className="form-control" placeholder="Password" required />
                            </div>
                            <div className="form-group">
                                <div className="row">
                                    <div className="col-md-6 col-sm-6">
                                        <button type="submit" className="btn btn-primary">Login</button>
                                    </div>
                                    <div className="col-md-6 col-sm-6">
                                        <Link to="./Register" className="btn btn-success">Register</Link>
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
export default Login;