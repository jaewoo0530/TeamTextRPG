using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    internal class Monster: Living
    {

        public Monster(string name, int attack, int defense, int hp): base(name, attack, defense, hp)
        { }

    }
}
