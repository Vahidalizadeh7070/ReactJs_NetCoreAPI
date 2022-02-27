import React, { useState } from 'react';
import { Link } from 'react-router-dom'
import axios from 'axios';
import Cookies from 'universal-cookie';

const Nav = (props) => {
  const logout = async () => {
    const cookies = new Cookies();
    cookies.remove('Token');
    await fetch("https://localhost:44352/api/logout", {
      method: "POST",
      headers: { 'Content-Type': 'application/json' },
      credentials: 'include'
    });
    props.setName('');
  }
  
  let menu;
  if (props.email === undefined || props.email==="") {
    menu = (
      <ul className="navbar-nav me-auto mb-lg-0">
        <li className="nav-item">
          <Link className="nav-link active" aria-current="page" to="/register">Register</Link>
        </li>
        <li className="nav-item">
          <Link className="nav-link" to="/login">Login</Link>
        </li>
      </ul>
    )
  }
  else {
    menu = (
      <ul className="navbar-nav me-auto mb-lg-0">
        <li className="nav-item">
          <p className="nav-link">Hi {props.email} </p>
        </li>
        <li className="nav-item">
          <Link to="/" className="nav-link" onClick={logout}>Logout</Link>
        </li>
      </ul>
    )
  }
  return (
    <nav className="navbar navbar-expand-md navbar-light bg-light">
      <div className="container">
        <Link className="navbar-brand" to="/">Gamehub</Link>
        <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent"
          aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
          <span className="navbar-toggler-icon"></span>
        </button>

        <div className="collapse navbar-collapse" id="navbarSupportedContent">
          <ul className="navbar-nav me-auto mb-lg-0">
            <li className="nav-item">
              <Link className="nav-link active" aria-current="page" to="/">Home</Link>
            </li>
            <li className="nav-item">
              <Link className="nav-link" to="/writing">Writing</Link>
            </li>
            <li className="nav-item">
              <a className="nav-link" href="#">About</a>
            </li>
          </ul>
          <div className="d-flex">
            {menu}
          </div>
        </div>
      </div>
    </nav>
  )
}
export default Nav;