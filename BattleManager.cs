using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    internal class BattleManager
    {
        private GameManager gameManager;
        Random random = new Random();

        public BattleManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }
        private List<Monster> Monsters => gameManager.Monsters;
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
            else if (random.Next(0, 100) < 15)
            {
                int CriticalDamage = (int)(randDamage * 1.6f);
                Console.WriteLine("치명타!");
                return CriticalDamage;
            }
            else { return randDamage; }
        }

        Character player;

        public void Skill()
        {
            void FullpowerAttack(Living attacker, Living target)//마나 15를 소모하여, 전력으로 돌진해서 적 하나에게 공력력의 3배의 피해를 입힙니다. 
            {
                int manacount = 15;
                if (player.Mp < 15)
                {
                    Console.WriteLine($"MP가 부족합니다! (현재 MP: {player.Mp}, 필요 MP: {manacount})"); 
                    return;
                }
                else
                {
                    int damage = (int)(attacker.Atk * 3);
                    target.TakeDamage(damage);
                    player.Mp -= manacount;
                    Console.WriteLine($"MP {manacount}를 소모했습니다. 남은 MP: {player.Mp}");
                }
            }

            void ChainAttack(Living attacker)//마나를 20소모하여 랜덤한 3명에게 공격력의 2배의 피해를 입힙니다.
            {
                int manacount = 20;
                if (player.Mp < 20)
                {
                    Console.WriteLine($"MP가 부족합니다! (현재 MP: {player.Mp}, 필요 MP: {manacount})");
                    return;
                }
                else
                {
                    if (Monsters == null || Monsters.Count == 0)
                    {
                        Console.WriteLine("공격할 몬스터가 없습니다!");
                        return;
                    }
                    player.Mp -= manacount;
                    Console.WriteLine($"MP {manacount}를 소모했습니다. 남은 MP: {player.Mp}");
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
        }
    }
}