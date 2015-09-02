using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace PoeHUD.Hud.Settings.Converters
{
    public class FileNodeConverter : CustomCreationConverter<FileNode>
    {
        public override bool CanWrite
        {
            get { return true; }
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override FileNode Create(Type objectType)
        {
            return string.Empty;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return new FileNode(serializer.Deserialize<string>(reader));
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, (value as FileNode).Value);
        }
    }
}