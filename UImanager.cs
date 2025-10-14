using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    internal class UIManager
    {
        public UIManager() // 생성자
        {

        }

        public void Mainmenu() // 게임 시작 화면
        {
            Console.Clear();
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.\n이제 전투를 시작할 수 있습니다.");
            Console.WriteLine();
            Console.WriteLine("1. 상태 보기\n2. 전투 시작");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.\n >>");
        }

        public void Status() // 1. 상태 보기
        {
            Console.Clear();
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv. {player.level}");
            Console.WriteLine($"{player.name} ({player.job})");
            Console.WriteLine($"공격력 : {player.basePower}");
            Console.WriteLine($"방어력 : {player.baseDefense}");
            Console.WriteLine($"체 력 : {player.hp}");
            Console.WriteLine($"Gold : {player.gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.\n >>");
        }

        public void BattleStart() // 2. 전투 시작
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            for (int i = 0; i < monster.Count; i++)
            {
                Console.WriteLine($"Lv.{monster[i].level} {monster[i].name} HP {monster[i].hp}");
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.level} {player.name} ({player.job})");
            Console.WriteLine($"HP {player.hp}/100");
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.\n >>");
        }
    }
}
