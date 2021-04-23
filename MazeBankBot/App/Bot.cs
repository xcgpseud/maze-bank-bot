using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using MazeBankBot.App.Controllers;
using MazeBankBot.App.EventHandlers;
using MazeBankBot.Database;

namespace MazeBankBot.App
{
    public class Bot
    {
        private DiscordClient _discordClient;
        private CommandsNextExtension _commandsNextModule;

        public Bot()
        {
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

            _discordClient.MessageReactionAdded += ReactionEventHandler.MessageReactionAddEvent;

            _commandsNextModule = _discordClient.UseCommandsNext(GetCommandsNextConfiguration());

            _commandsNextModule.RegisterCommands<TestController>();
            _commandsNextModule.RegisterCommands<RoleController>();

            await _discordClient.ConnectAsync();
            await Task.Delay(-1);
        }

        public DiscordConfiguration GetDiscordConfiguration()
        {
            return new DiscordConfiguration
            {
                Token = Config.Config.Get().Bot.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            };
        }

        public CommandsNextConfiguration GetCommandsNextConfiguration()
        {
            return new CommandsNextConfiguration
            {
                StringPrefixes = new List<string>
                {
                    Config.Config.Get().Bot.CommandPrefix,
                },
            };
        }

        public InteractivityConfiguration GetInteractivityConfiguration()
        {
            return new InteractivityConfiguration
            {
                PaginationBehaviour = PaginationBehaviour.Ignore,
                Timeout = TimeSpan.FromMinutes(2),
            };
        }
    }
}