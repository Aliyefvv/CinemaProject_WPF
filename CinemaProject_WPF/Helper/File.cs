using CinemaProject_WPF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaProject_WPF.Helper
{
    public static class File
    {
        public static void WriteJSON(object obj, string filename)
        {
            var serializer = new JsonSerializer();
            using (var sw = new StreamWriter(filename))
            using (var jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Newtonsoft.Json.Formatting.Indented;
                serializer.Serialize(jw, obj);
            }
        }

        public static List<User> ReadJSON_Users(string filename)
        {
            var serializer = new JsonSerializer();
            var fs = new FileStream(filename, FileMode.OpenOrCreate);
            using (var sr = new StreamReader(fs))
            using (var jr = new JsonTextReader(sr))
                return serializer.Deserialize<List<User>>(jr);
        }

        public static void PrintVaucher(Ticket ticket)
        {
            Guid ID = Guid.NewGuid();
            using (FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
                 + "\\\\" + ID.ToString() + ".txt", FileMode.Create))
            using (StreamWriter sw = new StreamWriter(fs, Encoding.UTF8))
            {
                sw.WriteLine($"============================================");
                sw.WriteLine($" Date : {DateTime.Now.ToLongDateString()} {DateTime.Now.ToLongTimeString()}");
                sw.WriteLine($"============================================");
                sw.WriteLine($" Movie Name : {ticket?.MovieName}");
                sw.WriteLine($" Date : {ticket?.Date}");
                sw.WriteLine($" Time : {ticket?.Time}");
                sw.WriteLine($" Ticket Count : {ticket?.TicketCount}");
                string nums = "";
                foreach (var number in ticket?.TicketNumbers)
                    nums += number.ToString() + ", ";
                sw.WriteLine($" Ticket Numbers : {nums}");
                sw.WriteLine($"--------------------------------------------");
                sw.WriteLine($" TOTAL : {ticket?.Total} azn");
                sw.WriteLine($"============================================");
            }
        }
    }
}
