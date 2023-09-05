using OnMed.DataAccess.ViewModels.Heads;
using OnMed.Persistance.Dtos.Auth;
using OnMed.Persistance.Dtos.Heads;
using OnMed.Persistance.Dtos.Users;

namespace OnMed.Service.Interfaces.Heads;

public interface IHeadProfileService
{
    public Task<HeadViewModel?> GetProfileInfoAsync();
    public Task<bool> UpdateAsync(HeadUpdateDto dto);
    public Task<bool> DeleteAsync();
    public Task<bool> DeleteImageAsync();
    public Task<bool> ResetPasswordAsync(ResetPasswordDto dto);
    public Task<bool> UploadImageAsync(UploadImageDto file);
}
