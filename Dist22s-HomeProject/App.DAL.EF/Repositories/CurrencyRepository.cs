using App.Contracts.DAL;
using App.DAL.EF.Mappers;
using App.Domain;
using Base.Contracts;
using Base.Contracts.Base;
using Base.DAL.EF;

using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class CurrencyRepository : BaseEntityRepository<App.DAL.DTO.Currency, Currency, AppDbContext>, ICurrencyRepository
{
    public CurrencyRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Currency, Currency> mapper) : base(dbContext, mapper)
    {
    }

    // public async Task<IEnumerable<App.DAL.DTO.Currency>> GetAllByNameAsync(string nameFragment, bool noTracking = true)
    // {
    //     var query = CreateQuery(noTracking);
    //     return (await query.Where(a => a.CurrencyName.ContainsValue(nameFragment.ToUpper())).ToListAsync())
    //         .Select(x => Mapper.Map(x)!);
    // }

    // public async Task<IEnumerable<DTO.Currency>> GetAllAsync(Guid userId, bool noTracking = true)
    // {
    //     var query = CreateQuery(noTracking);
    //
    //     return (await query.ToListAsync()).Select(r => Mapper.Map(r)!);
    // }
}