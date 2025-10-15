using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    public enum JobType { 전사, 궁수, 마법사 }
    internal class Character : Living
    {
        public JobType Job { get; }

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

        public Character(string name, JobType job)
        {
            Job = job;
            Level = 1;

            switch (job)
            {
                case JobType.전사: Atk = 5; Def = 10; break;
                case JobType.궁수: Atk = 10; Def = 5; break;
                case JobType.마법사: Atk = 3; Def = 5; break;
            }

            Hp = 100;
        }
    }
}
