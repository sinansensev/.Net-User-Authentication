import React, { useState } from 'react';
import axios from 'axios';
import './register.css';

const RegisterForm = () => {
  const [email, setEmail] = useState('');
  const [name, setName] = useState('');
  const [surname, setSurname] = useState('');
  const [password, setPassword] = useState('');
  const [phoneNumber, setPhoneNumber] = useState('');
  const [role, setRole] = useState('');
  const [error, setError] = useState('');

  const handleRegister = async (e) => {
    e.preventDefault();
    setError('');
    if (!email || !name || !surname || !password || !phoneNumber || !role) {
      setError('Tüm alanlar gereklidir');
      return;
    }
    try {
      const response = await axios.post(
        'http://localhost:5265/auth/register',
        {
          email: email,
          password: password,
          firstName: name,
          surname: surname,
          phoneNumber: phoneNumber,
          role: role
        },
        {
          headers: {
            'Content-Type': 'application/json'
          }
        }
      );
      console.log(response.data);
      window.location.href = '/login'; // Yönlendirme yap
    } catch (error) {
      setError(error.response.data.message || 'Bir hata oluştu');
    }
  };

  return (
    <div className="all-register">
      <div className="container-register">
        <h2>Kayıt Ol</h2>
        <form className="register-form" onSubmit={handleRegister}>
          <label className="label-register" htmlFor="name">Ad</label>
          <input className="input-register" type="text" id="name" placeholder="Ad" value={name} onChange={(e) => setName(e.target.value)} />
          <label className="label-register" htmlFor="surname">Soyad</label>
          <input className="input-register" type="text" id="surname" placeholder="Soyad" value={surname} onChange={(e) => setSurname(e.target.value)} />
          <label className="label-register" htmlFor="email">Email</label>
          <input className="input-register" type="email" id="email" placeholder="Email" value={email} onChange={(e) => setEmail(e.target.value)} />
          <label className="label-register" htmlFor="password">Şifre</label>
          <input className="input-register" type="password" id="password" placeholder="Şifre" value={password} onChange={(e) => setPassword(e.target.value)} />
          <label className="label-register" htmlFor="phoneNumber">Telefon Numarası</label>
          <input className="input-register" type="text" id="phoneNumber" placeholder="Telefon Numarası" value={phoneNumber} onChange={(e) => setPhoneNumber(e.target.value)} />
          <label className="label-register" htmlFor="role">Rol</label>
          <select className="input-register" id="role" value={role} onChange={(e) => setRole(e.target.value)}>
            <option value="">Rol Seçin</option>
            <option value="Genel Müdür">Genel Müdür</option>
            <option value="Müşteri">Müşteri</option>
            <option value="Personel">Personel</option>
          </select>
          <button className="button-register" type="submit">Kayıt Ol</button>
          {error && <p>{error}</p>}
        </form>
        <div className="login-link">
          <p>Zaten hesabınız var mı? <a href="/login">Giriş Yapın</a></p>
        </div>
      </div>
    </div>
  );
};

export default RegisterForm;
