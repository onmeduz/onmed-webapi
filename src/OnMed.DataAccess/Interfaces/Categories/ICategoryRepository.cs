using OnMed.DataAccess.Common.Interfaces;
using OnMed.DataAccess.ViewModels.Categories;
using OnMed.Domain.Entities.Categories;

namespace OnMed.DataAccess.Interfaces.Categories;

public interface ICategoryRepository : IRepository<Category, CategoryViewModel>, IGetAll<CategoryViewModel>
{
}
