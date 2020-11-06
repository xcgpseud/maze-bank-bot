using System;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using MazeBankBot.App.Controllers;
using MazeBankBot.Database;

namespace MazeBankBot.App
{
    public class Bot
    {
        private DiscordClient _discordClient;
        private CommandsNextModule _commandsNextModule;
        private readonly Config.Config _config;

        public Bot()
        {
            _config = Config.Config.Make();

            using var db = new SqliteContext();
            db.Database.EnsureCreated();
        }

        public static Bot Make()
        {
            return new Bot();
        }

        public async Task Start()
        {
            _discordClient = new DiscordClient(GetDiscordConfiguration());
            _discordClient.UseInteractivity(GetInteractivityConfiguration());

            _commandsNextModule = _discordClient.UseCommandsNext(GetCommandsNextConfiguration());

            _commandsNextModule.RegisterCommands<TestController>();
            _commandsNextModule.RegisterCommands<PollController>();

            _commandsNextModule.SetHelpFormatter<MazeHelpFormatter>();

            await _discordClient.ConnectAsync();
            await Task.Delay(-1);
        }

        public DiscordConfiguration GetDiscordConfiguration()
        {
            return new DiscordConfiguration
            {
                Token = _config.Get().Bot.Token,
                TokenType = TokenType.Bot,

                AutoReconnect = true,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug,
            };
        }

        public CommandsNextConfiguration GetCommandsNextConfiguration()
        {
            var deps = new DependencyCollectionBuilder()
                .AddInstance(_config)
                .Build();

            return new CommandsNextConfiguration
            {
                StringPrefix = _config.Get().Bot.CommandPrefix,
                Dependencies = deps,
            };
        }

        public InteractivityConfiguration GetInteractivityConfiguration()
        {
            return new InteractivityConfiguration
            {
                PaginationBehaviour = TimeoutBehaviour.Ignore,
                PaginationTimeout = TimeSpan.FromMinutes(5),
                Timeout = TimeSpan.FromMinutes(2),
            };
        }
    }
}