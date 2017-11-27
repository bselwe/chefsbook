using System.IO;
using System.Security;
using System.Security.Cryptography.X509Certificates;

namespace ChefsBook.Data
{
    public static class Cert
    {
        private const string LocalhostCert = "Data/localhost.pfx";
        private const string CertPassword = "Food1App!";

        public static X509Certificate2 Load()
        {
            return new X509Certificate2(FindCert(), CertPassword);
        }

        private static string FindCert()
        {
            return File.Exists("./" + LocalhostCert) ?
                "./" + LocalhostCert :
                "../" + LocalhostCert;
        }

    }
}
