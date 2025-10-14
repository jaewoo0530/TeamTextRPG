using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    internal class Character : Living
    {
        public string Job { get; }
        public int Level { get; private set; } = 1;

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

        public Character(string name, string job, int basePower, int baseDefense) : base(name, job)
        {

        }
    }
}
