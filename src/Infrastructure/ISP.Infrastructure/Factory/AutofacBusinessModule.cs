using Autofac;
using ISP.Application.Repositories.Contracts;
using ISP.Application.Services;
using ISP.Application.Validators.Identity;
using ISP.Domain.Entities.Common;
using ISP.Infrastructure.Auth.CurrentUser;
using ISP.Infrastructure.Services;
using ISP.Persistence.Repository;

namespace ISP.Infrastructure.Factory;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        //USER
        builder.RegisterType<CurrentUser>().As<ICurrentUser>();
        builder.RegisterType<UserService>().As<IUserService>();
        builder.RegisterType<UserRepository>().As<IUserRepository>();

        //AUTHENTICATION
        builder.RegisterType<RoleService>().As<IRoleService>();

        //IDENTITY
        builder.RegisterType<ErrorDescriber>().AsSelf();
        builder.RegisterType<TokenService>().As<ITokenService>();
    }
}