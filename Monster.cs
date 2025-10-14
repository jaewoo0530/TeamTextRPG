using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    internal class Monster: Living
    {
        public List<Monster> Monsters { get; private set; }

        private Random random = new Random();

        public Monster(string name, int basePower, int baseDefense, int hp): base(name, basePower, baseDefense, hp)
        {
            Monsters = new List<Monster>
            {
            };
        }

        public Monster GetRandomMonster()
        {
            int index = random.Next(0, Monsters.Count);
            return Monsters[index];
        }
    }
}
