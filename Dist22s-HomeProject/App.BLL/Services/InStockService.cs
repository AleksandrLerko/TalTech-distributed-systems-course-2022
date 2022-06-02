using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class InStockService : BaseEntityService<App.BLL.DTO.InStock, App.DAL.DTO.InStock, IInStockRepository>, IInStockService
{
    public InStockService(IInStockRepository repository, IMapper<InStock, DAL.DTO.InStock> mapper) : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<InStock>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync(userId, noTracking)).Select(r => Mapper.Map(r)!);
        return res;
    }
    
    public new async Task<InStock?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var res = Mapper.Map(await Repository.FirstOrDefaultAsync(id, noTracking));
        return res;
    }
}