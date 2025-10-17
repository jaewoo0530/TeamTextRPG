using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTextRPG.Entities;

namespace TeamTextRPG.Data
{
    internal class MonsterData
    {
        private Random random = new Random();

        public List<Monster> MonsterDex {get; private set;}

        public MonsterData()
        {
            MonsterDex = new List<Monster>
            {
                new Monster("저글링", 1, 0, 20, 1, 30),
                new Monster("히드라", 3, 0, 40, 3, 40),
                new Monster("뮤탈", 5, 0, 100, 5, 70),
                new Monster("울라리", 10, 0, 200, 10, 100)
            };
        }

        public Monster GetRandomMonster(int stageNumber)
        {
            int index = random.Next(0, stageNumber);
            return new Monster(MonsterDex[index]);
        }
    }
}
