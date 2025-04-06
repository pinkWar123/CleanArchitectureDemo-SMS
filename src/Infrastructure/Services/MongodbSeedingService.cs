using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Faculties;
using Domain.Entities.LearningPrograms;
using Domain.Entities.Statuses;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Services
{
    public class MongodbSeedingService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public MongodbSeedingService(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Ruin seed");
            using var scope = _serviceProvider.CreateScope();
            var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var existingFaculties = await _unitOfWork.Faculties.GetAll();
            if(!existingFaculties.Any())
            {   
                var faculties = new List<Faculty>
                {
                    Faculty.Create("Luật").Value,
                    Faculty.Create("Tiếng anh thương mại").Value,
                    Faculty.Create("Tiếng nhật").Value,
                    Faculty.Create("Tiếng Pháp").Value,
                };

                foreach(var faculty in faculties)
                {
                    await _unitOfWork.Faculties.Create(faculty);
                }

            }

            var existingPrograms = await _unitOfWork.LearningPrograms.GetAll();

            if(!existingPrograms.Any())
            {
                var programs = new List<LearningProgram>
                {
                    LearningProgram.Create("Cử nhân tài năng").Value,
                    LearningProgram.Create("Tiên tiến").Value,
                    LearningProgram.Create("Chất lượng cao").Value,
                    LearningProgram.Create("Đại trà").Value,
                };

                foreach(var program in programs)
                {
                    await _unitOfWork.LearningPrograms.Create(program);
                }
            }

            var existingStatuses = await _unitOfWork.Statuses.GetAll();

            if(!existingStatuses.Any())
            {
                var statuses = new List<Status>
                {
                    Status.Create("Đang học").Value,
                    Status.Create("Đã tốt nghiệp").Value,
                    Status.Create("Đã thôi học").Value,
                    Status.Create("Tạm dừng học").Value,
                };

                foreach(var status in statuses)
                {
                    await _unitOfWork.Statuses.Create(status);
                }
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}