using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    internal class Living
    {
        public string Name { get; protected set; }
        public int Attack { get; protected set; }
        public int Defense { get; protected set; }
        public int Hp 
        { 
            get=> Hp;
            protected set
            {
                Hp = value;
                if (Hp < 0)
                    Hp = 0;
            } 
        }

        public Living(string name, int attack, int defense, int hp)
        {
            Name = name;
            Attack = attack;
            Defense = defense;
            Hp = hp;
        }

        public virtual int CalculateDamage()
        {
            return Attack;
        }

        public void TakeDamage(int damage)
        {
            int Finaldamage = Math.Max(damage-Defense, 0);
            Hp -= Finaldamage;
            
        }
    }
}
