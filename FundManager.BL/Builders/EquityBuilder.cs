namespace FundManager.BL
{
    public class EquityBuilder : StockBuilder
    {
        protected override decimal TransactionCostPercent => 0.5M;

        protected override IStock GetInstance()
        {
            return new Equity();
        }
    }
}
