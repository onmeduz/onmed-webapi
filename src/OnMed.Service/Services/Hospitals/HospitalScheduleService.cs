using OnMed.DataAccess.Interfaces.Hospitals;
using OnMed.Domain.Entities.Hospitals;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Hospitals;
using OnMed.Service.Interfaces.Hospitals;

namespace OnMed.Service.Services.Hospitals;

public class HospitalScheduleService : IHospitalScheduleService
{
    private readonly IHospitalScheduleRepository _hospitalScheduleRepository;

    public HospitalScheduleService(IHospitalScheduleRepository hospitalSchedule)
    {
        this._hospitalScheduleRepository = hospitalSchedule;
    }

    public async Task<bool> CreateAsync(HospitalScheduleCreateDto dto)
    {
        var res = 0;
        for (int i = 0; i < 7; i++)
        {
            var hospitalSchedule = new HospitalSchedule();
            hospitalSchedule.HospitalBranchId = dto.HospitalBranchId;
            hospitalSchedule.DoctorId = dto.DoctorId;
            hospitalSchedule.Weekday[i] = dto.WeekDay[i].ToString();
            if (dto.StartTime.Count > i)
            {
                hospitalSchedule.StartTime = dto.StartTime[i];
                hospitalSchedule.EndTime = dto.EndTime[i];
            }


            hospitalSchedule.CreatedAt = hospitalSchedule.UpdatedAt = TimeHelper.GetDateTime();

            res += await _hospitalScheduleRepository.CreateAsync(hospitalSchedule);

        }
        return res == 7;

    }
}
