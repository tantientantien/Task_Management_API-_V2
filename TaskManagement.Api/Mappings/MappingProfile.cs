using AutoMapper;

class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TaskItem, TaskDataDto>();
            // .ForMember(dest => dest.AttachmentCount, opt => opt.MapFrom(src => src.Attachments.Count))
            // .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.TaskComments.Count));
        CreateMap<TaskWriteDto, TaskItem>();
    }
}