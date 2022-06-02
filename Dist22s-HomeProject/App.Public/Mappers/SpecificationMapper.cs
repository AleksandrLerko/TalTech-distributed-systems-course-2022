using App.BLL.DTO;
using AutoMapper;
using Base.DAL;
using Specification = App.Public.DTO.v1.Specification;

namespace App.Public.Mappers;

public class SpecificationMapper : BaseMapper<Specification ,App.BLL.DTO.Specification>
{
    public SpecificationMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.BLL.DTO.Specification MapToBll(Specification specification)
    {
        var res = new BLL.DTO.Specification()
        {
            Id = specification.Id,
            SpecificationName = specification.SpecificationName,
            ProductId = specification.ProductId,
            // Product = specification.Product != null ? ProductMapper.MapToBll(specification.Product) : null,
            SpecificationTypes = specification.SpecificationTypes != null ? specification.SpecificationTypes.Select(x => SpecificationTypeMapper.MapToBll(x)).ToList() : new List<SpecificationType>()
        };
        return res;
    }
    
    public static Specification MapFromBll(BLL.DTO.Specification specification)
    {
        return new Specification()
        {
            Id = specification.Id,
            SpecificationName = specification.SpecificationName,
            ProductId = specification.ProductId,
            // Product = specification.Product != null ? ProductMapper.MapFromBll(specification.Product) : null,
            SpecificationTypes = specification.SpecificationTypes != null ? specification.SpecificationTypes.Select(x => SpecificationTypeMapper.MapFromBll(x)).ToList() : new List<App.Public.DTO.v1.SpecificationType>()
        };
    }
}