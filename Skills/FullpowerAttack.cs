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
        public FullpowerAttack(GameManager gameManager) : base(gameManager)
        {
            manaCost = 15;
        }

        public override void Execute(Monster? target = null)
        {
            int damage = Player.Atk * 5;
            Player.Mp -= manaCost;

            gameManager.PlayerSkillAttack(target, damage);
        }
    }
}
