using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG.Items
{
    internal class Inventory
    {
        public List<Item> items;

        public Inventory()
        {
            items = new List<Item>
            {
                new Item("가죽갑옷", ItemType.방어구, 5, "가죽으로 만들어져 움직이기 편한 갑옷입니다.", false, true),
                new Item("낡은 검", ItemType.무기, 4, "쉽게 볼 수 있는 낡은 검 입니다.", false, true),
                new Item("낡은 지팡이", ItemType.무기, 8, "쉽게 볼 수 있는 낡은 지팡이입니다.", false, true),
                new Item("낡은 활", ItemType.무기, 2, "쉽게 볼 수 있는 낡은 지팡이 입니다.", false, true)
            };
        }

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
