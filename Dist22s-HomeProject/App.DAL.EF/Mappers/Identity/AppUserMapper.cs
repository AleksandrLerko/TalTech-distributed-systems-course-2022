using App.Domain.Identity;
using AutoMapper;
using Base.Contracts;
using Base.DAL;

namespace App.DAL.EF.Mappers.Identity;

public class AppUserMapper : BaseMapper<App.DAL.DTO.Identity.AppUser, AppUser>
{
    public AppUserMapper(IMapper mapper) : base(mapper)
    {
    }
}