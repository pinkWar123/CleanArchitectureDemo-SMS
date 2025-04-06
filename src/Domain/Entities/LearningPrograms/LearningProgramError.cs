using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.Entities.LearningPrograms
{
    public static class LearningProgramError
    {
        public static Error LearningProgramNotFound => Error.NotFound(
            "LearningProgramError.LearningProgramNotFound",
            "Learning program not found"
        );
    }
}