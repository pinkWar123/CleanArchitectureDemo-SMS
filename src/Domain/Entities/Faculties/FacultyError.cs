using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.Entities.Faculties
{
    public static class FacultyError
    {
        public static Error FacultyNotFound => Error.NotFound(
            "FacultyError.FacultyNotFound",
            "Faculty not found"
        );
    }
}