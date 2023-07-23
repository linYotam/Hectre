import React from 'react';
import './LegendBox.css';

//- This component show charts legend by color and name
const LegendBox = ({ colors, names }) => {
  return (
    <>
      <div className="legend">
        {names.map((name, index) => (
          <div key={name} className="legend-box">
            <div style={{ backgroundColor: `${colors[index]}`, height: '11px', width: '35px' }}></div>
            <span>{names[index]}</span>
          </div>
        ))}
      </div>
    </>
  );
};

export default LegendBox;
