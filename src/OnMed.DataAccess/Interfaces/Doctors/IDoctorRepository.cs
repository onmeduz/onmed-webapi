using OnMed.Application.Utils;
using OnMed.DataAccess.Common.Interfaces;
using OnMed.DataAccess.ViewModels.Doctors;
using OnMed.Domain.Entities.Doctors;

namespace OnMed.DataAccess.Interfaces.Doctors;

public interface IDoctorRepository : IRepository<Doctor>, IGetByPhoneNumber<Doctor?>, IGetAll<DoctorViewModel>,
    ISearchable<DoctorViewModel>
{
    public Task<long> CreateReturnIdAsync(Doctor entity);
    public Task<IList<DoctorViewModel>> GetAllHospitalIdAsync(long hospitalId, PaginationParams @params);
    public Task<DoctorViewModel> GetByIdViewAsync(long doctorId);
    public Task<IList<DoctorViewModel>> GetAllHospitalIdAndCategoryIdAsync(long hospitalId, long? categoryId, PaginationParams @params);
    public Task<IList<DoctorViewModel>> SearchAsync(long branchId, string search);
}
