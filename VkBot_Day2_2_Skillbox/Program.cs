using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Utils;

namespace VkBot_Day2_2_Skillbox
{
    

    class Program
    {
        static Dictionary<string, int> map = new Dictionary<string, int>();
        static string Key = "c9f910d285a17c605c059115b87e8b3cd1ff80caf4bd5e5d716b32dc254053e50a237ba71e24d7800092c";
        static void Main(string[] args)
        {
            VkApi vk = new VkApi();

            var webClient = new WebClient() { Encoding = Encoding.UTF8 };

            Console.WriteLine("Введите логин, пароль, ид группы вк. Каждое значение в новой строке");
            string log = Console.ReadLine();
            string pass = Console.ReadLine();
            string group = Console.ReadLine();
            vk.Authorize(new ApiAuthParams
            {
                ApplicationId = 6719999,
                Login = log,
                Password = pass,

                Settings = Settings.All | Settings.Messages
            });


            var param = new VkParameters() { };

            param.Add<string>("group_id", group);
            param.Add<string>("offset", "0");
            param.Add<string>("count 100", "");

            var json = JObject.Parse(vk.Call("groups.getMembers",param).RawJson);
           
                for (int i = 0; i < 100; i++)
                {

                    string idus = json["response"]["items"].ToArray()[i].ToString();
                    var paramUser = new VkParameters() { };
                    param.Add<string>("user_ids", idus);
                    param.Add<string>("fields", "city");
                    var city = JObject.Parse(vk.Call("users.get", param).RawJson);

                try
                {
                    string cityName = city["response"].ToArray()[0]["city"]["title"].ToString();

                    if (map.ContainsKey(cityName)) map[cityName]++;
                    else map.Add(cityName, 1);
                }
                catch (Exception) { }
                Console.WriteLine(i);
            }
                foreach (var temp in map)
                {
                    Console.WriteLine(temp.Key+":"+ temp.Value);
                }
             
            Console.WriteLine("end");
            Console.ReadKey();
        }
    }
}
