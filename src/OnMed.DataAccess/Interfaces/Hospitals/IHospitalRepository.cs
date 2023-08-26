using OnMed.DataAccess.Common.Interfaces;
using OnMed.Domain.Entities.Hospitals;

namespace OnMed.DataAccess.Interfaces.Hospitals;

public interface IHospitalRepository : IRepository<Hospital>,
    IGetAll<Hospital>, ISearchable<Hospital>
{ }
