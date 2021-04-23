using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MazeBankBot.Database.Entities
{
    public class ApplicationQuestionAnswerEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ApplicationQuestionId { get; set; }

        public string Content { get; set; }
    }
}