using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace QuickHelper.Configuration
{
    public class JsonAppConfigProvider : IAppConfigProvider
    {
        private static string UserDocumentsFolder => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private static string UserFolder => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

        private const string AppConfigFileName = "_quickhelper.json";

        private static IEnumerable<string> ConfigFilePath =>
            new List<string>()
            {
                Path.Combine(UserFolder, AppConfigFileName),
                Path.Combine(UserDocumentsFolder, AppConfigFileName)
            };

        private static string FirstConfigFile => ConfigFilePath.FirstOrDefault(File.Exists);

        public IAppConfig GetConfig()
        {
            try
            {
                if (FirstConfigFile == null)
                {
                    var configPath = ConfigFilePath.First();
                    File.WriteAllText(configPath, "");
                    return JsonAppConfig.EmptyConfig;
                }

                var configContent = File.ReadAllText(FirstConfigFile);

                if (string.IsNullOrWhiteSpace(configContent))
                    return JsonAppConfig.EmptyConfig;

                var config = JsonConvert.DeserializeObject<JsonAppConfig>(configContent);
                return config;
            }
            catch (Exception) { }

            return JsonAppConfig.EmptyConfig;
        }
    }
}