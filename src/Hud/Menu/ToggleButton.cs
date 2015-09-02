using PoeHUD.Framework.Helpers;
using PoeHUD.Hud.Settings;
using PoeHUD.Hud.UI;
using SharpDX;
using SharpDX.Direct3D9;
using System;
using System.Linq;

namespace PoeHUD.Hud.Menu
{
    public class ToggleButton : MenuItem
    {
        private readonly string key;

        public readonly string Name;

        private readonly ToggleNode node;

        private MenuItem parent;

        private Func<MenuItem, bool> hide;

        public ToggleButton(MenuItem parent, string name, ToggleNode node, string key, Func<MenuItem, bool> hide)
        {
            this.Name = name;
            this.node = node;
            this.key = key;
            this.parent = parent;
            this.hide = hide;
            if (hide != null)
            {
                node.OnValueChanged = Hide;
            }
        }

        private void Hide()
        {
            parent.Children.Where(hide).ForEach(y => y.SetVisible(!node.Value));
        }

        public override int DesiredWidth => 170;
        public override int DesiredHeight => 25;

        public override void Render(Graphics graphics, MenuSettings settings)
        {
            if (!IsVisible)
            {
                return;
            }
            Color color = node.Value ? settings.EnabledBoxColor : settings.DisabledBoxColor;
            var textPosition = new Vector2(Bounds.X - 45 + Bounds.Width / 3, Bounds.Y + Bounds.Height / 2);
            if (key != null) graphics.DrawText(string.Concat("[", key, "]"), 12, Bounds.TopRight.Translate(-45, 0), settings.MenuFontColor);
            graphics.DrawText(Name, settings.MenuFontSize, textPosition, settings.MenuFontColor, FontDrawFlags.VerticalCenter | FontDrawFlags.Left);
            graphics.DrawImage("menu-background.png", new RectangleF(Bounds.X, Bounds.Y, Bounds.Width, Bounds.Height), settings.BackgroundColor);
            graphics.DrawImage("menu-slider.png", new RectangleF(Bounds.X + 5, Bounds.Y + 3 * Bounds.Height / 4 + 2, Bounds.Width - 10, 4), color);

            if (Children.Count > 0)
            {
                float width = (Bounds.Width - 2) * 0.08f;
                float height = (Bounds.Height - 2) / 2;
                var imgRect = new RectangleF(Bounds.X + Bounds.Width - 1 - width, Bounds.Y + 1 + height - height / 2, width, height);
                graphics.DrawImage("menu-arrow.png", imgRect);
            }
            Children.ForEach(x => x.Render(graphics, settings));
        }

        protected override void HandleEvent(MouseEventID id, Vector2 pos)
        {
            if (id == MouseEventID.LeftButtonDown)
            {
                node.Value = !node.Value;
            }
        }

        public override void SetHovered(bool hover)
        {
            Action func = null;
            Children.ForEach(x =>
            {
                x.SetVisible(hover);
                var toggleButton = (x as ToggleButton);
                if (hover && toggleButton?.hide != null)
                {
                    func = toggleButton.Hide;
                }
            });
            func?.Invoke();
        }
    }
}