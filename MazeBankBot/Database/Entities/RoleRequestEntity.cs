using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MazeBankBot.Database.Entities
{
    public class RoleRequestEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public ulong RoleId { get; set; }

        public ulong UserId { get; set; }

        public bool Approved { get; set; }
    }
}