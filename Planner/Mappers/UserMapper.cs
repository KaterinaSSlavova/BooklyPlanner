using AutoMapper;
using Domain.Entities;
using Planner.ViewModels;

namespace Planner.Mappers
{
	public class UserMapper: Profile
	{
        public UserMapper()
        {
            CreateMap<AccountLogIn, User>();
            CreateMap<AccountRegister, User>()
                .ForSourceMember(src => src.Image, opt =>opt.DoNotValidate());
        }
    }
}
