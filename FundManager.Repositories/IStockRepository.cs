using System.Collections.Generic;
using FundManager.Repositories.DTO;

namespace FundManager.Repositories
{
    public  interface IStockRepository
    {
        void AddStock(IStockDTO stock);

        void RemoveStock(string stock);

        List<IStockDTO> GetAllStocks();

        List<T> GetStocks<T>() where T : IStockDTO;

        int GetCount<T>() where T : IStockDTO;
    }
}
