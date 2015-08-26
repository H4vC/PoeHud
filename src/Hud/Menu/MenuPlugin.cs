using PoeHUD.Controllers;
using PoeHUD.Framework;
using PoeHUD.Framework.InputHooks;
using PoeHUD.Hud.AdvancedTooltip;
using PoeHUD.Hud.Health;
using PoeHUD.Hud.Loot;
using PoeHUD.Hud.Settings;
using PoeHUD.Hud.UI;
using SharpDX;
using System;
using System.Windows.Forms;

namespace PoeHUD.Hud.Menu
{
    public class MenuPlugin : Plugin<MenuSettings>
    {
        private readonly SettingsHub settingsHub;

        private readonly Action<MouseInfo> onMouseDown, onMouseUp, onMouseMove;

        private bool holdKey;

        private RootButton root;

        public MenuPlugin(GameController gameController, Graphics graphics, SettingsHub settingsHub)
            : base(gameController, graphics, settingsHub.MenuSettings)
        {
            this.settingsHub = settingsHub;
            CreateMenu();
            MouseHook.MouseDown += onMouseDown = info => info.Handled = OnMouseEvent(MouseEventID.LeftButtonDown, info.Position);
            MouseHook.MouseUp += onMouseUp = info => info.Handled = OnMouseEvent(MouseEventID.LeftButtonUp, info.Position);
            MouseHook.MouseMove += onMouseMove = info => info.Handled = OnMouseEvent(MouseEventID.MouseMove, info.Position);
        }

        public override void Dispose()
        {
            MouseHook.MouseDown -= onMouseDown;
            MouseHook.MouseUp -= onMouseUp;
            MouseHook.MouseMove -= onMouseMove;
        }

        public override void Render()
        {
            if (!holdKey && WinApi.IsKeyDown(Keys.F12))
            {
                holdKey = true;
                Settings.Enable.Value = !Settings.Enable.Value;
                if (!Settings.Enable.Value)
                {
                    SettingsHub.Save(settingsHub);
                }
            }
            else if (holdKey && !WinApi.IsKeyDown(Keys.F12))
            {
                holdKey = false;
            }

            if (Settings.Enable)
            {
                root.Render(Graphics, Settings);
            }
        }

        private static MenuItem AddChild(MenuItem parent, string text, ToggleNode node, string key = null)
        {
            var item = new ToggleButton(text, node, key);
            parent.AddChild(item);
            return item;
        }

        private static void AddChild(MenuItem parent, string text, ColorNode node)
        {
            var item = new ColorButton(text, node);
            parent.AddChild(item);
        }

        private static void AddChild<T>(MenuItem parent, string text, RangeNode<T> node) where T : struct
        {
            var item = new Picker<T>(text, node);
            parent.AddChild(item);
        }

