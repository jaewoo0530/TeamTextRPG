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
        private List<Monster> Monsters => gameManager.Monsters;

        private int Input(int maxOption)
        {
            while (true)
            {
                bool valid = int.TryParse(Console.ReadLine(), out int choice);

                if (valid && (choice >= 0 && choice <= maxOption))
                {
                    return choice;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("잘못된 입력입니다.");
                }
            }
        }

        public void MainmenuUI() // 게임 시작 화면
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기\n2. 전투 시작\n3.회복 아이템");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.\n >>");

            int choice = Input(3);
            if (choice == 1)
            {
                StatusUI();
            }
            else if (choice == 2)
            {
                gameManager.StartBattle();
            }
            else if (choice == 3)
            {
                HealItem();
            }
        }

        public void StatusUI() // 1. 상태 보기
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
            Console.WriteLine("원하시는 행동을 입력해주세요.\n >>");

            int choice = Input(0);
            if (choice == 0)
            {
                MainmenuUI();
            }
        }

        public void BattleMainUI() // 2. 전투 시작 // 1~4 마리의 몬스터가 랜덤으로 출현
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            for (int i = 0; i < Monsters.Count; i++)
            {
                if (Monsters[i].Hp == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine($"Lv.{Monsters[i].Level} {Monsters[i].Name} Dead");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"Lv.{Monsters[i].Level} {Monsters[i].Name} HP {Monsters[i].Hp}");
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

            int choice = Input(1);
            if (choice == 1)
            {
                PlayerAttackStartUI();
            }

        }

        public void PlayerAttackStartUI()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();

            for (int i = 0; i < Monsters.Count; i++) // 단순 출력 기능
            {
                if (Monsters[i].Hp == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine($"{i + 1} Lv.{Monsters[i].Level} {Monsters[i].Name} Dead");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"{i + 1} Lv.{Monsters[i].Level} {Monsters[i].Name} HP {Monsters[i].Hp}");
                }
            }

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{Player.Level} {Player.Name} ({Player.Job})");
            Console.WriteLine($"HP {Player.Hp}/100");
            Console.WriteLine();
            Console.WriteLine("0. 취소");
            Console.WriteLine();
            Console.WriteLine("대상을 선택해주세요.\n >>");

            int choice = Input(Monsters.Count);
            if (choice == 0)
            {
                BattleMainUI();
            }

            if (choice > 0 && choice <= Monsters.Count)
            {
                if (!Monsters[choice - 1].IsDead)
                {
                    gameManager.PlayerAttack(Monsters[choice - 1]);
                }
            }

        }

        public void PlayerAttackUI(Monster target, int damage, int beforeMonsterHp) // 플레이어 공격
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine($"{Player.Name}의 공격!");
            Console.WriteLine($"Lv.{target.Name}을(를) 맞췄습니다. [데미지 : {damage}]");
            Console.WriteLine();
            Console.WriteLine($"Lv.{target.Name}");

            if (target.Hp == 0)
            {
                Console.WriteLine($"HP {beforeMonsterHp} -> Dead");
            }
            else
            {
                Console.WriteLine($"HP {beforeMonsterHp} -> {target.Hp}");
            }

            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.WriteLine(">>");

            int choice = Input(0);
            if (choice == 0)
            {
                gameManager.MonsterAttack(Player);
            }

        }

        public void MonsterAttackUI(Monster attacker, int damage, int beforePlayerHp) // 몬스터 공격
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine($"{attacker.Name}의 공격!");
            Console.WriteLine($"Lv.{Player.Name}을(를) 맞췄습니다. [데미지 : {damage}]");
            Console.WriteLine();
            Console.WriteLine($"Lv.{Player.Name}");

            Console.WriteLine($"HP {beforePlayerHp} -> {Player.Hp}");
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.WriteLine(">>");
            Input(0);
        }

        public void BattlePlayerWinUI(int damage, int beforeDungeonHp) // 플레이어 승리 결과창
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();
            Console.WriteLine("Victory");
            Console.WriteLine();
            Console.WriteLine($"던전에서 몬스터 {Monsters.Count}마리를 잡았습니다.");
            Console.WriteLine();
            Console.WriteLine("[캐릭터 정보]");
            Console.WriteLine($"Lv.{Player.Level}{Player.Name} -> Lv.{Player.Level}{Player.Name}");
            Console.WriteLine($"exp.{Player.Exp} -> exp.{Player.Exp}");
            Console.WriteLine($"HP {beforeDungeonHp} -> {Player.Hp}");
            Console.WriteLine();
            Console.WriteLine("[획득 아이템]");
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.\n >>");

            int choice = Input(0);
            if (choice == 0)
            {
                MainmenuUI();
            }
        }

        public void BattlePlayerLoseUI(int damage, int beforeDungeonHp)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result");
            Console.WriteLine();
            Console.WriteLine("You Lose");
            Console.WriteLine();
            Console.WriteLine($"던전에서 몬스터 {Monsters.Count}마리를 잡았습니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv.{Player.Name}");
            Console.WriteLine($"HP {beforeDungeonHp} -> 0");
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.WriteLine(">>");

            int choice = Input(0);
            if (choice == 0)
            {
                MainmenuUI();
            }
        }

        public void HealItem()
        {
            Console.Clear();
            Console.WriteLine("회복");
            Console.WriteLine("포션을 사용하면 체력을 30 회복 할 수 있습니다. (남은 포션 : 3 )");
            Console.WriteLine();
            Console.WriteLine("1. 사용하기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.\n>>");

            int choice = Input(1);
            if (choice == 0)
            {
                MainmenuUI();
            }
            else if (choice == 1)
            {
                
            }
        }
    }
}
