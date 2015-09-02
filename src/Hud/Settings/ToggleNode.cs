using System;

namespace PoeHUD.Hud.Settings
{
    public sealed class ToggleNode
    {
        public Action OnValueChanged;
        private bool value;

        public ToggleNode()
        {
        }

        public ToggleNode(bool value)
        {
            Value = value;
        }

        public bool Value
        {
            get { return value; }
            set
            {
                if (this.value != value)
                {
                    this.value = value;
                    OnValueChanged?.Invoke();
                }
            }
        }

        public static implicit operator bool (ToggleNode node)
        {
            return node.Value;
        }

        public static implicit operator ToggleNode(bool value)
        {
            return new ToggleNode(value);
        }
    }
}