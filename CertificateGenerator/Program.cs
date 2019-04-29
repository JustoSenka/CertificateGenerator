using System;

namespace CertificateGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 4)
            {
                Console.WriteLine("Incorrect arguments");
                Console.WriteLine("");
                Console.WriteLine("1st arg should be subject & issuer name.");
                Console.WriteLine("2nd arg should amount of years for certificate to be valid.");
                Console.WriteLine("3rd arg should be file path without extension. ");
                Console.WriteLine("4th arg should be password for private key.");
                return;
            }

            var cert = Cert.CreateCertificate(args[0], int.Parse(args[1]));
            Cert.ExportCertificate(cert, args[2], args[3]);
        }
    }
}
