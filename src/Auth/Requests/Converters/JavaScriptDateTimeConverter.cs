using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Firebase.Auth.Requests.Converters
{
    internal class JavaScriptDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var t = long.Parse(reader.GetString()!);
            return DateTime.UnixEpoch.AddMilliseconds(t);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options) =>
            throw new NotImplementedException();
    }
}
