using Microsoft.Extensions.Configuration;
using Microsoft.Toolkit.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace SampleAppStoreCore.Services
{
    public class ConfigurationParser<T> : IConfigurationParser<T> where T : class
    {
        private const string RootDirectory = @"E:\repos\dotnet\SampleAppStore\src\SampleAppStoreCore\AppConfigs";
        private const string FilePattern = "config_*";

        private readonly IConfigurationLoader configurationLoader;
        private readonly IConfigurationBuilder configurationBuilder;
        private IConfiguration configuration;
        private readonly IEnumerable<string> configFilePaths;

        public ConfigurationParser(IConfigurationLoader configurationLoader)
        {
            this.configurationLoader = configurationLoader;
            configurationBuilder = new ConfigurationBuilder();

            configFilePaths = configurationLoader.LoadConfigurationFiles(RootDirectory, FilePattern);
        }

        public T Parse(string filePath)
        {
            configurationBuilder.AddJsonFile(filePath, true, true);
            configuration = configurationBuilder.Build();
            T configModel = configuration.Get<T>();
            return configModel;
        }

        public IEnumerable<T> Parse(IEnumerable<string> filePaths)
        {
            IEnumerable<string> paths = filePaths.ToList();
            Guard.IsNotNull(paths, nameof(filePaths));
            return paths.Select(Parse).ToList();
        }
    }
}