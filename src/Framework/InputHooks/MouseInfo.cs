using System.Windows.Forms;
using PointDX = SharpDX.Point;
using PointGdi = System.Drawing.Point;

namespace PoeHUD.Framework.InputHooks
{
    public sealed class MouseInfo
    {
        public MouseInfo(MouseButtons buttons, PointGdi position, int wheelDelta)
        {
            Buttons = buttons;
            Position = new PointDX(position.X, position.Y);
            WheelDelta = wheelDelta;
        }

        public MouseButtons Buttons { get; private set; }

        public PointDX Position { get; private set; }

        public int WheelDelta { get; private set; }

        public bool Handled { get; set; }
    }
}