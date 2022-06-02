using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class ProductService : BaseEntityService<App.BLL.DTO.Product, App.DAL.DTO.Product, IProductRepository>, IProductService
{
    // protected IAppUnitOfWork UnitOfWork;
    
    public ProductService(IProductRepository repository, IMapper<Product, DAL.DTO.Product> mapper) : base(repository, mapper)
    {
        // UnitOfWork = unitOfWork;
    }

    public new async Task<IEnumerable<Product>> GetAllAsync(bool noTracking = true)
    {
        var list = new List<Product>();
        foreach (var elem in await Repository.GetAllAsync(noTracking))
        {
            list.Add(Mapper.Map(elem)!);
        }
        return list.AsEnumerable();
    }
    
    public new async Task<Product?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var res = Mapper.Map(await Repository.FirstOrDefaultAsync(id, noTracking));
        return res;
    }

    public async Task<IEnumerable<Product>> GetProductsByCategory(Guid categoryId)
    {
        var list = new List<Product>();
        foreach (var elem in await Repository.GetProductsByCategory(categoryId))
        {
            list.Add(Mapper.Map(elem)!);
        }
        // var res = (await Repository.GetProductsByCategory(categoryId)).Select(r => Mapper.Map(r)!);
        return list.AsEnumerable();
    }

    public async Task<IEnumerable<Product>> GetProductByName(string productName)
    {
        var list = new List<Product>();
        foreach (var elem in await Repository.GetProductByName(productName))
        {
            list.Add(Mapper.Map(elem)!);
        }
        // var res = (await Repository.GetProductByName(productName)).Select(r => Mapper.Map(r)!);
        return list.AsEnumerable();
    }
}