import React from 'react';
import './Header.css';
import { useSelector } from 'react-redux';
import { useDispatch } from 'react-redux';
import { logoutUser } from '../../actions/authActions';
import Cookies from 'js-cookie';

const Header = () => {
  const dispatch = useDispatch();

  // Get user info from token or Cookie
  let user = useSelector(state => state.auth.user);
  if (user === undefined && Cookies.get('user')) user = JSON.parse(Cookies.get('user'));

  //- Logout user (clear Cookie and token)
  const handleLogout = () => {
    dispatch(logoutUser())
      .then(() => {})
      .catch(error => {
        throw error.response.data;
      });
  };

  return (
    <header className="header">
      <img className="logo" alt="Hectre logo" src="images/Hectre_Logo.png" />
      {user === undefined || user == null ? null : (
        <>
          <button className="btn-logout" onClick={handleLogout}>
            Logout
          </button>
        </>
      )}
    </header>
  );
};

export default Header;
