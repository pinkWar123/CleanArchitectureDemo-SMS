using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.Entities.Statuses
{
    public static class StatusError
    {
        public static Error StatusNotFound => Error.NotFound(
            "StatusError.StatusNotFound",
            "Status not found"
        );
    }
}