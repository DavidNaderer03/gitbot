using DSharpPlus;
using System.Text;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using DiscordBot.Utils;

namespace DiscordBot;

class Program
{
    public static DiscordClient Client;
    public static JSONReader Reader = new("config.json");
    private static CommandsNextExtension _command;
    public static async Task Main(string[] args)
    {
        Reader.Structor.Token = args[0];
        Task init = DiscordBot.Utils.Data.Init();

        var config = new DiscordConfiguration()
        {
            Intents = DiscordIntents.All,
            Token = Reader.Structor.Token,
            TokenType = TokenType.Bot,
            AutoReconnect = true
        };

        Client = new DiscordClient(config);
        Client.UseInteractivity(new DSharpPlus.Interactivity.InteractivityConfiguration()
        {
            Timeout = TimeSpan.FromMinutes(2)
        });
        Client.Ready += Ready;
        Client.MessageCreated += MessageCreatedHandler;
        Client.MessageDeleted += DeleteHandler;

        var commandsConfig = new CommandsNextConfiguration()
        {
            StringPrefixes = new[] { Reader.Structor.Prefix },
            EnableMentionPrefix = true,
            EnableDms = true,
            EnableDefaultHelp = false
        };

        _command = Client.UseCommandsNext(commandsConfig);
        var slash = Client.UseSlashCommands();

        _command.RegisterCommands<Commands>();
        slash.RegisterCommands<SlashCommands>();
        await init;
        await Client.ConnectAsync();
        await Task.Delay(-1);
    }

    private static Task Ready(DiscordClient client, ReadyEventArgs args)
    {
        return Task.CompletedTask;
    }
    private static async Task MessageCreatedHandler(DiscordClient client, MessageCreateEventArgs args)
    {
        await Data.AppendText($"{args.Author} has written a message");
    }
    private static async Task DeleteHandler(DiscordClient client, MessageDeleteEventArgs args)
    {
        await Data.AppendText($"{args.Message} has been deleted");
    }
}