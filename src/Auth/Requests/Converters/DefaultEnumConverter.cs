using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Firebase.Auth.Requests.Converters
{
    /// <inheritdoc />
    /// <summary>
    /// デフォルト値（0）にフォールバックするenum用コンバーターファクトリ。
    /// </summary>
    public class DefaultEnumConverter : JsonConverterFactory
    {
        private readonly int defaultValue;

        public DefaultEnumConverter() { }

        public DefaultEnumConverter(int defaultValue)
        {
            this.defaultValue = defaultValue;
        }

        public override bool CanConvert(Type typeToConvert) =>
            typeToConvert.IsEnum && Enum.IsDefined(typeToConvert, defaultValue);

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options) =>
            (JsonConverter)Activator.CreateInstance(
                typeof(DefaultEnumConverter<>).MakeGenericType(typeToConvert),
                defaultValue)!;
    }

    internal class DefaultEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        private readonly T defaultValue;

        public DefaultEnumConverter(int defaultValue)
        {
            this.defaultValue = (T)Enum.ToObject(typeof(T), defaultValue);
        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                var str = reader.GetString();
                if (str != null && Enum.TryParse<T>(str, ignoreCase: true, out var result))
                    return result;
            }
            catch { }
            return defaultValue;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString());
    }
}
