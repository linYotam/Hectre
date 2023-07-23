import React, { useEffect, useState } from 'react';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import './Filter.css';
import { Box, FormControl, InputLabel, MenuItem, Select } from '@mui/material';
import { useDispatch } from 'react-redux';
import { getHarvests, getOrchards, getHarvestsByOrchardsAndDateRange } from '../../actions/dataActions';
import { setDateRange } from '../../reducers/dataSlice';
import { useSelector } from 'react-redux';

const Filter = () => {
  const [fromDate, setFromDate] = useState(null);
  const [toDate, setToDate] = useState(null);
  const [selectedOrchards, setSelectedOrchards] = useState(['0']);
  const orchards = useSelector(state => state.data.orchards);
  const dispatch = useDispatch();

  //- Load list of all Orchards and Harvests from DB at component load
  useEffect(() => {
    // Call the async function to fetch Orchards data in dataActions Thunk
    dispatch(getOrchards())
      .then(result => {})
      .catch(error => {
        throw error.response.data;
      });

    // Call the async function to fetch Harvests data in dataActions Thunk
    dispatch(getHarvests())
      .then(() => {})
      .catch(error => {
        throw error.response.data;
      });

    // Set default date range
    const currentDate = new Date();
    const dateStringTo = currentDate.toLocaleDateString('en-US');
    // Save default date range in redux state
    dispatch(setDateRange('01/01/1900-' + dateStringTo));
  }, [dispatch]);

  //- Handle Orchards drop down change
  const handleOrchardsSelectedChange = event => {
    const {
      target: { value },
    } = event;

    // If last value is ALL remove all other values
    if (value[value.length - 1] === '0') {
      setSelectedOrchards(['0']);
    } else {
      // If '0' exists in the array, remove it
      const newArray = value.includes('0') ? value.filter(val => val !== '0') : value;
      setSelectedOrchards(newArray);
    }
  };

  //- Filter data according to selected dates range and selected orchards
  const handleFilter = async () => {
    if (fromDate != null && toDate != null && fromDate > toDate) return;

    const dataObject = {
      selectedOrchards,
      fromDate,
      toDate,
    };

    // Call the async function to fetch Harvests data by orchards and date range in dataActions Thunk
    dispatch(getHarvestsByOrchardsAndDateRange(dataObject))
      .then(() => {})
      .catch(error => {
        throw error.response.data;
      });
  };

  return (
    <div className="filter">
      <div className="date-picker">
        <LocalizationProvider dateAdapter={AdapterDayjs}>
          <DatePicker label="From date" value={fromDate} onChange={newValue => setFromDate(newValue)} />
        </LocalizationProvider>
        <LocalizationProvider dateAdapter={AdapterDayjs}>
          <DatePicker label="to date" value={toDate} onChange={newValue => setToDate(newValue)} />
        </LocalizationProvider>
      </div>

      <img className="filter-icon" src="/images/filter.png" alt="filter icon" onClick={handleFilter} />

      <Box sx={{ minWidth: 240 }}>
        <FormControl fullWidth>
          <InputLabel id="orchards-filter-label">Orchards</InputLabel>
          <Select
            labelId="orchards-filter"
            id="orchards-filter"
            multiple
            value={selectedOrchards}
            onChange={handleOrchardsSelectedChange}
            label="Orchards">
            <MenuItem value="0">All</MenuItem>
            {orchards.map(orchard => (
              <MenuItem key={orchard.id} value={orchard.id}>
                {orchard.name}
              </MenuItem>
            ))}
          </Select>
        </FormControl>
      </Box>
    </div>
  );
};

export default Filter;
