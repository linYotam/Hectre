import React, { useState } from 'react';
import './Auth.css';
import CryptoJS from 'crypto-js';
import { useDispatch } from 'react-redux';
import { registerUser, loginUser } from '../../actions/authActions';
import { useSelector } from 'react-redux';
import Cookies from 'js-cookie';

var key = CryptoJS.enc.Utf8.parse('8080808080808080');
var iv = CryptoJS.enc.Utf8.parse('8080808080808080');

const Auth = () => {
  const [loginPassword, setLoginPassword] = useState('');
  const [loginEmail, setLoginEmail] = useState('');
  const [registerName, setRegisterName] = useState('');
  const [registerEmail, setRegisterEmail] = useState('');
  const [registerPassword, setRegisterPassword] = useState('');

  const dispatch = useDispatch();

  // Get user info from Token or Cookie
  let user = useSelector(state => state.auth.user);
  if (user === undefined && Cookies.get('user')) user = JSON.parse(Cookies.get('user'));

  //- Toggle Auth page panel (Sign in/Sign up)
  function toggleSign() {
    document.querySelector('.container').classList.toggle('right-panel-active');
  }

  //- Encrypt user password before sending it to the Server side
  const encryptPassword = password => {
    var encryptedPassword = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(password), key, {
      keySize: 128 / 8,
      iv: iv,
      mode: CryptoJS.mode.CBC,
      padding: CryptoJS.pad.Pkcs7,
    });

    return encryptedPassword.toString();
  };

  //- Registration
  const handleRegistration = async e => {
    e.preventDefault();

    // Encrypt user password before sending to Server side
    const encryptedPassword = encryptPassword(registerPassword);

    // Create registration data object
    const registrationData = {
      Name: registerName,
      Email: registerEmail,
      password: encryptedPassword,
      Type: 'customer',
    };

    // Call registeration function in authActions Thunk
    dispatch(registerUser(registrationData))
      .then(() => {})
      .catch(error => {
        console.log('Registration Error:', error);
      });
  };

  //- Login
  const handleLogin = async e => {
    e.preventDefault();

    const encryptedPassword = encryptPassword(loginPassword);
    const loginData = {
      Email: loginEmail,
      password: encryptedPassword,
    };

    dispatch(loginUser(loginData))
      .then(() => {})
      .catch(error => {
        console.log('Login Error:', error);
      });
  };

  return (
    <>
      {user === undefined || user == null ? (
        <div className="sign">
          <div className="container" id="container">
            <div className="form-container sign-up-container">
              <form className="auth-form" onSubmit={handleRegistration}>
                <img className="hectre-logo" src="images/Hectre_Logo.png" alt="hectre logo" />
                <h1 className="auth-header-1">Create Account</h1>
                <input
                  className="auth-input"
                  type="text"
                  placeholder="Name"
                  onChange={e => setRegisterName(e.target.value)}
                />
                <input
                  className="auth-input"
                  type="email"
                  placeholder="Email"
                  onChange={e => setRegisterEmail(e.target.value)}
                />
                <input
                  className="auth-input"
                  type="password"
                  placeholder="Password"
                  onChange={e => setRegisterPassword(e.target.value)}
                />
                <button className="btn-auth">Sign Up</button>
              </form>
            </div>
            <div className="form-container sign-in-container">
              <form className="auth-form" onSubmit={handleLogin}>
                <img className="hectre-logo" src="images/Hectre_Logo.png" alt="hectre logo" />
                <input
                  className="auth-input"
                  type="email"
                  placeholder="Email"
                  onChange={event => setLoginEmail(event.target.value)}
                />
                <input
                  className="auth-input"
                  type="password"
                  placeholder="Password"
                  onChange={event => setLoginPassword(event.target.value)}
                />
                <button className="btn-auth">Sign In</button>
              </form>
            </div>
            <div className="overlay-container">
              <div className="overlay">
                <div className="overlay-panel overlay-left">
                  <h1 className="auth-header-1">Welcome Back!</h1>
                  <p className="auth-p">To keep connected with us please login with your personal info</p>
                  <button className="ghost btn-auth" id="signIn" onClick={toggleSign}>
                    Sign In
                  </button>
                </div>
                <div className="overlay-panel overlay-right">
                  <h1 className="auth-header-1">Hello, Grower!</h1>
                  <p className="auth-p">Enter your personal details and start your journey with us</p>
                  <button className="ghost btn-auth" id="signUp" onClick={toggleSign}>
                    Sign Up
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      ) : null}
    </>
  );
};

export default Auth;
