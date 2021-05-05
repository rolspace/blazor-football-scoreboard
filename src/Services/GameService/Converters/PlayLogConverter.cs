using Football.Core.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Football.Services.GameService.Converters
{
    public class PlayLogConverter : JsonConverter<PlayLog>
    {
        private readonly JsonSerializerOptions serializerOptions = new JsonSerializerOptions
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

            PlayLog playLog = new();

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
                            playLog.OffensePlayLog = JsonSerializer.Deserialize<OffensePlayLog>(ref reader, serializerOptions);
                            break;
                        case "defensePlayLog":
                            playLog.DefensePlayLog = JsonSerializer.Deserialize<DefensePlayLog>(ref reader, serializerOptions);
                            break;
                        case "specialPlayLog":
                            playLog.SpecialPlayLog = JsonSerializer.Deserialize<SpecialPlayLog>(ref reader, serializerOptions);
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
