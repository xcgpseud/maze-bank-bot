using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MazeBankBot.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace MazeBankBot.Database.Repositories
{
    public class TestRepository
    {
        public async Task<TestEntity> Create(string title, string desc)
        {
            using var db = new SqliteContext();

            var entity = new TestEntity
            {
                Title = title,
                Description = desc,
            };

            await db.Tests.AddAsync(entity);
            await db.SaveChangesAsync();

            return entity;
        }

        public async Task<IEnumerable<TestEntity>> GetWhereTitleContains(string search)
        {
            using var db = new SqliteContext();

            return await db.Tests
                .Where(x => x.Title.ToLower().Contains(search.ToLower()))
                .ToListAsync();
        }
    }
}