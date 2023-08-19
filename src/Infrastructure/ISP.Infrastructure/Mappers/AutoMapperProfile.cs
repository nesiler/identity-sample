using AutoMapper;
using ISP.Application.Requests.Identity.Role;
using ISP.Application.Requests.Identity.User;
using ISP.Application.ViewModels.Role;
using ISP.Application.ViewModels.User;
using ISP.Domain.Entities.Identity;

namespace ISP.Infrastructure.Mappers;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        //USER
        CreateMap<User, CreateUserRequest>().ReverseMap();
        CreateMap<User, UpdateUserRequest>().ReverseMap();
        CreateMap<User, DeleteUserRequest>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<User, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));


        //ROLE
        CreateMap<Role, CreateRoleRequest>().ReverseMap();
        CreateMap<Role, UpdateRoleRequest>().ReverseMap();
        CreateMap<Role, Role>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<RoleDto, Role>().ReverseMap();
    }
}