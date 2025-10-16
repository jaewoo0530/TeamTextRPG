using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TeamTextRPG
{
    internal class Skill
    {
        private Character player;
        private List<Monster> monsters;
        private Random random = new Random();

        public Skill(Character player, List<Monster> monsters)
        {
            this.player = player;
            this.monsters = monsters;
        }

        public void FullpowerAttack(Character player, Living target)//마나 15를 소모하여, 전력으로 돌진해서 적 하나에게 공력력의 5배의 피해를 입힙니다. 
        {
            int manaCount = 15;
            if (player.Mp < 15)
            {
                Console.WriteLine($"MP가 부족합니다! (현재 MP: {player.Mp}, 필요 MP: {manaCount})");
                return;
            }
            else
            {
                int damage = (int)(player.Atk * 5);
                
                player.Mp -= manaCount;
                Console.WriteLine($"MP {manaCount}를 소모했습니다. 남은 MP: {player.Mp}");
            }
        }

        public void ChainAttack(Character player)//마나를 20소모하여 랜덤한 3명에게 공격력의 2배의 피해를 입힙니다.
        {
            int manaCount = 20;
            if (player.Mp < 20)
            {
                Console.WriteLine($"MP가 부족합니다! (현재 MP: {player.Mp}, 필요 MP: {manaCount})");
                return;
            }
            else
            {
                if (monsters == null || monsters.Count == 0)
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
                    Monster target = monsters[random.Next(monsters.Count)];

                    int damage = (int)(player .Atk * 2);
                    
                }
            }
        }
        public void FullSmash(Character player)//마나 30를 소모하고 적 전체에게 공격력의 10배의 피해를 입힌다
        {
            int manaCount = 30;
            if (player.Mp < 30)
            {
                Console.WriteLine($"MP가 부족합니다! (현재 MP: {player.Mp}, 필요 MP: {manaCount})");
                return;
            }
            else if (monsters == null || monsters.Count == 0)
            {
                Console.WriteLine("공격할 몬스터가 없습니다!");
                return;
            }
            player.Mp -= manaCount;

            foreach (var monster in monsters)
            {
                if(monster.IsDead)  continue; 

               int damage = (int)(player.Atk * 10);
              
               Console.WriteLine($"→ {monster.Name}이(가) {damage} 피해를 입었습니다! (남은 HP: {monster.Hp})");
            }
            
        }
    }
}
