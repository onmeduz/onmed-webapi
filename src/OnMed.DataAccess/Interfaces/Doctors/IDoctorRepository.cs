using OnMed.Application.Utils;
using OnMed.DataAccess.Common.Interfaces;
using OnMed.DataAccess.ViewModels.Doctors;
using OnMed.Domain.Entities.Doctors;
using static Dapper.SqlMapper;

namespace OnMed.DataAccess.Interfaces.Doctors;

public interface IDoctorRepository : IRepository<Doctor>, IGetByPhoneNumber<Doctor?>, IGetAll<DoctorViewModel>
{
    public Task<long> CreateReturnIdAsync(Doctor entity);
    public Task<IList<DoctorViewModel>> GetAllHospitalIdAsync(long hospitalId, PaginationParams @params);
    public Task<DoctorViewModel> GetByIdViewAsync(long doctorId);
}
