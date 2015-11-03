using System;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PoeHUD.Hud.Settings.Converters
{
    public class ColorNodeConverter : CustomCreationConverter<ColorNode>
    {
        public override bool CanWrite
        {
            get { return true; }
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override ColorNode Create(Type objectType)
        {
            return new ColorNode();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            uint argb;
            return uint.TryParse(reader.Value.ToString(), NumberStyles.HexNumber, null, out argb)
                ? new ColorNode(argb)
                : Create(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var color = (ColorNode)value;
            serializer.Serialize(writer, string.Format("{0:x8}", color.Value.ToAbgr()));
        }
    }
}