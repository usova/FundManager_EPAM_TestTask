namespace FundManager.BL
{
    public interface IStock
    {
        string Name { get; set; }

        decimal Price { get; set; }

        decimal Quantity { get; set; }

        decimal MarketValue { get; set; }

        decimal TransactionCost { get; set; }

        decimal StockWeight { get; set; }
    }
}
