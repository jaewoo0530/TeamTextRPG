using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TeamTextRPG.Entities;
using TeamTextRPG.Manager;

namespace TeamTextRPG.Skills
{
    internal class FullpowerAttack : Skill
    {
        private int manaCost = 15;

        public FullpowerAttack(GameManager gameManager) : base(gameManager) { }

        public override void Execute(Monster? target = null)
        {
            if (target == null)
            {
                return;
            }

            if (Player.Mp < manaCost)
            {
                return;
            }

            int damage = Player.Atk * 5;
            Player.Mp -= manaCost;

            gameManager.PlayerSkillAttack(target, damage);
        }
    }
}
