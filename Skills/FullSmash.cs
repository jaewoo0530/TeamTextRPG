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
    internal class FullSmash : Skill
    {
        public FullSmash(GameManager gameManager) : base(gameManager)
        {
            manaCost = 30;
        }

        public override void Execute(Monster? target = null)
        {
            int damage = Player.Atk * 10;
            Player.ReduceMp(manaCost);

            gameManager.PlayerSkillAttackAll(damage);
        }
    }
}
