using Football.Core.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Football.Services.GameService.Converters
{
    public class DefensePlayLogConverter : JsonConverter<DefensePlayLog>
    {
        public override DefensePlayLog Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException();
            if (reader.TokenType == JsonTokenType.Null) return null;

            DefensePlayLog defensePlayLog = new();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject) return defensePlayLog;

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();
                    reader.Read();

                    switch (propertyName)
                    {
                        case "sacks":
                            defensePlayLog.Sacks = reader.GetInt32();
                            break;
                        default:
                            break;
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, DefensePlayLog value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
