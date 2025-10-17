using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTextRPG.Entities;

namespace TeamTextRPG.Items
{
    internal class Consumable : Item
    {
        public int HealAmount { get; private set; }

        public Consumable(string name, ItemType itemType, int value, string info, int count) : base(name, itemType, value, info, count)
        {
            HealAmount = value;
        }

        public override void Use(Character player)
        {
            player.Heal(HealAmount);
        }
    }
}
