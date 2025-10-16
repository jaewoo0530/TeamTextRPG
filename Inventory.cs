using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    internal class Inventory
    {
        public void InventoryEquip(List<Item> items, int choice)
        {
                if (choice < 1 || choice > items.Count)

                    return;

                Item selected = items[choice - 1];
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].isEquip && items[i].ItemType == selected.ItemType)
                    {
                        items[i].isEquip = false;
                    }
                }
                selected.isEquip = !selected.isEquip;
        }   
    }
}
