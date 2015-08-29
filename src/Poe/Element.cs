using System;
using System.Collections.Generic;
using System.Linq;
using PoeHUD.Framework;

using SharpDX;

namespace PoeHUD.Poe.UI
{
    public  class Element : RemoteMemoryObject
    {
        public const int OffsetBuffers = 0x788;
        // dd id
        // dd (something zero)
        // 16 dup <128-bytes structure>
        // then the rest is

        public int vTable => M.ReadInt(Address + 0);

        public Element Root => base.ReadObject<Element>(Address + 0x5c + OffsetBuffers);

        public Element Parent => base.ReadObject<Element>(Address + 0x60 + OffsetBuffers);

        public float X => M.ReadFloat(Address + 0x64 + OffsetBuffers);

        public float Y => M.ReadFloat(Address + 0x68 + OffsetBuffers);

        public float Width => M.ReadFloat(Address + 0xFC + OffsetBuffers);

        public float Height => M.ReadFloat(Address + 0x100 + OffsetBuffers);

        public int ChildCount => (M.ReadInt(Address + 20 + OffsetBuffers) - M.ReadInt(Address + 16 + OffsetBuffers))/4;

        public bool IsVisibleLocal => (M.ReadInt(Address + 0x7e0) & 1) == 1;

        public bool IsVisible
        {
            get { return IsVisibleLocal && GetParentChain().All(current => current.IsVisibleLocal); }
        }

        public List<Element> Children => GetChildren<Element>();


        protected List<T> GetChildren<T> () where T: Element,new()
        {
            const int listOffset = 0x10 + OffsetBuffers;
            var list = new List<T>();
            if (M.ReadInt(Address + listOffset + 4) == 0 || M.ReadInt(Address + listOffset) == 0 ||
                ChildCount > 1000)
            {
                return list;
            }
            for (int i = 0; i < ChildCount; i++)
            {
                list.Add(base.GetObject<T>(M.ReadInt(Address + listOffset, i * 4)));
            }
            return list;
        }

        private IEnumerable<Element> GetParentChain()
        {
            var list = new List<Element>();
            var hashSet = new HashSet<Element>();
            Element root = Root;
            Element parent = Parent;
            while (!hashSet.Contains(parent) && root.Address != parent.Address && parent.Address != 0)
            {
                list.Add(parent);
                hashSet.Add(parent);
                parent = parent.Parent;
            }
            return list;
        }

        public Vector2 GetParentPos()
        {
            float num = 0;
            float num2 = 0;
            foreach (Element current in GetParentChain())
            {
                num += current.X;
                num2 += current.Y;
            }
            return new Vector2(num, num2);
        }

        public RectangleF GetClientRect()
        {
            var vPos = GetParentPos();
            float width = Game.IngameState.Camera.Width;
            float height = Game.IngameState.Camera.Height;
            float ratioFixMult = width/height/1.6f;
            float xScale = width/2560f/ratioFixMult;
            float yScale = height/1600f;

            float num = (vPos.X + X)*xScale;
            float num2 = (vPos.Y + Y)*yScale;
            return new RectangleF(num, num2, xScale*Width, yScale*Height);
        }

        public Element GetChildFromIndices(params int[] indices)
        {
            Element poe_UIElement = this;
            for (int i = 0; i < indices.Length; i++)
            {
                int index = indices[i];
                poe_UIElement = poe_UIElement.GetChildAtIndex(index);
                if (poe_UIElement == null)
                {
                    return poe_UIElement;
                }
            }
            return poe_UIElement;
        }

        public Element GetChildAtIndex(int index)
        {
            if (index >= ChildCount)
            {
                return null;
            }
            return base.GetObject<Element>(M.ReadInt(Address + 0x10 + OffsetBuffers, index * 4));
        }

    }
}