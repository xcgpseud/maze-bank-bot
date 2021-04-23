using System;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using MazeBankBot.App.Services;

namespace MazeBankBot.App.Handlers
{
    public class RoleHandler
    {
        private readonly RoleService _roleService;

        public RoleHandler() => _roleService = new RoleService();

        public async Task RequestRole(CommandContext ctx, string roleName)
        {
            var userId = ctx.Member.Id;

            var roles = _roleService.SearchForRoles(ctx.Guild, roleName);

            if (roles.Count == 0)
            {
                await ctx.RespondAsync($"There were no roles found with the name **{roleName}**");
                return;
            }

            var role = roles.Count == 1
                ? roles.First()
                : await _roleService.ChooseRole(ctx, ctx.Member, roles);

            await _roleService.CreateRoleRequest(role.Id, userId);

            await ctx.RespondAsync($"Requested the **{role.Name}** role.");

            var embedBuilder = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Red,
                Title = $"**Role Request**",
                Description = $"{ctx.Member.DisplayName} has requested a role."
            };

            embedBuilder.AddField(
                "Role Requested",
                $"{role.Name}"
            );

            embedBuilder.AddField(
                "Username",
                $"{ctx.Member.Username}#{ctx.Member.Discriminator}"
            );

            var channel = ctx.Guild.Channels.FirstOrDefault(x =>
                x.Value.Id == Config.Config.Get().Bot.RoleRequestChannel
            ).Value;

            if (channel == null)
            {
                await ctx.RespondAsync("We've encountered an error. Please seek a member of staff.");
                return;
            }

            var msg = await channel.SendMessageAsync(embed: embedBuilder.Build());

            var tick = DiscordEmoji.FromName(ctx.Client, ":white_check_mark:");
            var cross = DiscordEmoji.FromName(ctx.Client, ":x:");

            await msg.CreateReactionAsync(tick);
            await msg.CreateReactionAsync(cross);
        }

        public async Task GiveRole(CommandContext ctx, DiscordMember member, string roleName)
        {
            var roles = _roleService.SearchForRoles(ctx.Guild, roleName);

            if (roles.Count == 0)
            {
                await ctx.RespondAsync($"There were no roles found with the name **{roleName}**");
                return;
            }

            var role = roles.Count == 1
                ? roles.First()
                : await _roleService.ChooseRole(ctx, member, roles);

            await _roleService.GiveRole(ctx, member, role);
        }
    }
}