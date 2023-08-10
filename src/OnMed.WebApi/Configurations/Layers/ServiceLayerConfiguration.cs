﻿using OnMed.Service.Interfaces.Auth;
using OnMed.Service.Interfaces.Categories;
using OnMed.Service.Interfaces.Common;
using OnMed.Service.Interfaces.Notifications;
using OnMed.Service.Services.Auth;
using OnMed.Service.Services.Categories;
using OnMed.Service.Services.Common;
using OnMed.Service.Services.Notifications;

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
            builder.Services.AddScoped<ISmsSender, SmsSender>();
        }
    }
}
