using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class Scouts
    {
        public Scout[] Scouting { get; private set; }
        private string _path = string.Empty;

        public Scouts(string pathScouts, string pathPairs)
        {
            _path += pathScouts + ";" + pathPairs;
        }

        public async Task Read()
        {
            string scouts = _path.Split(';')[0];
            string pair = _path.Split(";")[1];
            string[] linesPair = File.ReadAllLines(pair);
            string[] lines = File.ReadAllLines(scouts);
            List<Scout> list = new List<Scout>();
            foreach (string line in lines)
            {
                list.Add(new Scout(line));
            }
            Scouting = list.ToArray();
            foreach (string line in linesPair)
            {
                Scout scout = Scouting.Where(x => x.Name == line.Split(';')[0]).First();
                await scout.FindPair(Scouting, line);
            }
        }

        public async Task<(Scout, Scout)> GetRandom()
        {
            Random rd = new();
            int index = rd.Next(0, Scouting.Length);
            Scout[] selectedScouts = Scouting;
            if (Scouting[index].Pair != null)
            {
                selectedScouts = Scouting[index].Pair;
            }
            int pairIndex = rd.Next(0, selectedScouts.Length);
            while(index == pairIndex)
            {
                pairIndex = rd.Next(0, selectedScouts.Length);
            }
            return (Scouting[index], selectedScouts[pairIndex]);
        }

        public async Task<int> IndexOf(Scout scout)
        {
            for(int i = 0; i < Scouting.Length; i++)
            {
                if (Scouting[i] == scout)
                    return i;
            }
            return -1;
        }
    }
}
