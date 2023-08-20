using OnMed.DataAccess.Common.Interfaces;
using OnMed.DataAccess.ViewModels.Hospitals;
using OnMed.Domain.Entities.Hospitals;

namespace OnMed.DataAccess.Interfaces.Hospitals;

public interface IHospitalBranchRepository : IRepository<HospitalBranch>,
    IGetAll<HospitalBranchViewModel>
{ 
    public Task<long> CreateAndReturnIdAsync(HospitalBranch hospitalBranch);
}
