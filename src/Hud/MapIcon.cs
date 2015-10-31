using System;
using PoeHUD.Models;
using PoeHUD.Poe.Components;
using SharpDX;

namespace PoeHUD.Hud
{
    public class CreatureMapIcon : MapIcon
    {
        public CreatureMapIcon(EntityWrapper entityWrapper, string hudTexture, Func<bool> show, int iconSize)
            : base(entityWrapper, new HudTexture(hudTexture), show, iconSize) {}

        public override bool IsVisible()
        {
            return base.IsVisible() && EntityWrapper.IsAlive;
        }
    }

    public class ChestMapIcon : MapIcon
    {
        public ChestMapIcon(EntityWrapper entityWrapper, HudTexture hudTexture, Func<bool> show, int iconSize)
            : base(entityWrapper, hudTexture, show, iconSize) {}

        public override bool IsEntityStillValid()
        {
            return EntityWrapper.IsValid && !EntityWrapper.GetComponent<Chest>().IsOpened;
        }
    }

    public class MapIcon
    {
        private readonly Func<bool> show;

        public MapIcon(EntityWrapper entityWrapper, HudTexture hudTexture, Func<bool> show, int iconSize = 10)
        {
            EntityWrapper = entityWrapper;
            TextureIcon = hudTexture;
            this.show = show;
            Size = iconSize;
        }

        public int? SizeOfLargeIcon { get; set; }
        public EntityWrapper EntityWrapper { get; }
        public HudTexture TextureIcon { get; private set; }
        public HudTexture LargeMapIcon { get; set; }
        public int Size { get; private set; }
        public Vector2 WorldPosition => EntityWrapper.GetComponent<Positioned>().GridPos;

        public static Vector2 DeltaInWorldToMinimapDelta(Vector2 delta, double diag, float scale, float deltaZ = 0)
        {
            const float CAMERA_ANGLE = 38 * MathUtil.Pi / 180;
            // Values according to 40 degree rotation of cartesian coordiantes, still doesn't seem right but closer
            var cos = (float)(diag * Math.Cos(CAMERA_ANGLE) / scale);
            var sin = (float)(diag * Math.Sin(CAMERA_ANGLE) / scale); // possible to use cos so angle = nearly 45 degrees
            // 2D rotation formulas not correct, but it's what appears to work?
            return new Vector2((delta.X - delta.Y) * cos, deltaZ - (delta.X + delta.Y) * sin);
        }

        public virtual bool IsEntityStillValid()
        {
            return EntityWrapper.IsValid;
        }

        public virtual bool IsVisible()
        {
            return show();
        }
    }
}