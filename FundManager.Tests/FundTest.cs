using NUnit.Framework;
using System;
using FundManager.BL;
using FundManager.Repositories;
using FundManager.Repositories.DTO;
using FakeItEasy;

namespace FundManager.Tests
{
    [TestFixture]
    public class FundTest
    {
        [Test]
        public void CreateInstance_BuilderFactoryIsNull_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => new Fund(null, new StockRepository()));
        }

        [Test]
        public void CreateInstance_RepositoryIsNull_ThrowException()
        {
            Assert.Throws<ArgumentNullException>(() => new Fund(new BuilderFactory(), null));
        }

        [Test]
        public void AddEquity_PriceIsLessThanZero_ThrowException()
        {
            //Arrange
            var price = -1M;
            var builderFactory = A.Fake<IBuilderFactory>();
            var stockRepository = A.Fake<IStockRepository>();
            var fund = new Fund(builderFactory, stockRepository);

            //Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => fund.AddEquity(price, 1));
        }

        [Test]
        public void AddEquity_QuantityIsLessThanZero_ThrowException()
        {
            //Arrange
            var price = 200M;
            var quantity = -1M;
            var builderFactory = A.Fake<IBuilderFactory>();
            var stockRepository = A.Fake<IStockRepository>();
            var fund = new Fund(builderFactory, stockRepository);

            //Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => fund.AddEquity(price, quantity));
        }

        [Test]
        public void AddEquity_PositiveTest_Equity()
        {
            //Arrange
            var price = 200M;
            var quantity = 1M;
            var builderFactory = A.Fake<IBuilderFactory>();
            var stockRepository = A.Fake<IStockRepository>();
            var fund = new Fund(builderFactory, stockRepository);

            //Act 
            fund.AddEquity(price, quantity);

            //Assert
            A.CallTo(stockRepository)
                .Where(call => call.Method.Name == "AddStock")
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void AddBond_PriceIsLessThanZero_ThrowException()
        {
            //Arrange
            var price = -1M;
            var builderFactory = A.Fake<IBuilderFactory>();
            var stockRepository = A.Fake<IStockRepository>();
            var fund = new Fund(builderFactory, stockRepository);

            //Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => fund.AddBond(price, 1));
        }

        [Test]
        public void AddBond_QuantityIsLessThanZero_ThrowException()
        {
            //Arrange
            var price = 200M;
            var quantity = -1M;
            var builderFactory = A.Fake<IBuilderFactory>();
            var stockRepository = A.Fake<IStockRepository>();
            var fund = new Fund(builderFactory, stockRepository);

            //Act and Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => fund.AddBond(price, quantity));
        }

        [Test]
        public void AddBond_PositiveTest_Equity()
        {
            //Arrange
            var price = 200M;
            var quantity = 1M;
            var builderFactory = A.Fake<IBuilderFactory>();
            var stockRepository = A.Fake<IStockRepository>();
            var fund = new Fund(builderFactory, stockRepository);

            //Act 
            fund.AddBond(price, quantity);

            //Assert
            A.CallTo(stockRepository)
                .Where(call => call.Method.Name == "AddStock")
                .MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void RemoveStock_StockNameIsNullOrWhiteSpace_ThrowException()
        {
            //Arrange
            var stockName = string.Empty;
            var builderFactory = A.Fake<IBuilderFactory>();
            var stockRepository = A.Fake<IStockRepository>();
            var fund = new Fund(builderFactory, stockRepository);

            //Act and Assert
            Assert.Throws<ArgumentNullException>(() => fund.RemoveStock(stockName));
        }

        [Test]
        public void RemoveStock_PositiveTest_StockRemoved()
        {
            //Arrange
            var stockName = "Bond1";
            var builderFactory = A.Fake<IBuilderFactory>();
            var stockRepository = A.Fake<IStockRepository>();
            var fund = new Fund(builderFactory, stockRepository);

            //Act 
            fund.RemoveStock(stockName);

            //Assert
            A.CallTo(stockRepository)
                .Where(call => call.Method.Name == "RemoveStock")
                .MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
