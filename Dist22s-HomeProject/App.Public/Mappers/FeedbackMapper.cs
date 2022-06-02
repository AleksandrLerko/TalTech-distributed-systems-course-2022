using App.Public.Mappers.Identity;
using AutoMapper;
using Base.DAL;
using Feedback = App.Public.DTO.v1.Feedback;

namespace App.Public.Mappers;

public class FeedbackMapper : BaseMapper<Feedback ,App.BLL.DTO.Feedback>
{
    public FeedbackMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.BLL.DTO.Feedback MapToBll(Feedback feedback)
    {
        return new BLL.DTO.Feedback()
        {
            Id = feedback.Id,
            Value = feedback.Value,
            TimeWhenPosted = feedback.TimeWhenPosted,
            AppUserId = feedback.AppUserId,
            // AppUser = feedback.AppUser != null ? AppUserMapper.MapToBll(feedback.AppUser) : null,
            // CustomerId = feedback.CustomerId,
            // Customer = feedback.Customer != null ? CustomerMapper.MapToBll(feedback.Customer): null,
            ProductId = feedback.ProductId,
            // Product = feedback.Product != null ? ProductMapper.MapToBll(feedback.Product) : null
        };
    }
    
    public static Feedback MapFromBll(BLL.DTO.Feedback feedback)
    {
        return new Feedback()
        {
            Id = feedback.Id,
            Value = feedback.Value,
            TimeWhenPosted = feedback.TimeWhenPosted,
            AppUserId = feedback.AppUserId,
            // AppUser = feedback.AppUser != null ? AppUserMapper.MapFromBll(feedback.AppUser) : null,
            // CustomerId = feedback.CustomerId,
            // Customer = feedback.Customer != null ? CustomerMapper.MapFromBll(feedback.Customer): null,
            ProductId = feedback.ProductId,
            // Product = feedback.Product != null ? ProductMapper.MapFromBll(feedback.Product) : null
        };
    }
}