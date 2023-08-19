using OnMed.Application.Exceptions.Hospitals;
using OnMed.Application.Utils;
using OnMed.DataAccess.Interfaces.Administrators;
using OnMed.DataAccess.Interfaces.Hospitals;
using OnMed.DataAccess.Repositories.Hospitals;
using OnMed.DataAccess.ViewModels.Administrators;
using OnMed.Domain.Entities.Administrators;
using OnMed.Domain.Entities.Hospitals;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Administrators;
using OnMed.Service.Common.Security;
using OnMed.Service.Interfaces.Administrators;
using OnMed.Service.Interfaces.Common;
using OnMed.Service.Services.Common;

namespace OnMed.Service.Services.Administrators;

public class AdministratorService : IAdministratorsService
{
    private readonly IAdministratorRepository _administratorRepository;
    private readonly IFileService _fileService;
    private readonly IHospitalBranchAdminRepository _hospitalBranchRepository;
    private readonly IPaginator _paginator;

    public AdministratorService(IAdministratorRepository administrator,
        IHospitalBranchAdminRepository hospitalBranchAdmin,
        IFileService fileService,
        IPaginator paginator)
    {
        this._administratorRepository = administrator;
        this._fileService = fileService;
        this._hospitalBranchRepository = hospitalBranchAdmin;
        this._paginator = paginator;
    }

    public Task<long> CountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CreateAsync(AdministratorCreateDto dto)
    {
        var administrator = new Administrator();
        administrator.FirstName = dto.FirstName;
        administrator.LastName = dto.LastName;
        administrator.MiddleName = dto.MiddleName;
        administrator.BirthDay = dto.BirthDay;
        administrator.PhoneNumber = dto.PhoneNumber;
        var security = PasswordHasher.Hash(dto.Password);
        administrator.PasswordHash = security.Hash;
        administrator.Salt = security.Salt;
        administrator.IsMale = dto.IsMale;

        if (dto.Image is not null)
        {
            string newImagePath = await _fileService.UploadImageAsync(dto.Image, "administrators");
            administrator.ImagePath = newImagePath;
        }
        administrator.Region = dto.Region;
        administrator.CreatedAt = administrator.UpdatedAt = TimeHelper.GetDateTime();

        var isThere = await _hospitalBranchRepository.GetByIdAsync(dto.HospitalId);
        if (isThere is null) throw new HospitalBranchNotFoundException();
        var lastAdminId = await _administratorRepository.CreateAndReturnIdAsync(administrator);
        if (lastAdminId > 0)
        {
            var hospitalBranchAdmin = new HospitalBranchAdmin();
            hospitalBranchAdmin.HospitalBranchId = dto.HospitalId;
            hospitalBranchAdmin.AdministratorId = lastAdminId;
            hospitalBranchAdmin.CreatedAt = hospitalBranchAdmin.UpdatedAt = TimeHelper.GetDateTime();
            var result = await _hospitalBranchRepository.CreateAsync(hospitalBranchAdmin);
            return result>0;
        }
        else return false;
    }

    public Task<bool> DeleteAsync(long adminId)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<AdministratorViewModel>> GetAllAsync(PaginationParams @params)
    {
        var hospitals = await _administratorRepository.GetAllAsync(@params);
        var count = await _administratorRepository.CountAsync();
        _paginator.Paginate(count, @params);
        return hospitals;
    }

    public Task<bool> UpdateAsync(long adminId, AdministratorUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}
