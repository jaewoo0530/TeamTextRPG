using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    internal class Character : Living
    {
        public enum JobType { 전사, 궁수, 마법사 }

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

        public Character(string name, string job)
        {
            Job = job.ToString();
            Level = 1;

            switch (job)
            {
                case "전사": Atk = 5; Def = 10; break;
                case "궁수": Atk = 10; Def = 5; break;
                case "마법사": Atk = 3; Def = 5; break;
            }
        }

        public override int CalculateDamage()
        {
            int min = (int)(Atk * 0.9f);
            int max = (int)(Atk * 1.1f) + 1;
            int randDamage = random.Next(min, max);
            return randDamage;
        }
    }
}
