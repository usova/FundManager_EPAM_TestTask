namespace FundManager.BL
{
    public class BondBuilder : StockBuilder
    {
        protected override decimal TransactionCostPercent => 2M;

        protected override IStock GetInstance()
        {
            return new Bond();
        }
    }
}
