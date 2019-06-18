using Dynamic.Domain.DynamicContext.Entities;
using Dynamic.Domain.DynamicContext.Repositories;
using Dynamic.Infra.DynamicContext.DataContext;

namespace Dynamic.Infra.DynamicContext.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DynamicDataContext context) : base(context) { }
    }
}