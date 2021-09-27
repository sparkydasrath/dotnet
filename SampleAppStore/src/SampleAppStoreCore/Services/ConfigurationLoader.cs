using Microsoft.Toolkit.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace SampleAppStoreCore.Services
{
    public class ConfigurationLoader : IConfigurationLoader
    {
        public string RootDirectory { get; set; }

        public IEnumerable<string> LoadConfigurationFiles(string rootDirectory, string configurationFilePattern)
        {
            Guard.IsNotNullOrWhiteSpace(rootDirectory, nameof(rootDirectory));
            Guard.IsNotNullOrWhiteSpace(configurationFilePattern, nameof(configurationFilePattern));

            string[] configFiles =
                Directory.GetFiles(rootDirectory, configurationFilePattern, SearchOption.AllDirectories);

            return configFiles;
        }
    }
}