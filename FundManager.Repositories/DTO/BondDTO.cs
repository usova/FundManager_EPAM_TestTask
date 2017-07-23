namespace FundManager.Repositories.DTO
{
    public class BondDTO : IStockDTO
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }
    }
}
