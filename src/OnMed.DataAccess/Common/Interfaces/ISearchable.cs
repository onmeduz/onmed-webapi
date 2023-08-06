using OnMed.Application.Utils;

namespace OnMed.DataAccess.Common.Interfaces;

public interface ISearchable<TViewModel>
{
    public Task<(int ItemsCount, IList<TViewModel>)> SearchAsync(string search,
        PaginationParams @params);
}
