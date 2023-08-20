using OnMed.DataAccess.Common.Interfaces;
using OnMed.DataAccess.ViewModels.Users;
using OnMed.Domain.Entities.Users;
using static Dapper.SqlMapper;

namespace OnMed.DataAccess.Interfaces.Users;

public interface IUserRepository : IRepository<User>, IGetByPhoneNumber<User?>
{
    public Task<UserViewModel?> GetByIdViewAsync(long id);
}
