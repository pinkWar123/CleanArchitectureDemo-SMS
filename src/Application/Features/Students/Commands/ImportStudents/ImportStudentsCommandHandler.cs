using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Domain.Repositories;
using SharedKernel;

namespace Application.Features.Students.Commands.ImportStudents
{
    public class ImportStudentsCommandHandler : ICommandHandler<ImportStudentsCommand>
    {
        private readonly IStudentImportFactory _factory;
        private readonly IUnitOfWork _unitOfWork;
        public ImportStudentsCommandHandler(IUnitOfWork unitOfWork, IStudentImportFactory factory)
        {
            _unitOfWork = unitOfWork;
            _factory = factory;
        }
        public async Task<Result> Handle(ImportStudentsCommand request, CancellationToken cancellationToken)
        {
            var strategy = _factory.Create(request.strategy);
            var students = await strategy.ImportStudentsAsync(request.data);
            if(students.Value.Any())
            {
                await _unitOfWork.Students.CreateMany(students.Value);
                await _unitOfWork.SaveChangesAsync();
                return Result.Success();
            }

            return Result.Failure(students.Error);
        }
    }
}