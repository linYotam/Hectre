import {
  convertToProductionData,
  convertToCostData,
  groupHarvestsByProduction,
  convertHarvestDataToBins,
} from './Helper';

describe('convertToProductionData', () => {
  it('should convert orchards data to production data format', () => {
    // Mock orchards data
    const orchardsData = [
      {
        binCount: 10,
        name: 'Orchard 1',
        // Add any other relevant properties here
      },
      {
        binCount: 15,
        name: 'Orchard 2',
        // Add any other relevant properties here
      },
    ];

    // Mock date range data
    const dateRangeData = '01/01/2023-12/31/2023';

    // Call the function to be tested
    const productionData = convertToProductionData(orchardsData, dateRangeData);

    // Assert the result
    expect(productionData).toHaveLength(2);

    // Assert the first object in the production data array
    expect(productionData[0]).toEqual({
      value: 10,
      name: 'Orchard 1',
      dateRange: dateRangeData,
      label: 'Bins',
    });

    // Assert the second object in the production data array
    expect(productionData[1]).toEqual({
      value: 15,
      name: 'Orchard 2',
      dateRange: dateRangeData,
      label: 'Bins',
    });
  });
});

describe('convertToCostData', () => {
  it('should convert orchards data to cost data format', () => {
    // Mock orchards data
    const orchardsData = [
      {
        totalCost: 100,
        name: 'Orchard 1',
      },
      {
        totalCost: 150,
        name: 'Orchard 2',
      },
    ];

    // Mock date range data
    const dateRangeData = '01/01/2023-12/31/2023';

    // Call the function to be tested
    const costData = convertToCostData(orchardsData, dateRangeData);

    // Assert the result
    expect(costData).toHaveLength(2);

    // Assert the first object in the cost data array
    expect(costData[0]).toEqual({
      value: 100,
      name: 'Orchard 1',
      dateRange: dateRangeData,
      label: '$',
    });

    // Assert the second object in the cost data array
    expect(costData[1]).toEqual({
      value: 150,
      name: 'Orchard 2',
      dateRange: dateRangeData,
      label: '$',
    });
  });
});

describe('groupHarvestsByProduction', () => {
  it('should group harvests data by production and calculate bin count for each variety', () => {
    // Mock harvests data
    const harvestsData = [
      {
        binCount: 10,
        type: 'Bin',
        variety: 'Variety 1',
        // Add any other relevant properties here
      },
      {
        binCount: 15,
        type: 'Bin',
        variety: 'Variety 2',
        // Add any other relevant properties here
      },
      {
        binCount: 5,
        type: 'Other',
        variety: 'Variety 1',
        // Add any other relevant properties here
      },
    ];

    // Call the function to be tested
    const tempData = groupHarvestsByProduction(harvestsData);

    // Assert the result
    expect(tempData).toEqual({
      'Variety 1': 10, // 10 (Variety 1)
      'Variety 2': 15, // 15 (Variety 2)
    });
  });
});

describe('convertHarvestDataToBins', () => {
  it('should convert grouped data to the desired array format for production', () => {
    // Mock data
    const tempData = {
      'Variety 1': 10,
      'Variety 2': 15,
    };
    const dateRangeData = '01/01/2023-12/31/2023';

    // Call the function to be tested
    const productionData = convertHarvestDataToBins(tempData, dateRangeData);

    // Assert the result
    expect(productionData).toEqual([
      {
        value: 10,
        name: 'Variety 1',
        dateRange: '01/01/2023-12/31/2023',
        label: 'Bins',
      },
      {
        value: 15,
        name: 'Variety 2',
        dateRange: '01/01/2023-12/31/2023',
        label: 'Bins',
      },
    ]);
  });
});
