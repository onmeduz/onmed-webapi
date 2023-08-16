using OnMed.Application.Utils;

namespace OnMed.Service.Interfaces.Common;

public interface IPaginator
{
    public void Paginate(long itemsCount, PaginationParams @params);

}
