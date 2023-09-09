using Dapper;
using OnMed.Application.Utils;
using OnMed.DataAccess.Interfaces.Categories;
using OnMed.DataAccess.ViewModels.Doctors;
using OnMed.Domain.Entities.Categories;

namespace OnMed.DataAccess.Repositories.Categories;

public class CategoryRepository : BaseRepository, ICategoryRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT COUNT(*) FROM categories";
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

    public async Task<int> CreateAsync(Category entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "INSERT INTO categories(image_path, professionality, " +
                "professionality_hint, professional, professional_hint, created_at, updated_at) " +
                    "VALUES (@ImagePath, @Professionality, @ProfessionalityHint, @Professional, " +
                        "@ProfessionalHint, @CreatedAt, @Updatedat);";
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
            string query = "DELETE FROM categories WHERE id=@Id";
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

    public async Task<IList<Category>> GetAllAsync(PaginationParams @params)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM categories order by id desc " +
                $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

            var result = (await _connection.QueryAsync<Category>(query)).ToList();
            return result;
        }
        catch
        {
            return new List<Category>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Category?> GetByIdAsync(long id)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"SELECT * FROM categories WHERE id=@Id";
            var result = await _connection.QuerySingleAsync<Category>(query, new { Id = id });

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

    public async Task<IList<Category>> SearchAsync(string search)
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select * from categories " +
                $"where professionality ilike '%{search}%' or professional ilike '%{search}%' ";

            var result = (await _connection.QueryAsync<Category>(query)).ToList();
            return result;
        }
        catch (Exception)
        {

            return new List<Category>();
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public Task<IList<Category>> SearchAsync(long branchId, string search)
    {
        throw new NotImplementedException();
    }

    public async Task<int> UpdateAsync(long id, Category entity)
    {
        try
        {
            await _connection.OpenAsync();
            string query = "UPDATE categories SET image_path=@ImagePath, professionality=@Professionality, " +
                "professionality_hint=@ProfessionalityHint, professional=@Professional, " +
                    "professional_hint=@ProfessionalHint, updated_at=@UpdatedAt " +
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
