using App.BLL.DTO;
using AutoMapper;
using Base.DAL;
using Customer = App.Public.DTO.v1.Customer;

namespace App.Public.Mappers;

public class CustomerMapper : BaseMapper<Customer ,App.BLL.DTO.Customer>
{
    public CustomerMapper(IMapper mapper) : base(mapper)
    {
    }
    
    public static App.BLL.DTO.Customer MapToBll(Customer customer)
    {
        return new BLL.DTO.Customer()
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            ShippingInfoId = customer.ShippingInfoId,
            // Feedbacks = customer.Feedbacks != null ? customer.Feedbacks.Select(x => FeedbackMapper.MapToBll(x)).ToList() : new List<Feedback>(),
            Orders = customer.Orders != null ? customer.Orders.Select(x => OrderMapper.MapToBll(x)).ToList() : new List<Order>()
        };
    }
    
    public static Customer MapFromBll(BLL.DTO.Customer customer)
    {
        return new Customer()
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            ShippingInfoId = customer.ShippingInfoId,
            // Feedbacks = customer.Feedbacks != null ? customer.Feedbacks.Select(x => FeedbackMapper.MapFromBll(x)).ToList() : new List<App.Public.DTO.v1.Feedback>(),
            Orders = customer.Orders != null ? customer.Orders.Select(x => OrderMapper.MapFromBll(x)).ToList() : new List<App.Public.DTO.v1.Order>()
        };
    }
}