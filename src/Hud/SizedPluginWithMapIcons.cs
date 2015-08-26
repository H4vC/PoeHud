using PoeHUD.Controllers;
using PoeHUD.Framework;
using PoeHUD.Hud.Interfaces;
using PoeHUD.Hud.Settings;
using PoeHUD.Hud.UI;
using SharpDX;
using System;
using System.Windows.Forms;

namespace PoeHUD.Hud
{
    public abstract class SizedPluginWithMapIcons<TSettings> : PluginWithMapIcons<TSettings>, IPanelChild
        where TSettings : SettingsBase
    {
        private bool _holdKey;
        private readonly SettingsHub _settingsHub;

        protected SizedPluginWithMapIcons(GameController gameController, Graphics graphics, TSettings settings)
            : base(gameController, graphics, settings)
        { }

        public Size2F Size { get; protected set; }

        public Func<Vector2> StartDrawPointFunc { get; set; }

        public Vector2 Margin { get; private set; }

        public override void Render()
        {
            Size = new Size2F();
            Margin = new Vector2(0, 0);
        }

        public void HideAll()
        {
            if (!_holdKey && WinApi.IsKeyDown(Keys.F10))
            {
                _holdKey = true;
                Settings.Enable.Value = !Settings.Enable.Value;
                if (!Settings.Enable.Value)
                {
                    SettingsHub.Save(_settingsHub);
                }
            }
            else if (_holdKey && !WinApi.IsKeyDown(Keys.F10))
            {
                _holdKey = false;
            }
        }
    }
}