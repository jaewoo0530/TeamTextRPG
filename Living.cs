using System;
using System.Collections;
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
        public int Atk { get; protected set; }
        public int Def { get; protected set; }
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


        // 🔹 몬스터용 생성자 (값 다 넘김)
        public Living(string name, int attack, int defense, int hp)
        {
            Name = name;
            Atk = attack;
            Def = defense;
            Hp = hp;
        }

        // 🔹 캐릭터용 기본 생성자 (나중에 직접 세팅)
        public Living() { }

        public virtual int CalculateDamage()
        {
            return Atk;
        }

        public void TakeDamage(int damage)
        {
            int finalDamage = Math.Max(damage-Def, 0);
            Hp -= finalDamage;
            
        }
        // 죽음판단
        public bool IsDead => Hp <= 0;

    }
}
