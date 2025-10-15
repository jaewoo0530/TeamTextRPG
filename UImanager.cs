using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace TeamTextRPG
{
    internal class UIManager
    {
        private GameManager gameManager;
        public UIManager(GameManager gameManager) // 생성자
        {
            this.gameManager = gameManager;
        }
        private Character Player => gameManager.Player;
        public int Input()
        {
            Console.WriteLine("원하시는 행동을 입력해주세요.\n >>");
            return int.TryParse(Console.ReadLine(), out int choice) ? choice : -1;
        }


        public void MainmenuUI() // 게임 시작 화면
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n이제 전투를 시작할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("1. 상태 보기\n2. 전투 시작");
                Console.WriteLine();
                int choice = Input();
                if (choice == 1)
                {
                    StatusUI();
                }
                else if (choice == 2)
                {
                    gameManager.StartBattle();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(800);
                }
            }
        }

        public void StatusUI() // 1. 상태 보기
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                Console.WriteLine();
                Console.WriteLine($"Lv. {Player.Level}");
                Console.WriteLine($"{Player.Name} ({Player.Job})");
                Console.WriteLine($"공격력 : {Player.Atk}");
                Console.WriteLine($"방어력 : {Player.Def}");
                Console.WriteLine($"체 력 : {Player.Hp}");
                Console.WriteLine($"Gold : {Player.Gold} G");
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
                int choice = Input();
                if (choice == 0)
                {
                    MainmenuUI();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(800);
                }
            }
        }

        public void BattleMainUI(List<Monster> monsters) // 2. 전투 시작 // 1~4 마리의 몬스터가 랜덤으로 출현
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Battle!!");
                Console.WriteLine();
                for (int i = 0; i < monsters.Count; i++)
                {
                    if (monsters[i].Hp == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine($"Lv.{monsters[i].Level} {monsters[i].Name} Dead");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.WriteLine($"Lv.{monsters[i].Level} {monsters[i].Name} HP {monsters[i].Hp}");
                    }
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("[내정보]");
                Console.WriteLine($"Lv.{Player.Level} {Player.Name} ({Player.Job})");
                Console.WriteLine($"HP {Player.Hp}/100");
                Console.WriteLine();
                Console.WriteLine("1. 공격");
                Console.WriteLine();
                Console.WriteLine("원하시는 행동을 입력해주세요.\n >>");
                int choice = Input();
                if (choice == 1)
                {
                    PlayerAttackUI(damage, beforeMonsterHp);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(800);
                }
            }
        }

        public void PlayerAttackUI(int damage, int beforeMonsterHp) // 플레이어 공격

        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Battle!!");
                Console.WriteLine($"{Player.Name}의 공격!");
                Console.WriteLine($"Lv.{monster.Name}을(를) 맞췄습니다. [데미지 : {damage}]");
                Console.WriteLine();
                Console.WriteLine($"Lv.{monster.Name}");
                if (monster.Hp == 0)
                {
                    Console.WriteLine($"HP {beforeMonsterHp} -> Dead");
                }
                else
                {
                    Console.WriteLine($"HP {beforeMonsterHp} -> {monster.Hp}");
                }
                Console.WriteLine();
                Console.WriteLine("0. 다음");
                Console.WriteLine();
                Console.WriteLine(">>");
                int choice = Input();
                if (choice == 0)
                {
                    MonsterAttackUI(damage, beforeMonsterHp);
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(800);
                }
            }
        }

        public void MonsterAttackUI(int damage, int beforePlayerHp) // 몬스터 공격
                                                                    // 몬스터의 한 차례씩 3회 공격
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Battle!!");
                Console.WriteLine($"{monster.Name}의 공격!");
                Console.WriteLine($"Lv.{Player.Name}을(를) 맞췄습니다. [데미지 : {damage}]");
                Console.WriteLine();
                Console.WriteLine($"Lv.{Player.Name}");
                if (Player.Hp != 0)
                {
                    Console.WriteLine($"HP {beforePlayerHp} -> {Player.Hp}");
                    Console.WriteLine();
                    Console.WriteLine("0. 다음");
                    Console.WriteLine();
                    Console.WriteLine(">>");
                }
                else
                {
                    return;
                }
            }
        }

        public void BattlePlayerWinUI(int damage, int beforeDungeonHp) // 플레이어 승리 결과창
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Battle!! - Result");
                Console.WriteLine();
                Console.WriteLine("Victory");
                Console.WriteLine();
                Console.WriteLine($"던전에서 몬스터 {monster}마리를 잡았습니다.");
                Console.WriteLine();
                Console.WriteLine($"Lv.{Player.Name}");
                Console.WriteLine($"HP {beforeDungeonHp} -> {Player.Hp}");
                Console.WriteLine();
                Console.WriteLine("0. 다음");
                Console.WriteLine();
                Console.WriteLine(">>");
                int choice = Input();
                if (choice == 0)
                {
                    MainmenuUI();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(800);
                }
            }
        }
        public void BattlePlayerLoseUI(int damage, int beforeDungeonHp)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Battle!! - Result");
                Console.WriteLine();
                Console.WriteLine("You Lose");
                Console.WriteLine();
                Console.WriteLine($"던전에서 몬스터 {monster}마리를 잡았습니다.");
                Console.WriteLine();
                Console.WriteLine($"Lv.{Player.Name}");
                Console.WriteLine($"HP {beforeDungeonHp} -> 0");
                Console.WriteLine();
                Console.WriteLine("0. 다음");
                Console.WriteLine();
                Console.WriteLine(">>");
                int choice = Input();
                if (choice == 0)
                {
                    MainmenuUI();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("잘못된 입력입니다.");
                    Thread.Sleep(800);
                }
            }
        }
    }
}
