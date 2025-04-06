using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.Entities.Students.ValueObjects.Address
{
    public class AddressError
    {
        public static Error EmptyAddress => Error.Validation(
            "AddressError.EmptyAddress",
            "Address can not be empty"
        );

        public static Error InvalidFormat => Error.Validation(
            "AddressError.InvalidFormat",
            "Invalid format"
        );
    }
}