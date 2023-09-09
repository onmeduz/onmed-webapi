using OnMed.DataAccess.Interfaces.Hospitals;
using OnMed.Domain.Entities.Hospitals;
using OnMed.Domain.Enums;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Hospitals;
using OnMed.Service.Interfaces.Hospitals;
using System.Linq.Expressions;

namespace OnMed.Service.Services.Hospitals;

public class HospitalScheduleService : IHospitalScheduleService
{
    private readonly IHospitalScheduleRepository _hospitalScheduleRepository;

    public HospitalScheduleService(IHospitalScheduleRepository hospitalSchedule)
    {
        this._hospitalScheduleRepository = hospitalSchedule;
    }

    public Task<bool> CreateAsync(HospitalScheduleCreateDto dto)
    {
        throw new NotImplementedException();
    }
}
