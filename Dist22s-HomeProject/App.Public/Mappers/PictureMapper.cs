using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.Mappers;

public class PictureMapper : BaseMapper<Picture ,App.BLL.DTO.Picture>
{
    public PictureMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.BLL.DTO.Picture MapToBll(Picture category)
    {
        return new BLL.DTO.Picture()
        {
            Id = category.Id,
            FilePath = category.FilePath,
            ProductId = category.ProductId,
            // Product = ProductMapper.MapToBll(category.Product!)
        };
    }
    
    public static Picture MapFromBll(BLL.DTO.Picture category)
    {
        return new Picture()
        {
            Id = category.Id,
            FilePath = category.FilePath,
            ProductId = category.ProductId,
            // Product = ProductMapper.MapFromBll(category.Product!)
        };
    }
}