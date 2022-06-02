using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class ShippingInfoCustomerService : BaseEntityService<App.BLL.DTO.ShippingInfoCustomer,
        App.DAL.DTO.ShippingInfoCustomer,
        IShippingInfoCustomerRepository>,
    IShippingInfoCustomerService
{
    public ShippingInfoCustomerService(IShippingInfoCustomerRepository repository, IMapper<ShippingInfoCustomer, DAL.DTO.ShippingInfoCustomer> mapper) : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<ShippingInfoCustomer>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync(userId, noTracking)).Select(r => Mapper.Map(r)!);
        return res;
    }
    
    public new async Task<ShippingInfoCustomer?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var res = Mapper.Map(await Repository.FirstOrDefaultAsync(id, noTracking));
        return res;
    }
}