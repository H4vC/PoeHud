using System.Collections.Generic;
using System.Linq;

using PoeHUD.Hud.UI;

using SharpDX;

namespace PoeHUD.Hud.Menu
{
    public abstract class MenuItem
    {
        public readonly List<MenuItem> Children;

        public bool IsVisible;

        private MenuItem currentHover;

        protected MenuItem()
        {
            Children = new List<MenuItem>();
        }

        public RectangleF Bounds { get; set; }

        public abstract int DesiredWidth { get; }

        public abstract int DesiredHeight { get; }

        public virtual void AddChild(MenuItem item)
        {
            float x = Bounds.X + Bounds.Width;
            float y = Bounds.Y + Children.Sum(current => current.Bounds.Height);
            item.Bounds = new RectangleF(x, y, item.DesiredWidth, item.DesiredHeight);
            Children.Add(item);
        }

        public void OnEvent(MouseEventID id, Vector2 pos)
        {
            if (id == MouseEventID.MouseMove)
            {
                if (TestBounds(pos))
                {
                    HandleEvent(id, pos);
                    if (currentHover != null)
                    {
                        currentHover.SetHovered(false);
                        currentHover = null;
                    }
                    return;
                }
                if (currentHover != null)
                {
                    if (currentHover.TestHit(pos))
                    {
                        currentHover.OnEvent(id, pos);
                        return;
                    }
                    currentHover.SetHovered(false);
                }
                MenuItem childAt = Children.FirstOrDefault(current => current.TestHit(pos));
                if (childAt != null)
                {
                    childAt.SetHovered(true);
                    currentHover = childAt;
                    return;
                }
                currentHover = null;
            }
            else
            {
                if (TestBounds(pos))
                {
                    HandleEvent(id, pos);
                }
                else
                {
                    currentHover?.OnEvent(id, pos);
                }
            }
        }
        public abstract void Render(Graphics graphics, MenuSettings settings);

        public virtual void SetHovered(bool hover)
        {
            Children.ForEach(x => x.SetVisible(hover));
        }

        public void SetVisible(bool visible)
        {
            IsVisible = visible;
            if (!visible)
            {
                Children.ForEach(x => x.SetVisible(false));
            }
        }

        public bool TestHit(Vector2 pos)
        {
            return IsVisible && (TestBounds(pos) || Children.Any(current => current.TestHit(pos)));
        }

        protected abstract void HandleEvent(MouseEventID id, Vector2 pos);

        protected virtual bool TestBounds(Vector2 pos)
        {
            return Bounds.Contains(pos);
        }
    }
}