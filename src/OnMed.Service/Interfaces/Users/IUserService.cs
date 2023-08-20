using OnMed.Application.Utils;
using OnMed.DataAccess.ViewModels.Users;
using OnMed.Persistance.Dtos.Users;

namespace OnMed.Service.Interfaces.Users;

public interface IUserService
{
    public Task<IList<UserViewModel>> GetAllAsync(PaginationParams @params);
    public Task<bool> UpdateAsync(long UserId, UserUpdateDto dto);
    public Task<bool> DeleteAsync(long UserId);
}
