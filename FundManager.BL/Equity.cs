namespace FundManager.BL
{
    public class Equity : IStock
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public decimal MarketValue { get; set; }

        public decimal TransactionCost { get; set; }

        public decimal StockWeight { get; set; }
    }
}
