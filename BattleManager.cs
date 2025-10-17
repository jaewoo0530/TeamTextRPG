using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace TeamTextRPG.Manager
{
    internal class BattleManager
    {
        private GameManager gameManager;

        Random random = new Random();

        public bool isCritical = false;
        public bool isEvaded = false;
        public bool isEnoughMana;

        public BattleManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        private Character Player => gameManager.Player;

        public int CalculateDamage(Entity attacker, Entity target)
        {
            int min = (int)Math.Round(attacker.Atk * 0.9f);
            int max = (int)Math.Round(attacker.Atk * 1.1f) + 1;
            int randDamage = random.Next(min, max);
            int finalDamage = Math.Max(randDamage - target.Def, 0);

            if (random.Next(0, 100) < 10)
            {
                isEvaded = true;
                return 0;
            }
            else if (random.Next(0, 100) < 15)
            {
                int CriticalDamage = (int)Math.Round(randDamage * 1.6f);
                isCritical = true;
                return CriticalDamage;
            }
            else
            {
                return finalDamage;
            }
        }
        public void UseSkill(int skillNumber, Monster? target = null)
        {
            Skill skill;

            if (skillNumber == 1)
            {
                skill = new FullpowerAttack(gameManager);
                if (Player.Mp < skill.manaCost)
                {
                    isEnoughMana = false;
                    return;
                }
            }
            else if (skillNumber == 2)
            {
                skill = new FullSmash(gameManager);
                if (Player.Mp < skill.manaCost)
                {
                    isEnoughMana = false;
                    return;
                }
            }
            else
            {
                isEnoughMana = false;
                return;
            }

            isEnoughMana = true;

            skill.Execute(target);
        }

    }
}