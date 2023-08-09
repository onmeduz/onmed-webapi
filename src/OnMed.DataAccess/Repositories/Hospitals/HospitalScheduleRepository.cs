using Dapper;
using OnMed.DataAccess.Interfaces.Hospitals;
using OnMed.Domain.Entities.Hospitals;

namespace OnMed.DataAccess.Repositories.Hospitals;

public class HospitalScheduleRepository : BaseRepository, IHospitalScheduleRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select count(*) from hospital_schedule";
            var result = await _connection.QuerySingleAsync<long>(query);

            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> CreateAsync(HospitalSchedule entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.hospital_schedule(hospital_branch_id, doctor_id, weekday, " +
                "start_time, end_time, description, created_at, updated_at) " +
                    "VALUES (@HospitalBranchId, @DoctorId, @Weekday, @StartTime, @EndTime, " +
                        "@Description, @CreatedAt, @UpdatedAt);";
            var result = await _connection.ExecuteAsync(query, entity);

            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"delete from hospital_schedule where id = @Id";
            var result = await _connection.ExecuteAsync(query, new { Id = id });

            return result;
        }
        catch
        {
            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<HospitalSchedule?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "select * from hospitals where id = @Id";
            var result = await _connection.QuerySingleAsync<HospitalSchedule>(query, new { Id = id });

            return result;
        }
        catch
        {
            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, HospitalSchedule entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE public.hospital_schedule SET hospital_branch_id=@HospitalBranchId, " +
                "doctor_id=@DoctorId weekday=@Weekday, start_time=@StartTime, end_time=@EndTime, " +
                    "description=@Description, created_at=@CreatedAt, updated_at=@UpdatedAt " +
                        $"WHERE id = {id};";
            var result = await _connection.ExecuteAsync(query, entity);

            return result;
        }
        catch
        {

            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
}
