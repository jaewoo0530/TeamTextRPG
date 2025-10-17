using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG.Entities
{
    internal class Monster: Entity
    {
        public int RewardExp { get; private set; }

        public Monster(string name, int attack, int defense, int maxHp, int hp, int level, int rewardExp) : base(name, attack, defense, maxHp, hp, level)
        {
            RewardExp = rewardExp;
        }

        public Monster(Monster template)
        {
            Name = template.Name;
            Level = template.Level;
            Atk = template.Atk;
            MaxHp = template.MaxHp;
            Hp = template.Hp;
            Def = template.Def;
            RewardExp = template.RewardExp;
        }
    }
}
