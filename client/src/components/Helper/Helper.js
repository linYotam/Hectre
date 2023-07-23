//! This helper section help filter, group and convert data
//! to show on charts, tooltip and other visible info on page

//- Group the orchard objects by "name" and calculate the sum of Bin Count and Cost
export const groupOrchardsAndCalculate = (harvestsData, orchardsData) => {
  const result = [];

  // Group harvestsData by orchardId
  const groupedHarvests = harvestsData.reduce((acc, harvest) => {
    if (!acc[harvest.orchardId]) {
      acc[harvest.orchardId] = {
        orchardId: harvest.orchardId,
        name: orchardsData.find(orchard => orchard.id === harvest.orchardId)?.name || '',
        binCount: 0,
        totalCost: 0,
      };
    }

    acc[harvest.orchardId].binCount += harvest.binCount;
    acc[harvest.orchardId].totalCost += harvest.hourlyWageRate * harvest.hoursWorked;

    return acc;
  }, {});

  // Convert the groupedHarvests object to an array of objects
  for (const orchardId in groupedHarvests) {
    result.push(groupedHarvests[orchardId]);
  }

  return result;
};

//- Function to convert data for production
export const convertToProductionData = (orchardsData, dateRangeData) => {
  const productionData = orchardsData.map(data => ({
    value: data.binCount,
    name: data.name,
    dateRange: dateRangeData, // Replace this with your actual date range
    label: 'Bins',
  }));

  return productionData;
};

//- Function to convert data for cost
export const convertToCostData = (orchardsData, dateRangeData) => {
  const costData = orchardsData.map(data => ({
    value: data.totalCost,
    name: data.name,
    dateRange: dateRangeData, // Replace this with your actual date range
    label: '$',
  }));

  return costData;
};

//- Group the harvest objects by "variety" and calculate the sum of Bin Count for each variety of type "Bin"
export const groupHarvestsByProduction = harvestsData => {
  const tempData = harvestsData.reduce((acc, curr) => {
    if (curr.type === 'Bin') {
      if (!acc[curr.variety]) {
        acc[curr.variety] = 0;
      }
      acc[curr.variety] += curr.binCount;
    }
    return acc;
  }, {});
  return tempData;
};

//- Convert the grouped data to the desired array format
export const convertHarvestDataToBins = (tempData, dateRangeData) => {
  const productionData = Object.keys(tempData).map(variety => ({
    value: tempData[variety],
    name: variety,
    dateRange: dateRangeData,
    label: 'Bins',
  }));
  return productionData;
};

//- Group the harvest objects by "variety" and calculate the sum of "hourlyWageRate * hoursWorked" for each variety of type "Bin"
export const groupHarvestsByCost = harvestsData => {
  const groupedData = harvestsData.reduce((acc, curr) => {
    if (curr.type === 'Bin') {
      if (!acc[curr.variety]) {
        acc[curr.variety] = 0;
      }
      acc[curr.variety] += curr.hourlyWageRate * curr.hoursWorked;
    }
    return acc;
  }, {});
  return groupedData;
};

//- Convert the grouped data to the desired array format
export const convertHarvestDataToCosts = (tempData, dateRangeData) => {
  const costData = Object.keys(tempData).map(variety => ({
    value: tempData[variety],
    name: variety,
    dateRange: dateRangeData,
    label: '$',
  }));
  return costData;
};
