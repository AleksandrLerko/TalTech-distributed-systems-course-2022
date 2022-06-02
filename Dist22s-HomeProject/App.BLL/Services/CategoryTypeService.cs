using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class CategoryTypeService : BaseEntityService<App.BLL.DTO.CategoryType, App.DAL.DTO.CategoryType, ICategoryTypeRepository>, ICategoryTypeService
{
    public CategoryTypeService(
        ICategoryTypeRepository repository,
        IMapper<CategoryType,
            DAL.DTO.CategoryType> mapper) : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<App.BLL.DTO.CategoryType>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync(userId, noTracking)).Select(r => Mapper.Map(r)!).ToList();
        return res;
    }

    public new async Task<CategoryType?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var res = (await Repository.FirstOrDefaultAsync(id, noTracking));
        return Mapper.Map(res);
    }
}