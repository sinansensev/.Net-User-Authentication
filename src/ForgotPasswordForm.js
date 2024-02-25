// ForgotPasswordForm.js

import React, { useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import './forgotpassword.css';

const ForgotPasswordForm = () => {
  const [email, setEmail] = useState('');
  const [errorMessage, setErrorMessage] = useState('');
  const [successMessage, setSuccessMessage] = useState('');

  const handleForgotPassword = async (e) => {
    e.preventDefault();
    setErrorMessage('');
    setSuccessMessage('');

    try {
      const response = await axios.post('http://localhost:5265/api/passwordreset/send-reset-code', {
        email: email
      });

      if (response.data.success) {
        setSuccessMessage(response.data.message);
      } else {
        setErrorMessage(response.data.message);
      }
    } catch (error) {
      if (error.response) {
        setErrorMessage(error.response.data.message);
      } else {
        setErrorMessage("Error while making the request");
      }
    }
  };

  return (
    <div className="forgot-password-container">
      <form className="forgot-password-form" onSubmit={handleForgotPassword}>
        <div className="forgot-password-input-container">
          <input 
            type="email" 
            className="forgot-password-input" 
            placeholder="Email" 
            value={email} 
            onChange={(e) => setEmail(e.target.value)} 
            required 
          />
        </div>
        <button type="submit" className="forgot-password-button">Send Reset Code</button>
        {errorMessage && <p className="forgot-password-error">{errorMessage}</p>}
        {successMessage && <p className="forgot-password-success">{successMessage}</p>}
        {/* Yeni buton eklendi */}
        <Link to={`/reset-password/`} className="back-to-login">Reset Password</Link>
        <Link to="/login" className="back-to-login">Back to Login</Link>
      </form>
    </div>
  );
};

export default ForgotPasswordForm;
