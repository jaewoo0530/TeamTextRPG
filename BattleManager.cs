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
        Character player;
        public void Attack(Living attacker, Living target)
        {
            Random random = new Random();
            if (random.Next(0, 100) < 10)
            {
                Console.WriteLine($"{player.Name}의 공격이 빗나갔습니다!");
            }
            else
            {
            int damage = attacker.CalculateDamage();
            target.TakeDamage(damage);
            }
        }
    }
}