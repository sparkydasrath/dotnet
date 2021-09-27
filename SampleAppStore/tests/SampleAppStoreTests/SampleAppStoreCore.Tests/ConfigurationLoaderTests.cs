using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SampleAppStoreCore.Services;

namespace SampleAppStoreTests.SampleAppStoreCore.Tests
{
    [TestFixture]
    public class ConfigurationLoaderTests
    {
        private const string RootDirectory = @"E:\repos\dotnet\SampleAppStore\src\SampleAppStoreCore\AppConfigs";
        private const string FilePattern = "config_*";


        [Test]
        public void ShouldLoadCorrectNumberOfConfigurationFiles()
        {

            ConfigurationLoader configurationLoader = new();
            IEnumerable<string> files = configurationLoader.LoadConfigurationFiles(RootDirectory, FilePattern);

            Assert.That(files.Count(), Is.EqualTo(4));
        }
        
    }
}