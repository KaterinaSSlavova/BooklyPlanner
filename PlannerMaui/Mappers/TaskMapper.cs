using AutoMapper;
using Domain.Entities;
using Planner.ViewModels;

namespace Planner.Mappers
{
    public class TaskMapper: Profile
    {
        public TaskMapper()
        {
            CreateMap<CreateTaskViewModel, ReadingTask>();
            CreateMap<Book, BookViewModel>().ReverseMap();
            CreateMap<ReadingTask, ReadingTaskViewModel>()
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.Ignore());
        }
    }
}
