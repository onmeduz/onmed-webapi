using Dapper;
using OnMed.DataAccess.Interfaces.Hospitals;
using OnMed.Domain.Entities.Hospitals;

namespace OnMed.DataAccess.Repositories.Hospitals;

public class HospitalBranchCategoryRepository : BaseRepository, IHospitalBranchCategoryRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select count(*) from hospital_branch_categories";
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

    public async Task<int> CreateAsync(HospitalBranchCategory entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.hospital_branch_categories(hospital_branch_id, " +
                "category_id, created_at, updated_at) " +
                    "VALUES (@HospitalBranchId, @CategoryId, @CreatedAt, @UpdatedAt);";
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
            string query = $"delete from hospital_branch_categories where id = @Id";
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

    public async Task<HospitalBranchCategory?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "select * from hospital_branch_categories where id = @Id";
            var result = await _connection.QuerySingleAsync<HospitalBranchCategory>(query, new { Id = id });

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

    public async Task<int> UpdateAsync(long id, HospitalBranchCategory entity)
    {
        try
        {
            await _connection.OpenAsync();

            string query = "UPDATE public.hospital_branch_categories SET hospital_branch_id=@HospitalBranchId, " +
                "category_id=@CategoryId, created_at=@CreatedAt, updated_at=@UpdatedAt " +
                    $"WHERE id = {id}";
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
