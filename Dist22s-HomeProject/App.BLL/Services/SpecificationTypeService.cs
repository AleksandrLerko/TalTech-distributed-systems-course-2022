using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class SpecificationTypeService : BaseEntityService<App.BLL.DTO.SpecificationType,
    App.DAL.DTO.SpecificationType,
    ISpecificationTypeRepository>,
    ISpecificationTypeService
{
    public SpecificationTypeService(ISpecificationTypeRepository repository, IMapper<SpecificationType, DAL.DTO.SpecificationType> mapper) : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<SpecificationType>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync(userId, noTracking)).Select(r => Mapper.Map(r)!);
        return res;
    }
    
    public new async Task<SpecificationType?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var res = Mapper.Map(await Repository.FirstOrDefaultAsync(id, noTracking));
        return res;
    }
}