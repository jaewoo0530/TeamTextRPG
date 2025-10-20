using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTextRPG.Items;

namespace TeamTextRPG.Entities
{
    public enum JobType { 전사, 궁수, 마법사 }
    internal class Character : Entity
    {
        public JobType Job { get; }

        public int BaseAtk { get; private set; }
        public int BaseDef { get; private set; }
        public int ItemAtk { get; private set; }
        public int ItemDef { get; private set; }
        public int Gold { get; private set; } = 500;
        public int Exp { get; private set; } = 0;

        public int Mp { get; private set; }
        public int MaxMp { get; private set; }

        private Item equippedAttackItem;
        private Item equippedDefenseItem;

        public Character(string name, JobType job)
        {
            Name = name;
            Job = job;
            Level = 1;

            switch (job)
            {
                case JobType.전사: BaseAtk = 5; BaseDef = 10; MaxHp = 100; MaxMp = 100; break;
                case JobType.궁수: BaseAtk = 10; BaseDef = 5; MaxHp = 100; MaxMp = 100; break;
                case JobType.마법사: BaseAtk = 3; BaseDef = 5; MaxHp = 100; MaxMp = 200; break;
            }

            Hp = MaxHp;
            Mp = MaxMp;

            CalculateItemStat();
        }

        public void AddExp(int value)
        {
            Exp += value;
            LevelUp();
        }

        public void ApplyItem(Item item)
        {
            switch (item.ItemType)
            {
                case ItemType.무기:
                    equippedAttackItem = item.isEquip ? item : null;
                    break;
                case ItemType.방어구:
                    equippedDefenseItem = item.isEquip ? item : null;
                    break;
            }

            CalculateItemStat();
        }
         
        public void CalculateItemStat()
        {
            ItemAtk = 0;
            ItemDef = 0;

            if (equippedAttackItem != null)
                ItemAtk += equippedAttackItem.value;

            if (equippedDefenseItem != null)
                ItemDef += equippedDefenseItem.value;

            Atk = BaseAtk + ItemAtk;
            Def = BaseDef+ ItemDef;
        }

        public void LevelUp()
        {
            while (Exp >= 100)
            {
                Exp -= 100;
                Level++;
                Atk++;
                Def++;
            }
        }

        public void ReduceMp(int manaCost)
        {
            Mp -= manaCost;
            if (Mp <= 0)
            {
                Mp = 0;
            }
        }

        public void UseItem(Item item)
        {
            item.Use(this);
        }

        public void Heal(int amount)
        {
            Hp += amount;
        }
    }
}
