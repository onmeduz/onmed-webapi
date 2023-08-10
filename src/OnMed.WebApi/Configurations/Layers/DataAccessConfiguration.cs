using OnMed.DataAccess.Interfaces.Categories;
using OnMed.DataAccess.Interfaces.Users;
using OnMed.DataAccess.Repositories.Categories;
using OnMed.DataAccess.Repositories.Users;

namespace OnMed.WebApi.Configurations.Layers
{
    public static class DataAccessConfiguration
    {
        public static void ConfigureDataAccess(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
