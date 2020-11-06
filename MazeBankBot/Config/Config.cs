using System.IO;
using MazeBankBot.Config.DataModels;
using Newtonsoft.Json;

namespace MazeBankBot.Config
{
    public class Config
    {
        private const string ConfigFile = "config.json";
        private ConfigModel _configModel;

        public static Config Make()
        {
            return new Config();
        }

        public ConfigModel Get()
        {
            if (_configModel == null)
            {
                SetConfigModel();
            }

            return _configModel;
        }

        private void SetConfigModel()
        {
            using var file = File.OpenText(ConfigFile);
            using var reader = new JsonTextReader(file);

            var s = new JsonSerializer();
            _configModel = s.Deserialize<ConfigModel>(reader);
        }
    }
}