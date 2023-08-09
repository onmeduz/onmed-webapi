using OnMed.DataAccess.Interfaces.Categories;
using OnMed.DataAccess.Repositories.Categories;

namespace OnMed.WebApi.Configurations.Layers
{
    public static class DataAccessConfiguration
    {
        public static void ConfigureDataAccess(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        }
    }
}
