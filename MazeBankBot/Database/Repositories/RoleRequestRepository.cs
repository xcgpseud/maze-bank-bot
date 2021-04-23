using System.Linq;
using System.Threading.Tasks;
using MazeBankBot.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace MazeBankBot.Database.Repositories
{
    public class RoleRequestRepository
    {
        public async Task<RoleRequestEntity> Create(ulong roleId, ulong userId)
        {
            using var db = new SqliteContext();

            var entity = new RoleRequestEntity
            {
                RoleId = roleId,
                UserId = userId,
                Approved = false,
            };

            await db.RoleRequests.AddAsync(entity);
            await db.SaveChangesAsync();

            return entity;
        }

        public async Task<RoleRequestEntity> GetById(int id)
        {
            using var db = new SqliteContext();

            return await db.RoleRequests.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<RoleRequestEntity> ApproveRoleRequest(int id)
        {
            using var db = new SqliteContext();

            var entity = await db.RoleRequests.FirstOrDefaultAsync(x => x.Id == id);
            if (entity != null)
            {
                entity.Approved = true;
                await db.SaveChangesAsync();
            }

            return entity;
        }
    }
}