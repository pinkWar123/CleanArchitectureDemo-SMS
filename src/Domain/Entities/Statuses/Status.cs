using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Domain.Common.ValueObjects.Names;
using SharedKernel;

namespace Domain.Entities.Statuses
{
    public class Status : BaseEntity
    {
        public Name Name { get; }
        private Status(Name name)
        {
            Name = name;
        }

        public static Result<Status> Create(string name)
        {
            var createdName = Name.Create(name);
            if(createdName.IsFailure) return Result.Failure<Status>(createdName.Error);
            return Result.Success(new Status(createdName.Value));
        }
    }
}