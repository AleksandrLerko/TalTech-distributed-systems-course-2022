using App.Contracts.DAL;
using App.Domain;
using Base.Contracts.Base;
using Base.DAL.EF;


namespace App.DAL.EF.Repositories;

public class CustomerRepository : BaseEntityRepository<App.DAL.DTO.Customer, Customer,AppDbContext>, ICustomerRepository
{
    public CustomerRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Customer, Customer> mapper) : base(dbContext, mapper)
    {
    }
}