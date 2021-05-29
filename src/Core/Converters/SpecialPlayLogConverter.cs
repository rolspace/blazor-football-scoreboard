using Football.Core.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Football.Core.Converters
{
    public class SpecialPlayLogConverter : JsonConverter<SpecialPlayLog>
    {
        public override SpecialPlayLog Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException();
            if (reader.TokenType == JsonTokenType.Null) return null;

            var specialPlayLog = new SpecialPlayLog();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject) return specialPlayLog;

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();
                    reader.Read();

                    switch (propertyName)
                    {
                        case "punts":
                            specialPlayLog.Punts = reader.GetInt32();
                            break;
                        case "returnYards":
                            specialPlayLog.ReturnYards = reader.GetInt32();
                            break;
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, SpecialPlayLog value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
