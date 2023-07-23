import { Tabs, Tab } from '@mui/material';
import React from 'react';

//- Custom MUI tabs to show data in pie chart based on varieties/orchards
export const CustomTabs = ({ tabValue, handleTabChange }) => {
  return (
    <Tabs
      className="tabs"
      value={tabValue}
      onChange={handleTabChange}
      aria-label="graph labels"
      TabIndicatorProps={{ style: { background: 'red' } }}>
      <Tab
        value="0"
        label="VARIETIES"
        className="tab"
        sx={{
          '&.Mui-selected': {
            color: 'black',
          },
          fontSize: '16px',
          padding: '0',
        }}
      />
      <Tab
        value="1"
        label="ORCHARDS"
        className="tab"
        sx={{
          '&.Mui-selected': {
            color: 'black',
          },
          fontSize: '16px',
        }}
      />
    </Tabs>
  );
};
