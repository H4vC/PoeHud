using PoeHUD.Hud.Settings;

using SharpDX;

namespace PoeHUD.Hud.InventoryPreview
{
    public sealed class InventoryPreviewSettings : SettingsBase
    {
        public InventoryPreviewSettings()
        {
            Enable = false;
            AutoUpdate = true;
            CellUsedColor = new Color(128, 128, 128, 180);
            CellFreeColor = new Color(255, 255, 255, 255);
            CellSize = new RangeNode<int>(20, 1, 100);
            CellPadding = new RangeNode<int>(0, 0, 10);
            PositionX = new RangeNode<float>(27.0f, 0.0f, 100.0f);
            PositionY = new RangeNode<float>(88.0f, 0.0f, 100.0f);
        }

        public ToggleNode AutoUpdate { get; set; }

        public ColorNode CellUsedColor { get; set; }

        public ColorNode CellFreeColor { get; set; }

        public RangeNode<int> CellSize { get; set; }

        public RangeNode<int> CellPadding { get; set; }

        public RangeNode<float> PositionX { get; set; }

        public RangeNode<float> PositionY { get; set; }
    }
}