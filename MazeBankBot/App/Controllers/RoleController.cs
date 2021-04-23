using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MazeBankBot.App.Handlers;

namespace MazeBankBot.App.Controllers
{
    [Group("role")]
    [RequirePermissions(Permissions.Administrator)]
    public class RoleController : BaseCommandModule
    {
        private readonly RoleHandler _roleHandler;

        public RoleController() => _roleHandler = new RoleHandler();

        [Command("add")]
        public async Task GiveRole(CommandContext ctx, DiscordMember member, string roleName)
        {
            await Executor.Execute(async () =>
                await _roleHandler.GiveRole(ctx, member, roleName)
            );
        }

        [Command("request")]
        public async Task RequestRole(CommandContext ctx, string roleName)
        {
            await Executor.Execute(async () =>
                await _roleHandler.RequestRole(ctx, roleName)
            );
        }
    }
}