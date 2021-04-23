using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using MazeBankBot.App.Handlers;

namespace MazeBankBot.App.Controllers
{
    [Group("test")]
    public class TestController : BaseCommandModule
    {
        private readonly TestHandler _testHandler;

        public TestController() => _testHandler = new TestHandler();

        [Command("bot-working")]
        public async Task BotWorking(CommandContext ctx)
        {
            await Executor.Execute(async () => await ctx.RespondAsync("I am working."));
        }

        [Command("create")]
        [Description("Create a test entity in the database")]
        public async Task Create(CommandContext ctx, string title, string desc)
        {
            await Executor.Execute(async () =>
                await ctx.RespondAsync(embed: await _testHandler.CreateTest(title, desc))
            );
        }

        [Command("find")]
        [Description("Find all test entities with a title search")]
        public async Task Find(CommandContext ctx, string search)
        {
            await Executor.Execute(async () =>
                await ctx.RespondAsync(embed: await _testHandler.GetTestsWhereTitleContains(search))
            );
        }
    }
}