using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    public enum ItemType { 방어구, 무기 }
    internal class Item
    {
        public ItemType ItemType { get; set; }

        public string name;

        public int value;
        public string info;
        public bool isEquip;

        public Item(string name, ItemType itemType, int value, string info, bool isEquip)
        {
            this.name = name;
            ItemType = itemType;
            this.value = value;
            this.info = info;
            this.isEquip = false;
        }
    }
}
