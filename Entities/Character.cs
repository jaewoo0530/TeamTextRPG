using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG.Entities
{
    public enum JobType { 전사, 궁수, 마법사 }
    internal class Character : Entity
    {
        public JobType Job { get; }

        public int Gold { get; private set; } = 500;
        public int Exp { get; private set; } = 0;

        private int mp;
        public int Mp
        {
            get => mp;
            set
            {
                if (value < 0)
                    mp = 0;
                else if (value > 100)
                    mp = 100;
                else
                    mp = value;
            }
        }

        public Character(string name, JobType job)
        {
            Name = name;
            Job = job;
            Level = 1;

            switch (job)
            {
                case JobType.전사: Atk = 5; Def = 10; MaxHp = 100; break;
                case JobType.궁수: Atk = 10; Def = 5; MaxHp = 100; break;
                case JobType.마법사: Atk = 3; Def = 5; MaxHp = 100; break;
            }

            Hp = MaxHp;
            Mp = 100;
        }

        public void AddExp(int value)
        {
            Exp += value;
            LevelUp();
        }

        public void LevelUp()
        {
            while (Exp >= 100)
            {
                Exp -= 100;
                Level++;
                Atk++;
                Def++;
            }
        }
    }
}
