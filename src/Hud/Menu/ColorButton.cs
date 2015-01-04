﻿using System.Threading.Tasks;
using System.Windows.Forms;

using PoeHUD.Hud.Settings;
using PoeHUD.Hud.UI;

using SharpDX;
using SharpDX.Direct3D9;

using ColorGdi = System.Drawing.Color;

namespace PoeHUD.Hud.Menu
{
    public sealed class ColorButton : MenuItem
    {
        private readonly string name;

        private readonly ColorNode node;

        public ColorButton(string name, ColorNode node)
        {
            this.name = name;
            this.node = node;
        }

        public override int DesiredWidth
        {
            get { return 210; }
        }

        public override int DesiredHeight
        {
            get { return 25; }
        }

        public override void Render(Graphics graphics)
        {
            if (!IsVisible)
            {
                return;
            }

            graphics.DrawBox(Bounds, Color.Black);
            graphics.DrawBox(new RectangleF(Bounds.X + 1, Bounds.Y + 1, Bounds.Width - 2, Bounds.Height - 2), Color.Gray);

            float colorSize = DesiredHeight - 2; // TODO move to settings
            var textPosition = new Vector2(Bounds.X + Bounds.Width / 2 - colorSize / 2, Bounds.Y + Bounds.Height / 2);
            // TODO textSize to Settings
            graphics.DrawText(name, 18, textPosition, Color.White, FontDrawFlags.VerticalCenter | FontDrawFlags.Center);
            var colorBox = new RectangleF(Bounds.Right - colorSize - 1, Bounds.Top + 1, colorSize, colorSize);
            graphics.DrawBox(colorBox, node.Value);
            graphics.DrawBox(new RectangleF(colorBox.X, colorBox.Y, 1, colorSize), Color.Black);
        }

        protected override void HandleEvent(MouseEventID id, Vector2 pos)
        {
            if (id == MouseEventID.LeftButtonDown)
            {
                /*Task.Run(() =>
                {
                    var colorDialog = new ColorDialog();
                    colorDialog.Color = GetColorGdi(node);
                    colorDialog.SolidColorOnly = false;
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        node.Value = GetColor(colorDialog.Color);
                    }
                });*/
            }
        }

        private static Color GetColor(ColorGdi color)
        {
            return Color.FromRgba(color.R | (color.G << 8) | (color.B << 16) | (color.A << 24));
        }

        private static ColorGdi GetColorGdi(ColorNode node)
        {
            Color color = node.Value;
            return ColorGdi.FromArgb(color.A | (color.R << 8) | (color.G << 16) | (color.B << 24));
        }
    }
}