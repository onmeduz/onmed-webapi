using OnMed.Persistance.Dtos.Categories;

namespace OnMed.Service.Interfaces.Categories;

public interface ICategoryService
{
    public Task<bool> CreateAsync(CategoryCreateDto dto);

}
