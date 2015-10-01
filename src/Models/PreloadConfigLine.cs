using System;
using SharpDX;

namespace PoeHUD.Models
{
    public class PreloadConfigLine : ConfigLineBase
    {
        public Func<Color> FastColor;
        public string SoundFile { get; set; }
    }
}