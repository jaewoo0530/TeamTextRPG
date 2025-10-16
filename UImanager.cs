using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
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
        private BattleManager Battle => gameManager.Battle;

        private int Input(int maxOption)
        {
            while (true)
            {
                Console.Write("\n>> ");
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

        public string CreateName()
        {
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.Write("원하시는 이름을 입력해주세요.\n\n>> ");
            string name = Console.ReadLine();
            return name;
        }

        public JobType CreateJob()
        {
            while (true)
            {
                Console.WriteLine("\n\n원하시는 직업의 이름을 입력해주세요.");
                Console.WriteLine("\n1.전사\n2.궁수\n3.마법사");
                Console.Write("\n\n>> ");
                string input = Console.ReadLine();
                JobType selectedJob;

                if (int.TryParse(input, out int number))
                {
                    int jobCount = Enum.GetValues<JobType>().Length;

                    if (number >= 1 && number <= jobCount)
                    {
                        selectedJob = (JobType)(number - 1);
                    }
                    else
                    {
                        Console.WriteLine("다시 입력해주세요.");
                        continue;
                    }
                }
                else if (Enum.TryParse(input, out JobType parsedJob))
                {
                    selectedJob = parsedJob;
                }
                else
                {
                    Console.WriteLine("다시 입력해주세요.");
                    continue;
                }
                return selectedJob;
            }
        }

        public void MainmenuUI() // 게임 시작 화면
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기\n2. 전투 시작\n3. 회복 아이템\n4. 인벤토리");
            Console.WriteLine();
            Console.WriteLine("9. 불러오기\n0. 저장");
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요.");

            int choice = Input(4);
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
                HealItemUI();
            }
            else if (choice == 4)
            {
                // InventoryUI();
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
            Console.Write("원하시는 행동을 입력해주세요.");

            int choice = Input(0);
            if (choice == 0)
            {
                MainmenuUI();
            }
        }

        public void BattleMainUI() // 2. 전투 시작 // 1~4 마리의 몬스터가 랜덤으로 출현
        {
            Console.Clear();
            Console.WriteLine($"Battle!! - Stage - {gameManager.stageNumber}");
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
            Console.WriteLine("1. 공격\n2. 스킬");
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요.");

            int choice = Input(2);
            if (choice == 1)
            {
                PlayerAttackStartUI();
            }
            else if (choice == 2)
            {

            }
        }

        public void PlayerAttackStartUI()
        {
            Console.Clear();
            Console.WriteLine($"Battle!! - Stage - {gameManager.stageNumber}");
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
            Console.Write("대상을 선택해주세요.");

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
            Console.WriteLine($"Battle!! - Stage - {gameManager.stageNumber}");
            Console.WriteLine($"{Player.Name}의 공격!");

            if(Battle.isEvaded)
            {
                Console.WriteLine($"{Player.Name}의 공격이 빗나갔습니다!!");
            }
            else
            {
                Console.Write($"Lv.{target.Level} {target.Name}을(를) 맞췄습니다. [데미지 : {damage}]");
                if (Battle.isCritical == true)
                {
                    Console.WriteLine(" - 치명타 공격!!");
                }
                else
                {
                    Console.WriteLine();
                }
            }

            Console.WriteLine();
            Console.WriteLine($"Lv.{target.Level} {target.Name}");

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

            int choice = Input(0);
        }

        

        public void MonsterAttackUI(Monster attacker, int damage, int beforePlayerHp) // 몬스터 공격
        {
            Console.Clear();
            Console.WriteLine($"Battle!! - Stage - {gameManager.stageNumber}");
            Console.WriteLine($"{attacker.Name}의 공격!");

            if (Battle.isEvaded)
            {
                Console.WriteLine($"{attacker.Name}의 공격이 빗나갔습니다!!");
            }
            else
            {
                Console.Write($"Lv.{Player.Level} {Player.Name}을(를) 맞췄습니다. [데미지 : {damage}]");
                if (Battle.isCritical == true)
                {
                    Console.WriteLine(" - 치명타 공격!!");
                }
                else
                {
                    Console.WriteLine();
                }
            }

            Console.WriteLine();
            Console.WriteLine($"Lv.{Player.Level} {Player.Name}");
            Console.WriteLine($"HP {beforePlayerHp} -> {Player.Hp}");
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Input(0);
        }

        public void BattlePlayerWinUI(int beforeDungeonHp, int beforeLevel, int beforeExp) // 플레이어 승리 결과창
        {
            Console.Clear();
            Console.WriteLine($"Battle!! - Stage - {gameManager.stageNumber} Result");
            Console.WriteLine();
            Console.WriteLine("Victory");
            Console.WriteLine();
            Console.WriteLine($"던전에서 몬스터 {Monsters.Count}마리를 잡았습니다.");
            Console.WriteLine();
            Console.WriteLine("[캐릭터 정보]");
            Console.WriteLine($"Lv.{beforeLevel} {Player.Name} -> Lv.{Player.Level} {Player.Name}");
            Console.WriteLine($"exp.{beforeExp} -> exp.{Player.Exp}");
            Console.WriteLine($"HP {beforeDungeonHp} -> {Player.Hp}");
            Console.WriteLine();
            Console.WriteLine("[획득 아이템]");
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int choice = Input(0);
            if (choice == 0)
            {
                MainmenuUI();
            }
        }

        public void BattlePlayerLoseUI(int beforeDungeonHp)
        {
            Console.Clear();
            Console.WriteLine($"Battle!! - Stage - {gameManager.stageNumber} Result");
            Console.WriteLine();
            Console.WriteLine("You Lose");
            Console.WriteLine();
            Console.WriteLine($"Lv.{Player.Level} {Player.Name}");
            Console.WriteLine($"HP {beforeDungeonHp} -> 0");
            Console.WriteLine();
            Console.WriteLine("0. 종료");

            int choice = Input(0);
        }

        public void HealItemUI()
        {
            Console.Clear();
            Console.WriteLine("회복");
            Console.WriteLine("포션을 사용하면 체력을 30 회복 할 수 있습니다. (남은 포션 : 3 )");
            Console.WriteLine();
            Console.WriteLine("1. 사용하기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.");

            int choice = Input(1);
            if (choice == 0)
            {
                MainmenuUI();
            }
            else if (choice == 1)
            {

            }
        }

        public void InventoryUI(List<Item> items)
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ItemType == ItemType.방어구 && items[i].isHave)
                {
                    Console.WriteLine($" - {items[i].name}   | 방어력 + {items[i].value}  | {items[i].info}");
                }
                else if (items[i].ItemType == ItemType.무기 && items[i].isHave)
                {
                    Console.WriteLine($" - {items[i].name}   | 공격력 + {items[i].value}  | {items[i].info}");
                }
            }
            Console.WriteLine("=========================");
            Console.WriteLine("1. 장착 관리\n0. 나가기");
            int choice = Input(1);
            if (choice == 0)
            {
                MainmenuUI();
            }
            else if (choice == 1)
            {

            }
        }
        public void EquipManagment()
        {

        }
    }
}
