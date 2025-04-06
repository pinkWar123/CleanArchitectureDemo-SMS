using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.Entities.Students.ValueObjects.Email
{
    public static class EmailError
    {
        public static Error EmptyEmail => Error.Validation(
            "EmailError.EmptyEmail",
            "Email can not be empty"
        );

        public static Error InvalidFormat => Error.Validation(
            "EmailError.InvalidFormat",
            "Invalid format"
        );
    }
}