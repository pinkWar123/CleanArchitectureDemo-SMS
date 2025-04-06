using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Domain.Common.ValueObjects.Names;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure.Persistence.Mongodb
{
    public class DefaultNameDecoder : INameDecoder
    {
        private string _name;

        // Called to "inform" the decoder of the field name.
        public void Inform(string name)
        {
            _name = name;
        }

        // Called to decode the name.
        // In this simple implementation, we simply return the previously informed name.
        public string Decode(BsonStream stream, UTF8Encoding encoding)
        {
            return _name;
        }
    }
    public class NameSerializer : SerializerBase<Name>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, Name value)
        {
            // Write the underlying string value.
            context.Writer.WriteStartDocument();
            context.Writer.WriteName("value");
            context.Writer.WriteString(value.Value);
            context.Writer.WriteEndDocument();
        }

        public override Name Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            context.Reader.ReadStartDocument();
            var fieldName = context.Reader.ReadName(new DefaultNameDecoder());
            if (fieldName != "value")
            {
                throw new FormatException($"Expected field 'value', but got '{fieldName}'");
            }
            var str = context.Reader.ReadString();
            context.Reader.ReadEndDocument();

            var result = Name.Create(str);
            if (result.IsSuccess)
            {
                Console.WriteLine("Value is:--------------------------" + result.Value.ToString());
                return result.Value;
            }
            else
                throw new FormatException($"Failed to deserialize Name: {result.Error}");
            }
    }
}