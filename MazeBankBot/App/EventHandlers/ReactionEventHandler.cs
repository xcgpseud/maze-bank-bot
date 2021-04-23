using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace MazeBankBot.App.EventHandlers
{
    public static class ReactionEventHandler
    {
        public static async Task MessageReactionAddEvent(DiscordClient client, MessageReactionAddEventArgs args)
        {
            if (args.User.IsBot)
            {
                return;
            }

            if (args.Channel.Id == Config.Config.Get().Bot.RoleRequestChannel)
            {
                await InvokeRoleRequestResponse(client, args);
            }
        }

        private static async Task InvokeRoleRequestResponse(DiscordClient client, MessageReactionAddEventArgs args)
        {
            var member = args.Guild.Members.FirstOrDefault(x =>
                x.Value.Username == args.User.Username &&
                x.Value.Discriminator == args.User.Discriminator
            ).Value;

            if (member.Roles.FirstOrDefault(x => x.Id == Config.Config.Get().Bot.RoleGuyId) != null)
            {
                await args.Channel.SendMessageAsync($"You reacted: {args.Emoji.Name}");
            }
            else
            {
                await args.Channel.SendMessageAsync("You don't have the role guy role.");
            }
        }
    }
}