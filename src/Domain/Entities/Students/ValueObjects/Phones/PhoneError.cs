using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.Entities.Students.ValueObjects.Phones
{
    public static class PhoneError
    {
        public static Error EmptyPhone => Error.Validation(
            "PhoneError.EmptyPhone",
            "Phone can not be empty"
        );

        public static Error InvalidFormat => Error.Validation(
            "PhoneError.InvalidFormat",
            "Invalid format"
        );
    }
}