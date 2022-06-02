using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;
using Base.Contracts.Base;
using Base.DAL;

namespace App.BLL.Mappers;

public class SpecificationMapper : BaseMapper<App.BLL.DTO.Specification, App.DAL.DTO.Specification>
{
    public SpecificationMapper(IMapper mapper) : base(mapper)
    {
    }
}