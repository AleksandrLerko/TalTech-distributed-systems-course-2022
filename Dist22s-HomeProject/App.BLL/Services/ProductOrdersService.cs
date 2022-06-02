using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class ProductOrdersService : BaseEntityService<App.BLL.DTO.ProductOrders, App.DAL.DTO.ProductOrders, IProductOrdersRepository>, IProductOrdersService
{
    public ProductOrdersService(IProductOrdersRepository repository, IMapper<ProductOrders, DAL.DTO.ProductOrders> mapper) : base(repository, mapper)
    {
    }
}