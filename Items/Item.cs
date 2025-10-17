using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG.Items
{
    public enum ItemType { 방어구, 무기 }
    internal class Item
    {
        public ItemType ItemType { get; set; }

        public string name;

        public int value;
        public string info;
        public bool isEquip;
        public bool isHave;

        public Item(string name, ItemType itemType, int value, string info, bool isEquip, bool isHave)
        {
            this.name = name;
            ItemType = itemType;
            this.value = value;
            this.info = info;
            this.isEquip = isEquip;
            this.isHave = isHave;
        }
    }
}
