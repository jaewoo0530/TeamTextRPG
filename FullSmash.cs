using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    internal class FullSmash : Skill
    {
        private int manaCost = 30;

        public FullSmash(GameManager gameManager) : base(gameManager) { }

        public override void Execute(Monster? target = null)
        {
            if (Player.Mp < manaCost)
            {
                return;
            }

            if (Monsters == null || Monsters.Count == 0)
            {
                return;
            }

            int damage = Player.Atk * 10;
            Player.Mp -= manaCost;

            gameManager.PlayerSkillAttackAll(damage);
        }
    }
}
