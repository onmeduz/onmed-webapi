using Dapper;
using OnMed.DataAccess.Interfaces.Doctors;
using OnMed.Domain.Entities.Doctors;

namespace OnMed.DataAccess.Repositories.Doctors;

public class DoctorRepository : BaseRepository, IDoctorRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT count(*) FROM doctors ;";
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

    public async Task<int> CreateAsync(Doctor entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO doctors (first_name, last_name, middle_name, birth_day, phone_number, " +
                "degree, is_male, image_path, region, appointment_money, password_hash, salt, created_at, updated_at) " +
                    "VALUES (@FirstName, @LastName, @MiddleName, @BirthDay, @PhoneNumber, @Degree, @IsMale, @ImagePath, @Region," +
                        " @AppointmentMoney, @PasswordHash, @Salt, @CreatedAt, @UpdatedAt);";
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

    public async Task<long> CreateReturnIdAsync(Doctor entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO doctors (first_name, last_name, middle_name, birth_day, phone_number, " +
                "degree, is_male, image_path, region, appointment_money, password_hash, salt, created_at, updated_at) " +
                    "VALUES (@FirstName, @LastName, @MiddleName, @BirthDay, @PhoneNumber, @Degree, @IsMale, @ImagePath, @Region," +
                        " @AppointmentMoney, @PasswordHash, @Salt, @CreatedAt, @UpdatedAt) RETURNING id;";
            var result = await _connection.ExecuteScalarAsync<long>(query, entity);

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
            string query = $"DELETE FROM doctors WHERE id=@Id";
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

    public async Task<Doctor?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM doctors WHERE id = {id} ;";
            var result = await _connection.QuerySingleAsync<Doctor>(query);
            
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

    public async Task<Doctor?> GetByPhoneNumberAsync(string phoneNumber)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "SELECT * FROM doctors WHERE phone_number = @PhoneNumber";
            var data = await _connection.QuerySingleAsync<Doctor>(query, new { PhoneNumber = phoneNumber });

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

    public async Task<int> UpdateAsync(long id, Doctor entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE doctors SET first_name=@FirstName, last_name=@LastName, middle_name=@MiddleName," +
                " birth_day=@BirthDay, phone_number=@PhoneNumber, degree=@Degree, is_male=@IsMale, " +
                    "image_path=@ImagePath, region=@Region, appointment_money=@AppointmentMoney, " +
                        "password_hash=@PasswordHash, salt=@Salt, updated_at=@UpdatedAt  " +
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
