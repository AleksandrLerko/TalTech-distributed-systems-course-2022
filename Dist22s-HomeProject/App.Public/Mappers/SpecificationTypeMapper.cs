using AutoMapper;
using Base.DAL;
using SpecificationType = App.Public.DTO.v1.SpecificationType;

namespace App.Public.Mappers;

public class SpecificationTypeMapper : BaseMapper<SpecificationType ,App.BLL.DTO.SpecificationType>
{
    public SpecificationTypeMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.BLL.DTO.SpecificationType MapToBll(SpecificationType specificationType)
    {
        var res = new BLL.DTO.SpecificationType()
        {
            Id = specificationType.Id,
            TypeName = specificationType.TypeName,
            TypeValue = specificationType.TypeValue,
            SpecificationId = specificationType.SpecificationId,
            // Specification = specificationType.Specification != null ? SpecificationMapper.MapToBll(specificationType.Specification) : null
        };
        return res;
    }
    
    public static SpecificationType MapFromBll(BLL.DTO.SpecificationType specificationType)
    {
        return new SpecificationType()
        {
            Id = specificationType.Id,
            TypeName = specificationType.TypeName,
            TypeValue = specificationType.TypeValue,
            SpecificationId = specificationType.SpecificationId,
            // Specification = specificationType.Specification != null ? SpecificationMapper.MapFromBll(specificationType.Specification) : null
        };
    }
}