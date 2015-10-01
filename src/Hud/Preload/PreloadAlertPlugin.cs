using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using PoeHUD.Controllers;
using PoeHUD.Framework;
using PoeHUD.Framework.Helpers;
using PoeHUD.Hud.UI;
using PoeHUD.Models;
using SharpDX;
using SharpDX.Direct3D9;

namespace PoeHUD.Hud.Preload
{
    public class PreloadAlertPlugin : SizedPlugin<PreloadAlertSettings>
    {
        private readonly HashSet<PreloadConfigLine> alerts;
        private readonly Dictionary<string, PreloadConfigLine> alertStrings;
        private bool areaChanged = true;
        private DateTime maxParseTime = DateTime.Now;
        private int lastCount;
        private bool holdKey;

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
                    Color = line.ConfigColorValueExtractor(2),
                    SoundFile = line.ConfigValueExtractor(3)
                };
                if (preloadConfigLine.SoundFile != null)
                    Sounds.AddSound(preloadConfigLine.SoundFile);
                return preloadConfigLine;
            });
        }

        public override void Render()
        {
            base.Render();

            if (!holdKey && WinApi.IsKeyDown(Keys.F10))
            {
                holdKey = true;
                Settings.Enable.Value = !Settings.Enable.Value;
            }
            else if (holdKey && !WinApi.IsKeyDown(Keys.F10))
            {
                holdKey = false;
            }

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
                    Size2 size = Graphics.DrawText(preloadConfigLine.Text, Settings.FontSize, position + 1, preloadConfigLine.FastColor?.Invoke() ?? preloadConfigLine.Color ?? Settings.DefaultFontColor, FontDrawFlags.Right);
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
                    //PreloadConfigLine PreloadConfigLine = null;
                    string text = memory.ReadStringU(memory.ReadInt(listIterator + 8));
                    if (text.Contains('@')) text = text.Split('@')[0];
                    if (alertStrings.ContainsKey(text)) alerts.Add(alertStrings[text]);
                    if (text.EndsWith("BossInvasion"))
                        alerts.Add(new PreloadConfigLine { Text = "Invasion Boss" });
                    if (text.Contains("human_heart") || text.Contains("Demonic_NoRain.ogg"))
                    {
                        alerts.Add(new PreloadConfigLine { Text = "Corrupted Area", FastColor = () => Settings.CorruptedColor });
                        PlaySound();
                    }
                        
                    

                    else if (text.EndsWith("Metadata/NPC/Missions/Wild/StrDexInt"))
                        alerts.Add(new PreloadConfigLine { Text = "Zana, Master Cartographer", FastColor = () => Settings.MasterZana });
                    else if (text.EndsWith("Metadata/NPC/Missions/Wild/Int"))
                        alerts.Add(new PreloadConfigLine { Text = "Catarina, Master of the Dead", FastColor = () => Settings.MasterCatarina });
                    else if (text.EndsWith("Metadata/NPC/Missions/Wild/Dex"))
                        alerts.Add(new PreloadConfigLine { Text = "Tora, Master of the Hunt", FastColor = () => Settings.MasterTora });
                    else if (text.EndsWith("Metadata/NPC/Missions/Wild/DexInt"))
                        alerts.Add(new PreloadConfigLine { Text = "Vorici, Master Assassin", FastColor = () => Settings.MasterVorici });
                    else if (text.EndsWith("Metadata/NPC/Missions/Wild/Str"))
                        alerts.Add(new PreloadConfigLine { Text = "Haku, Armourmaster", FastColor = () => Settings.MasterHaku });
                    else if (text.EndsWith("Metadata/NPC/Missions/Wild/StrInt"))
                        alerts.Add(new PreloadConfigLine { Text = "Elreon, Loremaster", FastColor = () => Settings.MasterElreon });
                    else if (text.EndsWith("Metadata/NPC/Missions/Wild/Fish"))
                        alerts.Add(new PreloadConfigLine { Text = "Krillson, Master Fisherman", FastColor = () => Settings.MasterKrillson });
                    else if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex1"))
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (2HSword)", FastColor = () => Settings.MasterVagan });
                    else if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex2"))
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (Staff)", FastColor = () => Settings.MasterVagan });
                    else if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex3"))
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (Bow)", FastColor = () => Settings.MasterVagan });
                    else if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex4"))
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (DaggerRapier)", FastColor = () => Settings.MasterVagan });
                    else if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex5"))
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (Blunt)", FastColor = () => Settings.MasterVagan });
                    else if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex6"))
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (Blades)", FastColor = () => Settings.MasterVagan });
                    else if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex7"))
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (SwordAxe)", FastColor = () => Settings.MasterVagan });
                    else if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex8"))
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (Punching)", FastColor = () => Settings.MasterVagan });
                    else if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex9"))
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (Flickerstrike)", FastColor = () => Settings.MasterVagan });
                    else if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex10"))
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (Elementalist)", FastColor = () => Settings.MasterVagan });
                    else if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex11"))
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (Cyclone)", FastColor = () => Settings.MasterVagan });
                    else if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex12"))
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (PhysSpells)", FastColor = () => Settings.MasterVagan });
                    else if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex13"))
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (Traps)", FastColor = () => Settings.MasterVagan });
                    else if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex14"))
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (RighteousFire)", FastColor = () => Settings.MasterVagan });
                    else if (text.EndsWith("Metadata/Monsters/Missions/MasterStrDex15"))
                        alerts.Add(new PreloadConfigLine { Text = "Vagan, (CastOnHit)", FastColor = () => Settings.MasterVagan });


                    else if (text.Contains("Metadata/Chests/StrongBoxes/Arcanist"))
                        alerts.Add(new PreloadConfigLine { Text = "Arcanist's Strongbox", FastColor = () => Settings.ArcanistStrongbox });
                    else if (text.Contains("Metadata/Chests/StrongBoxes/Artisan"))
                        alerts.Add(new PreloadConfigLine { Text = "Artisan's Strongbox", FastColor = () => Settings.ArtisanStrongbox });
                    else if (text.Contains("Metadata/Chests/StrongBoxes/Cartographer"))
                        alerts.Add(new PreloadConfigLine { Text = "Cartographer's Strongbox", FastColor = () => Settings.CartographerStrongbox });
                    else if (text.Contains("Metadata/Chests/StrongBoxes/Gemcutter"))
                        alerts.Add(new PreloadConfigLine { Text = "Gemcutter's Strongbox", FastColor = () => Settings.GemcutterStrongbox });
                    else if (text.Contains("Metadata/Chests/StrongBoxes/Jeweller"))
                        alerts.Add(new PreloadConfigLine { Text = "Jeweller's Strongbox", FastColor = () => Settings.JewellerStrongbox });
                    else if (text.Contains("Metadata/Chests/StrongBoxes/ArsenalStrongBox"))
                        alerts.Add(new PreloadConfigLine { Text = "Blacksmith's Strongbox", FastColor = () => Settings.BlacksmithStrongbox });
                    else if (text.Contains("Metadata/Chests/StrongBoxes/ArmoryStrongBox"))
                        alerts.Add(new PreloadConfigLine { Text = "Armourer's Strongbox", FastColor = () => Settings.ArmourerStrongbox });
                    else if (text.Contains("Metadata/Chests/StrongBoxes/Ornate"))
                        alerts.Add(new PreloadConfigLine { Text = "Ornate Strongbox", FastColor = () => Settings.OrnateStrongbox });
                    else if (text.Contains("Metadata/Chests/StrongBoxes/Large"))
                        alerts.Add(new PreloadConfigLine { Text = "Large Strongbox", FastColor = () => Settings.LargeStrongbox });
                    else if (text.Contains("Metadata/Chests/StrongBoxes/PerandusBox"))
                        alerts.Add(new PreloadConfigLine { Text = "Perandus Strongbox", FastColor = () => Settings.PerandusStrongbox });
                    else if (text.Contains("Metadata/Chests/StrongBoxes/KaomBox"))
                        alerts.Add(new PreloadConfigLine { Text = "Kaom Strongbox", FastColor = () => Settings.KaomStrongbox });
                    else if (text.Contains("Metadata/Chests/StrongBoxes/MalachaisBox"))
                        alerts.Add(new PreloadConfigLine { Text = "Malachai Strongbox", FastColor = () => Settings.MalachaiStrongbox });
                    else if (text.Contains("Metadata/Chests/CopperChestEpic3"))
                        alerts.Add(new PreloadConfigLine { Text = "Epic Strongbox", FastColor = () => Settings.EpicStrongbox });
                    else if (text.Contains("Metadata/Chests/StrongBoxes/Strongbox"))
                        alerts.Add(new PreloadConfigLine { Text = "Simple Strongbox", FastColor = () => Settings.SimpleStrongbox });


                    else if (text.EndsWith("Metadata/Monsters/Exiles/ExileRanger1"))
                        alerts.Add(new PreloadConfigLine { Text = "Orra Greengate", FastColor = () => Settings.OrraGreengate });
                    else if (text.EndsWith("Metadata/Monsters/Exiles/ExileRanger2"))
                        alerts.Add(new PreloadConfigLine { Text = "Thena Moga", FastColor = () => Settings.ThenaMoga });
                    else if (text.EndsWith("Metadata/Monsters/Exiles/ExileRanger3"))
                        alerts.Add(new PreloadConfigLine { Text = "Antalie Napora", FastColor = () => Settings.AntalieNapora });
                    else if (text.EndsWith("Metadata/Monsters/Exiles/ExileDuelist1"))
                        alerts.Add(new PreloadConfigLine { Text = "Torr Olgosso", FastColor = () => Settings.TorrOlgosso });
                    else if (text.EndsWith("Metadata/Monsters/Exiles/ExileDuelist2"))
                        alerts.Add(new PreloadConfigLine { Text = "Armios Bell", FastColor = () => Settings.ArmiosBell });
                    else if (text.EndsWith("Metadata/Monsters/Exiles/ExileDuelist4"))
                        alerts.Add(new PreloadConfigLine { Text = "Zacharie Desmarais", FastColor = () => Settings.ZacharieDesmarais });
                    else if (text.EndsWith("Metadata/Monsters/Exiles/ExileWitch1"))
                        alerts.Add(new PreloadConfigLine { Text = "Minara Anenima", FastColor = () => Settings.MinaraAnenima });
                    else if (text.EndsWith("Metadata/Monsters/Exiles/ExileWitch2"))
                        alerts.Add(new PreloadConfigLine { Text = "Igna Phoenix", FastColor = () => Settings.IgnaPhoenix });
                    else if (text.EndsWith("Metadata/Monsters/Exiles/ExileMarauder1"))
                        alerts.Add(new PreloadConfigLine { Text = "Jonah Unchained", FastColor = () => Settings.JonahUnchained });
                    else if (text.EndsWith("Metadata/Monsters/Exiles/ExileMarauder2"))
                        alerts.Add(new PreloadConfigLine { Text = "Damoi Tui", FastColor = () => Settings.DamoiTui });
                    else if (text.EndsWith("Metadata/Monsters/Exiles/ExileMarauder3"))
                        alerts.Add(new PreloadConfigLine { Text = "Xandro Blooddrinker", FastColor = () => Settings.XandroBlooddrinker });
                    else if (text.EndsWith("Metadata/Monsters/Exiles/ExileMarauder5"))
                        alerts.Add(new PreloadConfigLine { Text = "Vickas Giantbone", FastColor = () => Settings.VickasGiantbone });
                    else if (text.EndsWith("Metadata/Monsters/Exiles/ExileTemplar1"))
                        alerts.Add(new PreloadConfigLine { Text = "Eoin Greyfur", FastColor = () => Settings.EoinGreyfur });
                    else if (text.EndsWith("Metadata/Monsters/Exiles/ExileTemplar2"))
                        alerts.Add(new PreloadConfigLine { Text = "Tinevin Highdove", FastColor = () => Settings.TinevinHighdove });
                    else if (text.EndsWith("Metadata/Monsters/Exiles/ExileTemplar4"))
                        alerts.Add(new PreloadConfigLine { Text = "Magnus Stonethorn", FastColor = () => Settings.MagnusStonethorn });
                    else if (text.EndsWith("Metadata/Monsters/Exiles/ExileShadow1_"))
                        alerts.Add(new PreloadConfigLine { Text = "Ion Darkshroud", FastColor = () => Settings.IonDarkshroud });
                    else if (text.EndsWith("Metadata/Monsters/Exiles/ExileShadow2"))
                        alerts.Add(new PreloadConfigLine { Text = "Ash Lessard", FastColor = () => Settings.AshLessard });
                    else if (text.EndsWith("Metadata/Monsters/Exiles/ExileShadow4"))
                        alerts.Add(new PreloadConfigLine { Text = "Wilorin Demontamer", FastColor = () => Settings.WilorinDemontamer });
                    else if (text.EndsWith("Metadata/Monsters/Exiles/ExileScion2"))
                        alerts.Add(new PreloadConfigLine { Text = "Augustina Solaria", FastColor = () => Settings.AugustinaSolaria });
                }
            }
        }

        public void PlaySound()
        {
            if (!Settings.PlaySound) return;
            Sounds.AlertSound.Play();
        }
    }
}