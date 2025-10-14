using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    internal class Living
    {

        private int hp;

        public string Name { get; protected set; }
        public int Attack { get; protected set; }
        public int Defense { get; protected set; }
        public int Level { get; protected set; }
        public int Hp 
        { 
            get=> hp;
            protected set
            {
                hp = value;
                if (hp < 0)
                    hp = 0;
            } 
        }
        public int Exp { get; protected set; }

        public Living(string name, int attack, int defense, int hp, int level)
        {
            Name = name;
            Attack = attack;
            Defense = defense;
            Hp = hp;
            Level = level;
        }

        public virtual int CalculateDamage()
        {
            return Attack;
        }

        public void TakeDamage(int damage)
        {
            int finalDamage = Math.Max(damage-Defense, 0);
            Hp -= finalDamage;
            
        }
        // 죽음판단
        public bool IsDead => Hp <= 0;

    }
}
