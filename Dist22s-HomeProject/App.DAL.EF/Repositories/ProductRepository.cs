using System.Collections;
using App.Contracts.DAL;
using App.Domain;
using AutoMapper;
using Base.Contracts.Base;
using Base.DAL.EF;

using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ProductRepository : BaseEntityRepository<App.DAL.DTO.Product, Product, AppDbContext>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Product, Product> mapper) : base(dbContext, mapper)
    {
    }
    
    public override async Task<IEnumerable<DTO.Product>> GetAllAsync(bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(p => p.Category)
            .Include(p => p.Currency)
            .Include(p => p.Seller)
            .Include(p => p.Picture)
            .Include(p => p.Specifications)!
            .ThenInclude(x => x.SpecificationTypes)
            .Include(p => p.InStocks)
            .Include(p => p.Feedbacks);
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }

    public override async Task<App.DAL.DTO.Product?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var query = CreateQuery(noTracking);
        query = query
            .Include(p => p.Category)
            .Include(p => p.Currency)
            .Include(p => p.Seller)
            .Include(p => p.Picture)
            .Include(p => p.Specifications)!
            .ThenInclude(x => x.SpecificationTypes)
            .Include(p => p.InStocks)
            .Include(p => p.Feedbacks)
            .Include(p => p.ProductOrders);
    
        var res = await query.FirstOrDefaultAsync(o => o.Id == id);

        return Mapper.Map(res);
    }

    public async Task<IEnumerable<DTO.Product>> GetProductsByCategory(Guid categoryId)
    {
        var query = CreateQuery();
        query = query
            .Include(p => p.Category)
            .Where(i => i.CategoryId == categoryId)
            .Include(p => p.Currency)
            .Include(p => p.Seller)
            .Include(p => p.Picture)
            .Include(p => p.Specifications)
            .Include(p => p.InStocks)
            .Include(p => p.Feedbacks)
            .Include(p => p.ProductOrders);
        
        return (await query.ToListAsync()).Select(x => Mapper.Map(x)!);
    }

    public Task<IEnumerable<DTO.Product>> GetProductByName(string productName)
    {
        var query = CreateQuery();
        query = query
            .Include(p => p.Category)
            .Include(p => p.Currency)
            .Include(p => p.Seller)
            .Include(p => p.Picture)
            .Include(p => p.Specifications)
            .Include(p => p.InStocks)
            .Include(p => p.Feedbacks)
            .Include(p => p.ProductOrders);

        var results = new List<Product>();
        foreach (var product in query)
        {
            if (product.ProductName.ToString().ToLower().Contains(productName.ToLower()))
            {
                results.Add(product);
            }
        }

        return Task.FromResult(( results.ToList()).Select(x => Mapper.Map(x)!));
    }
}