using System.Text.Json.Serialization;
using System.Text.Json;

namespace FactorioRateCalculator
{
    internal class EnumTypeJsonConverter<T>(EnumTypeJsonConverter<T>.EnumTypeReadConverter enumTypeJsonConverter, Action<Utf8JsonWriter, T?, JsonSerializerOptions> enumTypeWriteConverter) : JsonConverter<T>() where T : struct, Enum
    {
        EnumTypeReadConverter enumTypeReadConverter { get; } = enumTypeJsonConverter;
        Action<Utf8JsonWriter, T?, JsonSerializerOptions> enumTypeWriteConverter { get; } = enumTypeWriteConverter;

        public EnumTypeJsonConverter(Func<string, string>? readModifier = null, Func<string, string>? writeModifier = null) : this((ref Utf8JsonReader reader, Type _, JsonSerializerOptions _) => Enum.Parse<T>(readModifier?.Invoke(reader.GetString()!) ?? reader.GetString()!, true), (Utf8JsonWriter writer, T? value, JsonSerializerOptions _) => {
            if (value != null)
            {
                writer.WriteStringValue(writeModifier?.Invoke(value.Value.ToString()) ?? value.ToString());
            }
        })
        {

        }

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => enumTypeReadConverter(ref reader, typeToConvert, options);

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            enumTypeWriteConverter(writer, value, options);
        }

        internal delegate T EnumTypeReadConverter(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options);
    }
}
