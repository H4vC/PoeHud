using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PoeHUD.Hud.Settings.Converters
{
    public class ToggleNodeConverter : CustomCreationConverter<ToggleNode>
    {
        public override bool CanWrite
        {
            get { return true; }
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override ToggleNode Create(Type objectType)
        {
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return new ToggleNode(serializer.Deserialize<bool>(reader));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, (value as ToggleNode).Value);
        }
    }
}