using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class CustomerService : BaseEntityService<App.BLL.DTO.Customer, App.DAL.DTO.Customer, ICustomerRepository>, ICustomerService
{
    public CustomerService(ICustomerRepository repository, IMapper<Customer, DAL.DTO.Customer> mapper) : base(repository, mapper)
    {
    }
}