﻿using Dapper;
using OnMed.DataAccess.Interfaces.Users;
using OnMed.Domain.Entities.Users;

namespace OnMed.DataAccess.Repositories.Users;

public class UserRepository : BaseRepository, IUserRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT count(*) FROM users ;";
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

    public async Task<int> CreateAsync(User entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO users (first_name, last_name, middle_name, birth_day, " +
                "phone_number, phone_number_confirmed, is_male, image_path, region, password_hash, " +
                    "salt, created_at, updated_at) " +
                        "VALUES (@FirstName, @LastName, @MiddleName, @BirthDay, @PhoneNumber, @PhoneNumberConfirmed, " +
                            "@IsMale, @ImagePath, @Region, @PasswordHash, @Salt, @CreatedAt, @UpdatedAt);";
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
            string query = $"DELETE FROM users WHERE id=@Id";
            var result = await _connection.QuerySingleAsync<int>(query);

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

    public async Task<User?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM users WHERE id=@Id";
            var result = await _connection.QuerySingleAsync<User>(query, new { Id = id });

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

    public async Task<int> UpdateAsync(long id, User entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE users SET first_name=@FirstName, last_name=@lastName, middle_name=@MiddleName, " +
                    "birth_day=@BirthDay, phone_number=@PhoneNumber, phone_number_confirmed=@PhoneNumberConfirmed, " +
                        "is_male=@IsMale, image_path=@ImagePath, region=@Region, password_hash=@PasswordHash, " +
                            "salt=@Salt, updated_at=@UpdatedAt " +
                                $"WHERE id = {id};";

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
