using OnMed.Domain.Entities.Hospitals;

namespace OnMed.DataAccess.Interfaces.Hospitals;

public interface IHospitalBranchDoctorRepository : IRepository<HospitalBranchDoctor>
{
    public Task<long> CountByHospitalAsync(long hospitalId);
}
