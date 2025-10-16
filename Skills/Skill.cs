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
    internal abstract class Skill
    {
        protected GameManager gameManager;
        protected Character Player => gameManager.Player;
        protected List<Monster> Monsters => gameManager.Monsters;

        protected Skill(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        // 스킬 실행 메서드
        // 단일 대상 스킬은 target 필요, 광역 스킬은 target 무시 가능
        public abstract void Execute(Monster? target = null);
    }
}
