using OnMed.Application.Exceptions.Categories;
using OnMed.Application.Exceptions.Files;
using OnMed.DataAccess.Interfaces;
using OnMed.DataAccess.Interfaces.Categories;
using OnMed.Domain.Entities.Categories;
using OnMed.Persistance.Common.Helpers;
using OnMed.Persistance.Dtos.Categories;
using OnMed.Service.Interfaces.Categories;
using OnMed.Service.Interfaces.Common;
using System.Net.WebSockets;

namespace OnMed.Service.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly IFileService _fileService;
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository,
        IFileService fileService)
    {
        this._fileService = fileService;
        this._categoryRepository = categoryRepository;
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
        category.ProfessionalHint= dto.ProfessionalHint;
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

            string newImagePath = await _fileService.UploadImageAsync(dto.Image,"categories");

            category.ImagePath = newImagePath;
        }

        category.UpdatedAt = TimeHelper.GetDateTime();
        var dbResult = await _categoryRepository.UpdateAsync(categoryId, category);

        return dbResult > 0;
    }
}
