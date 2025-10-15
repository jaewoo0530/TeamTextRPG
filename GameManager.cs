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
        public Character Player { get; private set; }

        public UIManager UI { get; private set; }
        public BattleManager Battle { get; private set; }

        private MonsterData monsterData;

        public void StartGame()
        {
            string name = Console.ReadLine();
            JobType job = Enum.TryParse(Console.ReadLine(), out JobType selectJob) ? selectJob : JobType.전사;

            Player = new Character(name, job);

            UI = new UIManager(this);

            UI.Mainmenu();
        }

        public void StartBattle()
        {

        }
    }
}
