

using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using SmartStudentManagementSystemRESTfulAPI.Configurations;
using SmartStudentManagementSystemRESTfulAPI.Infrastructure;
using SmartStudentManagementSystemRESTfulAPI.Infrastructure.Seeders;
using System.Text;

namespace SmartStudentManagementSystemRESTfulAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add Validation
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();// Add services to the container.

            builder.Services.AddAutoMapper(cfg => {
                cfg.LicenseKey = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ikx1Y2t5UGVubnlTb2Z0d2FyZUxpY2Vuc2VLZXkvYmJiMTNhY2I1OTkwNGQ4OWI0Y2IxYzg1ZjA4OGNjZjkiLCJ0eXAiOiJKV1QifQ.eyJpc3MiOiJodHRwczovL2x1Y2t5cGVubnlzb2Z0d2FyZS5jb20iLCJhdWQiOiJMdWNreVBlbm55U29mdHdhcmUiLCJleHAiOiIxODE3NTEwNDAwIiwiaWF0IjoiMTc4NjAxMDA0OCIsImFjY291bnRfaWQiOiIwMTlmZDY3ZGI4NjM3OGVjOGE5ZjU5MzYwN2RkOTJhYiIsImN1c3RvbWVyX2lkIjoiMDE5ZmQ2N2RiODYzNzhlYzhhOWY1OTM2MDdkZDkyYWIiLCJzdWJfaWQiOiItIiwiZWRpdGlvbiI6IjAiLCJ0eXBlIjoiMiJ9.lZCUL-vWwuPJCzugXgPo5t27zLWCeltCiWf-WZACBVoLLMsJCuFSyXbcvt4vT7zzyYAotmiyWvnjJfg8HQA6m9r7tHfUSVPUGt85sqnaq2Ks65x5RF_x1dCtnNavBDRnNKyzZGDIDeZVB3LqT3sP_nHbE1vfmKVa_PyBG_faDj7oS2ppX7C-Jq52EsLkGg9u33KGemC6d2Ls_XvxSYyRMgNOK3RjbhaJH0qONUwIHZOo-PlaR4dEuoUCZkj2dq18Sp1LEQjpY0wfsYBS_ducSIUzccPqpIBP5lp1irUB3UYBY1jlxudxI_d01o0EU80jSbxlZSrqzR0Z7fYeU3J7Hw"; // مطلوب في الإصدارات الحديثة
            }, typeof(Program)); // يمكنك تمرير typeof(Program) أو الـ Assembly الخاص بمشروعك
            
            
            // Add DbContext
            builder.Services.AddDbContext<SchoolDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                )
            );

            // Add Services By Reflection
            var serviceAssembly = typeof(StudentService).Assembly;
            var serviceTypes = serviceAssembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service"));
            foreach (var serviceType in serviceTypes)
            {
                builder.Services.AddScoped(serviceType);
            }
            // Register Jwt Settings
            builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

            // Add Identity services
            builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>()
                .AddEntityFrameworkStores<SchoolDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    var jwtSettings = builder.Configuration
                                             .GetSection("Jwt")
                                             .Get<JwtSettings>()!;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings.Issuer,
                    
                        ValidateAudience = true,
                        ValidAudience = jwtSettings.Audience,
                    
                        ValidateLifetime = true,
                    
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    
                        ClockSkew = TimeSpan.Zero
                    };
    });

            // Add Controllers
            builder.Services.AddControllers();


            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            //// Add Swagger/OpenAPI
            //builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen();


            var app = builder.Build();
            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

            // Apply migrations on startup (Optional)
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var db = services.GetRequiredService<SchoolDbContext>();
                 db.Database.MigrateAsync();

                var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();

                 RoleSeeder.SeedAsync(roleManager);
            }

            // Configure Swagger middleware
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                // Optional: If you want to expose the Scalar API reference in development
                //http://localhost:7152/scalar/v1
                app.MapScalarApiReference(); 
                
                //app.UseSwagger();
                //app.UseSwaggerUI();
            }


            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}
