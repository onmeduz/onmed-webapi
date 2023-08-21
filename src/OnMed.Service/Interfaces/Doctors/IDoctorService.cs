using OnMed.Application.Utils;
using OnMed.DataAccess.ViewModels.Doctors;
using OnMed.Domain.Entities.Doctors;
using OnMed.Persistance.Dtos.Doctors;

namespace OnMed.Service.Interfaces.Doctors;

public interface IDoctorService
{
    public Task<bool> CreateAsync(DoctorCreateDto dto);
    public Task<bool> DeleteAsync(long doctorId);
    public Task<long> CountByHospitalAsync(long hospitalId);
    public Task<long> CountAsync();
    public Task<IList<DoctorViewModel>> GetAllByHospitalAsync(long hospitalId, PaginationParams @params);
    public Task<IList<DoctorViewModel>> GetAllAsync(PaginationParams @params);
    public Task<DoctorViewModel> GetByIdAsync(long doctorId);
    public Task<bool> UpdateAsync(long doctorId, DoctorUpdateDto dto);
}
