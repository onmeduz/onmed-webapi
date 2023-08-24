using OnMed.DataAccess.Interfaces.Heads;
using OnMed.DataAccess.ViewModels.Heads;
using OnMed.Service.Interfaces.Auth;
using OnMed.Service.Interfaces.Heads;

namespace OnMed.Service.Services.Heads;

public class HeadService : IHeadService
{
    private IIdentityService _identity;
    private readonly IHeadRepository _repository;

    public HeadService(IIdentityService identityService,
        IHeadRepository headRepository)
    {
        this._identity = identityService;
        this._repository = headRepository;
    }

    public async Task<HeadViewModel?> GetProfileInfoAsync() =>
        await _repository.GetByIdViewAsync(_identity.UserId);
}
