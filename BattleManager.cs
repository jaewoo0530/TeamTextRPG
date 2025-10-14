using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    internal class BattleManager
    {
        Monster monster;
        Character character;
        public void BattleDisPlayer()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            Console.WriteLine($"Lv.{monster.Attack} {monster.Name} Hp. {monster.Hp}");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"Lv.{character.Level} {character.Name}({character.Job})");
            Console.WriteLine($"HP {character.Hp}/{character.Hp}");
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 도주");
            Console.WriteLine();
            Console.WriteLine("원하는 행동을 입력");
            Console.Write(">>");
            int act1;
            if (int.TryParse(Console.ReadLine(), out act1))
            {
                switch (act1)
                {
                    case 1:
                        Console.Clear();
                        AttackDisplayer();
                        break;
                }
            }
            else
            {
                Console.Clear();
                BattleDisPlayer();
            }
        }

        public void AttackDisplayer()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            Console.WriteLine($"1  Lv.{monster.Attack} {monster.Name} Hp. {monster.Hp}");
            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"Lv.{character.Level} {character.Name}({character.Job})");
            Console.WriteLine($"HP {character.Hp}/{character.Hp}");
            Console.WriteLine();
            Console.WriteLine("0. 취소");
            Console.WriteLine();
            Console.WriteLine("대상을 선택 하세요.");
            Console.Write(">>");
            Console.ReadLine();
        }
    }
}
