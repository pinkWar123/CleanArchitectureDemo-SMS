using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Common.ValueObjects.Names;
using SharedKernel;

namespace Domain.Entities.Faculties
{
    public class Faculty : BaseEntity
    {
        public Name Name { get; }

        private Faculty() {}
        private Faculty(Name name)
        {
            Name = name;
        }

        public static Result<Faculty> Create(string name)
        {
            var facultyName = Name.Create(name);
            if(facultyName.IsFailure) return Result.Failure<Faculty>(facultyName.Error);
            return new Faculty(facultyName.Value);
        }
    }
}