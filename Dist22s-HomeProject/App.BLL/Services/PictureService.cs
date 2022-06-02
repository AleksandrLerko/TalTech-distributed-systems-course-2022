using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class PictureService : BaseEntityService<App.BLL.DTO.Picture, App.DAL.DTO.Picture, IPictureRepository>, IPictureService
{
    public PictureService(IPictureRepository repository, IMapper<Picture, DAL.DTO.Picture> mapper) : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<Picture>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync(userId, noTracking)).Select(r => Mapper.Map(r)!);
        return res;
    }
    
    public new async Task<Picture?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var res = Mapper.Map(await Repository.FirstOrDefaultAsync(id, noTracking));
        return res;
    }

    public async Task<Picture?> GetPictureByProductId(Guid productId, bool noTracking = true)
    {
        var res = Mapper.Map(await Repository.GetPictureByProductId(productId, noTracking));
        return res;
    }
}