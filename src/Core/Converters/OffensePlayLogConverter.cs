using Football.Core.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Football.Core.Converters
{
    public class OffensePlayLogConverter : JsonConverter<OffensePlayLog>
    {
        public override OffensePlayLog Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException();
            if (reader.TokenType == JsonTokenType.Null) return null;

            var offensePlayLog = new OffensePlayLog();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject) return offensePlayLog;

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();
                    reader.Read();

                    switch (propertyName)
                    {
                        case "airYards":
                            offensePlayLog.AirYards = reader.GetInt32();
                            break;
                        default:
                            break;
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, OffensePlayLog value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
