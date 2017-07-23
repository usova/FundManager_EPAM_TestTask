﻿namespace FundManager.Repositories.DTO
{
    public class EquityDTO : IStockDTO
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }
    }
}
