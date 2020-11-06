using Newtonsoft.Json;

namespace MazeBankBot.Config.DataModels
{
    public class ConfigModel
    {
        [JsonProperty("bot")] public BotModel Bot { get; set; }
    }
}