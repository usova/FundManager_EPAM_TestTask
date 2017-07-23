namespace FundManager.Repositories.DTO
{
    public interface IStockDTO
    {
        string Name { get; set; }

        decimal Price { get; set; }

        decimal Quantity { get; set; }
    }
}
