using OnMed.Application.Exceptions.Files;
using OnMed.Application.Exceptions.Users;
using OnMed.DataAccess.Interfaces.Users;
using OnMed.DataAccess.ViewModels.Users;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Auth;
using OnMed.Persistance.Dtos.Users;
using OnMed.Service.Common.Security;
using OnMed.Service.Interfaces.Auth;
using OnMed.Service.Interfaces.Common;
using OnMed.Service.Interfaces.Users;


namespace OnMed.Service.Services.Users;

public class UserProfileService : IUserProfileService
{
    private readonly IIdentityService _identity;
    private readonly IUserRepository _repository;
    private readonly IFileService _fileService;

    public UserProfileService(IIdentityService identityService,
        IUserRepository userRepository,
        IFileService fileService)
    {
        this._identity = identityService;
        this._repository = userRepository;
        this._fileService = fileService;
    }

    public Task<bool> DeleteAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<UserViewModel?> GetProfileInfoAsync() =>
        await _repository.GetByIdViewAsync(_identity.UserId);

    public async Task<bool> UpdateAsync(UserUpdateDto dto)
    {
        var date = DateTime.Parse(dto.BirthDay);

        DateOnly BirthDay = new DateOnly(date.Year, date.Month, date.Day);

        var user = await _repository.GetByIdAsync(_identity.UserId);
        if (user is null) throw new UserNotFoundException();

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.MiddleName = dto.MiddleName;
        user.BirthDay = BirthDay;
        user.IsMale = dto.IsMale;
        user.Region = dto.Region;
        user.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.UpdateAsync(user.Id, user);
        return dbResult > 0;
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
    {
        var user = await _repository.GetByIdAsync(_identity.UserId);
        if (user is null) throw new UserNotFoundException();
        var hasherResult = PasswordHasher.Hash(dto.Password);
        user.PasswordHash = hasherResult.Hash;
        user.Salt = hasherResult.Salt;
        user.UpdatedAt = TimeHelper.GetDateTime();
        var result = await _repository.UpdateAsync(user.Id, user);

        return result > 0;
    }

    public async Task<bool> DeleteImageAsync()
    {
        var user = await _repository.GetByIdAsync(_identity.UserId);
        if (user is null) throw new UserNotFoundException();
        var fileResult = await _fileService.DeleteImageAsync(user.ImagePath);
        if (fileResult) user.ImagePath = "";
        user.UpdatedAt = TimeHelper.GetDateTime();
        var result = await _repository.UpdateAsync(user.Id, user);

        return result > 0;
    }

    public async Task<bool> UploadImageAsync(UploadImageDto file)
    {
        var user = await _repository.GetByIdAsync(_identity.UserId);
        if (user is null) throw new UserNotFoundException();

        if (file.Image is not null && user.ImagePath != "")
        {
            var deleteResult = await _fileService.DeleteImageAsync(user.ImagePath);
            if (deleteResult is false) throw new ImageNotFoundException();
        }

        var fileResult = await _fileService.UploadImageAsync(file.Image!, "users");
        if (fileResult is not null) user.ImagePath = fileResult;
        user.UpdatedAt = TimeHelper.GetDateTime();
        var result = await _repository.UpdateAsync(user.Id, user);

        return result > 0;
    }
}
