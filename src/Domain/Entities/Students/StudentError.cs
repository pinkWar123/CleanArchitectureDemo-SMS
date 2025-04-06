using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.Entities.Students
{
    public static class StudentError
    {
        public static Error StudentHasExisted => Error.Conflict(
            "StudentError.StudentHasExisted",
            $"Student has already existed"
        );

        public static Error StudentNotFound => Error.NotFound(
            "StudentError.StudentNotFound",
            "Student has not existed"
        );

        public static Error NoStudents => Error.NotFound(
            "StudentError.NoStudents",
            "No students can be found"
        ); 
    }
}