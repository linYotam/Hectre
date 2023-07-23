import React from 'react';
import './ChartTooltip.css';

//- Set custom tooltip for each slice in pie chart
const ChartTooltip = ({ active, payload, children }) => {
  // Make sure there's data
  if (active && payload && payload.length) {
    const { name, value } = payload[0];

    // Filter the relevant data by name
    const data = children.filter(d => d.name === name);

    // Foramt number with thousands separator
    const formattedNumber = value.toLocaleString();

    return (
      <div className="custom-tooltip">
        <p className="tooltip-date">
          <img src="/images/calendar.png" alt="calendar icon" />
          {data[0].dateRange}
        </p>

        <div>
          <span className="tooltip-color">{`${name}  `}</span>
          {data[0].label === '$' ? <span className="tooltip-color">$</span> : null}
          <span className="tooltip-color">{formattedNumber}</span>
          {data[0].label === 'Bins' ? <span> bins</span> : null}
        </div>
      </div>
    );
  }

  return null;
};

export default ChartTooltip;
