using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Students.ValueObjects.Address;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure.Persistence.Mongodb
{
    public class AddressSerializer : SerializerBase<Address>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Address value)
        {
            // Write the underlying string value.
            context.Writer.WriteString(value.ToString());
        }

        public override Address Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            // Read the string and convert it into a Name.
            var str = context.Reader.ReadString();
            var result = Address.Create(str);
            if (result.IsSuccess)
            {
                return result.Value;
            }
            else
            {
                throw new FormatException($"Failed to deserialize address: {result.Error}");
            }
        }
    }
}