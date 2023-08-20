using OnMed.DataAccess.Interfaces.Administrators;
using OnMed.DataAccess.Interfaces.Categories;
using OnMed.DataAccess.Interfaces.Doctors;
using OnMed.DataAccess.Interfaces.Hospitals;
using OnMed.DataAccess.Interfaces.Users;
using OnMed.DataAccess.Repositories.Administrators;
using OnMed.DataAccess.Repositories.Categories;
using OnMed.DataAccess.Repositories.Doctors;
using OnMed.DataAccess.Repositories.Hospitals;
using OnMed.DataAccess.Repositories.Users;

namespace OnMed.WebApi.Configurations.Layers
{
    public static class DataAccessConfiguration
    {
        public static void ConfigureDataAccess(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IHospitalRepository, HospitalRepository>();
            builder.Services.AddScoped<IHospitalBranchDoctorRepository, HospitalBranchDoctorRepository>();
            builder.Services.AddScoped<IHospitalBranchRepository, HospitalBranchRepository>();
            builder.Services.AddScoped<IAdministratorRepository, AdministratorRepository>();
            builder.Services.AddScoped<IHospitalBranchAdminRepository, HospitalBranchAdminRepository>();
            builder.Services.AddScoped<IHospitalBranchCategoryRepository, HospitalBranchCategoryRepository>();
        }
    }
}
