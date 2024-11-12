﻿using System.Text.Json.Serialization;
using System.Text.Json;

namespace Common.Helper.JsonConverter
{
    public class StringConverter : JsonConverter<string?>
    {

        public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? value = reader.GetString();

            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            else
            {
                return value;
            }
        }


        public override void Write(Utf8JsonWriter writer, string? value, JsonSerializerOptions options)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                value = null;
                writer.WriteStringValue(value);
            }
            else
            {
                writer.WriteStringValue(value.ToString());
            }
        }
    }
}
