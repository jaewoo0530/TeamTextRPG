using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    internal class Inventory
    {
        public void InventoryEquip(List<Item> items)
        {
            while (true)
            {
                string Choice = Console.ReadLine();
                if (int.TryParse(Choice, out int num))
                    continue;
                if (num == 0)
                {
                    //메인메뉴
                }
                if (num < 1 || num > items.Count)
                {
                    continue;
                }
                Item selected = items[num - 1];
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].isEquip == true && items[i].ItemType == selected.ItemType)
                    {
                        items[i].isEquip = false;
                    }
                }
                selected.isEquip = !selected.isEquip;
            }
        }   
    }
}
