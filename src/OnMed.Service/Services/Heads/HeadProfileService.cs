using OnMed.Application.Exceptions;
using OnMed.Application.Exceptions.Files;
using OnMed.DataAccess.Interfaces.Heads;
using OnMed.DataAccess.ViewModels.Heads;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Auth;
using OnMed.Persistance.Dtos.Heads;
using OnMed.Persistance.Dtos.Users;
using OnMed.Service.Common.Security;
using OnMed.Service.Interfaces.Auth;
using OnMed.Service.Interfaces.Common;
using OnMed.Service.Interfaces.Heads;

namespace OnMed.Service.Services.Heads;

public class HeadProfileService : IHeadProfileService
{
    private IIdentityService _identity;
    private readonly IHeadRepository _repository;
    private readonly IFileService _fileService;

    public HeadProfileService(IIdentityService identityService,
        IHeadRepository headRepository,
        IFileService fileService)
    {
        this._identity = identityService;
        this._repository = headRepository;
        this._fileService = fileService;
    }

    public async Task<HeadViewModel?> GetProfileInfoAsync() =>
        await _repository.GetByIdViewAsync(_identity.UserId);

    public async Task<bool> UpdateAsync(HeadUpdateDto dto)
    {
        var date = DateTime.Parse(dto.BirthDay);

        DateOnly BirthDay = new DateOnly(date.Year, date.Month, date.Day);

        var head = await _repository.GetByIdAsync(_identity.UserId);
        if (head is null) throw new HeadNotFoundException();

        head.FirstName = dto.FirstName;
        head.LastName = dto.LastName;
        head.MiddleName = dto.MiddleName;
        head.BirthDay = BirthDay;
        head.IsMale = dto.IsMale;
        head.Region = dto.Region;
        head.UpdatedAt = TimeHelper.GetDateTime();

        var dbResult = await _repository.UpdateAsync(head.Id, head);
        return dbResult > 0;
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
    {
        var head = await _repository.GetByIdAsync(_identity.UserId);
        if (head is null) throw new HeadNotFoundException();
        var hasherResult = PasswordHasher.Hash(dto.Password);
        head.PasswordHash = hasherResult.Hash;
        head.Salt = hasherResult.Salt;
        head.UpdatedAt = TimeHelper.GetDateTime();
        var result = await _repository.UpdateAsync(head.Id, head);

        return result > 0;
    }

    public async Task<bool> DeleteImageAsync()
    {
        var head = await _repository.GetByIdAsync(_identity.UserId);
        if (head is null) throw new HeadNotFoundException();
        var fileResult = await _fileService.DeleteImageAsync(head.ImagePath);
        if (fileResult) head.ImagePath = "";
        head.UpdatedAt = TimeHelper.GetDateTime();
        var result = await _repository.UpdateAsync(head.Id, head);

        return result > 0;
    }

    public async Task<bool> UploadImageAsync(UploadImageDto file)
    {
        var head = await _repository.GetByIdAsync(_identity.UserId);
        if (head is null) throw new HeadNotFoundException();

        if (file.Image is not null && head.ImagePath != "")
        {
            var deleteResult = await _fileService.DeleteImageAsync(head.ImagePath);
            if (deleteResult is false) throw new ImageNotFoundException();
        }

        var fileResult = await _fileService.UploadImageAsync(file.Image!, "heads");
        if (fileResult is not null) head.ImagePath = fileResult;
        head.UpdatedAt = TimeHelper.GetDateTime();
        var result = await _repository.UpdateAsync(head.Id, head);

        return result > 0;
    }

    public Task<bool> DeleteAsync()
    {
        throw new NotImplementedException();
    }
}
