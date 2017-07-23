using System;
using FundManager.BL.Utils;
using FundManager.Repositories.DTO;

namespace FundManager.BL
{
    public abstract class StockBuilder : IBuilder
    {
        protected abstract decimal TransactionCostPercent { get; }

        public virtual IStock Create(IStockDTO stockDto, decimal totalMarketValue)
        {
            if (stockDto == null)
                throw new ArgumentNullException(nameof(stockDto));

            var stock = GetInstance();

            PropertyMapper.Map(source: stockDto, dectination: stock);

            stock.MarketValue = Math.Round(CalculateMarketValue(stock.Price, stock.Quantity), 2);
            stock.TransactionCost = Math.Round(CalculateTransactionCost(stock.MarketValue), 2);
            stock.StockWeight = Math.Round(CalculateStockWeight(totalMarketValue: totalMarketValue,
                marketValue: stock.MarketValue), 2);

            return stock;
        }

        protected abstract IStock GetInstance();

        protected virtual decimal CalculateStockWeight(decimal totalMarketValue, decimal marketValue)
        {
            return totalMarketValue != 0
                ? marketValue * 100 / totalMarketValue
                : 100;
        }

        protected virtual decimal CalculateMarketValue(decimal price, decimal quantity)
        {
            return price * quantity;
        }

        protected virtual decimal CalculateTransactionCost(decimal marketValue)
        {
            return marketValue * TransactionCostPercent / 100;
        }
    }
}
