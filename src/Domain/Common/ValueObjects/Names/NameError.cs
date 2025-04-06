using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.Common.ValueObjects.Names
{
    public static class NameError
    {
        public static Error EmptyName => Error.Validation(
            "StudentNameError.EmptyName",
            "Student name can not be empty"
        );

        public static Error InvalidFormat => Error.Validation(
            "StudentNameError.InvalidFormat",
            "Name can only contain letters"
        );
    }
}