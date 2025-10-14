using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    internal class Monster: Living
    {
        public Monster(string name, int basePower, int baseDefense, int hp): base(name, basePower, baseDefense, hp)
        {
        }
    }
}
