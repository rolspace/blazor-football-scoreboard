using Football.Core.Interfaces.Models;
using Football.Core.Models;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Football.Workers.GameWorker.Converters
{
    public class GameConverter : JsonConverter<IGame>
    {
        public override IGame Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject) throw new JsonException();
            if (reader.TokenType == JsonTokenType.Null) return null;

            Game game = new();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject) return game;

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();
                    reader.Read();

                    switch (propertyName)
                    {
                        case "id":
                            game.Id = reader.GetInt32();
                            break;
                        case "":
                            game.Week = reader.GetInt32();
                            break;
                        case "homeTeam":
                            game.HomeTeam = reader.GetString();
                            break;
                        case "awayTeam":
                            game.HomeTeam = reader.GetString();
                            break;
                    }
                }
            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, IGame value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
