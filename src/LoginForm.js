// LoginForm.js
import React, { useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom'; // Link import edildi
import './login.css';

const LoginForm = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');

  const handleLogin = async (e) => {
    e.preventDefault();
    setError('');
    try {
      const response = await axios.post('http://localhost:5265/auth/login/', {
        email: email,
        password: password,
      });
      console.log(response.data); 
    } catch (error) {
      if (error.response) {
        setError(error.response.data);
      } else if (error.request) {
        setError("No response was received");
      } else {
        setError("Error while making the request");
      }
    }
  };
  
  return (
    <div className="login-container">
      <form className="login-form" onSubmit={handleLogin}>
        <div className="login-input-container">
          <label className="label-register" htmlFor="name">E-mail</label>
          <input type="email" className="login-input"  placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} />
        </div>
        <div className="login-input-container">
          <label className="label-register" htmlFor="name">Password</label>
          <input type="password" className="login-input" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} />
        </div>
        <button type="submit" className="login-button" >Login</button>
        {error && <p className="login-error">{error}</p>}
        <Link to="/forgot-password" className="forgot-password">Forgot Password?</Link> {/* Şifremi unuttum butonu */}
      </form>
    </div>
  );
};

export default LoginForm;
