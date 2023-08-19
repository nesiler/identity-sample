using ISP.Application.Repositories.Contracts;
using ISP.Domain.Entities.Identity;
using ISP.Persistence.Context;
using ISP.Persistence.Repository.Base;

namespace ISP.Persistence.Repository;

public class UserRepository : Repository<User, ISPContext>, IUserRepository
{
    public UserRepository(ISPContext context) : base(context)
    {
    }
}