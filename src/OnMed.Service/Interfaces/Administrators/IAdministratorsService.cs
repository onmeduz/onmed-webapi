using OnMed.Application.Utils;
using OnMed.DataAccess.ViewModels.Administrators;
using OnMed.Domain.Entities.Hospitals;
using OnMed.Persistance.Dtos.Administrators;
using OnMed.Persistance.Dtos.Hospitals;

namespace OnMed.Service.Interfaces.Administrators;

public interface IAdministratorsService
{
    public Task<IList<AdministratorViewModel>> GetAllAsync(PaginationParams @params);
    public Task<bool> CreateAsync(AdministratorCreateDto dto);
    public Task<bool> UpdateAsync(long adminId, AdministratorUpdateDto dto);
    public Task<bool> DeleteAsync(long adminId);
    public Task<long> CountAsync();
}
