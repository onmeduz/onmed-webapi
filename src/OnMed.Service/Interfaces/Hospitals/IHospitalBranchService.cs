using OnMed.Application.Utils;
using OnMed.DataAccess.ViewModels.Hospitals;
using OnMed.Persistance.Dtos.Hospitals;
using System.Drawing.Printing;

namespace OnMed.Service.Interfaces.Hospitals;

public interface IHospitalBranchService
{
    public Task<IList<HospitalBranchForCommonViewModel>> GetAllForCommonAsync(PaginationParams @params);
    public Task<IList<HospitalBranchViewModel>> GetAllAsync(PaginationParams @params);
    public Task<bool> CreateAsync(HospitalBranchCreateDto dto);
    public Task<bool> UpdateAsync(long hospitalBranchId, HospitalBranchUpdateDto dto);
    public Task<bool> DeleteAsync(long hospitalBranchId);
    public Task<long> CountAsync();
    public Task<IList<HospitalBranchViewModel>> SearchAsync(string search); 
}
