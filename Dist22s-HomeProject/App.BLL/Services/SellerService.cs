using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class SellerService : BaseEntityService<App.BLL.DTO.Seller, App.DAL.DTO.Seller, ISellerRepository>, ISellerService
{
    public SellerService(ISellerRepository repository, IMapper<Seller, DAL.DTO.Seller> mapper) : base(repository, mapper)
    {
    }
}