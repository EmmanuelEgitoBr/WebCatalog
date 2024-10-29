﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebCatalog.Application.DTOs.Mappings;
using WebCatalog.Application.Interfaces;
using WebCatalog.Application.Services;
using WebCatalog.Domain.Entities;
using WebCatalog.Domain.Interfaces;
using WebCatalog.Infrastructure.Context;
using WebCatalog.Infrastructure.Repositories;

namespace WebCatalog.CrossCutting.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            string secretKey = configuration["JWT:SecretKey"] 
                ?? throw new ArgumentException("Invalid Secret Key");
            string validIssuer = configuration["JWT:ValidIssuer"]!;
            string validAudience = configuration["JWT:ValidAudience"]!;

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
                options.AddPolicy("SuperAdminOnly",
                    policy => policy.RequireRole("Admin").RequireClaim("id", "eegito"));
                options.AddPolicy("ExclusiveOnly",
                                    policy => policy.RequireAssertion(context => 
                                        context.User.HasClaim(claim => claim.Type == "id" && 
                                                                claim.Value == "eegito" ||
                                                                context.User.IsInRole("SuperAdmin"))));
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = validAudience,
                    ValidIssuer = validIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
            
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://www.apirequest.io");
                });
            });
            
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();

            return services;
        }
    }
}
