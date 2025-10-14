using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    internal class MonsterData
    {
        private Random random = new Random();

        public List<Monster> Monsters { get; private set; }

        public MonsterData()
        {
            Monsters = new List<Monster>
            {
                new Monster("저글링", 3, 0, 20, 1),
                new Monster("히드라", 7, 3, 40, 3),
                new Monster("뮤탈", 10, 6, 100, 5),
                new Monster("울라리", 25, 15, 200, 10)
            };
        }

        public Monster GetRandomMonster()
        {
            int index = random.Next(0, Monsters.Count);
            return Monsters[index];
        }
    }
}
