import './App.css';
import Auth from './components/Auth/Auth';
import Filter from './components/Filter/Filter';
import Header from './components/Header/Header';
import Main from './components/Main/Main';
import { useSelector } from 'react-redux';
import Cookies from 'js-cookie';

function App() {
  // Get user info from token or Cookie
  let user = useSelector(state => state.auth.user);
  if (user === undefined && Cookies.get('user')) user = JSON.parse(Cookies.get('user'));

  return (
    <>
      {user === undefined || user == null ? (
        <Auth />
      ) : (
        <>
          <Header />
          <Filter />
          <Main />
        </>
      )}
    </>
  );
}

export default App;
