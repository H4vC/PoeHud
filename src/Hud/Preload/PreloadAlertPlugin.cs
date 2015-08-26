using PoeHUD.Controllers;
using PoeHUD.Framework;
using PoeHUD.Framework.Helpers;
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

        public PreloadAlertPlugin(GameController gameController, Graphics graphics, PreloadAlertSettings settings)
            : base(gameController, graphics, settings)
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
            base.Render();
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
                    Size2 size = Graphics.DrawText( preloadConfigLine.Text, Settings.FontSize, position - 1,
                        preloadConfigLine.FastColor?.Invoke() ?? preloadConfigLine.Color ?? Settings.FastColor, FontDrawFlags.Right);
                    maxWidth = Math.Max(size.Width, maxWidth);
                    position.Y += size.Height;
                }
                if (maxWidth <= 0) return;
                var bounds = new RectangleF(startPosition.X - 42 - maxWidth, startPosition.Y - 4,
                    maxWidth + 50, position.Y - startPosition.Y + 10);
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
                    if (text.Contains('@'))
                    {
                        text = text.Split('@')[0];
                    }
                    if (text.Contains("human_heart") || text.Contains("Demonic_NoRain.ogg"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Corrupted Area", FastColor = () => Settings.CorruptedColor});
                    }
                    if (alertStrings.ContainsKey(text))
                    {
                        alerts.Add(alertStrings[text]);
                    }
                    if (text.EndsWith("BossInvasion"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Invasion Boss" });
                    }

                    if (text.EndsWith("Metadata/NPC/Missions/Wild/StrDexInt"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Zana, Master Cartographer", FastColor = () => Settings.MasterZana });
                    }
                    if (text.EndsWith("Metadata/NPC/Missions/Wild/Int"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Catarina, Master of the Dead", FastColor = () => Settings.MasterCatarina });
                    }
                    if (text.EndsWith("Metadata/NPC/Missions/Wild/Dex"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Tora, Master of the Hunt", FastColor = () => Settings.MasterTora });
                    }
                    if (text.EndsWith("Metadata/NPC/Missions/Wild/DexInt"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Vorici, Master Assassin", FastColor = () => Settings.MasterVorici });
                    }
                    if (text.EndsWith("Metadata/NPC/Missions/Wild/Str"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Haku, Armourmaster", FastColor = () => Settings.MasterHaku });
                    }
                    if (text.EndsWith("Metadata/NPC/Missions/Wild/StrInt"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Elreon, Loremaster", FastColor = () => Settings.MasterElreon });
                    }
                    if (text.EndsWith("Metadata/NPC/Missions/Wild/Fish"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Krillson, Master Fisherman", FastColor = () => Settings.MasterKrillson });
                    }

                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex1"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (2HSword)", FastColor = () => Settings.MasterVagan });
                    }
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex2"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (Staff)", FastColor = () => Settings.MasterVagan });
                    }
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex3"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (Bow)", FastColor = () => Settings.MasterVagan });
                    }
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex4"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (DaggerRapier)", FastColor = () => Settings.MasterVagan });
                    }
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex5"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (Blunt)", FastColor = () => Settings.MasterVagan });
                    }
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex6"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (Blades)", FastColor = () => Settings.MasterVagan });
                    }
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex7"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (SwordAxe)", FastColor = () => Settings.MasterVagan });
                    }
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex8"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (Punching)", FastColor = () => Settings.MasterVagan });
                    }
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex9"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (Flickerstrike)", FastColor = () => Settings.MasterVagan });
                    }
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex10"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (Elementalist)", FastColor = () => Settings.MasterVagan });
                    }
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex11"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (Cyclone)", FastColor = () => Settings.MasterVagan });
                    }
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex12"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (PhysSpells)", FastColor = () => Settings.MasterVagan });
                    }
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex13"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (Traps)", FastColor = () => Settings.MasterVagan });
                    }
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex14"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (RighteousFire)", FastColor = () => Settings.MasterVagan });
                    }
                    if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex15"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (CastOnHit)", FastColor = () => Settings.MasterVagan });
                    }

                    if (text.Contains("Arcanist"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Arcanist Strongbox", FastColor = () => Settings.ArcanistStrongbox });
                    }
                    if (text.Contains("Artisan"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Artisan Strongbox", FastColor = () => Settings.ArtisanStrongbox });
                    }
                    if (text.Contains("Cartographer"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Cartographer Strongbox", FastColor = () => Settings.CartographerStrongbox });
                    }
                    if (text.Contains("Gemcutter"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Gemcutter Strongbox", FastColor = () => Settings.GemcutterStrongbox });
                    }
                    if (text.Contains("Jeweller"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Jeweller Strongbox", FastColor = () => Settings.JewellerStrongbox });
                    }
                    if (text.Contains("Arsenal"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Blacksmith Strongbox", FastColor = () => Settings.BlacksmithStrongbox });
                    }
                    if (text.Contains("Armory"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Armourer Strongbox", FastColor = () => Settings.ArmourerStrongbox });
                    }
                    if (text.Contains("Ornate"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Ornate Strongbox", FastColor = () => Settings.OrnateStrongbox });
                    }
                    if (text.Contains("LargeStrongbox"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Large Strongbox", FastColor = () => Settings.LargeStrongbox });
                    }
                    if (text.Contains("PerandusBox"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Perandus Strongbox", FastColor = () => Settings.PerandusStrongbox });
                    }
                    if (text.Contains("KaomBox"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Kaom Strongbox", FastColor = () => Settings.KaomStrongbox });
                    }
                    if (text.Contains("MalachaisBox"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Malachai Strongbox", FastColor = () => Settings.MalachaiStrongbox });
                    }
                    if (text.Contains("ExileRanger1"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Orra Greengate", FastColor = () => Settings.OrraGreengate });
                    }
                    if (text.Contains("ExileRanger2"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Thena Moga", FastColor = () => Settings.ThenaMoga });
                    }
                    if (text.Contains("ExileRanger3"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Antalie Napora", FastColor = () => Settings.AntalieNapora });
                    }
                    if (text.Contains("ExileDuelist1"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Torr Olgosso", FastColor = () => Settings.TorrOlgosso });
                    }
                    if (text.Contains("ExileDuelist2"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Armios Bell", FastColor = () => Settings.ArmiosBell });
                    }
                    if (text.Contains("ExileDuelist4"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Zacharie Desmarais", FastColor = () => Settings.ZacharieDesmarais });
                    }
                    if (text.Contains("ExileWitch1"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Minara Anenima", FastColor = () => Settings.MinaraAnenima });
                    }
                    if (text.Contains("ExileWitch2"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Igna Phoenix", FastColor = () => Settings.IgnaPhoenix });
                    }
                    if (text.Contains("ExileMarauder1"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Jonah Unchained", FastColor = () => Settings.JonahUnchained });
                    }
                    if (text.Contains("ExileMarauder2"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Damoi Tui", FastColor = () => Settings.DamoiTui });
                    }
                    if (text.Contains("ExileMarauder3"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Xandro Blooddrinker", FastColor = () => Settings.XandroBlooddrinker });
                    }
                    if (text.Contains("ExileMarauder5"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Vickas Giantbone", FastColor = () => Settings.VickasGiantbone });
                    }
                    if (text.Contains("ExileTemplar1"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Eoin Greyfur", FastColor = () => Settings.EoinGreyfur });
                    }
                    if (text.Contains("ExileTemplar2"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Tinevin Highdove", FastColor = () => Settings.TinevinHighdove });
                    }
                    if (text.Contains("ExileTemplar4"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Magnus Stonethorn", FastColor = () => Settings.MagnusStonethorn });
                    }
                    if (text.Contains("ExileShadow1_"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Ion Darkshroud", FastColor = () => Settings.IonDarkshroud });
                    }
                    if (text.Contains("ExileShadow2"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Ash Lessard", FastColor = () => Settings.AshLessard });
                    }
                    if (text.Contains("ExileShadow4"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Wilorin Demontamer", FastColor = () => Settings.WilorinDemontamer });
                    }
                    if (text.Contains("ExileScion2"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Augustina Solaria", FastColor = () => Settings.AugustinaSolaria });
                    }
                }
            }
        }
    }
}