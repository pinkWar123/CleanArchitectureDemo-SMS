using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Students.ValueObjects;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure.Persistence.Mongodb
{
    public class StudentIdSerializer : SerializerBase<StudentId>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, StudentId value)
        {
            // Write the underlying string value.
            context.Writer.WriteString(value.ToString());
        }

        public override StudentId Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            // Read the string and convert it into a Name.
            var str = context.Reader.ReadString();
            var result = StudentId.Create(str);
            if (result.IsSuccess)
            {
                return result.Value;
            }
            else
            {
                throw new FormatException($"Failed to deserialize student id: {result.Error}");
            }
        }
    }
}