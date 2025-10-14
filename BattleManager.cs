using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    internal class BattleManager
    {
        string name = "임시";
        public void BattleDisPlayer()
        {
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            Console.WriteLine($"Lv.{1} {name} Hp. {1}");
            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"Lv.{1} {name}({name})");
            Console.WriteLine($"HP {1}/{1}");
            Console.WriteLine();
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 도주");
            Console.WriteLine();
            Console.WriteLine("원하는 행동을 입력");
            Console.Write(">>");
            Console.ReadLine();
        }

        public void Attack()
        {
            Console.WriteLine("Battle!!");
            Console.WriteLine();
            Console.WriteLine($"1 Lv.{1} {name} Hp. {1}");
            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"Lv.{1} {name}({name})");
            Console.WriteLine($"HP {1}/{1}");
            Console.WriteLine();
            Console.WriteLine("0. 취소");
            Console.WriteLine();
            Console.WriteLine("대상을 선택 하세요.");
            Console.Write(">>");
            Console.ReadLine();
        }
    }
}
