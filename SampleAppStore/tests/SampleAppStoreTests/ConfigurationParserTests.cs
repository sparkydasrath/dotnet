using System.Collections.Generic;
using NUnit.Framework;
using SampleAppStoreCore.Services;

namespace SampleAppStoreTests
{
    [TestFixture]
    public class ConfigurationParserTests
    {
        private const string RootDirectory = @"E:\repos\dotnet\SampleAppStore\src\SampleAppStoreCore\AppConfigs";
        private const string FilePattern = "config_*";

        [Test]
        public void ShouldLoadConfigurationFromJsonFileSuccessfully()
        {
            ConfigurationLoader configurationLoader = new();
            IEnumerable<string> paths = configurationLoader.LoadConfigurationFiles(RootDirectory, FilePattern);
            ConfigurationParser<ApplicationConfiguration> cp = new(configurationLoader);

            IEnumerable<ApplicationConfiguration> configs = cp.Parse(paths);

            // ApplicationConfiguration result = cp.Parse();

            // Assert.IsNotNull(result);
        }
    }
}