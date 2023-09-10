using OnMed.Application.Utils;
using OnMed.DataAccess.ViewModels.Administrators;
using OnMed.Persistance.Dtos.Administrators;
using OnMed.Persistance.Dtos.Users;

namespace OnMed.Service.Interfaces.Administrators;

public interface IAdministratorsService
{
    public Task<AdministratorViewModel> GetProfileInfoAsync();
    public Task<IList<AdministratorViewModel>> GetAllAsync(PaginationParams @params);
    public Task<bool> CreateAsync(AdministratorCreateDto dto);
    public Task<bool> UpdateAsync(long adminId, AdministratorUpdateDto dto);
    public Task<bool> UpdateImageAsync(UploadImageDto dto);
    public Task<bool> DeleteAsync(long adminId);
    public Task<long> CountAsync();
    public Task<IList<AdministratorViewModel>> SearchAsync(string search);
}
