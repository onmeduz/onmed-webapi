using Dapper;
using OnMed.DataAccess.Interfaces.Doctors;
using OnMed.DataAccess.ViewModels.Users;
using OnMed.Domain.Entities.Doctors;
using Serilog;

namespace OnMed.DataAccess.Repositories.Doctors;

public class DoctorAppointmentRepository : BaseRepository, IDoctorAppointmentRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT count(*) FROM doctor_appointment ;";
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

    public async Task<int> CreateAsync(DoctorAppointment entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO doctor_appointment (user_id, doctor_id, status, hospital_branch_id, " +
                "register_date, start_time, duration_minutes, payment_type, payment_provider, is_paid, " +
                    "description, paid_money, payment_description, created_at, updated_at) " +
                        "VALUES (@UserId, @DoctorId, @Status, @HospitalBranchId, @RegisterDate, @StartTime, " +
                            "@DurationMinutes, @PaymentType, @PaymentProvider, @IsPaid, @Description, @PaidMoney, " +
                                "@PaymentDescription, @CreatedAt, @UpdatedAt);";
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
            string query = $"DELETE FROM doctor_appointment WHERE id=@Id";
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

    public async Task<IList<UserAppointmentViewModel>> GetByDateAndDoctorIdAsync(long doctorId, DateOnly date)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select * FROM doctor_appointment WHERE doctor_id = {doctorId} " +
                $"and register_date = '{date}' ";
            var result = (await _connection.QueryAsync<UserAppointmentViewModel>(query)).ToList();

            return result;
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            return new List<UserAppointmentViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<DoctorAppointment?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM doctor_appointment WHERE id = {id} ;";
            var result = await _connection.QuerySingleAsync<DoctorAppointment>(query);

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

    public async Task<int> GetEmptyAppointmentAsync(long doctorId, DateOnly registerDate, TimeOnly startTime)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select * FROM doctor_appointment WHERE doctor_id = {doctorId} " +
                $"and register_date = '{registerDate}' and start_time = time '{startTime}'";
            var result = await _connection.QuerySingleAsync<int>(query);

            return result;
        }
        catch (InvalidOperationException ex)
        {
            Log.Error(ex, ex.Message);
            return 0;
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            return -1;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, DoctorAppointment entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE doctor_appointment SET user_id=@UserId, doctor_id=@DoctorId, status=@Status, " +
                "hospital_branch_id=@HospitalBranchId, register_date=@RegisterDate, start_time=@StartTime, " +
                    "duration_minutes=@DurationMinutes, payment_type=@PaymentType, payment_provider=@PaymentProvider, " +
                        "is_paid=@IsPaid, description=@Description, paid_money=@PaidMoney, " +
                            "payment_description=@PaymentDescription, stars=@Stars, updated_at=@UpdatedAt " +
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
