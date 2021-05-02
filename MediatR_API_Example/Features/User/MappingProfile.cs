using AutoMapper;

namespace MediatR_API_Example.Features.User
{
    public class MappingProfile
        : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.User, User>().ReverseMap();

            CreateMap<Domain.Sex, Sex>().ReverseMap();

            CreateMap<Domain.User, AddUser.Command>().ReverseMap();
            CreateMap<Domain.User, UpdateUser.Command>().ReverseMap();
        }
    }
}
