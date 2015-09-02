using System;
using Antlr4.Runtime.Dfa;

namespace PoeHUD.Hud.Settings
{
    public sealed class FileNode
    {
        public FileNode()
        {
        }

        public FileNode(string value)
        {
            Value = value;
        }

        public Action OnFileChanged;

        private string value;

        public string Value
        {
            get { return value; }
            set
            {
                this.value = value;
                OnFileChanged?.Invoke();
            }
        }

        public static implicit operator string(FileNode node)
        {
            return node.Value;
        }

        public static implicit operator FileNode(string value)
        {
            return new FileNode(value);
        }
    }
}