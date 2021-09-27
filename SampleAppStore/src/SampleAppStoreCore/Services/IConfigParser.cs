using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleAppStoreCore
{
    public interface IConfigParser<out T> where T: class
    {
        T Parse(string filePath);
    }

    public class ConfigParser<T> : IConfigParser<T> where T : class
    {
        public T Parse(string filePath)
        {
            throw new NotImplementedException();
        }
    }

    public class AppConfig
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public string Publisher { get; set; }
        public string Repository { get; set; }
    }
}
