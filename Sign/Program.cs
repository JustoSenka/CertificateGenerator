using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Sign
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                Console.WriteLine("Incorrect arguments");
                Console.WriteLine("");
                Console.WriteLine("1st arg should be path to MS signtool.exe. It's usually in Microsoft SDKs\\ClickOnce\\SignTool");
                Console.WriteLine("2nd arg should be path to certificate. Either .pfx or .cer");
                Console.WriteLine("All following args should be paths to .dll and .exe to sign.");
                return;
            }

            foreach (var dllExePath in args.Skip(2))
            {
                if (args[1].EndsWith(".pfx", StringComparison.InvariantCultureIgnoreCase))
                    SighWithPfx(args[0], args[1], dllExePath);
                else if (args[1].EndsWith(".cer", StringComparison.InvariantCultureIgnoreCase))
                    SignWithCer(args[0], args[1], dllExePath);
                else
                    Console.WriteLine("Unknown certificate or wrong path: " + args[0]);
            }
        }

        public static void SighWithPfx(string signToolPath, string certPath, string dllExePath)
        {
            Console.WriteLine(RunCmdLine(signToolPath, $"sign /f {certPath} {dllExePath}"));
        }

        public static void SignWithCer(string signToolPath, string certPath, string dllExePath)
        {
            var cer = new X509Certificate2();
            cer.Import(ReadFile($"{certPath}"));
            var thumbprint = cer.Thumbprint;
            Console.WriteLine($"{thumbprint} certificate loaded.");

            //Add the certificate to a X509Store.
            X509Store store = new X509Store();
            store.Open(OpenFlags.MaxAllowed);
            store.Add(cer);
            store.Close();

            Console.WriteLine(RunCmdLine(signToolPath, $"sign /sha1 {thumbprint} {dllExePath}"));
        }

        private static string RunCmdLine(string exePath, string args)
        {
            var p = Process.Start(new ProcessStartInfo()
            {
                FileName = exePath,
                Arguments = args,
                RedirectStandardOutput = true,
                UseShellExecute = false,
            });

            var str = p.StandardOutput.ReadToEnd();
            p.WaitForExit();
            return str;
        }

        private static byte[] ReadFile(string fileName)
        {
            FileStream f = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            int size = (int)f.Length;
            byte[] data = new byte[size];
            size = f.Read(data, 0, size);
            f.Close();
            return data;
        }
    }
}
