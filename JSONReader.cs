using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class JSONReader
    {
        public JSONStructor Structor { get; set; }

        public JSONReader(string file)
        {
            using StreamReader sr = new StreamReader(file);
            string json = sr.ReadToEnd();
            Structor = JsonConvert.DeserializeObject<JSONStructor>(json);
        }
    }

    public class JSONStructor
    {
        public string Token { get; set; }
        public string Prefix { get; set; }
        public string[] Validid { get; set; }
    }
}
