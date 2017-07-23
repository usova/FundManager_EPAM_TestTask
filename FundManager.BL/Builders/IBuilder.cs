using FundManager.Repositories.DTO;

namespace FundManager.BL
{
    public interface IBuilder
    {
        IStock Create(IStockDTO stockDto, decimal totalMarketValue);
    }
}