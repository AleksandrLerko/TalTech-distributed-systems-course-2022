using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;

namespace App.BLL.Services;

public class CategoryService : BaseEntityService<App.BLL.DTO.Category, App.DAL.DTO.Category, ICategoryRepository>, ICategoryService
{
    public CategoryService(ICategoryRepository repository, IMapper<Category, DAL.DTO.Category> mapper) : base(repository, mapper)
    {
    }
}