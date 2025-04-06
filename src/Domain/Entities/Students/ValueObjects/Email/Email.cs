using System.Text.RegularExpressions;
using Domain.Entities.Students.ValueObjects.Email;
using SharedKernel;

namespace Domain.Entities.Students.ValueObjects.Email
{
    public sealed class Email : IEquatable<Email>
    {
        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        // Factory method with validation
        public static Result<Email> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Result.Failure<Email>(EmailError.EmptyEmail);
            
            value = value.Trim();

            // Basic email validation regex pattern (can be enhanced)
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase))
                return Result.Failure<Email>(EmailError.InvalidFormat);
            return new Email(value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Email);
        }

        public bool Equals(Email other)
        {
            if (other is null)
                return false;

            // Case-insensitive comparison
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