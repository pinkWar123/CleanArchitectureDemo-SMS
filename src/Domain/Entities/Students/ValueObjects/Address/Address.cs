using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SharedKernel;

namespace Domain.Entities.Students.ValueObjects.Address
{
    public sealed class Address : IEquatable<Address>
    {
        public string Street { get; }
        public string City { get; }
        public string StateOrProvince { get; }
        public string Country { get; }

        private Address(string street, string city, string stateOrProvince, string country)
        {
            Street = street;
            City = city;
            StateOrProvince = stateOrProvince;
            Country = country;
        }

        // Factory method with validation
        public static Result<Address> Create(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return Result.Failure<Address>(AddressError.EmptyAddress);

            var parts = address.Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 4)
                return Result.Failure<Address>(AddressError.InvalidFormat);

            return Result.Success(new Address(parts[0], parts[1], parts[2], parts[3]));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Address);
        }

        public bool Equals(Address other)
        {
            if (other is null)
                return false;

            return Street.Equals(other.Street, StringComparison.OrdinalIgnoreCase) &&
                City.Equals(other.City, StringComparison.OrdinalIgnoreCase) &&
                StateOrProvince.Equals(other.StateOrProvince, StringComparison.OrdinalIgnoreCase) &&
                Country.Equals(other.Country, StringComparison.OrdinalIgnoreCase);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(
                Street.ToLowerInvariant(), 
                City.ToLowerInvariant(), 
                StateOrProvince.ToLowerInvariant(), 
                Country.ToLowerInvariant()
            );
        }

        public override string ToString()
        {
            return $"{Street}, {City}, {StateOrProvince}, {Country}";
        }
    }
}