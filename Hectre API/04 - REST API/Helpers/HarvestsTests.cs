using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Hectre
{
    public class HarvestsTests
    {

        [Fact]
        public void GetAllHarvests_ShouldReturnValidHarvests()
        {
            // Create an instance of the HarvestsLogic class
            HarvestsLogic logic = new HarvestsLogic();

            // Act
            List<HarvestModel> harvests = logic.GetAllHarvests();

            // Assert
            Assert.NotNull(harvests);
            Assert.NotEmpty(harvests);

            foreach (var harvest in harvests)
            {
                Assert.True(harvest.BinCount >= 0, $"BinCount must be non-negative for Harvest Id: {harvest.Id}");
            }
        }

        [Fact]
        public void GetHarvestsByOrchardAndDate_ShouldReturnValidHarvests()
        {
            // Arrange
            var logic = new HarvestsLogic(); // Create an instance of the HarvestsLogic class

            // Create test data
            string[] orchardIds = new string[] { "769e6571-100e-4a18-a577-34a49f0c8ed3", "999e6571-100e-4a18-a577-34a49f0c8ed3" };
            DateTime startDate = new DateTime(2023, 1, 1);
            DateTime endDate = new DateTime(2023, 12, 31);

            // Act
            var result = logic.GetHarvestsByOrchardAndDate(orchardIds, startDate, endDate);

            // Assert
            // Example assertion for non-empty list:
            Assert.NotNull(result);
            Assert.True(result.Count > 0);
        }

        [Fact]
        public void AddNewHarvest_WhenOrchardExists_ShouldUseExistingOrchard()
        {
            // Arrange
            Guid existingOrchardId = new Guid("769e6571-100e-4a18-a577-34a49f0c8ed3");
            var existingOrchard = new Orchard
            {
                Id = existingOrchardId,
                Name = "Super Foods",
                Block = 17,
                SubBlock = "22B"
            };

            var dbContextMock = new Mock<HectreContext>();
            dbContextMock.Setup(db => db.Orchards.SingleOrDefault(It.IsAny<Func<Orchard, bool>>()))
                .Returns(existingOrchard);

            var harvestModel = new HarvestModel
            {
                Orchard = new OrchardModel
                {
                    Id = existingOrchardId
                },
                // Other properties of the harvest model
            };

            var logic = new HarvestsLogic(dbContextMock.Object); // Inject the mock dbContext

            // Act
            var result = logic.AddNewHarvest(harvestModel);

            // Assert
            Assert.Same(existingOrchard, result.Orchard);
            dbContextMock.Verify(db => db.Orchards.Add(It.IsAny<Orchard>()), Times.Never);
            dbContextMock.Verify(db => db.Harvests.Add(It.IsAny<Harvest>()), Times.Once);
            dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
        }

        [Fact]
        public void AddNewHarvest_WhenOrchardDoesNotExist_ShouldCreateNewOrchard()
        {
            // Arrange
            var dbContextMock = new Mock<HectreContext>();
            dbContextMock.Setup(db => db.Orchards.SingleOrDefault(It.IsAny<Func<Orchard, bool>>()))
                .Returns((Orchard)null);

            var addedOrchard = new Orchard();
            dbContextMock.Setup(db => db.Orchards.Add(It.IsAny<Orchard>()))
                .Callback<Orchard>(orchard => addedOrchard = orchard);

            var harvestModel = new HarvestModel
            {
                Orchard = new OrchardModel
                {
                    Id = new Guid("769e6571-100e-4a18-a577-34a49f0c8ed3"),
                    Name = "New Orchard",
                    Block = 5,
                    SubBlock = "C"
                },
                // Other properties of the harvest model
            };

            var logic = new HarvestsLogic(dbContextMock.Object); // Inject the mock dbContext

            // Act
            var result = logic.AddNewHarvest(harvestModel);

            // Assert
            Assert.Same(addedOrchard, result.Orchard);
            dbContextMock.Verify(db => db.Orchards.Add(It.IsAny<Orchard>()), Times.Once);
            dbContextMock.Verify(db => db.Harvests.Add(It.IsAny<Harvest>()), Times.Once);
            dbContextMock.Verify(db => db.SaveChanges(), Times.Once);
        }


    }
}
