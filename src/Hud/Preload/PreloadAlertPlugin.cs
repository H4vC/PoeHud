using PoeHUD.Controllers;
using PoeHUD.Framework;
using PoeHUD.Framework.Helpers;
using PoeHUD.Hud.Settings;
using PoeHUD.Hud.UI;
using PoeHUD.Models;
using SharpDX;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PoeHUD.Hud.Preload
{
    public class PreloadAlertPlugin : SizedPlugin<PreloadAlertSettings>
    {
        private readonly HashSet<PreloadConfigLine> alerts;
        private readonly Dictionary<string, PreloadConfigLine> alertStrings;
        private bool areaChanged = true;
        private DateTime maxParseTime = DateTime.Now;
        private int lastCount;

        public PreloadAlertPlugin(GameController gameController, Graphics graphics, PreloadAlertSettings settings, SettingsHub settingsHub)
            : base(gameController, graphics, settings, settingsHub)
        {
            alerts = new HashSet<PreloadConfigLine>();
            alertStrings = LoadConfig("config/preload_alerts.txt");
            GameController.Area.OnAreaChange += OnAreaChange;
        }

        public Dictionary<string, PreloadConfigLine> LoadConfig(string path)
        {
            return LoadConfigBase(path, 3).ToDictionary(line => line[0], line =>
            {
                var preloadConfigLine = new PreloadConfigLine
                {
                    Text = line[1],
                    Color = line.ConfigColorValueExtractor(2)
                };
                return preloadConfigLine;
            });
        }

        public override void Render()
        {
            base.Render(); HideAll();

            if (!Settings.Enable)
            {
                return;
            }
            if (areaChanged)
            {
                Parse();
                lastCount = GetNumberOfObjects();
            }
            else if (DateTime.Now <= maxParseTime)
            {
                int count = GetNumberOfObjects();
                if (lastCount != count)
                {
                    areaChanged = true;
                }
            }
            if (alerts.Count > 0)
            {
                Vector2 startPosition = StartDrawPointFunc();
                Vector2 position = startPosition;
                int maxWidth = 0;

                foreach (var preloadConfigLine in alerts)
                {
                    Size2 size = Graphics.DrawText(preloadConfigLine.Text, Settings.FontSize, position + 1,
                        preloadConfigLine.FastColor?.Invoke() ?? preloadConfigLine.Color ?? Settings.FastColor, FontDrawFlags.Right);
                    maxWidth = Math.Max(size.Width, maxWidth);
                    position.Y += size.Height;
                }
                if (maxWidth <= 0) return;
                var bounds = new RectangleF(startPosition.X - 42 - maxWidth, startPosition.Y - 4,
                    maxWidth + 50, position.Y - startPosition.Y + 11);
                Graphics.DrawImage("preload-end.png", bounds, Settings.BackgroundColor);
                Graphics.DrawImage("preload-start.png", bounds, Settings.BackgroundColor);
                Size = bounds.Size;
                Margin = new Vector2(0, 5);
            }
        }

        private int GetNumberOfObjects()
        {
            Memory memory = GameController.Memory;
            return memory.ReadInt(memory.AddressOfProcess + memory.offsets.FileRoot, 12);
        }

        private void OnAreaChange(AreaController area)
        {
            maxParseTime = area.CurrentArea.TimeEntered.AddSeconds(10);
            areaChanged = true;
        }

        private void Parse()
        {
            areaChanged = false; alerts.Clear();
            Memory memory = GameController.Memory;
            int pFileRoot = memory.ReadInt(memory.AddressOfProcess + memory.offsets.FileRoot);
            int count = memory.ReadInt(pFileRoot + 12);
            int listIterator = memory.ReadInt(pFileRoot + 20);
            int areaChangeCount = GameController.Game.AreaChangeCount;
            for (int i = 0; i < count; i++)
            {
                listIterator = memory.ReadInt(listIterator);
                if (memory.ReadInt(listIterator + 8) != 0 && memory.ReadInt(listIterator + 12, 36) == areaChangeCount)
                {
                    string text = memory.ReadStringU(memory.ReadInt(listIterator + 8));
                    if (text.Contains('@')) text = text.Split('@')[0];
                    if (alertStrings.ContainsKey(text)) alerts.Add(alertStrings[text]);
                    if (text.EndsWith("BossInvasion"))
                        alerts.Add(new PreloadConfigLine { Text = "Invasion Boss" });
                    if (text.Contains("human_heart") || text.Contains("Demonic_NoRain.ogg"))
                        alerts.Add(new PreloadConfigLine { Text = "Corrupted Area", FastColor = () => Settings.CorruptedColor });

                    if (text.EndsWith("Metadata/NPC/Missions/Wild/StrDexInt"))
                        alerts.Add(new PreloadConfigLine { Text = "test Zana, Master Cartographer", FastColor = () => Settings.MasterZana });
                    if (text.EndsWith("Metadata/NPC/Missions/Wild/Int"))
                        alerts.Add(new PreloadConfigLine { Text = "test Catarina, Master of the Dead", FastColor = () => Settings.MasterCatarina });
                    if (text.EndsWith("Metadata/NPC/Missions/Wild/Dex"))
                        alerts.Add(new PreloadConfigLine { Text = "test Tora, Master of the Hunt", FastColor = () => Settings.MasterTora });
                    if (text.EndsWith("Metadata/NPC/Missions/Wild/DexInt"))
                        alerts.Add(new PreloadConfigLine { Text = "test Vorici, Master Assassin", FastColor = () => Settings.MasterVorici });
                    if (text.EndsWith("Metadata/NPC/Missions/Wild/Str"))
                        alerts.Add(new PreloadConfigLine { Text = "test Haku, Armourmaster", FastColor = () => Settings.MasterHaku });
                    if (text.EndsWith("Metadata/NPC/Missions/Wild/StrInt"))
                        alerts.Add(new PreloadConfigLine { Text = "test Elreon, Loremaster", FastColor = () => Settings.MasterElreon });
                    if (text.EndsWith("Metadata/NPC/Missions/Wild/Fish"))
                        alerts.Add(new PreloadConfigLine { Text = "test Krillson, Master Fisherman", FastColor = () => Settings.MasterKrillson });
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex1"))
                        alerts.Add(new PreloadConfigLine { Text = "test Vagan, (2HSword)", FastColor = () => Settings.MasterVagan });
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex2"))
                        alerts.Add(new PreloadConfigLine { Text = "test Vagan, (Staff)", FastColor = () => Settings.MasterVagan });
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex3"))
                        alerts.Add(new PreloadConfigLine { Text = "test Vagan, (Bow)", FastColor = () => Settings.MasterVagan });
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex4"))
                        alerts.Add(new PreloadConfigLine { Text = "test Vagan, (DaggerRapier)", FastColor = () => Settings.MasterVagan });
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex5"))
                        alerts.Add(new PreloadConfigLine { Text = "test Vagan, (Blunt)", FastColor = () => Settings.MasterVagan });
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex6"))
                        alerts.Add(new PreloadConfigLine { Text = "test Vagan, (Blades)", FastColor = () => Settings.MasterVagan });
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex7"))
                        alerts.Add(new PreloadConfigLine { Text = "test Vagan, (SwordAxe)", FastColor = () => Settings.MasterVagan });
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex8"))
                        alerts.Add(new PreloadConfigLine { Text = "test Vagan, (Punching)", FastColor = () => Settings.MasterVagan });
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex9"))
                        alerts.Add(new PreloadConfigLine { Text = "test Vagan, (Flickerstrike)", FastColor = () => Settings.MasterVagan });
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex10"))
                        alerts.Add(new PreloadConfigLine { Text = "test Vagan, (Elementalist)", FastColor = () => Settings.MasterVagan });
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex11"))
                        alerts.Add(new PreloadConfigLine { Text = "test Vagan, (Cyclone)", FastColor = () => Settings.MasterVagan });
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex12"))
                        alerts.Add(new PreloadConfigLine { Text = "test Vagan, (PhysSpells)", FastColor = () => Settings.MasterVagan });
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex13"))
                        alerts.Add(new PreloadConfigLine { Text = "test Vagan, (Traps)", FastColor = () => Settings.MasterVagan });
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex14"))
                        alerts.Add(new PreloadConfigLine { Text = "test Vagan, (RighteousFire)", FastColor = () => Settings.MasterVagan });
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex15"))
                        alerts.Add(new PreloadConfigLine { Text = "test Vagan, (CastOnHit)", FastColor = () => Settings.MasterVagan });

                    //if (text.Contains("Arcanist"))
                    //    alerts.Add(new PreloadConfigLine { Text = "test Arcanist Strongbox", FastColor = () => Settings.ArcanistStrongbox });
                    //if (text.Contains("Artisan"))
                    //    alerts.Add(new PreloadConfigLine { Text = "test Artisan Strongbox", FastColor = () => Settings.ArtisanStrongbox });
                    //if (text.Contains("Cartographer"))
                    //    alerts.Add(new PreloadConfigLine { Text = "test Cartographer Strongbox", FastColor = () => Settings.CartographerStrongbox });
                    //if (text.Contains("Gemcutter"))
                    //    alerts.Add(new PreloadConfigLine { Text = "test Gemcutter Strongbox", FastColor = () => Settings.GemcutterStrongbox });
                    //if (text.Contains("Jeweller"))
                    //    alerts.Add(new PreloadConfigLine { Text = "test Jeweller Strongbox", FastColor = () => Settings.JewellerStrongbox });
                    //if (text.Contains("Arsenal"))
                    //    alerts.Add(new PreloadConfigLine { Text = "test Blacksmith Strongbox", FastColor = () => Settings.BlacksmithStrongbox });
                    //if (text.Contains("Armory"))
                    //    alerts.Add(new PreloadConfigLine { Text = "test Armourer Strongbox", FastColor = () => Settings.ArmourerStrongbox });
                    //if (text.Contains("Ornate"))
                    //    alerts.Add(new PreloadConfigLine { Text = "test Ornate Strongbox", FastColor = () => Settings.OrnateStrongbox });
                    //if (text.Contains("LargeStrongbox"))
                    //    alerts.Add(new PreloadConfigLine { Text = "test Large Strongbox", FastColor = () => Settings.LargeStrongbox });
                    //if (text.Contains("PerandusBox"))
                    //    alerts.Add(new PreloadConfigLine { Text = "test Perandus Strongbox", FastColor = () => Settings.PerandusStrongbox });
                    //if (text.Contains("KaomBox"))
                    //    alerts.Add(new PreloadConfigLine { Text = "test Kaom Strongbox", FastColor = () => Settings.KaomStrongbox });
                    //if (text.Contains("MalachaisBox"))
                    //    alerts.Add(new PreloadConfigLine { Text = "test Malachai Strongbox", FastColor = () => Settings.MalachaiStrongbox });
                    //if (text.Contains("Strongbox"))
                    //    alerts.Add(new PreloadConfigLine { Text = "test Simple Strongbox", FastColor = () => Settings.SimpleStrongbox });

                    if (text.Contains("ExileRanger1"))
                        alerts.Add(new PreloadConfigLine { Text = "test Orra Greengate", FastColor = () => Settings.OrraGreengate });
                    if (text.Contains("ExileRanger2"))
                        alerts.Add(new PreloadConfigLine { Text = "test Thena Moga", FastColor = () => Settings.ThenaMoga });
                    if (text.Contains("ExileRanger3"))
                        alerts.Add(new PreloadConfigLine { Text = "test Antalie Napora", FastColor = () => Settings.AntalieNapora });
                    if (text.Contains("ExileDuelist1"))
                        alerts.Add(new PreloadConfigLine { Text = "test Torr Olgosso", FastColor = () => Settings.TorrOlgosso });
                    if (text.Contains("ExileDuelist2"))
                        alerts.Add(new PreloadConfigLine { Text = "test Armios Bell", FastColor = () => Settings.ArmiosBell });
                    if (text.Contains("ExileDuelist4"))
                        alerts.Add(new PreloadConfigLine { Text = "test Zacharie Desmarais", FastColor = () => Settings.ZacharieDesmarais });
                    if (text.Contains("ExileWitch1"))
                        alerts.Add(new PreloadConfigLine { Text = "test Minara Anenima", FastColor = () => Settings.MinaraAnenima });
                    if (text.Contains("ExileWitch2"))
                        alerts.Add(new PreloadConfigLine { Text = "test Igna Phoenix", FastColor = () => Settings.IgnaPhoenix });
                    if (text.Contains("ExileMarauder1"))
                        alerts.Add(new PreloadConfigLine { Text = "test Jonah Unchained", FastColor = () => Settings.JonahUnchained });
                    if (text.Contains("ExileMarauder2"))
                        alerts.Add(new PreloadConfigLine { Text = "test Damoi Tui", FastColor = () => Settings.DamoiTui });
                    if (text.Contains("ExileMarauder3"))
                        alerts.Add(new PreloadConfigLine { Text = "test Xandro Blooddrinker", FastColor = () => Settings.XandroBlooddrinker });
                    if (text.Contains("ExileMarauder5"))
                        alerts.Add(new PreloadConfigLine { Text = "test Vickas Giantbone", FastColor = () => Settings.VickasGiantbone });
                    if (text.Contains("ExileTemplar1"))
                        alerts.Add(new PreloadConfigLine { Text = "test Eoin Greyfur", FastColor = () => Settings.EoinGreyfur });
                    if (text.Contains("ExileTemplar2"))
                        alerts.Add(new PreloadConfigLine { Text = "test Tinevin Highdove", FastColor = () => Settings.TinevinHighdove });
                    if (text.Contains("ExileTemplar4"))
                        alerts.Add(new PreloadConfigLine { Text = "test Magnus Stonethorn", FastColor = () => Settings.MagnusStonethorn });
                    if (text.Contains("ExileShadow1_"))
                        alerts.Add(new PreloadConfigLine { Text = "test Ion Darkshroud", FastColor = () => Settings.IonDarkshroud });
                    if (text.Contains("ExileShadow2"))
                        alerts.Add(new PreloadConfigLine { Text = "test Ash Lessard", FastColor = () => Settings.AshLessard });
                    if (text.Contains("ExileShadow4"))
                        alerts.Add(new PreloadConfigLine { Text = "test Wilorin Demontamer", FastColor = () => Settings.WilorinDemontamer });
                    if (text.Contains("ExileScion2"))
                        alerts.Add(new PreloadConfigLine { Text = "test Augustina Solaria", FastColor = () => Settings.AugustinaSolaria });
                }
            }
        }
    }
}