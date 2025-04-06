using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Students;
using SharedKernel;

namespace Application.Features.Students.Commands.ImportStudents
{
    public enum ImportStrategy
    {
        Csv,
        Json
    }
    public interface IStudentImportStrategy
    {
        Task<Result<IEnumerable<Student>>> ImportStudentsAsync(Stream fileStream);
    }
}