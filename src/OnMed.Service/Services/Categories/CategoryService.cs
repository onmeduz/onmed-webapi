using OnMed.Application.Exceptions.Categories;
using OnMed.Application.Exceptions.Files;
using OnMed.Application.Utils;
using OnMed.DataAccess.Interfaces.Categories;
using OnMed.DataAccess.ViewModels.Doctors;
using OnMed.Domain.Entities.Categories;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Categories;
using OnMed.Service.Interfaces.Categories;
using OnMed.Service.Interfaces.Common;
using System.Data.Common;

namespace OnMed.Service.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly IFileService _fileService;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IPaginator _paginator;

    public CategoryService(ICategoryRepository categoryRepository,
        IFileService fileService,
        IPaginator paginator)
    {
        this._fileService = fileService;
        this._categoryRepository = categoryRepository;
        this._paginator = paginator;
    }

    public Task<long> CountAsync()
    {
        var categoryCount = _categoryRepository.CountAsync();
        return categoryCount;
    }

    public async Task<bool> CreateAsync(CategoryCreateDto dto)
    {
        string imagepath = await _fileService.UploadImageAsync(dto.Image, "categories");
        Category category = new Category();
        category.ImagePath = imagepath;
        category.Professionality = dto.Professionality;
        category.ProfessionalityHint = dto.ProfessionalityHint;
        category.Professional = dto.Professional;
        category.ProfessionalHint = dto.ProfessionalHint;
        category.CreatedAt = TimeHelper.GetDateTime();
        category.UpdatedAt = TimeHelper.GetDateTime();
        var result = await _categoryRepository.CreateAsync(category);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(long categoryId)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category is null) throw new CategoryNotFoundException();

        var result = await _fileService.DeleteImageAsync(category.ImagePath);
        if (result == false) throw new ImageNotFoundException();

        var dbResult = await _categoryRepository.DeleteAsync(categoryId);

        return dbResult > 0;
    }

    public async Task<IList<Category>> GetAllAsync(PaginationParams @params)
    {
        var categories = await _categoryRepository.GetAllAsync(@params);
        var count = await _categoryRepository.CountAsync();
        _paginator.Paginate(count, @params);
        return categories;
    }

    public async Task<IList<Category>> SearchAsync(string search)
    {
        var searches = await _categoryRepository.SearchAsync(search);
        return searches;
    }

    public async Task<bool> UpdateAsync(long categoryId, CategoryUpdateDto dto)
    {
        var category = await _categoryRepository.GetByIdAsync(categoryId);
        if (category is null) throw new CategoryNotFoundException();

        category.Professionality = dto.Professionality;
        category.Professional = dto.Professional;

        if (dto.Image is not null)
        {
            var deleteResult = await _fileService.DeleteImageAsync(category.ImagePath);
            if (deleteResult is false) throw new ImageNotFoundException();

            string newImagePath = await _fileService.UploadImageAsync(dto.Image, "categories");

            category.ImagePath = newImagePath;
        }

        category.UpdatedAt = TimeHelper.GetDateTime();
        var dbResult = await _categoryRepository.UpdateAsync(categoryId, category);

        return dbResult > 0;
    }
}
