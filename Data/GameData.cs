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
        public Character Player { get; set; }
        public Inventory Inventory { get; set; }
        public QuestManager QuestManager { get; set; }
        public int StageNumber { get; set; }
    }
}
