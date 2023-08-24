using OnMed.DataAccess.ViewModels.Users;
using OnMed.Persistance.Dtos.Auth;
using OnMed.Persistance.Dtos.Users;

namespace OnMed.Service.Interfaces.Users;

public interface IUserProfileService
{
    public Task<UserViewModel?> GetProfileInfoAsync();
    public Task<bool> UpdateAsync(UserUpdateDto dto);
    public Task<bool> DeleteAsync();
    public Task<bool> DeleteImageAsync();
    public Task<bool> ResetPasswordAsync(ResetPasswordDto dto);
    public Task<bool> UploadImageAsync(UploadImageDto file);
}
