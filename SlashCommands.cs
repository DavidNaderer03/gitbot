using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordBot.Utils;

namespace DiscordBot
{
    public class SlashCommands : ApplicationCommandModule
    {
        
        [SlashCommand("getscout", "this is a test")]
        public async Task RandomPair(InteractionContext interactionContext)
        {
            await interactionContext.CreateResponseAsync(DSharpPlus.InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                    .WithContent("..."));
            if (Program.Reader.Structor.Validid.Any(x => x == interactionContext.User.Id.ToString()))
            {

                Task<(Scout, Scout)> task = Data.AllScouts.GetRandom();
                await task;
                var msg = new DiscordEmbedBuilder()
                {
                    Title = "Dating",
                    Description = $"{task.Result.Item1.Name} and {task.Result.Item2.Name}",
                    Color = DiscordColor.Red
                };
                await interactionContext.Channel.SendMessageAsync(msg);
            }
            else
            {
                Random rd = new();
                int index = rd.Next(0, Data.AllScouts.Scouting.Length);
                await interactionContext.Channel.SendMessageAsync(Data.AllScouts.Scouting[index].ToString());
                await interactionContext.Channel.Users.Where(x => x.Username == "daffi989989").First().SendMessageAsync($"{interactionContext.User.Username} used getscout command");
            }
        }

        [SlashCommand("getscoutbyname", "returns two scouts")]
        public async Task GetPairByName(InteractionContext context,
            [ChoiceProvider(typeof(DiscordBot.Utils.ChoiceProvider))]
            [Option("name", "The name of the scout")] string name)
        {

            await context.CreateResponseAsync(DSharpPlus.InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                .WithContent("..."));
            if (Program.Reader.Structor.Validid.Any(x => x == context.User.Id.ToString()))
            {
                Scout scout = Data.AllScouts.Scouting.Where(x => x.Name == name).First();
                Task<int> task = Data.AllScouts.IndexOf(scout);
                Random rd = new();
                Scout[] validScouts = Data.AllScouts.Scouting;
                if (scout.Pair != null)
                    validScouts = scout.Pair;
                int index = rd.Next(0, validScouts.Length);
                await task;
                while (task.Result == index)
                    index = rd.Next(0, validScouts.Length);
                var msg = new DiscordEmbedBuilder()
                {
                    Title = "Dating",
                    Description = $"{scout.Name} and {validScouts[index].Name}",
                    Color = DiscordColor.Red
                };
                await context.Channel.SendMessageAsync(msg);
            }
            else
            {
                Random rd = new();
                int index = rd.Next(0, Data.AllScouts.Scouting.Length);
                await context.Channel.SendMessageAsync(Data.AllScouts.Scouting[index].ToString());
                await context.Channel.Users.Where(x => x.Username == "daffi989989").First().SendMessageAsync($"{context.User.Username} used getscout command");
            }
        }
        

        [SlashCommand("tod", "Selects anyone on the server")]
        public async Task RandomSelect(InteractionContext context, [Option("Reason", "Enter a reason")] string reason)
        {
            Random rd = new();
            await context.CreateResponseAsync(DSharpPlus.InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                .WithContent("..."));
            var allMembers = await context.Guild.GetAllMembersAsync();
            if (reason == "top")
                allMembers = (IReadOnlyCollection<DiscordMember>)allMembers.Where(x => x.Username != "daffi");
            int index = rd.Next(0, allMembers.Count);
            int i = 0;
            bool found = index == i;
            DiscordMember selectedMember = allMembers.First();
            foreach ( var member in allMembers )
            {
                if (i == index)
                {
                    selectedMember = member;
                    break;
                }
                i++;
            }
            await context.Channel.SendMessageAsync(new DiscordEmbedBuilder()
            {
                Description = $"{selectedMember.Username} is the one"
            });
        }
    }
}
