using Football.Core.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Football.Core.Converters
{
    public class PlayLogConverter : JsonConverter<PlayLog>
    {
        private readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            Converters =
            {
                new OffensePlayLogConverter(),
                new DefensePlayLogConverter(),
                new SpecialPlayLogConverter()
            }
        };

        public override PlayLog Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException();
            if (reader.TokenType == JsonTokenType.Null) return null;

            var playLog = new PlayLog();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject) return playLog;

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();
                    reader.Read();

                    switch (propertyName)
                    {
                        case "offensePlayLog":
                            playLog.OffensePlayLog = JsonSerializer.Deserialize<OffensePlayLog>(ref reader, jsonSerializerOptions);
                            break;
                        case "defensePlayLog":
                            playLog.DefensePlayLog = JsonSerializer.Deserialize<DefensePlayLog>(ref reader, jsonSerializerOptions);
                            break;
                        case "specialPlayLog":
                            playLog.SpecialPlayLog = JsonSerializer.Deserialize<SpecialPlayLog>(ref reader, jsonSerializerOptions);
                            break;
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, PlayLog value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
