using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Students.ValueObjects.Phones;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure.Persistence.Mongodb
{
    public class PhoneSerializer : SerializerBase<PhoneNumber>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, PhoneNumber value)
        {
            // Write the underlying string value.
            context.Writer.WriteString(value.ToString());
        }

        public override PhoneNumber Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            // Read the string and convert it into a Name.
            var str = context.Reader.ReadString();
            var result = PhoneNumber.Create(str);
            if (result.IsSuccess)
            {
                return result.Value;
            }
            else
            {
                throw new FormatException($"Failed to deserialize phone: {result.Error}");
            }
        }
    }
}