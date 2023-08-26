using Dapper;
using OnMed.Application.Utils;
using OnMed.DataAccess.Interfaces.Administrators;
using OnMed.DataAccess.ViewModels.Administrators;
using OnMed.DataAccess.ViewModels.Doctors;
using OnMed.Domain.Entities.Administrators;
using static Dapper.SqlMapper;

namespace OnMed.DataAccess.Repositories.Administrators;

public class AdministratorRepository : BaseRepository, IAdministratorRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT COUNT(*) FROM administrators";
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

    public async Task<long> CreateAndReturnIdAsync(Administrator administrartor)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO administrators(first_name, last_name, middle_name, " +
                "birth_day, phone_number, phone_number_confirmed, is_male, image_path, region, " +
                    "password_hash, salt, created_at, updated_at) " +
                        "VALUES (@FirstName, @LastName, @MiddleName, @BirthDay, @PhoneNumber, " +
                            "@PhoneNumberConfirmed, @IsMale, @ImagePath, @Region, @PasswordHash, @Salt, " +
                                "@CreatedAt, @UpdatedAt) returning id ";
            var result = await _connection.ExecuteScalarAsync<long>(query, administrartor);

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

    public async Task<int> CreateAsync(Administrator entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO administrators(first_name, last_name, middle_name, " +
                "birth_day, phone_number, phone_number_confirmed, is_male, image_path, region, " +
                    "password_hash, salt, created_at, updated_at) " +
                        "VALUES (@FirstName, @LastName, @MiddleName, @BirthDay, @PhoneNumber, " +
                            "@PhoneNumberConfirmed, @IsMale, @ImagePath, @Region, @PasswordHash, @Salt, " +
                                "@CreatedAt, @UpdatedAt) returning id ";
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
            string query = "DELETE FROM administrators WHERE id=@Id";
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

    public async Task<IList<AdministratorViewModel>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select * from administrator_view order by first_name " +
                $"offset {@params.GetSkipCount()} limit {@params.PageSize}";
            var result = (await _connection.QueryAsync<AdministratorViewModel>(query)).ToList();

            return result;
        }
        catch
        {
            return new List<AdministratorViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Administrator?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM administrators WHERE id=@Id";
            var result = await _connection.QuerySingleAsync<Administrator>(query, new { Id = id });

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

    public async Task<AdministratorViewModel?> GetByIdViewModelAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM administrator_view WHERE admin_id = @Id";
            var result = await _connection.QuerySingleAsync<AdministratorViewModel>(query, new { Id = id });

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

    public async Task<Administrator?> GetByPhoneNumberAsync(string phoneNumber)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM administrators WHERE phone_number = @PhoneNumber";
            var data = await _connection.QuerySingleAsync<Administrator>(query, new { PhoneNumber = phoneNumber });

            return data;
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

    public async Task<IList<AdministratorViewModel>> SearchAsync(string search)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select * from administrator_view " +
                $"where first_name ilike '%{search}%' or last_name ilike '%{search}%' ";

            var result = (await _connection.QueryAsync<AdministratorViewModel>(query)).ToList();
            return result;
        }
        catch (Exception)
        {

            return new List<AdministratorViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, Administrator entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE public.administrators SET first_name=@FirstName, " +
                "last_name=@LastName, middle_name=@MiddleName, birth_day=@BirthDay, " +
                    "phone_number=@PhoneNumber, phone_number_confirmed=@PhoneNumberConfirmed, " +
                        "is_male=@IsMale, image_path=@ImagePath, region=@Region, password_hash=@PasswordHash, " +
                            "salt=@salt, updated_at=@UpdatedAt " +
                                $"WHERE id={id};";

            return await _connection.ExecuteAsync(query, entity);
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
