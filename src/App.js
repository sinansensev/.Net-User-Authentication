// Root.js
import React, { useState } from 'react';
import { BrowserRouter as Router, Routes, Route, Link, useNavigate } from 'react-router-dom';
import RegisterForm from './RegisterForm';
import LoginForm from './LoginForm';
import AdminPanel from './AdminPanel';
import ForgotPasswordForm from './ForgotPasswordForm'; // Yeni bileşen import edildi
import ResetPassword from './ResetPassword';

const Root = () => {
  return (
    <Router>
      <App />
    </Router>
  );
};

const App = () => {
  const [currentUser, setCurrentUser] = useState(null);
  const navigate = useNavigate();

  const handleLogout = () => {
    // Burada kullanıcı çıkış işlemlerini gerçekleştirebilirsiniz.
    setCurrentUser(null);
    navigate('/');
  };

  return (
    <div>
      <nav>
        <ul>
          <li>
            <Link to="/">Home</Link>
          </li>
          <li>
            <Link to="/login">Login</Link>
          </li>
          <li>
            <Link to="/register">Register</Link>
          </li>
          {currentUser && (
            <>
              {currentUser.role === 'admin' && (
                <li>
                  <Link to="/admin">Admin Panel</Link>
                </li>
              )}
              <li>
                <button onClick={handleLogout}>Logout</button>
              </li>
            </>
          )}
        </ul>
      </nav>
      <div>
        {currentUser !== null && (
          <div>
            <p>Email: {currentUser.email}</p>
            <p>Role: {currentUser.role}</p>
          </div>
        )}
      </div>
      <Routes>
        <Route path="/" element={<Home currentUser={currentUser} />} />
        <Route path="/login" element={<LoginForm />} />
        <Route path="/register" element={<RegisterForm />} />
        <Route path="/reset-password" element={<ResetPassword />} />
        <Route path="/forgot-password" element={<ForgotPasswordForm />} /> {/* ForgotPasswordForm bileşeni */}
        {currentUser && (
          <>
            {currentUser.role === 'admin' && (
              <Route path="/admin" element={<AdminPanel currentUser={currentUser} />} />
            )}
            {/* Add other role-based routes here */}
          </>
        )}
      </Routes>
    </div>
  );
};

const Home = () => {
  return (
    <div>
      <h1>Welcome to the Home Page</h1>
    </div>
  );
};

export default Root;
