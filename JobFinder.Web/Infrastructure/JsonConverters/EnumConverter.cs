namespace JobFinder.Web.Infrastructure.JsonConverters
{
    using Newtonsoft.Json;
    using System;

    public class EnumConverter<T> : JsonConverter<T> where T: struct
    {
        public override T ReadJson(JsonReader reader, Type objectType, T existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return Enum.Parse<T>(reader.Value.ToString(), ignoreCase: true);
        }

        public override void WriteJson(JsonWriter writer, T value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}
