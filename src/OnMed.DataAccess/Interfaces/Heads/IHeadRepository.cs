using OnMed.DataAccess.Common.Interfaces;
using OnMed.DataAccess.ViewModels.Heads;
using OnMed.DataAccess.ViewModels.Users;
using OnMed.Domain.Entities.Heads;

namespace OnMed.DataAccess.Interfaces.Heads;

public interface IHeadRepository : IRepository<Head>,
    IGetByPhoneNumber<Head?>
{
    public Task<HeadViewModel?> GetByIdViewAsync(long id);
}
