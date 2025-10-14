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
        public void Attack(Living attacker, Living target)
        {
            int damage = attacker.CalculateDamage();
            target.TakeDamage(damage);
        }
    }
}