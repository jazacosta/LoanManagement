﻿using Core.DTOs.InstallmentSimulator;
using Core.DTOs.Request;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using FluentValidation;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Infrastructure.Services.Authentication;
using Infrastructure.Validations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDatabase(configuration);
            services.AddServices();
            services.AddRepositories();
            //services.AddValidations();
            services.AddAuth();

            return services;
        }
        

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("LoanManagement");

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            return services;

        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ISimulatorService, SimulatorService>();
            services.AddScoped<ILoanRequestService, LoanRequestService>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITermInterestRateRepository, TermInterestRateRepository>();
            services.AddScoped<ILoanRequestRepository, LoanRequestRepository>();

            return services;
        }

        public static IServiceCollection AddValidations(this IServiceCollection services)
        {
            services.AddScoped<IValidator<InstallmentSimDTO>, SimulatorValidation>();
            services.AddScoped<IValidator<LoanRequestDTO>, LoanRequestValidation>();

            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConfig.Secret))
                };
            });

            services.AddTransient<AuthService>();

            return services;
        }
    }
}