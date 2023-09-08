using OnMed.Persistance.Dtos.Hospitals;

namespace OnMed.Service.Interfaces.Hospitals;

public interface IHospitalScheduleService
{
    public Task<bool> CreateAsync(HospitalScheduleCreateDto dto);
}
