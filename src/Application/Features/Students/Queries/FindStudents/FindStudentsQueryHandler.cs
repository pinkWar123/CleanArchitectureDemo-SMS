using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Messaging;
using Domain.Entities.Students;
using Domain.Repositories;
using SharedKernel;

namespace Application.Features.Students.Queries.FindStudents
{
    public class FindStudentsQueryHandler : IQueryHandler<FindStudentsQuery, List<Student>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public FindStudentsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<Student>>> Handle(FindStudentsQuery request, CancellationToken cancellationToken)
        {
            var students = await _unitOfWork.Students.GetAll();
            if(students == null || !students.Any()) return Result.Failure<List<Student>>(StudentError.NoStudents);
            return Result.Success(students.ToList());
        }
    }
}