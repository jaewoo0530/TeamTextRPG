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
        public void PreocessAttack(Living attacker, Living target)
        {

            if (random.Next(0, 100) < 10)
            {
                Console.WriteLine($"{attacker.Name}의 공격이 빗나갔습니다!");
            }
            else
            {
                int damage = attacker.CalculateDamage();
                target.TakeDamage(damage);
            }
        }

        public int CalculateDamage(Living attacker)
        {
            int min = (int)(attacker.Atk * 0.9f);
            int max = (int)(attacker.Atk * 1.1f) + 1;
            int randDamage = random.Next(min, max);
            return randDamage;
        }
    }
}