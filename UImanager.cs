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

        public void Status(Character player) // 1. 상태 보기
        {
            Console.Clear();
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine($"Lv. {player.Level}");
            Console.WriteLine($"{player.Name} ({player.Job})");
            Console.WriteLine($"공격력 : {player.Attack}");
            Console.WriteLine($"방어력 : {player.Defense}");
            Console.WriteLine($"체 력 : {player.Hp}");
            Console.WriteLine($"Gold : {player.Gold} G");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.\n >>");
        }

        public void BattleStart(Character player, List<Monster> monster) // 2. 전투 시작
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            for (int i = 0; i < monster.Count; i++)
            {
                if (monster[i].Hp == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine($"Lv.{monster[i].Level} {monster[i].Name} Dead");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"Lv.{monster[i].Level} {monster[i].Name} HP {monster[i].Hp}");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("[내정보]");
            Console.WriteLine($"Lv.{player.Level} {player.Name} ({player.Job})");
            Console.WriteLine($"HP {player.Hp}/100");
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 입력해주세요.\n >>");
        }

        public void PlayerAttack(Character player, Monster monster, int damage, int beforeHp) // 플레이어 공격
        {
            Console.WriteLine($"{player.Name}의 공격!");
            Console.WriteLine($"Lv.{monster.Name}을(를) 맞췄습니다. [데미지 : {damage}]");
            Console.WriteLine();
            Console.WriteLine($"Lv.{monster.Name}");
            if (monster.Hp == 0)
            {
                Console.WriteLine($"HP {beforeHp} -> Dead");
            }
            else
            {
                Console.WriteLine($"HP {beforeHp} -> {monster.Hp}");
            }
            Console.WriteLine();
            Console.WriteLine("0. 다음");
            Console.WriteLine();
            Console.WriteLine(">>");
        }

        public void MonsterAttack(Character player, Monster monster, int damage, int beforePlayerHp) // 몬스터 공격
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine($"{monster.Name}의 공격!");
            Console.WriteLine($"Lv.{player.Name}을(를) 맞췄습니다. [데미지 : {damage}]");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Name}");
            if (player.Hp == 0)
            {
                Console.WriteLine($"HP {beforePlayerHp} -> {player.Hp}");
                Console.WriteLine();
                Console.WriteLine("0. 다음");
                Console.WriteLine();
                Console.WriteLine(">>");
            }
            else
            {

            }
        }
    }
}
