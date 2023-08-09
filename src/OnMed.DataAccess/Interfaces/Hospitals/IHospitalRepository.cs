using OnMed.DataAccess.Common.Interfaces;
using OnMed.DataAccess.ViewModels.Hospitals;
using OnMed.Domain.Entities.Hospitals;

namespace OnMed.DataAccess.Interfaces.Hospitals;

public interface IHospitalRepository : IRepository<Hospital,HospitalViewModel>,
    IGetAll<HospitalViewModel>,ISearchable<HospitalViewModel>
{

}
