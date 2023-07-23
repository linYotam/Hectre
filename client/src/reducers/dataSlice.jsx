import { createSlice } from '@reduxjs/toolkit';

// Init data state
const initialState = {
  harvests: [],
  dateRange: '',
  orchards: [],
};

const dataSlice = createSlice({
  name: 'data',
  initialState,
  reducers: {
    setHarvests: (state, action) => {
      state.harvests = action.payload;
    },
    setDateRange: (state, action) => {
      state.dateRange = action.payload;
    },
    setOrchardsData: (state, action) => {
      state.orchards = action.payload;
    },
  },
});

export const { setHarvests, setDateRange, setOrchardsData } = dataSlice.actions;

export default dataSlice.reducer;
