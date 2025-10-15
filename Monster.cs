using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    internal class Monster: Living
    {

        public Monster(string name, int attack, int defense, int hp, int level): base(name, attack, defense, hp, level)
        { }

        public Monster(Monster template)
        {
            Name = template.Name;
            Level = template.Level;
            Atk = template.Atk;
            Hp = template.Hp;
            Def = template.Def;
        }
    }
}
