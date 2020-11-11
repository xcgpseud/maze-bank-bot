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

        public async Task GiveRole(CommandContext ctx, DiscordMember member, string roleName)
        {
            /*
             * Search for a role.
             * If there is only one role, assign it to the user.
             * If multiple match the search, return a list with IDs and use Interactivity to await a response from
             * the same user.
             */
            var roles = _roleService.SearchForRoles(ctx.Guild, roleName);

            if (roles.Count == 0)
            {
                await ctx.RespondAsync($"There was no role found with the name **{roleName}**");
                return;
            }

            if (roles.Count == 1)
            {
                var role = roles.First();
                await _roleService.GiveRole(ctx, member, role);
                return;
            }

            await _roleService.ChooseRole(ctx, member, roles);
        }
    }
}