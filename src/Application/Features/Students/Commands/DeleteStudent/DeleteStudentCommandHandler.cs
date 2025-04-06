using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Domain.Entities.Students;
using Domain.Repositories;
using SharedKernel;

namespace Application.Features.Students.Commands.DeleteStudent
{
    public class DeleteStudentCommandHandler : ICommandHandler<DeleteStudentCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteStudentCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _unitOfWork.Students.GetStudentByStudentId(request.studentId);
            if(student == null) return Result.Failure<Student>(StudentError.StudentNotFound);

            await _unitOfWork.Students.Delete(student);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}