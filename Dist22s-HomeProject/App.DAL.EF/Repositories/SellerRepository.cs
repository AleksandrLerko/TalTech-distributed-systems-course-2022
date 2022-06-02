using App.Contracts.DAL;
using App.Domain;
using Base.Contracts.Base;
using Base.DAL.EF;


namespace App.DAL.EF.Repositories;

public class SellerRepository : BaseEntityRepository<App.DAL.DTO.Seller, Seller, AppDbContext>, ISellerRepository
{
    public SellerRepository(AppDbContext dbContext, IMapper<App.DAL.DTO.Seller, Seller> mapper) : base(dbContext, mapper)
    {
    }
}