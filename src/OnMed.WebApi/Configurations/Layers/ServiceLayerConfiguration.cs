using OnMed.Service.Interfaces.Administrators;
using OnMed.Service.Interfaces.Auth;
using OnMed.Service.Interfaces.Categories;
using OnMed.Service.Interfaces.Common;
using OnMed.Service.Interfaces.Doctors;
using OnMed.Service.Interfaces.Heads;
using OnMed.Service.Interfaces.Hospitals;
using OnMed.Service.Interfaces.Notifications;
using OnMed.Service.Interfaces.Users;
using OnMed.Service.Services.Administrators;
using OnMed.Service.Services.Auth;
using OnMed.Service.Services.Categories;
using OnMed.Service.Services.Common;
using OnMed.Service.Services.Doctors;
using OnMed.Service.Services.Heads;
using OnMed.Service.Services.Hospitals;
using OnMed.Service.Services.Notifications;
using OnMed.Service.Services.Users;

namespace OnMed.WebApi.Configurations.Layers
{
    public static class ServiceLayerConfiguration
    {
        public static void ConfigureServiceLayer(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddSingleton<ISmsSender, SmsSender>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IHospitalService, HospitalService>();
            builder.Services.AddScoped<IPaginator, Paginator>();
            builder.Services.AddScoped<IHospitalBranchService, HospitalBranchService>();
            builder.Services.AddScoped<IAdministratorsService, AdministratorService>();
            builder.Services.AddScoped<IDoctorAuthService, DoctorAuthService>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IHospitalService, HospitalService>();
            builder.Services.AddScoped<IIdentityService, IdentityService>();
            builder.Services.AddScoped<IUserProfileService, UserProfileService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IHeadAuthService, HeadAuthService>();
            builder.Services.AddScoped<IAdminAuthService, AdminAuthService>();
            builder.Services.AddScoped<IHeadService, HeadService>();
            builder.Services.AddScoped<IUserAppointmentService, UserAppointmentService>();
        }
    }
}
