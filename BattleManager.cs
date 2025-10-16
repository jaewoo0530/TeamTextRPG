using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace TeamTextRPG
{
    internal class BattleManager
    {
        private GameManager gameManager;

        Random random = new Random();

        public bool isCritical = false;
        public bool isEvaded = false;

        public BattleManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }
        private List<Monster> Monsters => gameManager.Monsters;
        public int CalculateDamage(Living attacker, Living target)
        {
            int min = (int)Math.Round(attacker.Atk * 0.9f);
            int max = (int)Math.Round(attacker.Atk * 1.1f) + 1;
            int randDamage = random.Next(min, max);
            int finalDamage = Math.Max(randDamage - target.Def, 0);

            if (random.Next(0, 100) < 10)
            {
                isEvaded = true;
                return 0;
            }
            else if (random.Next(0, 100) < 15)
            {
                int CriticalDamage = (int)Math.Round(randDamage * 1.6f);
                isCritical = true;
                return CriticalDamage;
            }
            else
            {
                return finalDamage;
            }
        }

        Character player;

        //Skill 정보
        public void FullpowerAttack(Living attacker, Living target)//마나 15를 소모하여, 전력으로 돌진해서 적 하나에게 공력력의 5배의 피해를 입힙니다. 
        {
            int manaCount = 15;
            if (player.Mp < 15)
            {
                Console.WriteLine($"MP가 부족합니다! (현재 MP: {player.Mp}, 필요 MP: {manaCount})");
                return;
            }
            else
            {
                int damage = (int)(attacker.Atk * 5);
                target.TakeDamage(damage);
                player.Mp -= manaCount;
                Console.WriteLine($"MP {manaCount}를 소모했습니다. 남은 MP: {player.Mp}");
            }
        }

        public void ChainAttack(Living attacker)//마나를 20소모하여 랜덤한 3명에게 공격력의 2배의 피해를 입힙니다.
        {
            int manaCount = 20;
            if (player.Mp < 20)
            {
                Console.WriteLine($"MP가 부족합니다! (현재 MP: {player.Mp}, 필요 MP: {manaCount})");
                return;
            }
            else
            {
                if (Monsters == null || Monsters.Count == 0)
                {
                    Console.WriteLine("공격할 몬스터가 없습니다!");
                    return;
                }
                player.Mp -= manaCount;
                Console.WriteLine($"MP {manaCount}를 소모했습니다. 남은 MP: {player.Mp}");
                // 랜덤으로 3회 공격
                for (int i = 0; i < 3; i++)
                {
                    // 등장한 몬스터 중 하나를 무작위로 선택
                    Monster target = Monsters[random.Next(Monsters.Count)];

                    int damage = (int)(attacker.Atk * 2);
                    target.TakeDamage(damage);
                }
            }
        }
        public void FullSmash(Living attacker, Living target)//마나 30를 소모하고 적 전체에게 공격력의 10배의 피해를 입힌다
        {
            int manaCount = 30;
            if (player.Mp < 30)
            {
                Console.WriteLine($"MP가 부족합니다! (현재 MP: {player.Mp}, 필요 MP: {manaCount})");
                return;
            }
            else if (Monsters == null || Monsters.Count == 0)
            {
                Console.WriteLine("공격할 몬스터가 없습니다!");
                return;
            }
            else
            {
                foreach (var monster in Monsters)
                {
                    player.Mp -= manaCount;
                    int damage = (int)(attacker.Atk * 10);
                    target.TakeDamage(damage);

                    Console.WriteLine($"→ {monster.Name}이(가) {damage} 피해를 입었습니다! (남은 HP: {monster.Hp})");
                }
            }
        }
    }
}