using OnMed.DataAccess.Common.Interfaces;
using OnMed.Domain.Entities.Doctors;
using static Dapper.SqlMapper;

namespace OnMed.DataAccess.Interfaces.Doctors;

public interface IDoctorRepository : IRepository<Doctor>, IGetByPhoneNumber<Doctor?>
{
    public Task<long> CreateReturnIdAsync(Doctor entity);
}
