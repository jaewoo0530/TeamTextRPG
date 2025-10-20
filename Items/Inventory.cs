using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG.Items
{
    internal class Inventory
    {
        public List<Item> equipableItems;
        public List<Item> consumableItems;

        public Inventory()
        {
            equipableItems = new List<Item>
            {
                new Item("가죽갑옷", ItemType.방어구, 5, "가죽으로 만들어져 움직이기 편한 갑옷입니다.", false, true),
                new Item("낡은 검", ItemType.무기, 4, "쉽게 볼 수 있는 낡은 검입니다.", false, true),
                new Item("낡은 지팡이", ItemType.무기, 2, "쉽게 볼 수 있는 낡은 지팡이입니다.", false, true),
                new Item("낡은 활", ItemType.무기, 3, "쉽게 볼 수 있는 낡은 활입니다.", false, true),
            };

            consumableItems = new List<Item>()
            {
                new Consumable("체력 포션", ItemType.소모템, 20, "체력을 회복시켜주는 포션입니다.", 3)
            };
        }

        public Item EquipItem(List<Item> items, Item selectedItem)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].isEquip && items[i].ItemType == selectedItem.ItemType)
                {
                    items[i].isEquip = false;
                }
            }

            selectedItem.isEquip = true;

            return selectedItem;
        }

        public Item UnEquipItem(List<Item> items, Item selectedItem)
        {
            selectedItem.isEquip = false;
            return selectedItem;
        }
    }
}
