using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.Entities.Students.ValueObjects
{
    public static class StudentIdError
    {
        public static Error EmptyStudentId => Error.Validation(
            "StudentIdError.EmptyStudentId", 
            "Student Id can not be empty");

        public static Error InvalidLength(int length) => Error.Validation(
            "StudentIdError.InvalidLength",
            $"Student ID must contain exactly {length} digits"
        );

        public static Error InvalidFormat => Error.Validation(
            "StudentIdError.InvalidFormat",
            "Student ID must only contain digits"
        );
    }
}