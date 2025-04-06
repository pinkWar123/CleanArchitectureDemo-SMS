using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Students.ValueObjects.Email;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure.Persistence.Mongodb
{
    public class EmailSerializer : SerializerBase<Email>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Email value)
        {
            // Write the underlying string value.
            context.Writer.WriteString(value.ToString());
        }

        public override Email Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            // Read the string and convert it into a Name.
            var str = context.Reader.ReadString();
            var result = Email.Create(str);
            if (result.IsSuccess)
            {
                return result.Value;
            }
            else
            {
                throw new FormatException($"Failed to deserialize email: {result.Error}");
            }
        }
    }
}