using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.Entities.Students.ValueObjects
{
    public sealed class StudentId : IEquatable<StudentId>
    {
        public string Value { get; }
        public static readonly int LENGTH = 8;
        private StudentId(string value)
        {
            Value = value;
        }
        
        public static Result<StudentId> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<StudentId>(StudentIdError.EmptyStudentId);
            
            if (value.Length != LENGTH)
                return Result.Failure<StudentId>(StudentIdError.InvalidLength(LENGTH));
            
            if (!value.All(char.IsDigit))
                return Result.Failure<StudentId>(StudentIdError.InvalidFormat);
            
            return Result.Success(new StudentId(value));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as StudentId);
        }

        public bool Equals(StudentId other)
        {
            if (other is null)
                return false;
            return Value == other.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}