using System.Diagnostics;
using System.Runtime.Versioning;

namespace WinFormsLibrary1
{
    [SupportedOSPlatform("windows")]
    public class Class1
    {
        public Class1()
        {
            Print();
        }

        public void Print()
        {
            Debug.WriteLine("testing");
        }
    }
}