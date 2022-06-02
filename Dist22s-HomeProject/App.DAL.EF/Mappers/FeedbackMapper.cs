using App.Domain;
using AutoMapper;
using Base.DAL;


namespace App.DAL.EF.Mappers;

public class FeedbackMapper : BaseMapper<App.DAL.DTO.Feedback, Feedback>
{
    public FeedbackMapper(IMapper mapper) : base(mapper)
    {
    }
}