        private void CreateMenu()
        {
            root = new RootButton(new Vector2(Settings.X, Settings.Y));

            // Health bars
            HealthBarSettings healthBarPlugin = settingsHub.HealthBarSettings;
            MenuItem healthMenu = AddChild(root, "Health bars", healthBarPlugin.Enable);
            MenuItem playersMenu = AddChild(healthMenu, "Players", healthBarPlugin.Players.Enable);
            MenuItem enemiesMenu = AddChild(healthMenu, "Enemies", healthBarPlugin.ShowEnemies);
            MenuItem minionsMenu = AddChild(healthMenu, "Minions", healthBarPlugin.Minions.Enable);
            AddChild(healthMenu, "Show ES", healthBarPlugin.ShowES);
            AddChild(healthMenu, "Show in town", healthBarPlugin.ShowInTown);
            MenuItem debuffPanelMenu = AddChild(healthMenu, "Debuff panel", healthBarPlugin.ShowDebuffPanel);
            AddChild(debuffPanelMenu, "Icon size", healthBarPlugin.DebuffPanelIconSize);
            AddChild(playersMenu, "Print percents", healthBarPlugin.Players.ShowPercents);
            AddChild(playersMenu, "Print health text", healthBarPlugin.Players.ShowHealthText);
            AddChild(playersMenu, "Width", healthBarPlugin.Players.Width);
            AddChild(playersMenu, "Height", healthBarPlugin.Players.Height);
            AddChild(minionsMenu, "Print percents", healthBarPlugin.Minions.ShowPercents);
            AddChild(minionsMenu, "Print health text", healthBarPlugin.Minions.ShowHealthText);
            AddChild(minionsMenu, "Width", healthBarPlugin.Minions.Width);
            AddChild(minionsMenu, "Height", healthBarPlugin.Minions.Height);
            MenuItem whiteEnemyMenu = AddChild(enemiesMenu, "White", healthBarPlugin.NormalEnemy.Enable);
            AddChild(whiteEnemyMenu, "Print percents", healthBarPlugin.NormalEnemy.ShowPercents);
            AddChild(whiteEnemyMenu, "Print health text", healthBarPlugin.NormalEnemy.ShowHealthText);
            AddChild(whiteEnemyMenu, "Width", healthBarPlugin.NormalEnemy.Width);
            AddChild(whiteEnemyMenu, "Height", healthBarPlugin.NormalEnemy.Height);
            MenuItem magicEnemyMenu = AddChild(enemiesMenu, "Magic", healthBarPlugin.MagicEnemy.Enable);
            AddChild(magicEnemyMenu, "Print percents", healthBarPlugin.MagicEnemy.ShowPercents);
            AddChild(magicEnemyMenu, "Print health text", healthBarPlugin.MagicEnemy.ShowHealthText);
            AddChild(magicEnemyMenu, "Width", healthBarPlugin.MagicEnemy.Width);
            AddChild(magicEnemyMenu, "Height", healthBarPlugin.MagicEnemy.Height);
            MenuItem rareEnemyMenu = AddChild(enemiesMenu, "Rare", healthBarPlugin.RareEnemy.Enable);
            AddChild(rareEnemyMenu, "Print percents", healthBarPlugin.RareEnemy.ShowPercents);
            AddChild(rareEnemyMenu, "Print health text", healthBarPlugin.RareEnemy.ShowHealthText);
            AddChild(rareEnemyMenu, "Width", healthBarPlugin.RareEnemy.Width);
            AddChild(rareEnemyMenu, "Height", healthBarPlugin.RareEnemy.Height);
            MenuItem uniquesEnemyMenu = AddChild(enemiesMenu, "Uniques", healthBarPlugin.UniqueEnemy.Enable);
            AddChild(uniquesEnemyMenu, "Print percents", healthBarPlugin.UniqueEnemy.ShowPercents);
            AddChild(uniquesEnemyMenu, "Print health text", healthBarPlugin.UniqueEnemy.ShowHealthText);
            AddChild(uniquesEnemyMenu, "Width", healthBarPlugin.UniqueEnemy.Width);
            AddChild(uniquesEnemyMenu, "Height", healthBarPlugin.UniqueEnemy.Height);

            // Map icons
            MenuItem mapIconsMenu = AddChild(root, "Map icons", settingsHub.MapIconsSettings.Enable);
            AddChild(mapIconsMenu, "Minimap icons", settingsHub.MapIconsSettings.IconsOnMinimap);
            AddChild(mapIconsMenu, "Large map icons", settingsHub.MapIconsSettings.IconsOnLargeMap);
            AddChild(mapIconsMenu, "Drop items", settingsHub.ItemAlertSettings.ShowItemOnMap);
            AddChild(mapIconsMenu, "Monsters", settingsHub.MonsterTrackerSettings.Monsters);
            AddChild(mapIconsMenu, "Minions", settingsHub.MonsterTrackerSettings.Minions);
            AddChild(mapIconsMenu, "Strongboxes", settingsHub.PoiTrackerSettings.Strongboxes);
            AddChild(mapIconsMenu, "Chests", settingsHub.PoiTrackerSettings.Chests);
            AddChild(mapIconsMenu, "Masters", settingsHub.PoiTrackerSettings.Masters);

            // Item Alert
            var itemAlertMenu = AddChild(root, "Item alert", settingsHub.ItemAlertSettings.Enable);
            AddChild(itemAlertMenu, "Uniques", settingsHub.ItemAlertSettings.Uniques);
            AddChild(itemAlertMenu, "Rares", settingsHub.ItemAlertSettings.Rares);

            var exaltedColor = AddChild(itemAlertMenu, "Exalted", settingsHub.ItemAlertSettings.Exalted);
            AddChild(exaltedColor, "font", settingsHub.ItemAlertSettings.ExaltedColor);
            AddChild(exaltedColor, "border", settingsHub.ItemAlertSettings.FrameExaltedColor);

            var chaosColor = AddChild(itemAlertMenu, "Chaos", settingsHub.ItemAlertSettings.Chaos);
            AddChild(chaosColor, "font", settingsHub.ItemAlertSettings.ChaosColor);
            AddChild(chaosColor, "border", settingsHub.ItemAlertSettings.FrameChaosColor);

            var jewelsColor = AddChild(itemAlertMenu, "Jewels", settingsHub.ItemAlertSettings.Jewels);
            AddChild(jewelsColor, "font", settingsHub.ItemAlertSettings.JewelsColor);
            AddChild(jewelsColor, "border", settingsHub.ItemAlertSettings.FrameJewelsColor);

            var cardsColor = AddChild(itemAlertMenu, "Cards", settingsHub.ItemAlertSettings.Cards);
            AddChild(cardsColor, "font", settingsHub.ItemAlertSettings.CardsColor);
            AddChild(cardsColor, "border", settingsHub.ItemAlertSettings.FrameCardsColor);

            var currencyColor = AddChild(itemAlertMenu, "Currency", settingsHub.ItemAlertSettings.Currency);
            AddChild(currencyColor, "font", settingsHub.ItemAlertSettings.CurrencyColor);
            AddChild(currencyColor, "border", settingsHub.ItemAlertSettings.FrameCurrencyColor);

            var mapsColor = AddChild(itemAlertMenu, "Maps", settingsHub.ItemAlertSettings.Maps);
            AddChild(mapsColor, "font", settingsHub.ItemAlertSettings.MapsColor);
            AddChild(mapsColor, "border", settingsHub.ItemAlertSettings.FrameMapsColor);

            var rgbColor = AddChild(itemAlertMenu, "Rgb", settingsHub.ItemAlertSettings.Rgb);
            AddChild(rgbColor, "font", settingsHub.ItemAlertSettings.RGBColor);
            AddChild(rgbColor, "border", settingsHub.ItemAlertSettings.FrameRGBColor);

            var craftingColor = AddChild(itemAlertMenu, "Crafting", settingsHub.ItemAlertSettings.Crafting);
            AddChild(craftingColor, "font", settingsHub.ItemAlertSettings.CraftingColor);
            AddChild(craftingColor, "border", settingsHub.ItemAlertSettings.FrameCraftingColor);

            var socketsColor = AddChild(itemAlertMenu, "Sockets", settingsHub.ItemAlertSettings.Sockets);
            AddChild(socketsColor, "font", settingsHub.ItemAlertSettings.SocketsColor);
            AddChild(socketsColor, "border", settingsHub.ItemAlertSettings.SocketsColor);

            var linkedColor = AddChild(itemAlertMenu, "Linked", settingsHub.ItemAlertSettings.Linked);
            AddChild(linkedColor, "font", settingsHub.ItemAlertSettings.LinkedColor);
            AddChild(linkedColor, "border", settingsHub.ItemAlertSettings.FrameLinkedColor);

            var qualityItemsSettings = settingsHub.ItemAlertSettings.QualityItems;
            var qualityMenu = AddChild(itemAlertMenu, "Quality items", qualityItemsSettings.Enable);
            var qualityWeaponMenu = AddChild(qualityMenu, "Weapons", qualityItemsSettings.Weapon.Enable);
            AddChild(qualityWeaponMenu, "Min. quality", qualityItemsSettings.Weapon.MinQuality);
            var qualityArmourMenu = AddChild(qualityMenu, "Armours", qualityItemsSettings.Armour.Enable);
            AddChild(qualityArmourMenu, "Min. quality", qualityItemsSettings.Armour.MinQuality);
            var qualityFlaskMenu = AddChild(qualityMenu, "Flasks", qualityItemsSettings.Flask.Enable);
            AddChild(qualityFlaskMenu, "Min. quality", qualityItemsSettings.Flask.MinQuality);
            var qualitySkillGemMenu = AddChild(qualityMenu, "Skill gems", qualityItemsSettings.SkillGem.Enable);
            AddChild(qualitySkillGemMenu, "Min. quality", qualityItemsSettings.SkillGem.MinQuality);
            AddChild(itemAlertMenu, "Play sound", settingsHub.ItemAlertSettings.PlaySound);
            var alertTextMenu = AddChild(itemAlertMenu, "Show text", settingsHub.ItemAlertSettings.ShowText);
            AddChild(itemAlertMenu, "Hide Others", settingsHub.ItemAlertSettings.HideOthers);
            AddChild(alertTextMenu, "Font size", settingsHub.ItemAlertSettings.TextSize);
            var borderSettings = settingsHub.ItemAlertSettings.BorderSettings;
            var showBorderMenu = AddChild(itemAlertMenu, "Show border", borderSettings.Enable);
            AddChild(showBorderMenu, "Border width", borderSettings.BorderWidth);
            AddChild(showBorderMenu, "Border color", borderSettings.BorderColor);
            AddChild(showBorderMenu, "Can't pick up", borderSettings.CantPickUpBorderColor);
            AddChild(showBorderMenu, "Not my item", borderSettings.NotMyItemBorderColor);
            AddChild(showBorderMenu, "Show timer", borderSettings.ShowTimer);
            AddChild(showBorderMenu, "Timer text size", borderSettings.TimerTextSize);
            AddChild(itemAlertMenu, "Background color", settingsHub.ItemAlertSettings.BackgroundColor);

            // Advanced tooltip
            AdvancedTooltipSettings tooltipSettings = settingsHub.AdvancedTooltipSettings;
            MenuItem tooltipMenu = AddChild(root, "Tooltips", tooltipSettings.Enable);
            MenuItem itemLevelMenu = AddChild(tooltipMenu, "Item level", tooltipSettings.ItemLevel.Enable);
            AddChild(itemLevelMenu, "Font size", tooltipSettings.ItemLevel.TextSize);
            MenuItem itemModsMenu = AddChild(tooltipMenu, "Item mods", tooltipSettings.ItemMods.Enable, "F9");
            AddChild(itemModsMenu, "Mods size", tooltipSettings.ItemMods.ModTextSize);
            MenuItem weaponDpsMenu = AddChild(tooltipMenu, "Weapon Dps", tooltipSettings.WeaponDps.Enable);
            AddChild(weaponDpsMenu, "Dps size", tooltipSettings.WeaponDps.DpsTextSize);
            AddChild(weaponDpsMenu, "Dps name size", tooltipSettings.WeaponDps.DpsNameTextSize);

            // Boss warnings
            MenuItem bossWarningsMenu = AddChild(root, "Boss warnings", settingsHub.MonsterTrackerSettings.Enable);
            AddChild(bossWarningsMenu, "Sound warning", settingsHub.MonsterTrackerSettings.PlaySound);
            MenuItem warningTextMenu = AddChild(bossWarningsMenu, "Text warning", settingsHub.MonsterTrackerSettings.ShowText);
            AddChild(warningTextMenu, "Font size", settingsHub.MonsterTrackerSettings.TextSize);
            AddChild(warningTextMenu, "Font color", settingsHub.MonsterTrackerSettings.DefaultTextColor);
            AddChild(warningTextMenu, "Background color", settingsHub.MonsterTrackerSettings.BackgroundColor);
            AddChild(warningTextMenu, "Position X", settingsHub.MonsterTrackerSettings.TextPositionX);
            AddChild(warningTextMenu, "Position Y", settingsHub.MonsterTrackerSettings.TextPositionY);

            // Xph Display
            MenuItem xpRateMenu = AddChild(root, "Experience", settingsHub.XpRateSettings.Enable, "F10");
            AddChild(xpRateMenu, "Show only area name", settingsHub.XpRateSettings.onlyArea);
            AddChild(xpRateMenu, "Font size", settingsHub.XpRateSettings.FontSize);
            AddChild(xpRateMenu, "Fps font color", settingsHub.XpRateSettings.FpsFontColor);
            AddChild(xpRateMenu, "Xph font color", settingsHub.XpRateSettings.XphFontColor);
            AddChild(xpRateMenu, "Area font color", settingsHub.XpRateSettings.AreaFontColor);
            AddChild(xpRateMenu, "Time left color", settingsHub.XpRateSettings.TimeLeftColor);
            AddChild(xpRateMenu, "Timer font color", settingsHub.XpRateSettings.TimerFontColor);
            AddChild(xpRateMenu, "Background color", settingsHub.XpRateSettings.BackgroundColor);
            

            // Preload Alert
            var preloadMenu = AddChild(root, "Preload Alert", settingsHub.PreloadAlertSettings.Enable);
            var masters = AddChild(preloadMenu, "Masters", settingsHub.PreloadAlertSettings.EnableMasters);
            AddChild(masters, "Zana", settingsHub.PreloadAlertSettings.MasterZana);
            AddChild(masters, "Tora", settingsHub.PreloadAlertSettings.MasterTora);
            AddChild(masters, "Haku", settingsHub.PreloadAlertSettings.MasterHaku);
            AddChild(masters, "Vorici", settingsHub.PreloadAlertSettings.MasterVorici);
            AddChild(masters, "Elreon", settingsHub.PreloadAlertSettings.MasterElreon);
            AddChild(masters, "Vagan", settingsHub.PreloadAlertSettings.MasterVagan);
            AddChild(masters, "Catarina", settingsHub.PreloadAlertSettings.MasterCatarina);
            AddChild(masters, "Krillson", settingsHub.PreloadAlertSettings.MasterKrillson);

            var exiles = AddChild(preloadMenu, "Exiles", settingsHub.PreloadAlertSettings.EnableExiles);
            AddChild(exiles, "Orra Greengate", settingsHub.PreloadAlertSettings.OrraGreengate);
            AddChild(exiles, "Thena Moga", settingsHub.PreloadAlertSettings.ThenaMoga);
            AddChild(exiles, "Antalie Napora", settingsHub.PreloadAlertSettings.AntalieNapora);
            AddChild(exiles, "Torr Olgosso", settingsHub.PreloadAlertSettings.TorrOlgosso);
            AddChild(exiles, "Armios Bell", settingsHub.PreloadAlertSettings.ArmiosBell);
            AddChild(exiles, "Zacharie Desmarais", settingsHub.PreloadAlertSettings.ZacharieDesmarais);
            AddChild(exiles, "Minara Anenima", settingsHub.PreloadAlertSettings.MinaraAnenima);
            AddChild(exiles, "Igna Phoenix", settingsHub.PreloadAlertSettings.IgnaPhoenix);
            AddChild(exiles, "Jonah Unchained", settingsHub.PreloadAlertSettings.JonahUnchained);
            AddChild(exiles, "Damoi Tui", settingsHub.PreloadAlertSettings.DamoiTui);
            AddChild(exiles, "Xandro Blooddrinker", settingsHub.PreloadAlertSettings.XandroBlooddrinker);
            AddChild(exiles, "Vickas Giantbone", settingsHub.PreloadAlertSettings.VickasGiantbone);
            AddChild(exiles, "Eoin Greyfur", settingsHub.PreloadAlertSettings.EoinGreyfur);
            AddChild(exiles, "Tinevin Highdove", settingsHub.PreloadAlertSettings.TinevinHighdove);
            AddChild(exiles, "Magnus Stonethorn", settingsHub.PreloadAlertSettings.MagnusStonethorn);
            AddChild(exiles, "Ion Darkshroud", settingsHub.PreloadAlertSettings.IonDarkshroud);
            AddChild(exiles, "Ash Lessard", settingsHub.PreloadAlertSettings.AshLessard);
            AddChild(exiles, "Wilorin Demontamer", settingsHub.PreloadAlertSettings.WilorinDemontamer);
            AddChild(exiles, "Augustina Solaria", settingsHub.PreloadAlertSettings.AugustinaSolaria);

            var strongboxes = AddChild(preloadMenu, "Strongboxes", settingsHub.PreloadAlertSettings.EnableStrongboxes);
            AddChild(strongboxes, "Arcanist", settingsHub.PreloadAlertSettings.ArcanistStrongbox);
            AddChild(strongboxes, "Artisan", settingsHub.PreloadAlertSettings.ArtisanStrongbox);
            AddChild(strongboxes, "Cartograph", settingsHub.PreloadAlertSettings.CartographerStrongbox);
            AddChild(strongboxes, "Gemcutter", settingsHub.PreloadAlertSettings.GemcutterStrongbox);
            AddChild(strongboxes, "Jeweller", settingsHub.PreloadAlertSettings.JewellerStrongbox);
            AddChild(strongboxes, "Blacksmith", settingsHub.PreloadAlertSettings.BlacksmithStrongbox);
            AddChild(strongboxes, "Armourer", settingsHub.PreloadAlertSettings.ArmourerStrongbox);
            AddChild(strongboxes, "Ornate", settingsHub.PreloadAlertSettings.OrnateStrongbox);
            AddChild(strongboxes, "Large", settingsHub.PreloadAlertSettings.LargeStrongbox);
            AddChild(strongboxes, "Perandus", settingsHub.PreloadAlertSettings.PerandusStrongbox);
            AddChild(strongboxes, "Kaom", settingsHub.PreloadAlertSettings.KaomStrongbox);
            AddChild(strongboxes, "Malachai", settingsHub.PreloadAlertSettings.MalachaiStrongbox);

            AddChild(preloadMenu, "Corrupted color", settingsHub.PreloadAlertSettings.CorruptedColor);
            AddChild(preloadMenu, "Background color", settingsHub.PreloadAlertSettings.BackgroundColor);
            AddChild(preloadMenu, "Font color", settingsHub.PreloadAlertSettings.FastColor);
            AddChild(preloadMenu, "Font size", settingsHub.PreloadAlertSettings.FontSize);

            // Show DPS
            MenuItem showDpsMenu = AddChild(root, "Show Dps", settingsHub.DpsMeterSettings.Enable);
            AddChild(showDpsMenu, "Dps font size", settingsHub.DpsMeterSettings.DpsTextSize);
            AddChild(showDpsMenu, "Peak dps font size", settingsHub.DpsMeterSettings.PeakDpsTextSize);
            AddChild(showDpsMenu, "Background color", settingsHub.DpsMeterSettings.BackgroundColor);
            AddChild(showDpsMenu, "Dps font color", settingsHub.DpsMeterSettings.DpsFontColor);
            AddChild(showDpsMenu, "Dps peak color", settingsHub.DpsMeterSettings.PeakFontColor);

            // Show monster kills
            MenuItem showMonsterKillsMenu = AddChild(root, "Monster kills", settingsHub.KillsCounterSettings.Enable);
            AddChild(showMonsterKillsMenu, "Show details", settingsHub.KillsCounterSettings.ShowDetail);
            AddChild(showMonsterKillsMenu, "Font color", settingsHub.KillsCounterSettings.FontColor);
            AddChild(showMonsterKillsMenu, "Background color", settingsHub.KillsCounterSettings.BackgroundColor);
            AddChild(showMonsterKillsMenu, "Label font size", settingsHub.KillsCounterSettings.LabelFontSize);
            AddChild(showMonsterKillsMenu, "Kills font size", settingsHub.KillsCounterSettings.KillsFontSize);

            // Show inventory preview
            MenuItem showInventoryPreviewMenu = AddChild(root, "Inventory preview", settingsHub.InventoryPreviewSettings.Enable);
            AddChild(showInventoryPreviewMenu, "Auto update", settingsHub.InventoryPreviewSettings.AutoUpdate);
            AddChild(showInventoryPreviewMenu, "Free cell color", settingsHub.InventoryPreviewSettings.CellFreeColor);
            AddChild(showInventoryPreviewMenu, "Used cell color", settingsHub.InventoryPreviewSettings.CellUsedColor);
            AddChild(showInventoryPreviewMenu, "Cell size", settingsHub.InventoryPreviewSettings.CellSize);
            AddChild(showInventoryPreviewMenu, "Cell padding", settingsHub.InventoryPreviewSettings.CellPadding);
            AddChild(showInventoryPreviewMenu, "Position X", settingsHub.InventoryPreviewSettings.PositionX);
            AddChild(showInventoryPreviewMenu, "Position Y", settingsHub.InventoryPreviewSettings.PositionY);

            //Menu Settings
            var menuSettings = AddChild(root, "Menu Settings", settingsHub.MenuSettings.ShowIncrements, "F12");
            AddChild(menuSettings, "Menu font color", settingsHub.MenuSettings.MenuFontColor);
            AddChild(menuSettings, "Title font color", settingsHub.MenuSettings.TitleFontColor);
            AddChild(menuSettings, "Enabled color", settingsHub.MenuSettings.EnabledBoxColor);
            AddChild(menuSettings, "Disabled color", settingsHub.MenuSettings.DisabledBoxColor);
            AddChild(menuSettings, "Slider color", settingsHub.MenuSettings.SliderColor);
            AddChild(menuSettings, "Background color", settingsHub.MenuSettings.BackgroundColor);
            AddChild(menuSettings, "Menu font size", settingsHub.MenuSettings.MenuFontSize);
            AddChild(menuSettings, "Title font size", settingsHub.MenuSettings.TitleFontSize);
            AddChild(menuSettings, "Picker font size", settingsHub.MenuSettings.PickerFontSize);
        }

        private bool OnMouseEvent(MouseEventID id, Point position)
        {
            if (!Settings.Enable || !GameController.Window.IsForeground())
            {
                return false;
            }

            Vector2 mousePosition = GameController.Window.ScreenToClient(position.X, position.Y);
            return root.OnMouseEvent(id, mousePosition);
        }
    }
}