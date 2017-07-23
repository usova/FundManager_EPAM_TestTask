using NUnit.Framework;
using System;
using FundManager.BL;
using FundManager.Repositories.DTO;

namespace FundManager.Tests
{
    [TestFixture]
    public class BondBuilderTest
    {
        [Test]
        public void Create_PassNullAsStockDtoParameter_ThrowException()
        {
            // Arrange
            var builder = new BondBuilder();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => builder.Create(null, 1));
        }

        [Test]
        public void Create_PositiveTest_ReturnsCreatedBond()
        {
            // Arrange
            var builder = new BondBuilder();
            var totalMarketValue = 5025;
            
            var bondDTO = 
                new BondDTO
                {
                    Quantity = 5,
                    Price = 257,
                    Name = "Bond1"
                };
            var expactedMarketValue = bondDTO.Price * bondDTO.Quantity;
            var expactedStockWeight = Math.Round(expactedMarketValue * 100 / totalMarketValue, 2);

            //Act
            var bond = builder.Create(bondDTO, totalMarketValue);
            
            // Assert
            Assert.That(bond, Is.Not.Null);
            Assert.That(bond.Name, Is.EqualTo(bondDTO.Name));
            Assert.That(bond.Price, Is.EqualTo(bondDTO.Price));
            Assert.That(bond.Quantity, Is.EqualTo(bondDTO.Quantity));
            Assert.That(bond.MarketValue, Is.EqualTo(expactedMarketValue));
            Assert.That(bond.TransactionCost, Is.EqualTo(bond.MarketValue * 0.02M));
            Assert.That(bond.StockWeight, Is.EqualTo(expactedStockWeight));
        }
    }
}
