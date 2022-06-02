using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;
using Order = App.BLL.DTO.Order;

namespace App.Public.Mappers;

public class PaymentTypeMapper : BaseMapper<PaymentType ,App.BLL.DTO.PaymentType>
{
    public PaymentTypeMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.BLL.DTO.PaymentType MapToBll(PaymentType paymentType)
    {
        return new BLL.DTO.PaymentType()
        {
            Id = paymentType.Id,
            TypeName = paymentType.TypeName,
            Comment = paymentType.Comment ?? "",
            Orders = paymentType.Orders != null ? paymentType.Orders.Select(x => OrderMapper.MapToBll(x)).ToList() : new List<Order>()
        };
    }
    
    public static PaymentType MapFromBll(BLL.DTO.PaymentType paymentType)
    {
        return new PaymentType()
        {
            Id = paymentType.Id,
            TypeName = paymentType.TypeName,
            Comment = paymentType.Comment ?? "",
            Orders = paymentType.Orders != null ? paymentType.Orders.Select(x => OrderMapper.MapFromBll(x)).ToList() : new List<App.Public.DTO.v1.Order>()
        };
    }
}