using OnMed.Application.Exceptions.Files;
using OnMed.Application.Exceptions.Users;
using OnMed.Application.Utils;
using OnMed.DataAccess.Interfaces.Users;
using OnMed.DataAccess.ViewModels.Users;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Users;
using OnMed.Service.Interfaces.Common;
using OnMed.Service.Interfaces.Users;

namespace OnMed.Service.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPaginator _paginator;
    private readonly IFileService _fileService;

    public UserService(IUserRepository userRepository,
        IPaginator paginator,
        IFileService fileService)
    {
        this._userRepository = userRepository;
        this._paginator = paginator;
        this._fileService = fileService;
    }

    public async Task<bool> DeleteAsync(long UserId)
    {
        var user = await _userRepository.GetByIdAsync(UserId);
        if (user is null) throw new UserNotFoundException();

        var result = await _fileService.DeleteImageAsync(user.ImagePath);
        if (result == false) throw new ImageNotFoundException();

        var dbResult = await _userRepository.DeleteAsync(UserId);

        return dbResult > 0;
    }

    public async Task<IList<UserViewModel>> GetAllAsync(PaginationParams @params)
    {
        var users = await _userRepository.GetAllAsync(@params);
        var count = await _userRepository.CountAsync();
        _paginator.Paginate(count, @params);
        return users;
    }

    public async Task<bool> UpdateAsync(long userId, UserUpdateDto dto)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null) throw new UserNotFoundException();
        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.MiddleName = dto.MiddleName;
        user.BirthDay = dto.BirthDay;
        user.Region = dto.Region;
        user.IsMale = dto.IsMale;
        user.UpdatedAt = TimeHelper.GetDateTime();
        var dbResult = await _userRepository.UpdateAsync(userId, user);

        return dbResult > 0;
    }
}
