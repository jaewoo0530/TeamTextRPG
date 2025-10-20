using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTextRPG.Data;
using TeamTextRPG.Entities;
using TeamTextRPG.Items;

namespace TeamTextRPG.Manager
{
    internal class RewardManager
    {
        private RewardData itemData;
        private readonly Random random;
        private readonly List<Item> items;

        public RewardManager(RewardData sharedData)
        {
            itemData = sharedData;
            random = new Random();
            items = itemData.Items;
        }

        public Item GiveReward()
        {
            var availableItems = items
                .Where(i => !i.isHave)
                .ToList();

            if (availableItems.Count == 0)
            {
                return null;
            }

            Item reward = availableItems[random.Next(availableItems.Count)];
            reward.isHave = true;
            return reward;
        }
    }
}
