using OnMed.Application.Utils;
using OnMed.DataAccess.Common.Interfaces;
using OnMed.DataAccess.ViewModels.Hospitals;
using OnMed.Domain.Entities.Hospitals;

namespace OnMed.DataAccess.Interfaces.Hospitals;

public interface IHospitalBranchRepository : IRepository<HospitalBranch>,
    IGetAll<HospitalBranchViewModel>
{
    public Task<IList<HospitalBranchForCommonViewModel>> GetAllForCommonAsync(PaginationParams @params);
    public Task<long> CreateAndReturnIdAsync(HospitalBranch hospitalBranch);
}
