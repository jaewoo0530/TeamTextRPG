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
            Battle = new BattleManager();
            UI = new UIManager(this);

            UI.Mainmenu();
        }

        public void StartBattle()
        {
            int monsterCount = random.Next(1, 5);
            Monsters = new List<Monster>();

            for (int i = 0; i < monsterCount; i++)
            {
                Monsters.Add(monsterData.GetRandomMonster());
            }

            UI.BattleMain(Monsters);
        }

        public void PlayerAttack(Living target)
        {
            int beforeMonsterHp = target.Hp;
            int damage = Battle.CalculateDamage(Player);
            Player.Attack(target, damage);
            UI.PlayerAttack(damage, beforeMonsterHp);
        }

        public void MonsterAttack(Living target)
        {
            for (int i = 0; i < Monsters.Count; i++)
            {
                int beforePlayerHp = target.Hp;
                int damage = Battle.CalculateDamage(Monsters[i]);
                Monsters[i].Attack(target, damage);
                UI.MonsterAttack(damage, beforePlayerHp);
            }
        }

        public void BattleWin()
        {
            Monsters.Clear();
        }
    }
}
