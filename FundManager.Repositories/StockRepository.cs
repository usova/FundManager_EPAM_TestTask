using System;
using System.Collections.Generic;
using System.Linq;
using FundManager.Repositories.DTO;

namespace FundManager.Repositories
{
    public class StockRepository : IStockRepository
    {
        //For testing purposes I use static List instead of Database
        private static List<IStockDTO> stocks = new List<IStockDTO>(); 

        public void AddStock(IStockDTO stock)
        {
            stocks.Add(stock);
        }

        public void RemoveStock(string stockName)
        {
            var stock = stocks.FirstOrDefault(x => x.Name.Equals(stockName, StringComparison.OrdinalIgnoreCase));
            
            if(stock != null)
                stocks.Remove(stock);
        }

        public List<IStockDTO> GetAllStocks()
        {
            return stocks.ToList();
        }

        public List<T> GetStocks<T>()
            where T : IStockDTO
        {
            return 
                stocks
                    .Where(s => s.GetType() == typeof(T))
                    .Cast<T>()
                    .ToList();
        }

        public int GetCount<T>()
            where T : IStockDTO
        {
            return stocks.Count(x => x.GetType() == typeof (T));
        }
    }
}
