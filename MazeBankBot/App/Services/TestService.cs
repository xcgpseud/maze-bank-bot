using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using MazeBankBot.App.DataModels;
using MazeBankBot.Database.Repositories;

namespace MazeBankBot.App.Services
{
    public class TestService
    {
        private readonly TestRepository _testRepository;

        public TestService() => _testRepository = new TestRepository();

        public async Task<TestModel> CreateTest(string title, string desc)
        {
            var entity = await _testRepository.Create(title, desc);

            return entity.Adapt<TestModel>();
        }

        public async Task<IEnumerable<TestModel>> GetTestsWhereTitleContains(string search)
        {
            var entities = await _testRepository.GetWhereTitleContains(search);

            return entities.Select(x => x.Adapt<TestModel>());
        }
    }
}