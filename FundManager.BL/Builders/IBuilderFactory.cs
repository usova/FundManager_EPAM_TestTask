using FundManager.Repositories.DTO;

namespace FundManager.BL
{
    public  interface IBuilderFactory
    {
        IBuilder GetBuilder(IStockDTO stock);
    }
}
