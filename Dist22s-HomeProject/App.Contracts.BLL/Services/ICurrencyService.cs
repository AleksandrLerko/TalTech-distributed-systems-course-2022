using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ICurrencyService : IEntityService<App.BLL.DTO.Currency>, ICurrencyRepositoryCustom<App.BLL.DTO.Currency>
{
    
}