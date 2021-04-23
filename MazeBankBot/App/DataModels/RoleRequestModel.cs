namespace MazeBankBot.App.DataModels
{
    public class RoleRequestModel
    {
        public int Id { get; set; }
        public ulong RoleId { get; set; }
        public ulong UserId { get; set; }
        public bool Approved { get; set; }
    }
}