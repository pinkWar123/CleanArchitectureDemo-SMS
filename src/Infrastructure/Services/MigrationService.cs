using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DnsClient.Internal;
using Domain.Entities.Faculties;
using Domain.Entities.LearningPrograms;
using Domain.Entities.Statuses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Services
{
    public class MigrationServices : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public MigrationServices(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            bool hasPendingMigrations = false;

            try
            {
                var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(cancellationToken);
                if (pendingMigrations.Any())
                {
                    hasPendingMigrations = true;
                    await dbContext.Database.MigrateAsync(cancellationToken);
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
            }

            if(!dbContext.Faculties.Any())
            {   
                var faculties = new List<Faculty>
                {
                    Faculty.Create("Luật").Value,
                    Faculty.Create("Tiếng anh thương mại").Value,
                    Faculty.Create("Tiếng nhật").Value,
                    Faculty.Create("Tiếng Pháp").Value,
                };

                await dbContext.Faculties.AddRangeAsync(faculties);
            }

            if(!dbContext.LearningPrograms.Any())
            {
                var programs = new List<LearningProgram>
                {
                    LearningProgram.Create("Cử nhân tài năng").Value,
                    LearningProgram.Create("Tiên tiến").Value,
                    LearningProgram.Create("Chất lượng cao").Value,
                    LearningProgram.Create("Đại trà").Value,
                };
                await dbContext.LearningPrograms.AddRangeAsync(programs);
            }

            if(!dbContext.Statuses.Any())
            {
                var statuses = new List<Status>
                {
                    Status.Create("Đang học").Value,
                    Status.Create("Đã tốt nghiệp").Value,
                    Status.Create("Đã thôi học").Value,
                    Status.Create("Tạm dừng học").Value,
                };
                await dbContext.Statuses.AddRangeAsync(statuses);
            }

            await dbContext.SaveChangesAsync();

            // Kiểm tra xem DB có dữ liệu hay chưa.
            // Ở đây ví dụ kiểm tra bảng Users (hoặc bạn có thể kiểm tra bảng cốt lõi khác).
            // bool hasData = dbContext.Users.Any(); // Hoặc bất kỳ bảng nào bạn muốn

            // // Chỉ seed nếu:
            // //  - Có pending migration vừa được áp dụng, HOẶC
            // //  - Chưa có dữ liệu trong DB
            // if (hasPendingMigrations || !hasData)
            // {

            //     // 1) Seed các dữ liệu chung
            //     var seedResult = await SeedData.Initialize(scope.ServiceProvider);
            //     var settings = new JsonSerializerSettings
            //     {
            //         ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            //         Formatting = Formatting.Indented
            //     };
            //     string jsonResult = JsonConvert.SerializeObject(seedResult, settings);
            //     Console.WriteLine(jsonResult);

            //     // 2) Seed user + hoạt động
            //     try
            //     {
            //         var seedUser = scope.ServiceProvider.GetRequiredService<SeedUser>();
            //         var userSettings = _configuration.GetSection("Seed:Users").Get<UserSettings>();
            //         await seedUser.SeedUsersWithActivitiesAsync(
            //             userSettings.Password, 
            //             userSettings.NumberOfUsers, 
            //             userSettings.NumberOfDays
            //         );
            //         _logger.LogInformation("Users and activities seeded successfully.");
            //     }
            //     catch (Exception ex)
            //     {
            //         _logger.LogError(ex, "An error occurred while seeding users.");
            //     }
            // }
            // else
            // {
            //     _logger.LogInformation("Skipping seeding: No pending migrations and DB already has data.");
            // }
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}