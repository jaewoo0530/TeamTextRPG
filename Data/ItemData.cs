using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTextRPG.Items;

namespace TeamTextRPG.Data
{
    internal class ItemData
    {
        public List<Item> Items { get; private set; }

        public ItemData()
        {
            Items = new List<Item>
            {
                new Item("가죽갑옷", ItemType.방어구, 5, "가죽으로 만들어져 움직이기 편한 갑옷입니다.", false, true),
                new Item("강철갑옷", ItemType.방어구, 8, "가죽으로 만들어져 움직이기 편한 갑옷입니다.", false, false),

                new Item("낡은 검", ItemType.무기, 4, "쉽게 볼 수 있는 낡은 검 입니다.", false, true),
                new Item("낡은 지팡이", ItemType.무기, 8, "쉽게 볼 수 있는 낡은 지팡이입니다.", false, true),
                new Item("낡은 활", ItemType.무기, 2, "쉽게 볼 수 있는 낡은 지팡이 입니다.", false, true),

                new Item("강철 검", ItemType.무기, 8, "튼튼한 강철로 만들어진 검입니다.", false, false),
                new Item("고목 지팡이", ItemType.무기, 12, "오래된 고목으로 만들어진 지팡이입니다.", false, false),
                new Item("롱보우", ItemType.무기, 4, "비거리와 위력이 강해진 긴 활입니다.", false, false)
            };
        }
    }
}
