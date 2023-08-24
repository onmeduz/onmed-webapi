using OnMed.Application.Exceptions.Administrators;
using OnMed.Application.Exceptions.Hospitals;
using OnMed.Application.Exceptions.Users;
using OnMed.Application.Utils;
using OnMed.DataAccess.Interfaces.Administrators;
using OnMed.DataAccess.Interfaces.Hospitals;
using OnMed.DataAccess.ViewModels.Administrators;
using OnMed.Domain.Entities.Administrators;
using OnMed.Domain.Entities.Hospitals;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Administrators;
using OnMed.Service.Common.Security;
using OnMed.Service.Interfaces.Administrators;
using OnMed.Service.Interfaces.Common;

namespace OnMed.Service.Services.Administrators;

public class AdministratorService : IAdministratorsService
{
    private readonly IAdministratorRepository _administratorRepository;
    private readonly IFileService _fileService;
    private readonly IHospitalBranchRepository _hospitalBranchRepository;
    private readonly IPaginator _paginator;
    private readonly IHospitalBranchAdminRepository _hospitalBranchAdminRepository;

    public AdministratorService(IAdministratorRepository administrator,
        IHospitalBranchRepository hospitalBranch,
        IHospitalBranchAdminRepository hospitalAdminRepository,
        IFileService fileService,
        IPaginator paginator)
    {
        this._administratorRepository = administrator;
        this._fileService = fileService;
        this._hospitalBranchRepository = hospitalBranch;
        this._paginator = paginator;
        this._hospitalBranchAdminRepository = hospitalAdminRepository;
    }

    public Task<long> CountAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> CreateAsync(AdministratorCreateDto dto)
    {
        var isThere = await _hospitalBranchRepository.GetByIdAsync(dto.HospitalId);
        if (isThere is null) throw new HospitalBranchNotFoundException();
        var admin = await _administratorRepository.GetByPhoneNumberAsync(dto.PhoneNumber);
        if (admin is not null) throw new UserAlreadyExistsException(dto.PhoneNumber);

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
        administrator.PhoneNumberConfirmed = true;

        if (dto.Image is not null)
        {
            string newImagePath = await _fileService.UploadImageAsync(dto.Image, "administrators");
            administrator.ImagePath = newImagePath;
        }
        administrator.Region = dto.Region;
        administrator.CreatedAt = administrator.UpdatedAt = TimeHelper.GetDateTime();

        var lastAdminId = await _administratorRepository.CreateAndReturnIdAsync(administrator);
        if (lastAdminId > 0)
        {
            var hospitalBranchAdmin = new HospitalBranchAdmin();
            hospitalBranchAdmin.HospitalBranchId = dto.HospitalId;
            hospitalBranchAdmin.AdministratorsId = lastAdminId;
            hospitalBranchAdmin.CreatedAt = hospitalBranchAdmin.UpdatedAt = TimeHelper.GetDateTime();
            var result = await _hospitalBranchAdminRepository.CreateAsync(hospitalBranchAdmin);
            return result > 0;
        }
        else return false;
    }

    public async Task<bool> DeleteAsync(long adminId)
    {
        var administrator = await _administratorRepository.GetByIdAsync(adminId);
        if (administrator is null) throw new AdminNotFoundException();

        var result = await _fileService.DeleteImageAsync(administrator.ImagePath);

        var dbResult = await _administratorRepository.DeleteAsync(adminId);
        return dbResult > 0;
    }

    public async Task<IList<AdministratorViewModel>> GetAllAsync(PaginationParams @params)
    {
        var hospitals = await _administratorRepository.GetAllAsync(@params);
        var count = await _administratorRepository.CountAsync();
        _paginator.Paginate(count, @params);
        return hospitals;
    }

    public async Task<bool> UpdateAsync(long adminId, AdministratorUpdateDto dto)
    {
        var administrator = await _administratorRepository.GetByIdAsync(adminId);
        if (administrator == null) throw new AdminNotFoundException();
        administrator.FirstName = dto.FirstName;
        administrator.LastName = dto.LastName;
        administrator.MiddleName = dto.MiddleName;
        administrator.Region = dto.Region;
        administrator.UpdatedAt = TimeHelper.GetDateTime();
        var dbResult = await _administratorRepository.UpdateAsync(adminId, administrator);

        return dbResult > 0;
    }
}
