using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class ShippingInfoAppUserService : BaseEntityService<App.BLL.DTO.ShippingInfoAppUser,
    App.DAL.DTO.ShippingInfoAppUser,
    IShippingInfoAppUserRepository>,
    IShippingInfoAppUserService
{
    public ShippingInfoAppUserService(IShippingInfoAppUserRepository repository, IMapper<ShippingInfoAppUser, DAL.DTO.ShippingInfoAppUser> mapper) : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<ShippingInfoAppUser>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync(userId, noTracking)).Select(r => Mapper.Map(r)!);
        return res;
    }
    
    public new async Task<ShippingInfoAppUser?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var res = Mapper.Map(await Repository.FirstOrDefaultAsync(id, noTracking));
        return res;
    }
}