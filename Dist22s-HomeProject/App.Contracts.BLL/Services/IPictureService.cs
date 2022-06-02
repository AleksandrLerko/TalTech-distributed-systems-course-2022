using App.Contracts.DAL;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IPictureService : IEntityService<App.BLL.DTO.Picture>, IPictureRepositoryCustom<App.BLL.DTO.Picture>
{
    
}