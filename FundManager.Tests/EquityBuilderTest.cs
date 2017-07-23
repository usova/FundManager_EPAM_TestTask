using NUnit.Framework;
using System;
using FundManager.BL;
using FundManager.Repositories.DTO;

namespace FundManager.Tests
{
    [TestFixture]
    public class EquityBuilderTest
    {
        [Test]
        public void Create_PassNullAsStockDtoParameter_ThrowException()
        {
            // Arrange
            var builder = new EquityBuilder();

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => builder.Create(null, 1));
        }

        [Test]
        public void Create_PositiveTest_ReturnsCreatedEquity()
        {
            // Arrange
            var builder = new EquityBuilder();
            var totalMarketValue = 9825;
            
            var equityDTO = 
                new EquityDTO
                {
                    Quantity = 5,
                    Price = 757,
                    Name = "Equity1"
                };
            var expectedMarketValue = Math.Round(equityDTO.Price * equityDTO.Quantity, 2);
            var expectedStockWeight = Math.Round(expectedMarketValue * 100 / totalMarketValue, 2);
            var expectedTransactionCost = Math.Round(expectedMarketValue*0.005M, 2);

            //Act
            var equity = builder.Create(equityDTO, totalMarketValue);
            
            // Assert
            Assert.That(equity, Is.Not.Null);
            Assert.That(equity.Name, Is.EqualTo(equityDTO.Name));
            Assert.That(equity.Price, Is.EqualTo(equityDTO.Price));
            Assert.That(equity.Quantity, Is.EqualTo(equityDTO.Quantity));
            Assert.That(equity.MarketValue, Is.EqualTo(expectedMarketValue), () => "MarketValue");
            Assert.That(equity.TransactionCost, Is.EqualTo(expectedTransactionCost), () => "TransactionCost");
            Assert.That(equity.StockWeight, Is.EqualTo(expectedStockWeight), () => "StockWeight");
        }
    }
}
