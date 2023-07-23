import { configureStore } from '@reduxjs/toolkit';
import authReducer from './reducers/authSlice';
import dataReducer from './reducers/dataSlice';

export const store = configureStore({
  reducer: {
    auth: authReducer,
    data: dataReducer,
  },
});
