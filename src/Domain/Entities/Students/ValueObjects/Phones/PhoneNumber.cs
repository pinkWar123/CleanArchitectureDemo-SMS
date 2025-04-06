using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.Entities.Students.ValueObjects.Phones
{
    public sealed class PhoneNumber : IEquatable<PhoneNumber>
    {
        public string Value { get; }

        private PhoneNumber(string value)
        {
            Value = value;
        }

        // Factory method with validation
        public static Result<PhoneNumber> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Phone number cannot be null or empty.", nameof(value));
            
            value = value.Trim();

            // Basic validation: allows optional '+' followed by 7 to 15 digits.
            string pattern = @"^\+?\d{7,15}$";
            if (!Regex.IsMatch(value, pattern))
                throw new ArgumentException("Invalid phone number format.", nameof(value));

            return new PhoneNumber(value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as PhoneNumber);
        }

        public bool Equals(PhoneNumber other)
        {
            if (other is null)
                return false;
            // Comparison is case-insensitive, though phone numbers typically don't vary by case.
            return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return Value.ToLowerInvariant().GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}