using System;
using System.Collections.Generic;
using PoeHUD.Framework;
using PoeHUD.Models;

namespace PoeHUD.Poe.FilesInMemory
{
    public class BaseItemTypes : FileInMemory
    {
        private readonly Dictionary<string, BaseItemType> contents = new Dictionary<string, BaseItemType>();
        private readonly ItemClassesDisplay itemClassesDisplay;
        public BaseItemTypes(Memory m, int address, ItemClassesDisplay itemClassesDisplay) : base(m, address)
        {
            this.itemClassesDisplay = itemClassesDisplay;
        }

        public BaseItemType Translate(string metadata)
        {
            if (!contents.ContainsKey(metadata))
            {
                LoadItemTypes();
            }
            if (!contents.ContainsKey(metadata))
            {
                Console.WriteLine("Key not found in BaseItemTypes: " + metadata);
                return null;
            }
            return contents[metadata];
        }

        private void LoadItemTypes()
        {
            foreach (int i in RecordAddresses())
            {
                string key = M.ReadStringU(M.ReadInt(i));
                var baseItemType = new BaseItemType
                {
                    BaseName = M.ReadStringU(M.ReadInt(i + 0x10)),
                    ClassName = itemClassesDisplay[M.ReadInt(i + 0x4) - 1],
                    Width = M.ReadInt(i + 0x8),
                    Height = M.ReadInt(i + 0xC),
                    DropLevel = M.ReadInt(i + 0x18)
                };
                if (!contents.ContainsKey(key))
                {
                    contents.Add(key, baseItemType);
                }
            }
        }
    }
}