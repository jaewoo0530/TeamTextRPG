using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    internal class GameManager
    {
        private Random random = new Random();
        public Character Player { get; private set; }
        public List<Monster> Monsters { get; private set; }
        public UIManager UI { get; private set; }
        public BattleManager Battle { get; private set; }

        private MonsterData monsterData = new MonsterData();

        private int beforeDungeonHp;

        public void StartGame()
        {
            UI = new UIManager(this);
            Battle = new BattleManager(this);

            string name = UI.CreateName();
            JobType job = UI.CreateJob();

            Player = new Character(name, job);

            UI.MainmenuUI();
        }

        public void StartBattle()
        {
            beforeDungeonHp = Player.Hp;

            int monsterCount = random.Next(1, 5);

            Monsters = new List<Monster>();

            for (int i = 0; i < monsterCount; i++)
            {
                Monsters.Add(monsterData.GetRandomMonster());
            }

            ProcessBattle();
        }

        public void ProcessBattle()
        {
            while (Monsters.Any(m => !m.IsDead) && !Player.IsDead)
            {
                UI.BattleMainUI();
            }

            if (!Player.IsDead)
            {
                BattleWin();
            }
            else
            {
                BattleLose();
            }

            Monsters.Clear();
        }

        public void PlayerAttack(Monster target)
        {
            int beforeMonsterHp = target.Hp;

            int damage = Battle.CalculateDamage(Player, target);
            Player.Attack(target, damage);
            UI.PlayerAttackUI(target, damage, beforeMonsterHp);

            Battle.isCritical = false;
            Battle.isEvaded = false;
        }

        public void MonsterAttack(Character player)
        {
            for (int i = 0; i < Monsters.Count; i++)
            {
                if (Monsters[i].IsDead)
                {
                    continue;
                }
                else
                {
                    if (!Player.IsDead)
                    {
                        int beforePlayerHp = player.Hp;

                        int damage = Battle.CalculateDamage(Monsters[i], Player);
                        Monsters[i].Attack(player, damage);
                        UI.MonsterAttackUI(Monsters[i], damage, beforePlayerHp);

                        Battle.isCritical = false;
                        Battle.isEvaded = false;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public void BattleWin()
        {
            UI.BattlePlayerWinUI(beforeDungeonHp);
        }

        public void BattleLose()
        {
            UI.BattlePlayerLoseUI(beforeDungeonHp);
        }
    }
}
