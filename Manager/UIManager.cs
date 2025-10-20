using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using TeamTextRPG.Entities;
using TeamTextRPG.Items;
using static System.Net.Mime.MediaTypeNames;

namespace TeamTextRPG.Manager
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
        private Inventory Inventory => gameManager.Inventory;
        private QuestManager QuestManager => gameManager.QuestManager;

        private int Input(int maxOption)
        {
            while (true)
            {
                Console.Write("\n>> ");
                bool valid = int.TryParse(Console.ReadLine(), out int choice);

                if (valid && choice >= 0 && choice <= maxOption)
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
            Console.WriteLine("1. 상태 보기\n2. 전투 시작\n3. 인벤토리\n4. 퀘스트");
            Console.WriteLine();
            Console.WriteLine("0. 저장");
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
                InventoryUI();
            }
            else if (choice == 4)
            {
                QuestUI();
            }
            else if (choice == 0)
            {
                SaveMenuUI();
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

            Console.WriteLine($"공격력 : {Player.Atk} + ({Player.ItemAtk})"); // ItemType == 무기
            Console.WriteLine($"방어력 : {Player.Def} + ({Player.ItemDef})"); // ItemType == 방어구
            Console.WriteLine($"H P : {Player.Hp}/{Player.MaxHp}");
            Console.WriteLine($"M P : {Player.Mp}/{Player.MaxMp}");
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
            Console.WriteLine($"HP {Player.Hp}/{Player.MaxHp}");
            Console.WriteLine($"MP {Player.Mp}/{Player.MaxMp}");
            Console.WriteLine();
            Console.WriteLine("1. 공격\n2. 스킬");
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요.");

            int choice = Input(2);
            if (choice == 0)
            {
                BattleMainUI();
            }
            else if (choice == 1)
            {
                PlayerAttackStartUI();
            }
            else if (choice == 2)
            {
                SkillSelectUI();
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
            Console.WriteLine($"HP {Player.Hp}/{Player.MaxHp}");
            Console.WriteLine($"MP {Player.Mp}/{Player.MaxMp}");
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
                gameManager.PlayerAttack(choice);
            }
        }

        public void SkillSelectUI()
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
            Console.WriteLine($"HP {Player.Hp}/{Player.MaxHp}");
            Console.WriteLine($"MP {Player.Mp}/{Player.MaxMp}");
            Console.WriteLine();
            Console.WriteLine($"1. 풀 파워 어설트\t 사용 MP : 15"); // 단일 FullpowerAttack
            Console.WriteLine($"2. 풀 파워 스매시\t 사용 MP : 30"); // 광역 FullSmash
            Console.WriteLine();
            Console.Write("사용할 스킬을 선택해주세요.");

            int choice = Input(2);
            if (choice == 0)
            {
                BattleMainUI();
            }
            if (choice == 1)
            {
                SkillTargetUI(choice);
            }
            if (choice == 2)
            {
                gameManager.PlayerUseSkill(choice);
            }
        }

        public void SkillTargetUI(int skillNumber)
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
            Console.WriteLine($"HP {Player.Hp}/{Player.MaxHp}");
            Console.WriteLine($"MP {Player.Mp}/{Player.MaxMp}");
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
                gameManager.PlayerUseSkill(skillNumber, choice);
            }
        }

        public void NotEnoughManaUI()
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
            Console.WriteLine($"HP {Player.Hp}/{Player.MaxHp}");
            Console.WriteLine($"MP {Player.Mp}/{Player.MaxMp}");

            Console.WriteLine("\n마나가 부족합니다.");

            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Input(0);
        }

        public void PlayerAttackUI(Monster target, int damage, int beforeMonsterHp) // 플레이어 공격
        {
            Console.Clear();
            Console.WriteLine($"Battle!! - Stage - {gameManager.stageNumber}");
            Console.WriteLine($"{Player.Name}의 공격!");

            if (Battle.isEvaded)
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
            Console.WriteLine("Victory");
            Console.WriteLine();
            Console.WriteLine($"던전에서 몬스터 {Monsters.Count}마리를 잡았습니다.");
            Console.WriteLine();
            Console.WriteLine("[캐릭터 정보]");
            Console.WriteLine($"Lv.{beforeLevel} {Player.Name} -> Lv.{Player.Level} {Player.Name}");
            Console.WriteLine($"exp.{beforeExp} -> exp.{Player.Exp}");
            Console.WriteLine($"HP {beforeDungeonHp} -> {Player.Hp}");
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
            Console.WriteLine("You Lose");
            Console.WriteLine();
            Console.WriteLine($"Lv.{Player.Level} {Player.Name}");
            Console.WriteLine($"HP {beforeDungeonHp} -> 0");
            Console.WriteLine();
            Console.WriteLine("0. 종료");

            int choice = Input(0);
        }

        public void InventoryUI()
        {
            Console.Clear();
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine();
            Console.WriteLine("[장착 아이템]");
            for (int i = 0; i < Inventory.equipableItems.Count; i++)
            {
                Console.WriteLine($" - {i + 1}. {Inventory.equipableItems[i].name}   | {Inventory.equipableItems[i].TypeName}  +  {Inventory.equipableItems[i].value}  | {Inventory.equipableItems[i].info}");
            }
            Console.WriteLine();
            Console.WriteLine("[소모 아이템]");
            for (int i = 0; i < Inventory.consumableItems.Count; i++)
            {
                Console.WriteLine($" - {i + 1}. {Inventory.consumableItems[i].name}   | {Inventory.consumableItems[i].TypeName} + {Inventory.consumableItems[i].value}  | {Inventory.consumableItems[i].info} (보유 개수: {Inventory.consumableItems[i].count})");
            }
            Console.WriteLine();
            Console.WriteLine("1. 장착 관리\n2. 소모템 사용\n0. 나가기");
            int choice = Input(2);
            if (choice == 0)
            {
                MainmenuUI();
            }
            else if (choice == 1)
            {
                EquipManagementUI();
            }
            else if (choice == 2)
            {
                ConsumableItemUI();
            }
        }
        public void EquipManagementUI()
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 장착관리");
            Console.WriteLine("보유 중인 장착 아이템을 장착할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[장착 아이템]");
            for (int i = 0; i < Inventory.equipableItems.Count; i++)
            {
                string equipMark = Inventory.equipableItems[i].isEquip ? "[E] " : "";
                Console.WriteLine($"- {equipMark} {i + 1}. {Inventory.equipableItems[i].name}   | {Inventory.equipableItems[i].TypeName} + {Inventory.equipableItems[i].value}  | {Inventory.equipableItems[i].info}");
            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            int choice = Input(Inventory.equipableItems.Count);
            if (choice == 0)
            {
                InventoryUI();
            }
            else if (choice > 0 && choice <= Inventory.equipableItems.Count)
            {
                gameManager.ItemEquip(choice);
            }
        }

        public void ConsumableItemUI()
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 소모템 사용");
            Console.WriteLine("소모 아이템을 사용할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("[소모 아이템]");
            for (int i = 0; i < Inventory.consumableItems.Count; i++)
            {
                Console.WriteLine($" - {i + 1}. {Inventory.consumableItems[i].name}   | {Inventory.consumableItems[i].TypeName} + {Inventory.consumableItems[i].value}  | {Inventory.consumableItems[i].info} (보유 개수: {Inventory.consumableItems[i].count})");
            }
            Console.WriteLine();
            Console.WriteLine("1. 사용하기\n0. 나가기");

            int choice = Input(Inventory.consumableItems.Count);
            if (choice == 0)
            {
                InventoryUI();
            }
            else if (choice > 0 && choice <= Inventory.consumableItems.Count)
            {
                gameManager.UseConsumableItem(choice);
            }
        }

        public void ItemUseSuccessUI(int beforeHp)
        {
            Console.Clear();

            Console.WriteLine("성공");
            Console.WriteLine($"{beforeHp} HP -> {Player.Hp} HP (회복량: {Player.Hp - beforeHp})");

            Console.WriteLine();
            Console.WriteLine("\n0. 나가기");
            Input(0);
            ConsumableItemUI();
        }

        public void ItemUseFailUI()
        {
            Console.Clear();

            Console.WriteLine("실패");

            Console.WriteLine();
            Console.WriteLine("\n0. 나가기");
            Input(0);
            ConsumableItemUI();
        }

        public void QuestUI()
        {
            Console.Clear();
            Console.WriteLine("퀘스트\n");
            Console.WriteLine("[퀘스트 목록]");

            var quests = QuestManager.Quests;
            var current = QuestManager.CurrentQuest;

            for (int i = 0; i < quests.Count; i++)
            {
                string status;
                if (quests[i] == current)
                    status = "(진행중)";
                else if (quests[i].IsCompleted)
                    status = "(완료)";
                else if (i > quests.IndexOf(current))
                    status = "(잠김)";
                else
                    status = "";

                Console.WriteLine($"{i + 1}. {quests[i].Title} {status}");
            }

            Console.WriteLine();
            Console.WriteLine("진행 중인 퀘스트를 확인하겠습니까?");
            Console.WriteLine();

            Console.WriteLine("1. 예");
            Console.WriteLine("0. 나가기");

            int choice = Input(1);
            if (choice == 0)
            {
                MainmenuUI();
            }
            if (choice == 1)
            {
                ShowQuestUI(current);
            }
        }

        public void ShowQuestUI(Quest quest)
        {
            Console.Clear();

            if (quest == null)
            {
                Console.WriteLine("현재 진행 중인 퀘스트가 없습니다.");
                Console.WriteLine("\n0. 다음");
                Input(0);
                return;
            }

            Console.WriteLine($"제목: {quest.Title}");
            Console.WriteLine($"내용: {quest.Description}");
            Console.WriteLine();
            Console.WriteLine($"목표: {quest.TargetMonster} {quest.TargetCount}마리 처치");
            Console.WriteLine($"진행도: {quest.CurrentCount}/{quest.TargetCount}");
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            int choice = Input(0);

            if (choice == 0)
            {
                QuestUI();
            }
        }

        public void QuestProgressUI(Quest quest)
        {
            if (quest == null) return;

            Console.WriteLine();
            Console.WriteLine($"[퀘스트 진행 중] {quest.TargetMonster} 처치 {quest.CurrentCount}/{quest.TargetCount}");
        }

        public void QuestCompleteUI(Quest quest, Item reward)
        {
            if (quest == null) return;

            Console.WriteLine();
            Console.WriteLine($"[퀘스트 완료!] '{quest.Title}'을(를) 달성했습니다!");
            Console.WriteLine();
            Console.WriteLine($" - 보상으로 '{reward.name}'을(를) 획득했습니다!");
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Input(0);
        }

        public void  SaveMenuUI()
        {
            Console.Clear();
            Console.WriteLine(" 저장 메뉴\n\n ");
            Console.WriteLine("1. 저장하기");
            Console.WriteLine("2. 불러오기");
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.Write("원하시는 행동을 입력해주세요.");

            int choice = Input(2);
            if (choice == 0)
                MainmenuUI();
            else if (choice == 1)
                gameManager.SaveGame();
            else if (choice == 2)
                gameManager.LoadGame();
        }
        public void ShowSaveSuccessMessage()
        {
            Console.Clear();
            Console.WriteLine("게임이 저장되었습니다!");
            Console.WriteLine();
            Console.WriteLine("0. 저장메뉴로 돌아가기");
            Input(0);
            SaveMenuUI();
        }
        public void ShowSaveErrorMessage(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("저장 중 오류가 발생했습니다!");
            Console.WriteLine($"오류 내용: {message}");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("0. 저장메뉴로 돌아가기");
            Input(0);
            SaveMenuUI();
        }
        public void ShowLoadSuccessMessage()
        {
            Console.Clear();
            Console.WriteLine("게임 데이터를 불러왔습니다!");
            Console.WriteLine();
            Console.WriteLine("0. 저장메뉴로 돌아가기");
            Input(0);
            SaveMenuUI();
        }
        public void ShowLoadErrorMessage(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("불러오기 중 오류가 발생했습니다!");
            Console.WriteLine($"오류 내용: {message}");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("0. 저장메뉴로 돌아가기");
            Input(0);
            SaveMenuUI();
        }
    }
}
