using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MazeBankBot.App.Handlers;

namespace MazeBankBot.App.Controllers
{
    [Group("role")]
    public class RoleController
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
    }
}