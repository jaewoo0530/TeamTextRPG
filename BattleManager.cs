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
        Monster monster;
        Character player;
        public void GiveDamage()
        {
            int damage = player.CalculateDamage();

            Console.WriteLine($"{player.Name}이(가) {monster.Name}을(를) 공격했습니다! ({damage} 피해)");

            monster.TakeDamage(damage);

            Console.WriteLine($"{monster.Name}의 남은 HP: {monster.Hp}");
        }

        public void TakeDamage()
        {
            int damage = monster.CalculateDamage();

            Console.WriteLine($"{player.Name}이(가) {monster.Name}을(를) 공격했습니다! ({damage} 피해)");

            monster.TakeDamage(damage);

            Console.WriteLine($"{monster.Name}의 남은 HP: {monster.Hp}");
        }
    }
}