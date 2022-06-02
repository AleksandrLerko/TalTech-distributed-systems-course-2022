using App.Domain;
using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class PictureMapper : BaseMapper<App.DAL.DTO.Picture, Picture>
{
    public PictureMapper(IMapper mapper) : base(mapper)
    {
    }
    
}