using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts.Base;
using Base.Contracts.BLL;

namespace App.BLL.Services;

public class FeedbackService : BaseEntityService<App.BLL.DTO.Feedback, App.DAL.DTO.Feedback, IFeedbackRepository>, IFeedbackService
{
    public FeedbackService(IFeedbackRepository repository, IMapper<Feedback, DAL.DTO.Feedback> mapper) : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<Feedback>> GetAllAsync(Guid userId, bool noTracking = true)
    {
        var res = (await Repository.GetAllAsync(userId, noTracking)).Select(r => Mapper.Map(r)!);
        return res;
    }

    public new async Task<Feedback?> FirstOrDefaultAsync(Guid id, bool noTracking = true)
    {
        var res = Mapper.Map(await Repository.FirstOrDefaultAsync(id, noTracking));
        return res;
    }
}