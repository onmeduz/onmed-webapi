using Dapper;
using OnMed.Application.Utils;
using OnMed.DataAccess.Interfaces.Heads;
using OnMed.DataAccess.ViewModels;
using OnMed.DataAccess.ViewModels.Heads;
using OnMed.Domain.Entities.Heads;
using System.Data.Common;

namespace OnMed.DataAccess.Repositories.Heads;

public class HeadRepository : BaseRepository, IHeadRepository
{
    public async Task<long> CountAsync()
    {
        try
        {
            await _connection.OpenAsync();
            string query = $"select count(*) from heads";
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

    public Task<int> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IList<HeadViewModel>> GetAllAsync(PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public Task<Head?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<HeadViewModel?> GetByIdViewModelAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<(int ItemsCount, IList<HeadViewModel>)> SearchAsync(string search, PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public Task<int> UpdateAsync(long id, Head entity)
    {
        throw new NotImplementedException();
    }
}
