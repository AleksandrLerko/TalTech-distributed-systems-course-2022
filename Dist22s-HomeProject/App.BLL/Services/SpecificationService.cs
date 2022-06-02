using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class SpecificationService : BaseEntityService<App.BLL.DTO.Specification, App.DAL.DTO.Specification, ISpecificationRepository>, ISpecificationService
{
    public SpecificationService(ISpecificationRepository repository, IMapper<Specification, DAL.DTO.Specification> mapper) : base(repository, mapper)
    {
    }

    public new async Task<IEnumerable<Specification>> GetAllAsync(bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync(noTracking)).Select(r => Mapper.Map(r)!);
        return res;
    }
    
    public new async Task<Specification?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var res = Mapper.Map(await Repository.FirstOrDefaultAsync(id, noTracking));
        return res;
    }
}