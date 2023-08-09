using Dapper;
using OnMed.DataAccess.Interfaces.Hospitals;
using OnMed.Domain.Entities.Hospitals;

namespace OnMed.DataAccess.Repositories.Hospitals;

public class HospitalBranchDoctorRepository : BaseRepository, IHospitalBranchDoctorRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select count(*) from hospital_branch_doctors";
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

    public async Task<int> CreateAsync(HospitalBranchDoctor entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.hospital_branch_doctors(hospital_branch_id, doctor_id, is_active, " +
                "registered_at, stopped_at, created_at, updated_at) " +
                    "VALUES (@HospitalBranchId, @DoctorId, @IsActive, @RegisteredAt, " +
                        "@StoppedAt, @CreatedAt, @UpdatedAt);";
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
            string query = $"delete from hospital_branch_doctors where id = @Id";
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

    public async Task<HospitalBranchDoctor?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "select * from hospital_branch_doctors where id = @Id";
            var result = await _connection.QuerySingleAsync<HospitalBranchDoctor>(query, new { Id = id });

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

    public async Task<int> UpdateAsync(long id, HospitalBranchDoctor entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE public.hospital_branch_doctors SET hospital_branch_id=@HospitalBranchId, " +
                "doctor_id=@DoctorId, is_active=@IsActive, registered_at=@RegisteredAt, stopped_at=@StoppedAt, " +
                    "created_at=@CreatedAt, updated_at=@UpdatedAt " +
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
