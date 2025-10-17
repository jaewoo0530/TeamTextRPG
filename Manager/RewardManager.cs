using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTextRPG.Entities;
using TeamTextRPG.Items;

namespace TeamTextRPG.Manager
{
    internal class RewardManager
    {
        private readonly Random random;
        private readonly List<Item> items;

        public RewardManager(List<Item> allItems)
        {
            random = new Random();
            items = allItems;       //전체아이템 리스트 참조
        }

        public Item GiveReward()
        {
            int randomDrop = random.Next(0, 100);

            if (randomDrop < 50)        //50확률
            {
                var availableItems = items      //가졌나 안가졌나 판단
                    .Where(i => !i.isHave)
                    .ToList();

                if (availableItems.Count == 0)
                {
                    return null;
                }

                Item reward = availableItems[random.Next(availableItems.Count)];
                reward.isHave = true;       //주고 true
                return reward;
            }

            return null;    //실패시 null반환
            
        }
    }

}
