using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    internal class Quest
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string TargetMonster { get; private set; }
        public int TargetCount { get; private set; }
        public int CurrentCount { get; private set; }
        public bool IsCompleted { get; private set; }

        public Quest(string title, string description, string targetMonster = "", int targetCount = 0)
        {
            Title = title;
            Description = description;
            TargetMonster = targetMonster;
            TargetCount = targetCount;
            CurrentCount = 0;
            IsCompleted = false;
        }

        public bool DoingQuest(string monsterName)
        {
            if (IsCompleted || string.IsNullOrEmpty(TargetMonster))
                return false;

            if (monsterName == TargetMonster)
            {
                CurrentCount++;
                if (CurrentCount >= TargetCount)
                {
                    Complete();
                    return true;
                }
            }
            return false;
        }
        public void Complete()
        {
            IsCompleted = true;
        }
    }
}
