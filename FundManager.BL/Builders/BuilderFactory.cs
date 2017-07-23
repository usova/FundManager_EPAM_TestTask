using System;
using System.Collections.Generic;
using FundManager.Repositories.DTO;

namespace FundManager.BL
{
    public class BuilderFactory : IBuilderFactory
    {
        private Dictionary<Type, Func<IBuilder>> strategy = new Dictionary<Type, Func<IBuilder>>
        {
            { typeof(BondDTO), () => new BondBuilder() },
            { typeof(EquityDTO), () => new EquityBuilder() }
            //TODO: here add new types of stocks
        };

        public IBuilder GetBuilder(IStockDTO stock)
        {
            if (stock == null)
                throw new ArgumentNullException(nameof(stock));

            return strategy[stock.GetType()]();
        }
    }
}