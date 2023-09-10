using Dapper;
using OnMed.DataAccess.Interfaces.Doctors;
using OnMed.DataAccess.ViewModels.Appoinments;
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

    public async Task<long> CountByHospitalIdAsync(long hospitalBranchId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT count(*) FROM doctor_appointment where hospital_branch_id = {hospitalBranchId} ;";
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

    public async Task<IList<AppointmentViewModel>> GetAllAppointmentAsync(long adminId)
    {
        try
        {

            await _connection.OpenAsync();
            string query = $"select * FROM appointment_view " +
                $"where admin_id = {adminId}";

            var result = (await _connection.QueryAsync<AppointmentViewModel>(query)).ToList();

            return result;
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            return new List<AppointmentViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<AppointmentViewModel>> GetAllByDayAsync(long adminId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select * FROM appointment_view" +
                $" WHERE EXTRACT(day FROM register_date) = EXTRACT(day FROM now() ::DATE) and " +
                $"admin_id = {adminId}";

            var result = (await _connection.QueryAsync<AppointmentViewModel>(query)).ToList();

            return result;
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            return new List<AppointmentViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<AppointmentViewModel>> GetAllByMonthAsync(long adminId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select * FROM appointment_view" +
                $" WHERE EXTRACT(month FROM register_date) = EXTRACT(month FROM now() ::DATE) and " +
                $"admin_id = {adminId}";

            var result = (await _connection.QueryAsync<AppointmentViewModel>(query)).ToList();

            return result;
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            return new List<AppointmentViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<AppointmentViewModel>> GetAllByWeekAsync(long adminId)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select * FROM appointment_view" +
                $" WHERE EXTRACT(week FROM register_date) = EXTRACT(week FROM now() ::DATE) and " +
                $"admin_id = {adminId}";

            var result = (await _connection.QueryAsync<AppointmentViewModel>(query)).ToList();

            return result;
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            return new List<AppointmentViewModel>();
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
            string query = $"SELECT* FROM doctor_appointment WHERE doctor_id = {doctorId} " +
               $"AND register_date = CAST('{registerDate}' AS DATE) AND start_time = CAST('{startTime}' AS TIME)";
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

    public async Task<IList<AppointmentViewModel>> SearchAsync(long branchId, string search)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select * FROM appointment_view WHERE hospital_branch_id = {branchId} " +
                $"and (user_fullname ilike '%{search}%' or doctor_fullname ilike '%{search}%') ";
            var result = (await _connection.QueryAsync<AppointmentViewModel>(query)).ToList();

            return result;
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);
            return new List<AppointmentViewModel>();
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
