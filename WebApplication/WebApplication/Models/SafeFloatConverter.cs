using System.Text.Json;
using System.Text.Json.Serialization;

namespace RazorPagesApp.Models
{
    public class SafeFloatConverter : JsonConverter<float>
    {
        public override float Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetSingle();
        }

        public override void Write(Utf8JsonWriter writer, float value, JsonSerializerOptions options)
        {
            if (float.IsNaN(value) || float.IsInfinity(value))
                writer.WriteNumberValue(0.0f); // или writer.WriteNullValue();
            else
                writer.WriteNumberValue(value);
        }
    }
}
