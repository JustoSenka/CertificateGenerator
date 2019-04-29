using CertificateGenerator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void ExportCertificate()
        {
            var cert = Cert.CreateCertificate("MouseRobot", 10);
            Cert.ExportCertificate(cert, "TestCert", "test");
        }

        [TestMethod]
        public void ExportCertificateSelfSigned()
        {
            var cert = Cert.CreateSelfSignedCertificate("MouseRobot", 10);
            Cert.ExportCertificate(cert, "TestCertSelfSigned", "test");
        }
    }
}
