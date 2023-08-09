using OnMed.Service.Interfaces.Categories;
using OnMed.Service.Interfaces.Common;
using OnMed.Service.Services.Categories;
using OnMed.Service.Services.Common;

namespace OnMed.WebApi.Configurations.Layers
{
    public static class ServiceLayerConfiguration
    {
        public static void ConfigureServiceLayer(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IFileService, FileService>();
        }
    }
}
