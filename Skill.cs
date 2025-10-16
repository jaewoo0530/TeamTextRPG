using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    internal class Skill
    {
        private GameManager gameManager;

        private int manaCount = 0;

        public Skill(GameManager gameManger)
        {
            this.gameManager = gameManger;
        }

        private Character Player => gameManager.Player;
        private List<Monster> Monsters => gameManager.Monsters;

        public void FullpowerAttack(Monster target)//마나 15를 소모하여, 전력으로 돌진해서 적 하나에게 공력력의 5배의 피해를 입힙니다. 
        {
            manaCount = 15;

            if (Player.Mp < 15)
            {
                return;
            }
            else
            {
                int damage = (int)(Player.Atk * 5);
                
                Player.Mp -= manaCount;

                gameManager.PlayerSkillAttack(target, damage);
            }
        }

        public void FullSmash()//마나 30를 소모하고 적 전체에게 공격력의 10배의 피해를 입힌다
        {
            manaCount = 30;

            if (Player.Mp < manaCount)
                return;

            if (Monsters == null || Monsters.Count == 0)
                return;

            Player.Mp -= manaCount;

            int damage = (int)(Player.Atk * 10);

            gameManager.PlayerSkillAttackAll(damage);
        }
    }
}
