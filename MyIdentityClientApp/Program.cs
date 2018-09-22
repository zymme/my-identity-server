using System;
using IdentityModel.Client;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Xml;

namespace MyIdentityClientApp
{
    class Program
    {
        

        const string test_xml = "<saml2p:Response xmlns:saml2p=\"urn:oasis:names:tc:SAML:2.0:protocol\" " +
                 "xmlns:saml2=\"urn:oasis:names:tc:SAML:2.0:assertion\" ID=\"_07f7d23303114d22820250370ae8ee75\" " +
                 "Version=\"2.0\" IssueInstant=\"2018-09-19T22:38:57Z\" Destination=\"https://peak10dev.service-now.com/navpage.do\" " +
                 "InResponseTo=\"SNCc483548e4da330edfe50365baab94110\">" +
                "<saml2:Issuer>http://localhost:5000</saml2:Issuer>" +
            "</saml2p:Response>";


        static void Main(string[] args)
        {
            Console.WriteLine("Hello There! Grabbing the discovery info for  MyIdentityServer...");

            Program program = new Program();
            Thread.Sleep(3000); // arbitrary amount of time to allow multi project launch


            //program.GetTokenAsync().Wait();
            //program.GetTokenResourceOwnerPasswordAsync().Wait();
            //program.DecodeSamlRequest();
            program.SetupSamlResponse();

            Console.WriteLine("Press ctrl+c to quit ...");

            Console.ReadLine();
        }


        private void SetupSamlResponse()
        {

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(XMLConstants.SAML_RESPONSE);

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xml.NameTable);
            nsmgr.AddNamespace("saml2p", "urn:oasis:names:tc:SAML:2.0:protocol");

            XmlNamespaceManager nsmgr2 = new XmlNamespaceManager(xml.NameTable);
            nsmgr2.AddNamespace("saml2", "urn:oasis:names:tc:SAML:2.0:assertion");

            XmlNamespaceManager nsmg3 = new XmlNamespaceManager(xml.NameTable);
            nsmg3.AddNamespace("http://www.w3.org/2000/09/xmldsig#", "http://www.w3.org/2000/09/xmldsig#");

            XmlElement root = xml.DocumentElement;
            root.SetAttribute("InResponseTo", "99992929292292222");

            DateTime _now = DateTime.UtcNow;
            Console.WriteLine($"Datetime now is: {_now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK")}");

            DateTime _expire = _now.AddHours(1).AddMinutes(2);

            XmlNode sig = root.SelectSingleNode("//Signature", nsmg3);


            Console.WriteLine($"Expire time is: {_expire.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ssK")}");


            XmlNode assertion = root.SelectSingleNode("//saml2:Assertion/saml2:Subject/saml2:SubjectConfirmation/saml2:SubjectConfirmationData", nsmgr2);

            assertion.Attributes["InResponseTo"].Value = "99992929292292222";


            Console.WriteLine($"XML NOW .....");
            Console.WriteLine(xml.InnerXml);

        }


        private void DecodeSamlRequest() 
        {
            string samlRequest = "lZJbb%2BIwEIX%2FSuT3XEmAWAQpC1oVqZcIaB%2F2zbGHYq1jpx4n3f33DYFquw%2Bl6qvnzJxv5niBrFFJS8vOHfUWXjpA5%2F1plEZ6rhSks5oahhKpZg0gdZzuyrtbmgQRba1xhhtFvBIRrJNGr4zGrgG7A9tLDo%2Fb24IcnWuRhmEL7HccCegDPFd9bV4DbppQs75lzxAIQ7z1wCA1Ow07tw6dynCmjgYdnWaT2TQ8wYWIg%2FqnsRxG%2FIIcmEIg3mZdkN39Kp1P5rngdZrO6nqW5NkhmqV5zeNMRLnIDoMQK4Yoe%2FjXitjBRqNj2hUkieK5H%2BV%2BnO%2FjlGYRnWRBFie%2FiFddFv8htZD6%2BfqV6rMI6c1%2BX%2FnVw24%2FDuilAHs%2FqL97oCewOB5nGE6WizEoOpLbj9ldh2LvgZHll%2B6L8KPHxbGlJ%2FbNujJK8r9eqZR5XVlgbtjH2Q7GaBrmPseIg3h8kcI%2FjFIKDZOqFMICIgmXF9%2F%2Fv%2BfyDQ%3D%3D";
            string decodedSaml = "";


            if(samlRequest.Contains("%"))
            {
                decodedSaml = HttpUtility.UrlDecode(samlRequest);
            }

            //byte[] samlData = Convert.FromBase64String(decodedSaml);
            byte[] decompressedBytes; 

            var compressedStream = new MemoryStream(Convert.FromBase64String(decodedSaml));

            using (var decompressorStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
            {
                using (var decompressedStream = new MemoryStream())
                {
                    decompressorStream.CopyTo(decompressedStream);

                    decompressedBytes = decompressedStream.ToArray();
                }
            }

            Console.WriteLine(Encoding.UTF8.GetString(decompressedBytes));

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(Encoding.UTF8.GetString(decompressedBytes));

            XmlElement node = xml.DocumentElement;

            node.SetAttribute("Destination", "http://localhost:5000");


            Console.WriteLine(xml.InnerXml);


            byte[] compressedBytes;

            using (var uncompressedStream = new MemoryStream(Encoding.UTF8.GetBytes(xml.InnerXml)))
            {
                var compressedStream2 = new MemoryStream();

                // setting the leaveOpen parameter to true to ensure that compressedStream will not be closed when compressorStream is disposed
                // this allows compressorStream to close and flush its buffers to compressedStream and guarantees that compressedStream.ToArray() can be called afterward
                // although MSDN documentation states that ToArray() can be called on a closed MemoryStream, this approach avoids relying on that very odd behavior should it ever change
                using (var compressorStream = new DeflateStream(compressedStream2, CompressionLevel.Fastest, true))
                {
                    uncompressedStream.CopyTo(compressorStream);
                }

                // call compressedStream.ToArray() after the enclosing DeflateStream has closed and flushed its buffer to compressedStream
                compressedBytes = compressedStream2.ToArray();
            }

            Console.WriteLine("********* COMPRESSED BYTES **********");
            Console.WriteLine(Convert.ToBase64String(compressedBytes));

            var encodedSaml = HttpUtility.UrlEncode(Convert.ToBase64String(compressedBytes));

            Console.WriteLine();
            Console.WriteLine($"ENCODED SAML : {encodedSaml} ");

        }

      

        private async Task GetTokenAsync()
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            if(disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");

            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");



            if(tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine($"Token received: {tokenResponse.Json} ");

        }

        private async Task GetTokenResourceOwnerPasswordAsync()
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("alice", "password", "api1");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

        }
    }
}
