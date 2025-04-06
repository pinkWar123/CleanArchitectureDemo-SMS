using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Features.Students.Commands.ImportStudents
{
    public interface IStudentImportFactory
    {
        IStudentImportStrategy Create(ImportStrategy strategy);
    }
}