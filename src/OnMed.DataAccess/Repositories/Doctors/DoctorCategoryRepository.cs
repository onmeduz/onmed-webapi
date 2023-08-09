using Dapper;
using Onmed.Domain.Entities.Doctors;
using OnMed.DataAccess.Interfaces.Doctors;
using OnMed.Domain.Entities.Doctors;

namespace OnMed.DataAccess.Repositories.Doctors;

public class DoctorCategoryRepository : BaseRepository, IDoctorCategoryRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT count(*) FROM doctor_categories ;";
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

    public async Task<int> CreateAsync(DoctorCategory entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO doctor_categories (doctor_id, category_id, created_at, updated_at) " +
                "VALUES (@DoctorId, @CategoryId, @CreatedAt, @UpdatedAt);";
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
            string query = $"DELETE FROM doctor_categories WHERE id=@Id";
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

    public async Task<DoctorCategory?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM doctor_categories WHERE id = {id} ;";
            var result = await _connection.QuerySingleAsync<DoctorCategory>(query);

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

    public async Task<int> UpdateAsync(long id, DoctorCategory entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE doctor_categories SET doctor_id=@DoctorId, " +
                "category_id=@CategoryId, updated_at=@UpdatedAt " +
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
