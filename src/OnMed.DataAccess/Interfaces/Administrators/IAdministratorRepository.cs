using OnMed.DataAccess.Common.Interfaces;
using OnMed.DataAccess.ViewModels.Administrators;
using OnMed.Domain.Entities.Administrators;

namespace OnMed.DataAccess.Interfaces.Administrators;

public interface IAdministratorRepository : IRepository<Administrator>,
    IGetByPhoneNumber<Administrator?>, IGetAll<AdministratorViewModel>
{
    public Task<long> CreateAndReturnIdAsync(Administrator administrartor);
}
