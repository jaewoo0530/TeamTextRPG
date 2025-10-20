using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTextRPG.Items;

namespace TeamTextRPG.Data
{
    internal class RewardData
    {
        public List<Item> Items { get; private set; }

        public RewardData()
        {
            Items = new List<Item>
            {
                new Item("강철갑옷", ItemType.방어구, 8, "강철로 만들어져 튼튼한 갑옷입니다.", false, false),

                new Item("강철 검", ItemType.무기, 8, "튼튼한 강철로 만들어진 검입니다.", false, false),
                new Item("고목 지팡이", ItemType.무기, 4, "오래된 고목으로 만들어진 지팡이입니다.", false, false),
                new Item("롱보우", ItemType.무기, 6, "비거리와 위력이 강해진 긴 활입니다.", false, false)
            };
        }
    }
}
