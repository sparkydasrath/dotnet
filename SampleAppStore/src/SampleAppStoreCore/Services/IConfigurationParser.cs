using System.Collections.Generic;

namespace SampleAppStoreCore.Services
{
    public interface IConfigurationParser<out T> where T : class
    {
        T Parse(string filePath);

        IEnumerable<T> Parse(IEnumerable<string> filePaths);
    }
}