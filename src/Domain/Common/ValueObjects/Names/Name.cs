using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.Common.ValueObjects.Names
{
    public sealed class Name : IEquatable<Name>
    {
        public string Value { get; }

        private Name(string value)
        {
            Value = value;
        }

        // Factory method to create a VietnameseName with validation
        public static Result<Name> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<Name>(NameError.EmptyName);
            
            // Trim the input to remove leading/trailing spaces.
            value = value.Trim();

            // Check that the name does not contain any digits.
            if (value.Any(char.IsDigit))
                return Result.Failure<Name>(NameError.InvalidFormat);

            // Check that each character is a letter, whitespace, apostrophe, or hyphen.
            foreach (var ch in value)
            {
                if (!char.IsLetter(ch) && !char.IsWhiteSpace(ch) && ch != '\'' && ch != '-')
                    return Result.Failure<Name>(NameError.InvalidFormat);

            }

            return Result.Success(new Name(value));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Name);
        }

        public bool Equals(Name other)
        {
            if (other is null)
                return false;
            return Value.Equals(other.Value, StringComparison.Ordinal);
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