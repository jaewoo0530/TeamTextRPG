using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    internal class ItemData
    {
        private List<Item> items;

        public ItemData()
        {
            items = new List<Item>
            {
                new Item("가죽갑옷", ItemType.방어구, 5, "가죽으로 만들어져 움직이기 편한 갑옷입니다.", false),
                new Item("낡은 검", ItemType.무기, 5, "쉽게 볼 수 있는 낡은 검 입니다.", false),
                new Item("낡은 지팡이", ItemType.무기, 5, "쉽게 볼 수 있는 낡은 지팡이 입니다.", false)
            };
            
        }
    }
}
