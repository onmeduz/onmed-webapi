using Aspose.Pdf.Operators;
using Dapper;
using OnMed.Application.Utils;
using OnMed.DataAccess.Interfaces.Hospitals;
using OnMed.DataAccess.ViewModels.Hospitals;
using OnMed.Domain.Entities.Hospitals;
using Serilog;
using static Dapper.SqlMapper;

namespace OnMed.DataAccess.Repositories.Hospitals;

public class HospitalBranchRepository : BaseRepository, IHospitalBranchRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select count(*) from hospital_branches";
            var result = await _connection.QuerySingleAsync<long>(query);

            return result;
        }
        catch(Exception ex) 
        {
            Log.Error(ex, ex.Message);

            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<long> CreateAndReturnIdAsync(HospitalBranch hospitalBranch)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.hospital_branches(name, hospital_id, image_path, region, district, " +
                "address, destination, adress_latitude, adress_longitude, contact_phone_number, created_at, updated_at) " +
                    "VALUES( @Name, @HospitalId, @ImagePath, @Region, @District, @Address, @Destination, " +
                        "@AdressLatitude, @AdressLongitude, @ContactPhoneNumber, @CreatedAt, @UpdatedAt) returning id ; ";
            var result = await _connection.ExecuteScalarAsync<long>(query, hospitalBranch);

            return result;
        }
        catch(Exception ex)
        {
            Log.Error(ex, ex.Message);

            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> CreateAsync(HospitalBranch entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO public.hospital_branches(name, hospital_id, image_path, region, district, " +
                "address, destination, adress_latitude, adress_longitude, contact_phone_number, created_at, updated_at) " +
                    "VALUES( @Name, @HospitalId, @ImagePath, @Region, @District, @Address, @Destination, " +
                        "@AdressLatitude, @AdressLongitude, @ContactPhoneNumber, @CreatedAt, @UpdatedAt); ";
            var result = await _connection.ExecuteAsync(query, entity);

            return result;
        }
        catch(Exception ex) 
        {
            Log.Error(ex, ex.Message);

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
            string query = $"delete from hospital_branches where id = @Id";
            var result = await _connection.ExecuteAsync(query, new { Id = id });

            return result;
        }
        catch(Exception ex)
        {
            Log.Error(ex, ex.Message);

            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<HospitalBranchViewModel>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM hospitals_branch_view " +
                $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<HospitalBranchViewModel>(query)).ToList();
            return result;
        }
        catch(Exception ex)
        {
            Log.Error(ex, ex.Message);

            return new List<HospitalBranchViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<IList<HospitalBranchForCommonViewModel>> GetAllForCommonAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM hospital_branches " +
                $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<HospitalBranchForCommonViewModel>(query)).ToList();
            return result;
        }
        catch(Exception ex)
        {
            Log.Error(ex, ex.Message);

            return new List<HospitalBranchForCommonViewModel>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<HospitalBranch?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "select * from hospital_branches where id = @Id";
            var result = await _connection.QuerySingleAsync<HospitalBranch>(query, new { Id = id });

            return result;
        }
        catch(Exception ex)
        {
            Log.Error(ex, ex.Message);

            return null;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<int> UpdateAsync(long id, HospitalBranch entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE public.hospital_branches SET name=@Name, hospital_id=@HospitalId, " +
                "image_path=@ImagePath, region=@Region, district=@District, address=@Address, " +
                    "destination=@Destination, adress_latitude=@AdressLatitude, " +
                        "adress_longitude=@AdressLongitude, contact_phone_number=@ContactPhoneNumber, " +
                            "created_at=@CreatedAt, updated_at=@UpdatedAt " +
                                $"WHERE id = {id};";
            var result = await _connection.ExecuteAsync(query, entity);

            return result;
        }
        catch(Exception ex)
        {
            Log.Error(ex, ex.Message);

            return 0;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
}
