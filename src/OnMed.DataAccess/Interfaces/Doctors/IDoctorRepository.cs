using OnMed.DataAccess.Common.Interfaces;
using OnMed.Domain.Entities.Doctors;

namespace OnMed.DataAccess.Interfaces.Doctors;

public interface IDoctorRepository : IRepository<Doctor>, IGetByPhoneNumber<Doctor?>
{}
