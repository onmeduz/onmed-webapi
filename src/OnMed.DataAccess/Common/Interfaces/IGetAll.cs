using OnMed.Application.Utils;

namespace OnMed.DataAccess.Common.Interfaces;

public interface IGetAll<TViewModel>
{
    public Task<IList<TViewModel>> GetAllAsync(PaginationParams @params);
}

