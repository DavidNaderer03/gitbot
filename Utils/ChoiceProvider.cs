using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot.Utils
{
    public class ChoiceProvider : IChoiceProvider
    {
        public async Task<IEnumerable<DiscordApplicationCommandOptionChoice>> Provider()
        {
            var query = Data.AllScouts.Scouting.Select(x => new DiscordApplicationCommandOptionChoice(x.Name, x.Name));
            return query.ToList();
        }
    }
}
