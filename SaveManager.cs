using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace TeamTextRPG
{
    internal static class SaveManager
    {
        private static string saveFile = "save.json";   //저장할 파일 이름 지정 


        //저장하는 거
        public static void Save(Character player) //캐릭터 정보 저장 (단순 저장
        {
            try
            {
                //Player객체 Json 텍스트로 바꾸기        //보기좋게 들여쓰기
                string json = JsonConvert.SerializeObject(player, Formatting.Indented);
                File.WriteAllText(saveFile, json);                  //세이브파일 위치에 json텍스트 저장, 같은이름 있으면 덮어씀,
                Console.WriteLine(" 게임이 저장되었습니다!");    //새로만들때 자동파일생성
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"저장 중 오류가 생겼습니다! 오류내용 : {ex.Message}");
            }
            finally { Console.ResetColor(); }
        }
        
        public static Character Load()
        {
            try
            {
                if (!File.Exists(saveFile))      //저장파일 존재하는지 검사,  없으면 null리턴 메시지출력
                {
                    Console.WriteLine(" 저장 파일이 없습니다.");
                    return null;
                }


                string Json = File.ReadAllText(saveFile);       //save.json 내용을 문자열로 읽기
                Character player = JsonConvert.DeserializeObject<Character>(Json);      //Json텍스트를 character 객체로 복원, 저장할 때 쓴 클래스구조랑 같아야함 ,
                                                                                        //public으로 정의된 프로퍼티만 복원 가능
                Console.WriteLine("저장된 캐릭터를 불러왔습니다!");
                return player;      //복원된 Character반환
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
