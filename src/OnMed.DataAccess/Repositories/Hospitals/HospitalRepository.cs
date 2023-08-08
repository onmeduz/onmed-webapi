using Dapper;
using OnMed.Application.Utils;
using OnMed.DataAccess.Interfaces.Hospitals;
using OnMed.DataAccess.ViewModels;
using OnMed.Domain.Entities.Hospitals;

namespace OnMed.DataAccess.Repositories.Hospitals
{
    public class HospitalRepository : BaseRepository, IHospitalRepository
    {
        public async Task<long> CountAsync()
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"select count(*) from hospitals";
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

        public async Task<int> CreateAsync(Hospital entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "INSERT INTO public.hospitals(name, legal_name, brand_image_path, " +
                    "administrator_phone_number, fax_phone_number, description, email, website, license_number, " +
                        "license_given_date, legal_register_number, legal_register_number_given_date, " +
                            "created_at, updated_at) " +
                                "VALUES ( @Name,@LegalName, @BrandImagePath, @AdministratorPhoneNumber, @FaxPhoneNumber, " +
                                    "@Description, @Email, @Website, @LicenseNumber," +
                                        "@LicenseGivenDate, @LegalRegisterNumber, @LegalRegisterNumberGiveDate, @CreatedAt, UpdatedAt);";
                var result = await _connection.ExecuteAsync(query, entity);

                return result;
            }
            catch (Exception)
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
                string query = $"delete from hospitals where id = @Id";
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

        public Task<IList<HospitalViewModel>> GetAllAsync(PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public Task<Hospital?> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<HospitalViewModel?> GetByIdViewModelAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<(int ItemsCount, IList<HospitalViewModel>)> SearchAsync(string search, PaginationParams @params)
        {
            throw new NotImplementedException();
        }

        public async Task<int> UpdateAsync(long id, Hospital entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "UPDATE public.hospitals SET name=@Name, legal_name=@LegalName, " +
                    "brand_image_path=@BrandImagePath, administrator_phone_number=@AdministratorPhoneNumber, " +
                        "fax_phone_number=@FaxPhoneNumber, description=@Description, email=@Email, website=@Website, " +
                            "license_number=@LicenseNumber, license_given_date=@LicenseGivenDate, " +
                                "legal_register_number=@LegalRegisterNumber, " +
                                    "legal_register_number_given_date=@LegalRegisterNumberGivenDate, " +
                                    "created_at=@CreatedAt, updated_at=@UpdatedAt " +
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
}
