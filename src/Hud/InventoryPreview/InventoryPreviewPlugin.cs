using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using PoeHUD.Controllers;
using PoeHUD.Framework.Helpers;
using PoeHUD.Framework.InputHooks;
using PoeHUD.Poe;
using PoeHUD.Poe.Elements;
using PoeHUD.Poe.RemoteMemoryObjects;
using PoeHUD.Poe.UI;

using SharpDX;
using Graphics = PoeHUD.Hud.UI.Graphics;
using RectangleF = SharpDX.RectangleF;

namespace PoeHUD.Hud.InventoryPreview
{
    public class InventoryPreviewPlugin : Plugin<InventoryPreviewSettings>
    {
        private const int CELLS_Y_COUNT = 5;

        private const int CELLS_X_COUNT = 12;

        private CellData[,] cells;

        private IngameUIElements ingameUiElements;
        private readonly Action<MouseInfo> onMouseDown;

        public InventoryPreviewPlugin(GameController gameController, Graphics graphics,
            InventoryPreviewSettings settings)
            : base(gameController, graphics, settings)
        {
            MouseHook.MouseDown += onMouseDown =  info => info.Handled = OnMouseEvent(info);
            cells = new CellData[CELLS_Y_COUNT, CELLS_X_COUNT];
        }

        public override void Render()
        {
            if (!Settings.Enable)
            {
                return;
            }

            ingameUiElements = GameController.Game.IngameState.IngameUi;
            if (ingameUiElements.OpenLeftPanel.IsVisible || ingameUiElements.OpenRightPanel.IsVisible)
            {
                if (ingameUiElements.InventoryPanel.IsVisible)
                {
                    cells = new CellData[CELLS_Y_COUNT, CELLS_X_COUNT];
                    AddItems();
                }
                return;
            }
            RectangleF rect = GameController.Window.GetWindowRectangle();
            float xPos = rect.Width * Settings.PositionX * .01f;
            float yPos = rect.Height * Settings.PositionY * .01f;
            var startDrawPoint = new Vector2(xPos, yPos);
            for (int i = 0; i < cells.GetLength(0); i++)
            {
                for (int j = 0; j < cells.GetLength(1); j++)
                {
                    Vector2 d = startDrawPoint.Translate(j * Settings.CellSize, i * Settings.CellSize);
                    float cellWidth = GetCellSize(cells[i, j].ExtendsX);
                    float cellHeight = GetCellSize(cells[i, j].ExtendsY);
                    var rectangleF = new RectangleF(d.X, d.Y, cellWidth, cellHeight);
                    Graphics.DrawImage("cell.png", rectangleF, cells[i, j].Used ? Settings.CellUsedColor : Settings.CellFreeColor);
                }
            }
        }

        private void AddItem(int x, int y, int maxX, int maxY)
        {
            for (int i = y; i < maxY; i++)
            {
                for (int j = x; j < maxX; j++)
                {
                    cells[i, j].Used = true;
                    cells[i, j].ExtendsX = j < maxX - 1;
                    cells[i, j].ExtendsY = i < maxY - 1;
                }
            }
        }

        private void AddItems()
        {
            var inventoryZone = GetInventoryZone();
            RectangleF inventoryZoneRectangle = inventoryZone.GetClientRect();
            var oneCellSize = GetOneCellSize(inventoryZoneRectangle);
            foreach (Element itemElement in inventoryZone.Children)
            {
                RectangleF itemElementRectangle = itemElement.GetClientRect();
                var x = (int)((itemElementRectangle.X - inventoryZoneRectangle.X) / oneCellSize.Width + 0.5);
                var y = (int)((itemElementRectangle.Y - inventoryZoneRectangle.Y) / oneCellSize.Height + 0.5);
                Size itemSize = GetItemSize(itemElementRectangle, oneCellSize) + new Size(x, y);

                // BUG Old inventoryZoneRectangle when the window is moved/resized
                if (x < 0 || itemSize.Width > CELLS_X_COUNT || y < 0 || itemSize.Height > CELLS_Y_COUNT)
                {
                    break;
                }

                AddItem(x, y, itemSize.Width, itemSize.Height);
            }
        }

        private static Size GetItemSize(RectangleF itemElementRectangle, Size2F oneCellSize)
        {
            return new Size((int)(itemElementRectangle.Width / oneCellSize.Width + 0.5), (int)(itemElementRectangle.Height / oneCellSize.Height + 0.5));
        }

        private static Size2F GetOneCellSize(RectangleF inventoryZoneRectangle)
        {
            return new Size2F(inventoryZoneRectangle.Width / CELLS_X_COUNT,inventoryZoneRectangle.Height / CELLS_Y_COUNT);
        }

        private Element GetInventoryZone()
        {
            return ingameUiElements.ReadObject<Element>(ingameUiElements.InventoryPanel.Address + 0x788 + 0x250);
        }

        private int GetCellSize(bool extendsSide)
        {
            return extendsSide ? Settings.CellSize : Math.Max(1, Settings.CellSize - Settings.CellPadding);
        }

        private  bool OnMouseEvent(MouseInfo mouseInfo)
        {
            if (!Settings.Enable || !Settings.AutoUpdate || !GameController.Window.IsForeground() || mouseInfo.Buttons != MouseButtons.Left)
            {
                return false;
            }
            Element uiHover = GameController.Game.IngameState.UIHover;
            var inventoryItemIcon = uiHover.AsObject<InventoryItemIcon>();
            if (inventoryItemIcon.ToolTipType == ToolTipType.ItemOnGround)
            {
                 RectangleF itemElementRectangle  = inventoryItemIcon.Tooltip.GetChildAtIndex(0).GetChildAtIndex(0).GetClientRect();
                var item = inventoryItemIcon.Item;
                var inventoryZone = GetInventoryZone();
                RectangleF inventoryZoneRectangle = inventoryZone.GetClientRect();

                var oneCellSize = GetOneCellSize(inventoryZoneRectangle);
                Size itemSize = GetItemSize(itemElementRectangle, oneCellSize);
                if (TryToAutoAddItem(itemSize, item)) return true;
            }
            return true;
        }

        private bool TryToAutoAddItem(Size itemSize, Entity item)
        {
            lock (this)
            {
                for (int j = 0; j + itemSize.Width <= cells.GetLength(1); j++)
                    for (int i = 0; i + itemSize.Height <= cells.GetLength(0); i++)
                    {
                        if (!cells[i, j].Used)
                        {
                            bool found = true;
                            for (int jj = j; jj < j + itemSize.Width && found; jj++)
                            {
                                for (int ii = i; ii < i + itemSize.Height; ii++)
                                {
                                    if (cells[ii, jj].Used)
                                    {
                                        found = false;
                                        break;
                                    }
                                }
                            }
                            if (found)
                            {
                                Task.Factory.StartNew(async () =>
                                {
                                    Stopwatch sw = Stopwatch.StartNew();
                                    while (item.IsValid)
                                    {
                                        await Task.Delay(30);
                                        if (sw.ElapsedMilliseconds > 10000)
                                        {
                                            sw.Stop();
                                            break;
                                        }
                                    }
                                    if (!item.IsValid)
                                    {
                                        AddItem(j, i, j + itemSize.Width, i + itemSize.Height);
                                    }
                                });
                                return true;
                            }
                        }
                    }
                return false;
            }
        }

        public override void Dispose()
        {
            MouseHook.MouseDown -= onMouseDown;
        }

    }
}