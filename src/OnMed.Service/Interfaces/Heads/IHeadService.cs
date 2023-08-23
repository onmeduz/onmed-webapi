using OnMed.DataAccess.ViewModels.Heads;
using OnMed.DataAccess.ViewModels.Users;

namespace OnMed.Service.Interfaces.Heads;

public interface IHeadService
{
    public Task<HeadViewModel?> GetProfileInfoAsync();
}
