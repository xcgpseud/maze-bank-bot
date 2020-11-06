using System.Collections.Generic;
using System.Linq;
using System.Text;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;

namespace MazeBankBot.App
{
    public class MazeHelpFormatter : IHelpFormatter
    {
        private StringBuilder _messageBuilder { get; }

        public MazeHelpFormatter() => _messageBuilder = new StringBuilder();

        public IHelpFormatter WithCommandName(string name)
        {
            _messageBuilder.Append("Command: ")
                .AppendLine(Formatter.Bold(name))
                .AppendLine();

            return this;
        }

        public IHelpFormatter WithDescription(string description)
        {
            _messageBuilder.AppendLine("Description: ")
                .AppendLine(description)
                .AppendLine();

            return this;
        }

        public IHelpFormatter WithArguments(IEnumerable<CommandArgument> arguments)
        {
            _messageBuilder.Append("Arguments: ")
                .AppendLine(string.Join(
                    ", ",
                    arguments.Select(xarg => $"{xarg.Name} ({xarg.Type.ToUserFriendlyName()})")
                ))
                .AppendLine();

            return this;
        }

        public IHelpFormatter WithAliases(IEnumerable<string> aliases)
        {
            _messageBuilder.Append("Aliases: ")
                .AppendLine(string.Join(", ", aliases))
                .AppendLine();

            return this;
        }

        public IHelpFormatter WithSubcommands(IEnumerable<Command> subcommands)
        {
            _messageBuilder.Append("Subcommands: ")
                .AppendLine(string.Join(", ", subcommands.Select(x => x.Name)))
                .AppendLine();

            return this;
        }

        public IHelpFormatter WithGroupExecutable()
        {
            _messageBuilder.AppendLine("This group is a standalone command.")
                .AppendLine();

            return this;
        }

        public CommandHelpMessage Build()
        {
            return new CommandHelpMessage(_messageBuilder.ToString().Replace("\r\n", "\n"));
        }
    }
}