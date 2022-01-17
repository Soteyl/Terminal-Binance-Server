using Newtonsoft.Json;
using System.Linq.Expressions;

namespace Ixcent.CryptoTerminal.Domain.Converters
{
    using Interfaces;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Diagnostics.CodeAnalysis;

    public class AdvancedEnumConverter : JsonConverter<IAdvancedEnum>
    {
        // TODO avoid possible null reference
        public override IAdvancedEnum ReadJson(JsonReader reader, Type objectType, [AllowNull] IAdvancedEnum existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject items = JObject.Load(reader);
            string value = items["Name"].Value<string>();

            existingValue = (IAdvancedEnum)objectType.GetProperty(value).GetValue(this, null);
            return existingValue;
        }

        public override void WriteJson(JsonWriter writer, [AllowNull] IAdvancedEnum value, JsonSerializer serializer)
        {
            JToken token = JToken.FromObject(value);
            JObject obj = (JObject)token;

            obj.WriteTo(writer);
        }
    }
}
