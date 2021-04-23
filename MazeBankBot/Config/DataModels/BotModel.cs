using System.Collections.Generic;
using Newtonsoft.Json;

namespace MazeBankBot.Config.DataModels
{
    public class BotModel
    {
        [JsonProperty("command-prefix")] public string CommandPrefix { get; set; }

        [JsonProperty("superusers")] public List<ulong> Superusers { get; set; }

        [JsonProperty("token")] public string Token { get; set; }

        [JsonProperty("role-request-channel")] public ulong RoleRequestChannel { get; set; }

        [JsonProperty("role-guy-id")] public ulong RoleGuyId { get; set; }
    }
}