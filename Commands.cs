using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    public class Commands : BaseCommandModule
    {
        [Command("print")]
        public async Task PrintOut(CommandContext context, string ct)
        {
            await context.Channel.SendMessageAsync(ct);
        }

        [Command("scout")]
        public async Task WriteScout(CommandContext context)
        {
            var interact = Program.Client.GetInteractivity();
            var msg = await interact.WaitForMessageAsync(message => message.Content != string.Empty);
            await context.Channel.SendMessageAsync($"{msg.Result.Content} with user: {context.User.Username}");
        }
    }
}
