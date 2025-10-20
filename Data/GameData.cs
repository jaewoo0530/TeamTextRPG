using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamTextRPG.Entities;
using TeamTextRPG.Items;
using TeamTextRPG.Manager;

namespace TeamTextRPG.Data
{
    internal class GameData
    {
        public CharacterData Player { get; set; }
        public InventoryData Inventory { get; set; }
        public QuestData Quest { get; set; }
        public int StageNumber { get; set; }
    }

    // DTO
    internal class CharacterData
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public int Level { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int Exp { get; set; }
        public int Gold { get; set; }
    }

    internal class InventoryData
    {
        public List<Item> Equipable { get; set; }
        public List<Item> Consumable { get; set; }
    }

    internal class QuestData
    {
        public int CurrentQuestIndex { get; set; }
        public List<Quest> Quests { get; set; }
    }
}