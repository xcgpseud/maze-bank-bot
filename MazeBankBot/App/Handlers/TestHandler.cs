using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.Entities;
using MazeBankBot.App.Services;

namespace MazeBankBot.App.Handlers
{
    public class TestHandler
    {
        private readonly TestService _testService;

        public TestHandler() => _testService = new TestService();

        public async Task<DiscordEmbed> CreateTest(string title, string desc)
        {
            var test = await _testService.CreateTest(title, desc);

            return new DiscordEmbedBuilder
                {
                    Color = DiscordColor.Chartreuse,
                    Title = $"{test.Id}: {test.Title}",
                    Description = test.Description,
                }
                .Build();
        }

        public async Task<DiscordEmbed> GetTestsWhereTitleContains(string search)
        {
            var tests = await _testService.GetTestsWhereTitleContains(search);
            var testsList = tests.ToList();

            var embedBuilder = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Chartreuse,
                Title = $"Found: {testsList.Count}",
            };

            testsList.ForEach(test => embedBuilder.AddField($"{test.Id}: {test.Title}", test.Description));

            return embedBuilder.Build();
        }
    }
}