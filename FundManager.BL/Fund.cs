using System;
using System.Collections.Generic;
using System.Linq;
using FundManager.Repositories;
using FundManager.Repositories.DTO;

namespace FundManager.BL
{
    public class Fund
    {
        public static readonly decimal EquityTolerance = 200000M;
        public static readonly decimal BondTolerance = 100000M;

        private const string EquityNamePrefix = "Equity";
        private const string BondNamePrefix = "Bond";

        private readonly IBuilderFactory builderFactory;
        private readonly IStockRepository stockRepository;

        public Fund(IBuilderFactory builderFactory, IStockRepository stockRepository)
        {
            if (builderFactory == null)
                throw new ArgumentNullException(nameof(builderFactory));

            if (stockRepository == null)
                throw new ArgumentNullException(nameof(stockRepository));

            this.builderFactory = builderFactory;
            this.stockRepository = stockRepository;
        }

        public void AddEquity(decimal price, decimal quantity)
        {
            if (price <= 0)
                throw new ArgumentOutOfRangeException(nameof(price));

            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity));

            stockRepository.AddStock(
                new EquityDTO
                {
                    Name = GetStockName<EquityDTO>(),
                    Price = price,
                    Quantity = quantity
                });
        }

        public void AddBond(decimal price, decimal quantity)
        {
            if (price <= 0)
                throw new ArgumentOutOfRangeException(nameof(price));

            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity));

            stockRepository.AddStock(
                new BondDTO
                {
                    Name = GetStockName<BondDTO>(),
                    Price = price,
                    Quantity = quantity
                });
        }

        public void RemoveStock(string stockName)
        {
            if(string.IsNullOrWhiteSpace(stockName))
                throw new ArgumentNullException(nameof(stockName));

            stockRepository.RemoveStock(stockName);
        }

        public List<IStock> GetAllStock()
        {
            return
                stockRepository.GetAllStocks()
                    .Select(s => builderFactory.GetBuilder(s).Create(s, GetTotalMarketValue()))
                    .ToList();
        }

        public List<Equity> GetEquities()
        {
            return
                stockRepository.GetStocks<EquityDTO>()
                    .Select(s => builderFactory.GetBuilder(s).Create(s, GetTotalMarketValue()))
                    .Cast<Equity>()
                    .ToList();
        }

        public List<Bond> GetBonds()
        {
            return
                stockRepository.GetStocks<BondDTO>()
                    .Select(s => builderFactory.GetBuilder(s).Create(s, GetTotalMarketValue()))
                    .Cast<Bond>()
                    .ToList();
        }

        public StockTotals GetBondTotals()
        {
            var bonds = GetBonds();

            return 
                new StockTotals
                {
                    TotalMarketValue = bonds.Sum(b => b.MarketValue),
                    TotalNumbers = bonds.Count,
                    TotalQuantity = bonds.Sum(b => b.Quantity),
                    TotalStockWeight = bonds.Sum(b => b.StockWeight)
                };
        }

        public StockTotals GetEquityTotals()
        {
            var equities = GetEquities();

            return
                new StockTotals
                {
                    TotalMarketValue = equities.Sum(e => e.MarketValue),
                    TotalNumbers = equities.Count,
                    TotalQuantity = equities.Sum(e => e.Quantity),
                    TotalStockWeight = equities.Sum(e => e.StockWeight)
                };
        }

        public StockTotals GeStocksTotals()
        {
            var stocks = GetAllStock();

            return
                new StockTotals
                {
                    TotalMarketValue = stocks.Sum(x => x.MarketValue),
                    TotalNumbers = stocks.Count,
                    TotalQuantity = stocks.Sum(x => x.Quantity),
                    TotalStockWeight = stocks.Sum(x => x.StockWeight)
                };
        }

        private string GetStockName<T>()
            where T : IStockDTO
        {
            return typeof(T) == typeof(EquityDTO)
                ? EquityNamePrefix + (stockRepository.GetCount<T>() + 1)
                : BondNamePrefix + (stockRepository.GetCount<T>() + 1);
        }

        private decimal GetTotalMarketValue()
        {
            return
                stockRepository
                    .GetAllStocks()
                    .Sum(s => s.Price * s.Quantity);
        }
    }
}
