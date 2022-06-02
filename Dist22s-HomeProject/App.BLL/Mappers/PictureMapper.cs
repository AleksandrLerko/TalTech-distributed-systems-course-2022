using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class PictureMapper : BaseMapper<App.BLL.DTO.Picture, App.DAL.DTO.Picture>
{
    public PictureMapper(IMapper mapper) : base(mapper)
    {
    }
    
}