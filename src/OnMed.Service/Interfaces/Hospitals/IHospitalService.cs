using OnMed.Application.Utils;
using OnMed.Domain.Entities.Hospitals;
using OnMed.Persistance.Dtos.Hospitals;

namespace OnMed.Service.Interfaces.Hospitals;

public interface IHospitalService
{
    public Task<IList<Hospital>> GetAllAsync(PaginationParams @params);
    public Task<bool> CreateAsync(HospitalCreateDto dto);
    public Task<bool> UpdateAsync(long hospitalId, HospitalUpdateDto dto);
    public Task<bool> DeleteAsync(long hospitalId);
    public Task<long> CountAsync();
    public Task<IList<Hospital>> SearchAsync(string  search);
}
