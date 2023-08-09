using Dapper;
using OnMed.Application.Utils;
using OnMed.DataAccess.Interfaces.Heads;
using OnMed.DataAccess.ViewModels.Heads;
using OnMed.Domain.Entities.Doctors;
using OnMed.Domain.Entities.Heads;

namespace OnMed.DataAccess.Repositories.Heads;

public class HeadRepository : BaseRepository, IHeadRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT COUNT(*) FROM heads";
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

    public Task<int> CreateAsync(Head entity)
    {
        throw new NotImplementedException();
    }

    public async Task<int> DeleteAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"DELETE FROM heads WHERE id=@Id";
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

    public async Task<Head?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM heads WHERE id = {id} ;";
            var result = await _connection.QuerySingleAsync<Head>(query);

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

    public async Task<int> UpdateAsync(long id, Head entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE heads SET first_name=@FirstName, last_name=@LastName, middle_name=@MiddleName, " +
                "birth_day=@BirthDay, phone_number=@PhoneNumber, phone_number_confirmed=@PhoneNumberConfirmed, " +
                    "is_male=@IsMale, image_path=@ImagePath, region=@Region, password_hash=@PasswordHash, " +
                        "salt=@Salt, updated_at=@UpdatedAt " +
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
