import React, { useState } from 'react';
import axios from 'axios';
import './resetpassword.css'; // CSS dosyasını import et

const ResetPassword = () => {
  const [email, setEmail] = useState('');
  const [code, setCode] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [errorMessage, setErrorMessage] = useState('');
  const [successMessage, setSuccessMessage] = useState('');

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErrorMessage('');
    setSuccessMessage('');

    try {
      const response = await axios.post('http://localhost:5265/api/passwordreset/reset-password', {
        email,
        code,
        newPassword
      });
      setSuccessMessage(response.data);
    } catch (error) {
      if (error.response) {
        setErrorMessage(error.response.data);
      } else {
        setErrorMessage("Error while making the request");
      }
    }
  };

  return (
    <div className="reset-password-container"> {/* CSS sınıfını burada kullan */}
      <form className="reset-password-form" onSubmit={handleSubmit}> {/* CSS sınıfını burada kullan */}
        <h2>Reset Password</h2> {/* Başlık */}
        <div className="reset-password-input-container"> {/* CSS sınıfını burada kullan */}
          <label>Email:</label>
          <input 
            className="reset-password-input"
            type="email" 
            value={email} 
            onChange={(e) => setEmail(e.target.value)} 
            required 
          />
        </div>
        <div className="reset-password-input-container"> {/* CSS sınıfını burada kullan */}
          <label>Code:</label>
          <input 
            className="reset-password-input"
            type="text" 
            value={code} 
            onChange={(e) => setCode(e.target.value)} 
            required 
          />
        </div>
        <div className="reset-password-input-container"> {/* CSS sınıfını burada kullan */}
          <label>New Password:</label>
          <input 
            className="reset-password-input"
            type="password" 
            value={newPassword} 
            onChange={(e) => setNewPassword(e.target.value)} 
            required 
          />
        </div>
        <button className="reset-password-button" type="submit">Reset Password</button> {/* CSS sınıfını burada kullan */}
        {errorMessage && <p className="reset-password-error">{errorMessage}</p>} {/* CSS sınıfını burada kullan */}
        {successMessage && <p>{successMessage}</p>}
      </form>
    </div>
  );
};

export default ResetPassword;
