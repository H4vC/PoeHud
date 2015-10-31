using System;
using System.Collections.Generic;
using PoeHUD.Framework.Helpers;
using PoeHUD.Hud.Interfaces;
using SharpDX;

namespace PoeHUD.Hud
{
    public class PluginPanel : IPanelChild
    {
        private readonly List<IPanelChild> children = new List<IPanelChild>();
        private readonly Direction direction;

        private Func<Vector2> startDrawPointFunc;

        public PluginPanel(Func<Vector2> startDrawPointFunc, Direction direction = Direction.Down)
            : this(direction)
        {
            this.startDrawPointFunc = startDrawPointFunc;
        }

        public PluginPanel(Direction direction = Direction.Down)
        {
            this.direction = direction;
            Margin = new Vector2(0, 0);
        }

        public Size2F Size
        {
            get
            {
                if (children.Count > 0)
                {
                    switch (direction)
                    {
                        case Direction.Down:
                            return GetVerticalSize();
                        case Direction.Left:
                            return GetHorizontalSize();
                    }
                }
                return new Size2F(0, 0);
            }
                
            }

        private Size2F GetHorizontalSize()
        {
            float maxheight = 0f;
            float width = 0f;
            foreach (IPanelChild child in children)
            {
                width += child.Size.Width + child.Margin.X;
                float height = child.Size.Height + child.Margin.Y;

                if (height > maxheight)
                {
                    maxheight = height;
                }
            }
            return new Size2F(width, maxheight);
        }

        private Size2F GetVerticalSize()
        {
            float maxWidth = 0f;
            float height = 0f;
            foreach (IPanelChild child in children)
            {
                height += child.Size.Height + child.Margin.Y;
                float width = child.Size.Width + child.Margin.X;
                if (width > maxWidth)
                {
                    maxWidth = width;
                }
            }
            return new Size2F(maxWidth, height);
        }


        public Func<Vector2> StartDrawPointFunc
        {
            get
            {
                return startDrawPointFunc;
            }
            set
            {

                startDrawPointFunc = value;
            }
        }
        public Vector2 Margin { get; }

        public IEnumerable<IPlugin> GetPlugins()
        {
            foreach (IPanelChild panelChild in children)
            {
                if (panelChild is IPlugin)
                    yield return panelChild as IPlugin;
                if (panelChild is PluginPanel)
                {
                    IEnumerable<IPlugin> insideplugins = (panelChild as PluginPanel).GetPlugins();
                    foreach (IPlugin plugin in insideplugins)
                    {
                        yield return plugin;
                    }
                }
            }
        }

        public void AddChildren(IPanelChild child)
        {
            children.Add(child);
            int index = children.Count - 1;

            switch (direction)
            {
                case Direction.Down:
                    child.StartDrawPointFunc = () => ChoosingStartDrawPoint(index, prevChild => prevChild.StartDrawPointFunc().Translate(prevChild.Margin.X, prevChild.Size.Height + prevChild.Margin.Y));
                    break;
                case Direction.Left:
                    child.StartDrawPointFunc = () => ChoosingStartDrawPoint(index, prevChild => prevChild.StartDrawPointFunc().Translate(-prevChild.Margin.X - prevChild.Size.Width, prevChild.Margin.Y));
                    break;
            }
        }

        private Vector2 ChoosingStartDrawPoint(int index, Func<IPanelChild, Vector2> calcStarPoint)
        {
            if (index > 0)
            {
                IPanelChild prevChild = children[index - 1];
                return calcStarPoint(prevChild);
            }
            return startDrawPointFunc();
        }
    }

    public enum Direction
    {
        Down,
        Left
    }
}