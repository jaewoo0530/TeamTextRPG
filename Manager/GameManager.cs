using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TeamTextRPG.Data;
using TeamTextRPG.Entities;
using TeamTextRPG.Items;

namespace TeamTextRPG.Manager
{
    internal class GameManager
    {
        private Random random = new Random();
        public Character Player { get; private set; }
        public List<Monster> Monsters { get; private set; }
        public UIManager UI { get; private set; }
        public BattleManager Battle { get; private set; }

        private MonsterData monsterData = new MonsterData();
        public Inventory Inventory { get; private set; }
        public QuestManager QuestManager { get; private set; }
        public RewardManager RewardManager { get; private set; }

        private int beforeDungeonHp;

        public int stageNumber = 1;

        public void StartGame()
        {
            UI = new UIManager(this);
            Battle = new BattleManager(this);

            Inventory = new Inventory();
            QuestManager = new QuestManager();

            RewardManager = new RewardManager();

            string name = UI.CreateName();
            JobType job = UI.CreateJob();

            Player = new Character(name, job);

            Quest firstQuest = QuestManager.GetCurrentQuest();
            UI.MainmenuUI();
        }

        public void ItemEquip(int choice)
        {
            Item selectedItem = Inventory.equipableItems[choice - 1];

            if (!selectedItem.isEquip)
            {
                Inventory.EquipItem(Inventory.equipableItems, selectedItem);
            }
            else
            {
                Inventory.UnEquipItem(Inventory.equipableItems, selectedItem);
            }

            Player.ApplyItem(selectedItem);
            UI.EquipManagementUI();
        }

        public void UseConsumableItem(int choice)
        {
            int beforeHp = Player.Hp;
            Item item = Inventory.consumableItems[choice - 1];
            if (item.count > 0)
            {
                Player.UseItem(item);
                UI.ItemUseSuccessUI(beforeHp);
            }
            else
            {
                UI.ItemUseFailUI();
            }
        }

        public void AcceptQuest(int choice)
        {
            if (choice - 1 < 0 || choice - 1 >= QuestManager.Quests.Count)
                return;

            QuestManager.currentQuestIndex = choice - 1;
            Quest current = QuestManager.GetCurrentQuest();

            UI.ShowQuestUI(current);
        }

        public void StartBattle()
        {
            beforeDungeonHp = Player.Hp;

            int monsterCount = random.Next(1, 5);

            Monsters = new List<Monster>();

            for (int i = 0; i < monsterCount; i++)
            {
                Monsters.Add(monsterData.GetRandomMonster(stageNumber));
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
        }

        public void PlayerAttack(int choice)
        {
            Monster target = Monsters[choice - 1];
            if (target.IsDead) return;
            
            int beforeMonsterHp = target.Hp;
            int damage = Battle.CalculateDamage(Player, target);
            Player.Attack(target, damage);

            UI.PlayerAttackUI(target, damage, beforeMonsterHp);
            Battle.isCritical = false;
            Battle.isEvaded = false;
            MonsterAttack();
        }

        public void PlayerUseSkill(int skillNumber, int? choice = null)
        {
            // 광역 스킬
            if (choice == null)
            {
                Battle.UseSkill(skillNumber);

                if (!Battle.isEnoughMana)
                {
                    UI.NotEnoughManaUI();
                    return;
                }

                MonsterAttack();
                return;
            }

            // 단일 스킬
            Monster target = Monsters[(int)choice - 1];

            if (target.IsDead)
                return;

            Battle.UseSkill(skillNumber, target);
            if (!Battle.isEnoughMana)
            {
                UI.NotEnoughManaUI();
                return;
            }

            MonsterAttack();
        }

        public void PlayerSkillAttack(Monster target, int damage)
        {
            int beforeMonsterHp = target.Hp;
            Player.Attack(target, damage);
            UI.PlayerAttackUI(target, damage, beforeMonsterHp);
        }

        public void PlayerSkillAttackAll(int damage)
        {
            foreach (var monster in Monsters)
            {
                if (monster.IsDead)
                    continue;

                int beforeMonsterHp = monster.Hp;

                Player.Attack(monster, damage);

                UI.PlayerAttackUI(monster, damage, beforeMonsterHp);
            }
        }

        public void MonsterAttack()
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
                        int beforePlayerHp = Player.Hp;

                        int damage = Battle.CalculateDamage(Monsters[i], Player);
                        Monsters[i].Attack(Player, damage);
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
            if (stageNumber < monsterData.MonsterDex.Count)
            {
                stageNumber++;
            }

            int beforeLevel = Player.Level;
            int beforeExp = Player.Exp;

            int acquireExp = 0;

            foreach (var monster in Monsters)
            {
                acquireExp += monster.RewardExp;

                if (QuestManager.UpdateQuestProgress(monster.Name, out Quest currentQuest))
                {
                    UI.QuestProgressUI(currentQuest); // 진행 중인 퀘스트 출력

                    if (currentQuest.IsCompleted)
                    {
                        UI.QuestCompleteUI(currentQuest); // 완료 UI 표시
                        QuestManager.MoveToNextQuest();
                        UI.ShowQuestUI(QuestManager.GetCurrentQuest()); // 다음 퀘스트 자동 표시
                        
                        if (RewardManager.GiveReward().ItemType == ItemType.소모템)
                        {
                            Inventory.consumableItems.Add(RewardManager.GiveReward());
                        }
                        else
                        {
                            Inventory.equipableItems.Add(RewardManager.GiveReward());
                        }
                    }
                }
            }
            Player.AddExp(acquireExp);

            UI.BattlePlayerWinUI(beforeDungeonHp, beforeLevel, beforeExp);
        }

        public void BattleLose()
        {
            UI.BattlePlayerLoseUI(beforeDungeonHp);
        }
    }
}
