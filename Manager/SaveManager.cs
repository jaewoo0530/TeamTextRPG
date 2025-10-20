using System;
using System.IO;
using Newtonsoft.Json;
using TeamTextRPG.Data;

namespace TeamTextRPG.Manager
{
    internal static class SaveManager
    {
        // ★ 실행 EXE 폴더에 고정: bin\Debug\net8.0\save.json 또는 bin\Release\net8.0\save.json
        private static readonly string saveFile = "Save.Json";

        public static void Save(GameManager game)
        {
            try
            {

                var data = new GameData
                {
                    Player = new CharacterData
                    {
                        Name = game.Player.Name,
                        Job = game.Player.Job.ToString(),
                        Level = game.Player.Level,
                        Hp = game.Player.Hp,
                        Mp = game.Player.Mp,
                        Exp = game.Player.Exp,
                        Gold = game.Player.Gold
                    },
                    Inventory = new InventoryData
                    {
                        Equipable = game.Inventory.equipableItems,
                        Consumable = game.Inventory.consumableItems
                    },
                    Quest = new QuestData
                    {
                        CurrentQuestIndex = game.QuestManager.currentQuestIndex,
                        Quests = game.QuestManager.Quests
                    },
                    StageNumber = game.stageNumber
                };

                //Player객체 Json 텍스트로 바꾸기        //보기좋게 들여쓰기
                string Json = JsonConvert.SerializeObject(data, 
                    Formatting.Indented,
                    new JsonSerializerSettings {
                        TypeNameHandling = TypeNameHandling.Auto,
                        NullValueHandling = NullValueHandling.Ignore
                    });
                File.WriteAllText(saveFile, Json);                  //세이브파일 위치에 json텍스트 저장, 같은이름 있으면 덮어씀,
                Console.WriteLine(" 게임이 저장되었습니다!");    //새로만들때 자동파일생성

            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"저장 중 오류가 생겼습니다! 오류내용 : {ex.Message}");

            }
            finally { Console.ResetColor(); }
        }

        public static GameData Load()
        {
            try
            {
                if (!File.Exists(saveFile))
                {
                    Console.WriteLine("저장 파일이 없습니다");
                    return null;
                }

                string Json = File.ReadAllText(saveFile);    //save.json 내용을 문자열로 읽기
                var data = JsonConvert.DeserializeObject<GameData>(Json,
                    new JsonSerializerSettings {
                        TypeNameHandling = TypeNameHandling.Auto 
                    });   //JSON문자열  C#언어로 변환 역직렬화
                Console.WriteLine("저장된 캐릭터를 불러왔습니다!");
                return data;      //반환
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"불러오기 중 오류가 발생했습니다! 오류내용 : {ex.Message}");
                return null;
            }
            finally { Console.ResetColor(); }
        }
    } 
}

       