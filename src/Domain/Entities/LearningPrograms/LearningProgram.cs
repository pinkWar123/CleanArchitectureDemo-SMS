using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.ValueObjects.Names;
using SharedKernel;

namespace Domain.Entities.LearningPrograms
{
    public class LearningProgram : BaseEntity
    {
        public Name Name { get; }

        private LearningProgram() {}
        private LearningProgram(Name name)
        {
            Name = name;
        }

        public static Result<LearningProgram> Create(string name)
        {
            var programName = Name.Create(name);
            if(programName.IsFailure) return Result.Failure<LearningProgram>(programName.Error);

            return new LearningProgram(programName.Value);
        }
    }
}