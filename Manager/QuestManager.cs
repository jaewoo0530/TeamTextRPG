using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG.Manager
{
    internal class QuestManager
    {
        public List<Quest> Quests { get; private set; }
        public int currentQuestIndex = 0;

        public Quest CurrentQuest => (currentQuestIndex < Quests.Count) ? Quests[currentQuestIndex] : null;

        public QuestManager()
        {
            Quests = new List<Quest>
            {
            new Quest("저글링 숙청", "저글링 3마리 처치하기", "저글링", 3), //0
            new Quest("히드라 숙청", "히드라 3마리 처치하기", "히드라", 3),//1
            new Quest("뮤탈 숙청", "뮤탈 3마리 처치하기", "뮤탈", 3),
            new Quest("울라리 숙청", "울라리 3마리 처치하기", "울라리", 3)
            };
        }

        public Quest GetCurrentQuest()      //퀘스트 확인
        {
            if (currentQuestIndex < Quests.Count)
                return Quests[currentQuestIndex];   //퀘스트 번호
            else
                return null;

        }

        public bool UpdateQuestProgress(string monsterName, out Quest currentQuest)     //true면 퀘스트 완 false면 퀘스트 진행
        {
            currentQuest = GetCurrentQuest();       //진행중인 퀘스트 불러오기
            if (currentQuest == null) return false;      //없으면 종료

            bool completed = currentQuest.DoingQuest(monsterName);
            return completed;       //true면 완료, false면 아직 진행 중
        }

        public bool MoveToNextQuest()
        {
            if (currentQuestIndex + 1 < Quests.Count)
            {
                currentQuestIndex++;
                return true;
            }
            return false;
        }
    }
}
