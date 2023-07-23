import { createAsyncThunk } from '@reduxjs/toolkit';
import axios from 'axios';
import { setHarvests, setOrchardsData, setDateRange } from '../reducers/dataSlice';
import Cookies from 'js-cookie';
import dayjs from 'dayjs';
import API_PARTIAL_URL from '../config';

const API_URL = API_PARTIAL_URL;

//- Get Harvests data from DB
export const getHarvests = createAsyncThunk('harvests', async (_, { dispatch }) => {
  // Set token info for the request
  const token = JSON.parse(Cookies.get('user'));
  const config = {
    headers: {
      Authorization: `Bearer ${token.jwtToken}`,
    },
  };

  try {
    // Send request
    const response = await axios.get(`${API_URL}/Harvests`, config);
    const harvests = response.data;
    // Save data in redux state
    dispatch(setHarvests(harvests));
  } catch (error) {
    throw error.response.data;
  }
});

//- Get Orchards data from DB
export const getOrchards = createAsyncThunk('orchards', async (_, { dispatch }) => {
  // Set token info for the request
  const token = JSON.parse(Cookies.get('user'));
  const config = {
    headers: {
      Authorization: `Bearer ${token.jwtToken}`,
    },
  };

  try {
    // Send request
    const response = await axios.get(`${API_URL}/Orchards`, config);
    const orchards = response.data;
    // Save data in redux state
    dispatch(setOrchardsData(orchards));
    return orchards;
  } catch (error) {
    throw error.response.data;
  }
});

export const getHarvestsByOrchardsAndDateRange = createAsyncThunk(
  'harvestsByOrchardsAndDate',
  async (dataObject, { dispatch }) => {
    // Convert date range to string
    let dateStringFrom;
    let dateStringTo;

    const fromDate = dataObject.fromDate;
    const toDate = dataObject.toDate;
    const selectedOrchards = dataObject.selectedOrchards;

    // Make sure date set with default value if no value exist
    if (fromDate === null) dateStringFrom = '01/01/1900';
    else dateStringFrom = dayjs(fromDate).format('MM/DD/YYYY');

    // Make sure date set with default value if no value exist
    if (toDate === null) {
      const currentDate = new Date();
      dateStringTo = currentDate.toLocaleDateString('en-US');
    } else dateStringTo = dayjs(toDate).format('MM/DD/YYYY');

    // Convert array of GUIDs into string before sending to Server
    const commaSeparatedString = selectedOrchards.join(',');

    // Add user token to request
    const token = JSON.parse(Cookies.get('user'));
    const config = {
      params: {
        orchardIds: commaSeparatedString,
        startDate: fromDate,
        endDate: toDate,
      },
      headers: {
        Authorization: `Bearer ${token.jwtToken}`,
      },
    };

    try {
      // Send request

      const response = await axios.get(`${API_URL}/Harvests/GetHarvestsByOrchardAndDateRange`, config);

      // Update harvests state in redux
      dispatch(setHarvests(response.data));
      // Update date range state in redux
      dispatch(setDateRange(dateStringFrom + '-' + dateStringTo));
    } catch (error) {
      throw error.response.data;
    }
  }
);
