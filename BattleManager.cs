using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    internal class BattleManager
    {
        Random random = new Random();

        public BattleManager()
        {

        }
        public int CalculateDamage(Living attacker)
        {
            int min = (int)(attacker.Atk * 0.9f);
            int max = (int)(attacker.Atk * 1.1f) + 1;
            int randDamage = random.Next(min, max);
            if (random.Next(0, 100) < 10)
            {
                Console.WriteLine($"{attacker}의 공격이 빗나갔습니다!!");
                return 0;
            }
            if (random.Next(0, 100) < 15)
            {
                int CriticalDamage = (int)(randDamage * 1.6f);
                Console.WriteLine("치명타!");
                return CriticalDamage;
            }
            else { return randDamage; }
        }
    }
}