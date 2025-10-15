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

        public void StartGame()
        {
            Console.WriteLine("이름");
            string name = Console.ReadLine();
            Console.WriteLine("직업");
            JobType job = Enum.TryParse(Console.ReadLine(), out JobType selectJob) ? selectJob : JobType.전사;

            Player = new Character(name, job);
            Battle = new BattleManager(this);
            UI = new UIManager(this);

            UI.MainmenuUI();
        }

        public void StartBattle()
        {
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

            }
            else
            {

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
                    int beforePlayerHp = player.Hp;
                    int damage = Battle.CalculateDamage(Monsters[i], Player);
                    Monsters[i].Attack(player, damage);
                    UI.MonsterAttackUI(Monsters[i], damage, beforePlayerHp);
                    Battle.isCritical = false;
                    Battle.isEvaded = false;
                }
            }
        }

        public void BattleWin()
        {
        }
    }
}
