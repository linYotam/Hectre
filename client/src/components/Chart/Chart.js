import React from 'react';
import { Pie, PieChart, Tooltip, Cell } from 'recharts';
import './Chart.css';
import ChartLabel from '../ChartLabel/ChartLabel';
import ChartTooltip from '../ChartTooltip/ChartTooltip';

//- Create custom chart based on recharts library
export const Chart = ({ data, colors, totalSum, label, summary }) => {
  const RADIAN = Math.PI / 180;

  //- Create custom label for each slice of the pie chart
  const ChartLabelNew = ({ cx, cy, midAngle, innerRadius, outerRadius, percent }) => {
    const radius = innerRadius + (outerRadius - innerRadius) * 0.5;
    const x = cx + radius * Math.cos(-midAngle * RADIAN);
    const y = cy + radius * Math.sin(-midAngle * RADIAN);

    return (
      <text
        x={x}
        y={y}
        fill="white"
        fontSize={16}
        textAnchor={x > cx ? 'start' : 'end'}
        dominantBaseline="central">
        {`$${(totalSum * percent).toLocaleString()}`}
      </text>
    );
  };

  return (
    <>
      {label === 'Cost' ? (
        <div className="chart">
          <PieChart width={400} height={400}>
            <Pie
              data={data}
              dataKey="value"
              cx="50%"
              cy="50%"
              outerRadius={150}
              labelLine={false}
              label={ChartLabelNew}>
              {data.map((entry, index) => (
                <Cell key={`cell-${index}`} fill={colors[index % colors.length]} />
              ))}
            </Pie>
            <Tooltip content={<ChartTooltip>{data}</ChartTooltip>} />
          </PieChart>
          <p className="chart-label">{label}</p>
          <p className="chart-total">TOTAL: {summary}</p>
        </div>
      ) : (
        <div className="chart">
          <PieChart width={400} height={400}>
            <Pie
              data={data}
              dataKey="value"
              cx="50%"
              cy="50%"
              outerRadius={150}
              labelLine={false}
              label={ChartLabel}>
              {data.map((entry, index) => (
                <Cell key={`cell-${index}`} fill={colors[index % colors.length]} />
              ))}
            </Pie>
            <Tooltip content={<ChartTooltip>{data}</ChartTooltip>} />
          </PieChart>
          <p className="chart-label">{label}</p>
          <p className="chart-total">TOTAL: {summary}</p>
        </div>
      )}
    </>
  );
};
