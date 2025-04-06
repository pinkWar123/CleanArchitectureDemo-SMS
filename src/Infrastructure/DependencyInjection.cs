using Application.Features.Students.Commands.ImportStudents;
using Application.Interface;
using Domain.Repositories;
using Infrastructure.Data;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Mongodb;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Infrastructure.Services.ImportStudents;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        // var connectionString = configuration.GetConnectionString("SqlServer");

        #region Connection string for sql
        // Console.WriteLine(connectionString);
        services.AddDbContextFactory<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        // services.AddDbContextFactory<ApplicationDbContext>(options =>
        //     options.UseSqlServer(connectionString));
        #endregion

        #region register db context for sql
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        #endregion


        // Register MongoDB client as a singleton.
        #region register db context for nosql
//         services.AddSingleton<IMongoClient>(sp =>
//         {
//             var configuration = sp.GetRequiredService<IConfiguration>();
//             var connectionString = configuration["MongoDbSettings:ConnectionString"];
//             return new MongoClient(connectionString);
//         });

// // Register the MongoDB database.
//         services.AddScoped(sp =>
//         {
//             var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
//             var client = sp.GetRequiredService<IMongoClient>();
//             return client.GetDatabase(settings.DatabaseName);
//         });
        #endregion


        #region repo for sql
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IStudentRepository, StudentRepository>();
        services.AddTransient<IFacultyRepository, FacultyRepository>();
        services.AddTransient<ILearningProgramRepository, LearningProgramRepository>();
        services.AddTransient<IStatusRepository, StatusRepository>();
        #endregion

        #region repo for nosql
        // services.AddScoped<MongoDbContext>();
        // services.AddTransient<IUnitOfWork, MongoUnitOfWork>();
        // services.AddScoped<IStudentRepository, MongodbStudentRepository>();
        // services.AddScoped<IFacultyRepository, MongodbFacultyRepository>();
        // services.AddScoped<ILearningProgramRepository, MongodbLearningProgramRepository>();
        // services.AddScoped<IStatusRepository, MongodbStatusRepository>();
        // MongoMappings.RegisterMappings();
        #endregion
        
        services.AddScoped<IStudentImportFactory, StudentImportFactory>();
        

        #region seed for sql
        services.AddHostedService<MigrationServices>();
        #endregion

        #region seed for nosql
        // services.AddHostedService<MongodbSeedingService>();
        #endregion
        return services;
    }
}
