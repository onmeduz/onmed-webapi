using OnMed.DataAccess.Common.Interfaces;
using OnMed.Domain.Entities.Categories;

namespace OnMed.DataAccess.Interfaces.Categories;

public interface ICategoryRepository : IRepository<Category>,
    IGetAll<Category>
{ }
