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
    public abstract class SizedPlugin<TSettings> : Plugin<TSettings>, IPanelChild where TSettings : SettingsBase
    {
        private bool holdKey;
        private readonly SettingsHub _settingsHub;

        protected SizedPlugin(GameController gameController, Graphics graphics, TSettings settings,
            SettingsHub settingsHub)
            : base(gameController, graphics, settings)
        {
            _settingsHub = settingsHub;
        }

        public Size2F Size { get; set; }

        public Func<Vector2> StartDrawPointFunc { get; set; }

        public Vector2 Margin { get; set; }

        public override void Render()
        {
            Size = new Size2F();
            Margin = new Vector2(0, 0);
        }

        public void HideAll()
        {
            if (!holdKey && WinApi.IsKeyDown(Keys.F10))
            {
                holdKey = true;
                Settings.Enable.Value = !Settings.Enable;
                if (!Settings.Enable.Value)
                {
                    SettingsHub.Save(_settingsHub);
                }
            }
            if (holdKey && !WinApi.IsKeyDown(Keys.F10))
            {
                holdKey = false;
            }
        }
    }
}