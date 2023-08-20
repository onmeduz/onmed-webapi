using OnMed.DataAccess.Common.Interfaces;
using OnMed.DataAccess.ViewModels.Users;
using OnMed.Domain.Entities.Users;

namespace OnMed.DataAccess.Interfaces.Users;

public interface IUserRepository : IRepository<User>, IGetByPhoneNumber<User?>,
    IGetAll<UserViewModel>
{
    public Task<UserViewModel?> GetByIdViewAsync(long id);
}
