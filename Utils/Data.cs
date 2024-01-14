using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Utils
{
    public class Data
    {
        public static Scouts AllScouts = new("scouts.csv", "pair.csv");
        public const string LOG_FILE = "log.txt";
        public static async Task Init()
        {
            await AllScouts.Read();
        }

        public static async Task AppendText(string text)
        {
            using(StreamWriter sw = File.AppendText(LOG_FILE))
            {
                sw.WriteLine(text);
            }
        }
    }
}
