using OnMed.Application.Utils;
using OnMed.Domain.Entities.Categories;
using OnMed.Persistance.Dtos.Categories;

namespace OnMed.Service.Interfaces.Categories;

public interface ICategoryService
{
    public Task<IList<Category>> GetAllAsync(PaginationParams @params);
    public Task<bool> CreateAsync(CategoryCreateDto dto);
    public Task<bool> UpdateAsync(long categoryId, CategoryUpdateDto dto);
    public Task<bool> DeleteAsync(long categoryId);
    public Task<long> CountAsync();

}
