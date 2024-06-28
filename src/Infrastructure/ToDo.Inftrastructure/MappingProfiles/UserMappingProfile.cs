using AutoMapper;
using ToDo.Application.Users.LoginUser;
using ToDo.Application.Users.RegisterUser;
using ToDo.Contracts.Users.Request;
using ToDo.Contracts.Users.Response;
using ToDo.Domain.Entities;

namespace ToDo.Inftrastructure.MappingProfiles;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<RegisterUserRequest, RegisterUserCommand>()
            .ConvertUsing(src => new RegisterUserCommand(src.Email, src.Password));
        CreateMap<LoginUserRequest, LoginUserQuery>()
            .ConvertUsing(src => new LoginUserQuery(src.Email, src.Password));
        CreateMap<User, LoginUserResponse>();
    }
}