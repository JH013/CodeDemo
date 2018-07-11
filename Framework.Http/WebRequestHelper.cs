using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.Http
{
    public class WebRequestHelper
    {
        public static string Get(string url, string username = null, string password = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                BasicAuthentication(request, url, username, password);
            }
            return GetResponseString(request.GetResponse() as HttpWebResponse);
        }

        public static string Post(string url, string body, string contentType, string username = null, string password = null)
        {
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            }

            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ServicePoint.Expect100Continue = false;
            request.ContentType = contentType;
            request.KeepAlive = true;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                BasicAuthentication(request, url, username, password);
            }

            using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(body);
            }

            return GetResponseString(request.GetResponse() as HttpWebResponse);
        }

        public static void DownloadFile(string url, string destFilePath)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            using (WebResponse response = request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using(Stream stream = new FileStream(destFilePath, FileMode.Create))
                    {
                        byte[] bArr = new byte[1024];
                        int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                        while (size > 0)
                        {
                            stream.Write(bArr, 0, size);
                            size = responseStream.Read(bArr, 0, (int)bArr.Length);
                        }
                    }
                }
            }
        }

        private static string GetResponseString(HttpWebResponse webresponse)
        {
            using (Stream s = webresponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(s, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        private static void BasicAuthentication(HttpWebRequest request, string url, string username, string password)
        {
            string usernamePassword = username + ":" + password;
            CredentialCache mycache = new CredentialCache();
            mycache.Add(new Uri(url), "Basic", new NetworkCredential(username, password));
            request.Credentials = mycache;
            request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(usernamePassword)));
        }
    }
}
