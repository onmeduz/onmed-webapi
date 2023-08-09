using Dapper;
using OnMed.DataAccess.Interfaces.Hospitals;
using OnMed.Domain.Entities.Hospitals;

namespace OnMed.DataAccess.Repositories.Hospitals;

public class HospitalBranchAdminRepository : BaseRepository, IHospitalBranchAdminRepository
{
    public async Task<long> CountAsync()    
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select count(*) from hospital_branch_admins";
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

    public async Task<int> CreateAsync(HospitalBranchAdmin entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.hospital_branch_admins(hospital_branch_id, " +
                "administrator_id, created_at, updated_at) " +
                "VALUES (@HospitalBranchId, @AdministratorId, @CreatedAt, @UpdatedAt);";
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
            string query = $"delete from hospital_branch_admins where id = @Id";
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

    public async Task<HospitalBranchAdmin?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "select * from hospital_branch_admins where id = @Id";
            var result = await _connection.QuerySingleAsync<HospitalBranchAdmin>(query, new { Id = id });

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

    public async Task<int> UpdateAsync(long id, HospitalBranchAdmin entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE public.hospital_branch_admins SET hospital_branch_id=@HospitalBranchId, " +
                "administrator_id=@AdministratorId, created_at=@CreatedAt, updated_at=@UpdatedAt " +
                    $"WHERE id = {id}; ";
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
