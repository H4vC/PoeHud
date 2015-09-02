using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using PoeHUD.Framework;

namespace PoeHUD.Hud
{
    public class CustomColorDialog
    {
        private readonly TrackBar alphaSlider;

        private readonly TextBox alphaText;

        private readonly Panel alphaPanel;

        private ChooseColor chooseColor;

        private bool pressed = true;

        public CustomColorDialog(Color color)
        {
            const int RGBINIT = 0x1;
            const int FULL_OPEN = 0x2;
            const int Enable_HOOK = 0x10;
            chooseColor.lStructSize = Marshal.SizeOf(chooseColor);
            chooseColor.lpfnHook = HookProc;
            chooseColor.Flags = RGBINIT | FULL_OPEN | Enable_HOOK;
            chooseColor.lpCustColors = Marshal.AllocCoTaskMem(16 * sizeof(int));
            chooseColor.rgbResult = 0x00ffffff & color.ToArgb();

            alphaSlider = new TrackBar { Minimum = 0, Maximum = 255, Height = 200, TickStyle = TickStyle.TopLeft };
            alphaSlider.Orientation = Orientation.Vertical;
            alphaSlider.Left -= 10;
            alphaSlider.Value = color.A;
            alphaText = new TextBox { Width = 25, Height = 50, Top = 202, Text = Convert.ToString(color.A), MaxLength = 3 };
            alphaPanel = new Panel { BorderStyle = BorderStyle.None, Width = 25, Height = 268 };
            alphaPanel.Controls.Add(alphaSlider);
            alphaPanel.Controls.Add(alphaText);

            alphaText.KeyDown += (sender, args) =>
            {
                args.SuppressKeyPress = (args.KeyValue < 37 || args.KeyValue > 40) && args.KeyValue != 46 && args.KeyValue != 8
                    && !char.IsDigit((char)args.KeyValue);
            };
            alphaText.KeyUp += (sender, args) =>
            {
                pressed = false;
                int value;
                if (int.TryParse(alphaText.Text, out value) && alphaSlider.Value != value)
                {
                    if (value < 0)
                    {
                        value = 0;
                        alphaText.Text = Convert.ToString(value);
                    }
                    else if (value > 255)
                    {
                        value = 255;
                        alphaText.Text = Convert.ToString(value);
                    }
                    alphaSlider.Value = value;
                }
                pressed = true;
            };

            alphaSlider.ValueChanged += (sender, args) =>
            {
                if (pressed)
                {
                    alphaText.Text = Convert.ToString(alphaSlider.Value);
                }
            };
        }

        public Color SelectedColor { get; private set; }

        public bool Show()
        {
            bool result = WinApi.ChooseColor(ref chooseColor);
            if (result)
            {
                int value;
                if (int.TryParse(alphaText.Text, out value))
                {
                    SelectedColor = Color.FromArgb((value << 24) | chooseColor.rgbResult);
                }
            }
            return result;
        }

        private IntPtr HookProc(IntPtr hWnd, UInt16 msg, Int32 wParam, Int32 lParam)
        {
            const int INIT_DIALOG = 0x0110;
            const int RESIZE_DIALOG = 0x0005;

            if (hWnd == IntPtr.Zero)
            {
                return IntPtr.Zero;
            }

            Rectangle windowRect = WinApi.GetClientRectangle(hWnd);
            Rectangle panelRect = alphaPanel.ClientRectangle;
            switch (msg)
            {
                case INIT_DIALOG:
                    WinApi.SetForegroundWindow(hWnd);
                    WinApi.SetParent(alphaPanel.Handle, hWnd);
                    Rectangle screenSize = Screen.PrimaryScreen.Bounds;
                    int width = windowRect.Width + panelRect.Width + 25;
                    int height = windowRect.Height + 38;
                    WinApi.MoveWindow(hWnd, screenSize.Width / 2 - width / 2, screenSize.Height / 2 - height / 2,
                        width, height, true);
                    break;
                case RESIZE_DIALOG:
                    WinApi.MoveWindow(alphaPanel.Handle, windowRect.Right - windowRect.Left - panelRect.Width - 10,
                        0, panelRect.Width, panelRect.Height, true);
                    break;
            }

            return IntPtr.Zero;
        }
    }
}