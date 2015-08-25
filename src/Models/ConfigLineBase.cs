using System;
using SharpDX;

namespace PoeHUD.Models
{
    public abstract class ConfigLineBase
    {
        public string Text { get; set; }
        public Color? Color { get; set; }
        public string SoundFile { get; set; }
        public string MinimapIcon { get; set; }

        public Func<Color> FastColor;
        public override bool Equals(object obj)
        {
            return Text == ((ConfigLineBase)obj).Text;
        }
        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }
    }
}