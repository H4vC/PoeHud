using System;
using PoeHUD.Controllers;
using PoeHUD.Hud.Interfaces;
using PoeHUD.Hud.Settings;
using PoeHUD.Hud.UI;
using SharpDX;

namespace PoeHUD.Hud
{
    public abstract class SizedPluginWithMapIcons<TSettings> : PluginWithMapIcons<TSettings>, IPanelChild
        where TSettings : SettingsBase
    {
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
    }
}