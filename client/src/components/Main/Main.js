import React, { useState } from 'react';
import './Main.css';
import { Chart } from '../Chart/Chart';
import { CustomTabs } from '../CustomTabs/CustomTabs';
import LegendBox from '../LegendBox/LegendBox';
import { useSelector } from 'react-redux';
import {
  groupOrchardsAndCalculate,
  convertToProductionData,
  convertToCostData,
  groupHarvestsByProduction,
  convertHarvestDataToBins,
  groupHarvestsByCost,
  convertHarvestDataToCosts,
} from '../Helper/Helper';

//- Get unique varieties
const getUniqueVarieties = data => {
  const uniqueVarieties = new Set();
  const result = [];

  data.forEach(harvest => {
    if (!uniqueVarieties.has(harvest.variety)) {
      uniqueVarieties.add(harvest.variety);
      result.push(harvest.variety);
    }
  });

  return result;
};

//- Get unique Orchards names
const getUniqueOrchardNames = data => {
  const uniqueVarieties = new Set();
  const result = [];

  data.forEach(orchard => {
    if (!uniqueVarieties.has(orchard.name)) {
      uniqueVarieties.add(orchard.name);
      result.push(orchard.name);
    }
  });

  return result;
};

//- Main section of page (include: Tabs, legend, charts)
const Main = () => {
  // Get harvests data from redux state
  const harvestsData = useSelector(state => state.data.harvests);

  // Get orchards data from redux state
  const orchardsData = useSelector(state => state.data.orchards);

  // Get date range data from redux state
  const dateRangeData = useSelector(state => state.data.dateRange);

  // Init tab value to '0' (VARIETIES)
  const [tabValue, setTabValue] = useState('0');

  // Names of chart slices (orchards names or harverts varieties)
  let names = [];

  // Data of production (orchards or harvests)
  let productionData = [];

  // Data of cost (orchards or harvests)
  let costData = [];

  // Set default colors
  const colors = ['#77D5D4', '#F2E25A', '#1A248A'];

  //- Set selected tab
  const handleTabChange = (event, newValue) => {
    setTabValue(newValue);
  };

  // Filter Data depends on the selected tab
  if (tabValue === '0') {
    // Get harvest names
    names = getUniqueVarieties(harvestsData);

    // Group harvests by production (bin count)
    const harvestByProduction = groupHarvestsByProduction(harvestsData);

    // Group harvests by cost
    const harvestsByCost = groupHarvestsByCost(harvestsData);

    // Convert the data to send to chart
    productionData = convertHarvestDataToBins(harvestsByCost, dateRangeData);
    costData = convertHarvestDataToCosts(harvestByProduction, dateRangeData);
  } else {
    names = getUniqueOrchardNames(orchardsData);

    // Get new array of orchards group by total cost and total production
    const orchardsGroupedArray = groupOrchardsAndCalculate(harvestsData, orchardsData);

    // Convert the data to send to chart
    productionData = convertToProductionData(orchardsGroupedArray, dateRangeData);
    costData = convertToCostData(orchardsGroupedArray, dateRangeData);
  }

  // Get new array of harvests group by total cost and total production
  const totalProductionSum = productionData.reduce((sum, entry) => sum + entry.value, 0);
  const totalCostSum = costData.reduce((sum, entry) => sum + entry.value, 0);

  return (
    <main className="main">
      <h1 className="main-label">Percentage</h1>

      <div className="main-header">
        <CustomTabs tabValue={tabValue} handleTabChange={handleTabChange} />
      </div>

      {tabValue === '0' ? (
        <>
          <LegendBox colors={colors} names={names} />
          <div className="charts">
            <Chart
              data={productionData}
              colors={colors}
              totalSum={totalProductionSum}
              label={'Production'}
              summary={totalProductionSum.toLocaleString() + ' bins'}
            />
            <Chart
              data={costData}
              colors={colors}
              totalSum={totalCostSum}
              label={'Cost'}
              summary={'$' + totalCostSum.toLocaleString()}
            />
          </div>
        </>
      ) : (
        <>
          <LegendBox colors={colors} names={names} />
          <div className="charts">
            <Chart
              data={productionData}
              colors={colors}
              totalSum={totalProductionSum}
              label={'Production'}
              summary={totalProductionSum.toLocaleString() + ' bins'}
            />
            <Chart
              data={costData}
              colors={colors}
              totalSum={totalCostSum}
              label={'Cost'}
              summary={'$' + totalCostSum.toLocaleString()}
            />
          </div>
        </>
      )}
    </main>
  );
};

export default Main;
