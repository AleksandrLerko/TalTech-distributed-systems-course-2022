using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using AutoMapper;
using Base.BLL;
using Base.Contracts.Base;
using Base.DAL;

namespace App.BLL.Mappers;

public class FeedbackMapper : BaseMapper<App.BLL.DTO.Feedback, App.DAL.DTO.Feedback>
{
    public FeedbackMapper(IMapper mapper) : base(mapper)
    {
    }
}