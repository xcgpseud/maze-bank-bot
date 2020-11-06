using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;

namespace MazeBankBot.App.Controllers
{
    [Group("poll")]
    public class PollController
    {
        [Command("create")]
        [Description("Create a poll with emojis")]
        public async Task Poll(
            CommandContext ctx,
            [System.ComponentModel.Description("How long should the poll last?")]
            TimeSpan duration,
            [System.ComponentModel.Description("Which options should the poll have?")]
            params DiscordEmoji[] options
        )
        {
            var interactivity = ctx.Client.GetInteractivityModule();
            var pollOptions = options.Select(x => x.ToString());

            var embed = new DiscordEmbedBuilder
            {
                Title = "**Poll**",
                Description = string.Join(" ", pollOptions)
            };
            var msg = await ctx.RespondAsync(embed: embed);

            options.ToList().ForEach(x => msg.CreateReactionAsync(x));

            var pollResult = await interactivity.CollectReactionsAsync(msg, duration);
            var results = pollResult.Reactions
                .Where(x => options.Contains(x.Key))
                .Select(x => $"{x.Key}: {x.Value}");

            await ctx.RespondAsync(string.Join("\n", results));
        }
    }
}