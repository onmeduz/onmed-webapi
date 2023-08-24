using OnMed.DataAccess.ViewModels.Heads;

namespace OnMed.Service.Interfaces.Heads;

public interface IHeadService
{
    public Task<HeadViewModel?> GetProfileInfoAsync();
}
