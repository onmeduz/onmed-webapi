using OnMed.DataAccess.Common.Interfaces;
using OnMed.DataAccess.ViewModels.Administrators;
using OnMed.Domain.Entities.Administrators;

namespace OnMed.DataAccess.Interfaces.Administrators;

public interface IAdministratorRepository : IRepository<Administrator, AdministratorViewModel>,
    ISearchable<AdministratorViewModel>, IGetAll<AdministratorViewModel>
{
}
