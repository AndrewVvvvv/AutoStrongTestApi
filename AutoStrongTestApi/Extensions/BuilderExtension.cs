using AutoStrongTestApi.Interfaces;
using AutoStrongTestApi.Services;
using Microsoft.Net.Http.Headers;

namespace AutoStrongTestApi.Extensions
{
    public static class BuilderExtension
    {
        public static WebApplicationBuilder InitServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: Constants.PolicyName, policy =>
                {
                    var origins = builder.Configuration.GetSection("Origins").Get<string[]>()!;
                    policy.WithOrigins(origins)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<IStorageService, StorageService>();

            return builder;
        }
    }
}
