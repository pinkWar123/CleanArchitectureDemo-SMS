using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Features.Students.Commands.ImportStudents;
using Domain.Repositories;

namespace Infrastructure.Services.ImportStudents
{
    public class StudentImportFactory : IStudentImportFactory
    {
        private readonly IUnitOfWork _unitOfWork;
        public StudentImportFactory(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IStudentImportStrategy Create(ImportStrategy strategy)
        {
            return strategy switch 
            {
                ImportStrategy.Csv => new CsvStudentImportStrategy(_unitOfWork),
                ImportStrategy.Json => new JsonStudentImportStrategy(_unitOfWork),
                _ => throw new Exception("Invalid strategy")
            };
        }
    }
}