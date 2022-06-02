using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class OrderService : BaseEntityService<App.BLL.DTO.Order, App.DAL.DTO.Order, IOrderRepository>, IOrderService
{
    public OrderService(IOrderRepository repository, IMapper<Order, DAL.DTO.Order> mapper) : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<Order>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync(userId, noTracking)).Select(r => Mapper.Map(r)!).ToList();
        return res;
    }

    public new async Task<Order?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var res = Mapper.Map(await Repository.FirstOrDefaultAsync(id, noTracking));
        return res;
    }
}