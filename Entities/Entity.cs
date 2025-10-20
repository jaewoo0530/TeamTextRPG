using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG.Entities
{
    internal class Entity
    {

        private int hp;

        public string Name { get; protected set; }
        public int Atk { get; protected set; }
        public int Def { get; protected set; }
        public int Level { get; protected set; }
        public int MaxHp { get; protected set; }
        public int Hp
        {
            get => hp;
            set
            {
                if (value < 0)
                    hp = 0;
                else if (value > MaxHp)
                    hp = MaxHp;
                else
                    hp = value;
            }
        }

        // 🔹 몬스터용 생성자 (값 다 넘김)
        public Entity(string name, int attack, int defense, int maxHp, int level)
        {
            Name = name;
            Atk = attack;
            Def = defense;
            MaxHp = maxHp;
            Hp = maxHp;
            Level = level;
        }

        // 🔹 캐릭터용 기본 생성자 (나중에 직접 세팅)
        public Entity() { }

        public void Attack(Entity target, int damage)
        {
            target.TakeDamage(damage);
        }

        private void TakeDamage(int damage)
        {
            Hp -= damage;
        }

        // 죽음판단
        public bool IsDead => Hp <= 0;
    }
}
