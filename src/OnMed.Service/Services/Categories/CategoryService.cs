using OnMed.DataAccess.Interfaces.Categories;
using OnMed.Domain.Entities.Categories;
using OnMed.Persistance.Dtos.Categories;
using OnMed.Service.Common.Helpers;
using OnMed.Service.Interfaces.Categories;
using OnMed.Service.Interfaces.Common;

namespace OnMed.Service.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly IFileService _fileService;
    private readonly ICategoryRepository _categoryService;

    public CategoryService(ICategoryRepository categoryRepository,
        IFileService fileService)
    {
        this._fileService = fileService;
        this._categoryService = categoryRepository;
    }

    public async Task<bool> CreateAsync(CategoryCreateDto dto)
    {
        string imagepath = await _fileService.UploadImageAsync(dto.Image);
        Category category = new Category();
        category.ImagePath = imagepath;
        category.Professionality = dto.Professionality;
        category.ProfessionalityHint = dto.ProfessionalityHint;
        category.Professional = dto.Professional;
        category.ProfessionalHint= dto.ProfessionalHint;
        category.CreatedAt = TimeHelper.GetDateTime();
        category.UpdatedAt = TimeHelper.GetDateTime();
        var result = await _categoryService.CreateAsync(category);

        return result > 0;
    }
}
