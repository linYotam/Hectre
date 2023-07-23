import { createSlice } from '@reduxjs/toolkit';
import Cookies from 'js-cookie';

// Init auth state --> set user Cookie
const initialState = {
  user: Cookies.get('user') ? JSON.parse(Cookies.get('user')) : null,
};

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    setUser: (state, action) => {
      state.user = action.payload;
    },
    logout: state => {
      state.user = null;
      state.token = null;
      state.isAuthenticated = false;
    },
  },
});

export const { setUser, setToken, logout } = authSlice.actions;

export default authSlice.reducer;
