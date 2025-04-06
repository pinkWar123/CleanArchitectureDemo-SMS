using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel;

namespace Infrastructure.Services.ImportStudents
{
    public static class ImportError
    {
        public static Error InvalidFormat => Error.Validation(
            "ImportError.InvalidFormat",
            "Invalid student format"
        );
    }
}