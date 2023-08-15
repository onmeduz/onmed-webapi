using OnMed.Persistance.Dtos.Categories;
using OnMed.Persistance.Dtos.Hospitals;

namespace OnMed.Service.Interfaces.Categories;

public interface ICategoryService
{
    public Task<bool> CreateAsync(CategoryCreateDto dto);
    public Task<bool> UpdateAsync(long categoryId, CategoryUpdateDto dto);
    public Task<bool> DeleteAsync(long categoryId);
    public Task<long> CountAsync();

}
