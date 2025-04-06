using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Domain.Entities.Students;
using Domain.Repositories;
using SharedKernel;

namespace Application.Features.Students.Queries.FindStudentByStudentId
{
    public class FindStudentByStudentIdQueryHandler : IQueryHandler<FindStudentByStudentIdQuery, Student>
    {
        private readonly IUnitOfWork _unitOfWork;
        public FindStudentByStudentIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Student>> Handle(FindStudentByStudentIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _unitOfWork.Students.GetStudentByStudentId(request.studentId);
            if(student == null) return Result.Failure<Student>(StudentError.StudentNotFound);

            return Result.Success(student!);
        }
    }
}