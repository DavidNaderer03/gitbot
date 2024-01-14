using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class Scout
    {
        public readonly string Name;
        public readonly int Age;
        public Scout[]? Pair { get; private set; }

        public Scout(string line)
        {
            string[] splitted = line.Split(';');
            Name = splitted[0];
            Age = int.Parse(splitted[1]);
        }

        public async Task FindPair(Scout[] allScouts, string line)
        {
            string[] names = line.Split(';');
            string[] pairTwist = names[1].Split(',');
            Scout[] selectedScouts = allScouts.Where(x => pairTwist.Any(y => y == x.Name)).ToArray();
            if (selectedScouts.Length != 0)
                Pair = selectedScouts;
        }

        public override string ToString()
        {
            return $"{Name} is pinned";
        }
    }
}
