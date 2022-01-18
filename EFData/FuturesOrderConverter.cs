using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Ixcent.CryptoTerminal.Domain.Converters
{
    using CryptoExchanges.Data;
    using System;
    using System.Diagnostics.CodeAnalysis;

    public class FuturesOrderConverter : JsonConverter<FuturesOrder>
    {
        public override FuturesOrder ReadJson(JsonReader reader, Type objectType, [AllowNull] FuturesOrder existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize<FuturesOrder>(reader);
        }

        public override void WriteJson(JsonWriter writer, [AllowNull] FuturesOrder value, JsonSerializer serializer)
        {
            JToken token = JToken.FromObject(value);
            JObject obj = (JObject)token;
            obj.WriteTo(writer);
        }
    }
}