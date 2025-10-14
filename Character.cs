using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    internal class Character : Living
    {
        private Random random = new Random();
        public string Job { get; }

        private int stamina = 100;
        public int Stamina
        {
            get { return stamina; }
            private set
            {
                if (value < 0)
                    stamina = 0;
                else if (value > 100)
                    stamina = 100;
                else
                    stamina = value;
            }
        }

        public int Gold { get; private set; } = 500;
        public int Exp { get; private set; } = 0;

        public Character(string name, string job, int basePower, int baseDefense, int hp, int level) : base(name, basePower, baseDefense, hp, level)
        {
            Job = job;
        }

        public override int CalculateDamage()
        {
            int min = (int)(Attack * 0.9f);
            int max = (int)(Attack * 1.1f) + 1;
            int randDamage = random.Next(min, max);
            return randDamage;
        }
    }
}
