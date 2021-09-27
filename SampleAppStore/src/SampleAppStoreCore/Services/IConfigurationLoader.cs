using System.Collections.Generic;

namespace SampleAppStoreCore.Services
{
    /// <summary>
    /// The default <see langword="interface"/> for a service that handles loading JSON configuration files.
    /// </summary>
    public interface IConfigurationLoader
    {
        public string RootDirectory { get; set; }

        /// <summary>
        /// Returns a list of file paths for all the configuration files found matching the configurationFilePattern parameter starting at the given rootDirectory.
        /// </summary>
        /// <param name="rootDirectory">Location to start searching from.</param>
        /// <param name="configurationFilePattern">The name of the configuration file. This should be the same for each application being developed for the Helix platform.</param>
        /// <returns></returns>
        IEnumerable<string> LoadConfigurationFiles(string rootDirectory, string configurationFilePattern);
    }
}