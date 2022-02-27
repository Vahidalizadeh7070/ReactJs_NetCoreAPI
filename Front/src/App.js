import logo from './logo.svg';
import './App.css';
import Login from './Page/Login';
import { BrowserRouter, Route } from 'react-router-dom';
import Nav from './Components/Nav';
import Register from './Page/Register';
import Home from './Page/Home';
import Writing from './Page/Writing';
import { useState, useEffect } from 'react';
import axios from 'axios';
import NewsDetails from './Page/NewsDetails';
import ReviewDetails from './Page/ReviewDetails';
import { CookiesProvider } from "react-cookie";
import { set } from 'js-cookie';
import ConfirmEmail from './Page/ConfirmEmail';


function App() {
  const [email, setName] = useState('');
  const [UserId, setUserId] = useState(0);
  useEffect(() => {
    (
      //  axios.get("http://localhost:8000/api/users",{withCredentials:true})
      //   .then(function(response){
      //     setEmail(response.data.email);
      //   })
      async () => {
        const response = await fetch("https://localhost:44352/api/auth/users", {
          headers: { 'Content-Type': 'application/json' },
          credentials: 'include'
        })
        const content = await response.json();
        setName(content.email);
        setUserId(content.id)
      }
    )();
  })
  return (
    <div>
      <CookiesProvider>
        <BrowserRouter>
          <Nav setName={setName} email={email} />
          <Route path="/" exact component={Home} />
          <Route path="/ConfirmEmail/:token" component={ConfirmEmail} />
          <Route path="/writing" component={() => <Writing email={email} id={UserId} />} />
          <Route path="/register" component={Register} />
          <Route path="/Login" component={() => <Login setName={setName} />} />
          <Route path="/NewsDetails/:id" component={() => <NewsDetails />} />
          <Route path="/ReviewDetails/:id" component={() => <ReviewDetails />} />
        </BrowserRouter>
      </CookiesProvider>
    </div>
  );
}

export default App;
