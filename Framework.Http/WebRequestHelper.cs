using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Framework.Http
{
    /// <summary>
    /// Web请求
    /// </summary>
    public class WebRequestHelper
    {
        #region 公有方法

        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="username">账号</param>
        /// <param name="password">密码</param>
        /// <returns>响应</returns>
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

        /// <summary>
        /// POST请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="body">body</param>
        /// <param name="username">账号</param>
        /// <param name="password">密码</param>
        /// <returns>响应</returns>
        public static string Post(string url, string body, string username = null, string password = null)
        {
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            }

            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.Method = "POST";
            request.ServicePoint.Expect100Continue = false;
            request.ContentType = "application/x-www-form-urlencoded";
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

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="destFilePath">下载目录</param>
        public static void DownloadFile(string url, string destFilePath)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            using (WebResponse response = request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (Stream stream = new FileStream(destFilePath, FileMode.Create))
                    {
                        byte[] bArr = new byte[1024];
                        int size = responseStream.Read(bArr, 0, bArr.Length);
                        while (size > 0)
                        {
                            stream.Write(bArr, 0, size);
                            size = responseStream.Read(bArr, 0, bArr.Length);
                        }
                    }
                }
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取响应字符串
        /// </summary>
        /// <param name="webresponse">响应</param>
        /// <returns></returns>
        private static string GetResponseString(HttpWebResponse webresponse)
        {
            using (Stream s = webresponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(s, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// 检查有效结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        /// <summary>
        /// 基础认证
        /// </summary>
        /// <param name="request">请求</param>
        /// <param name="url">请求地址</param>
        /// <param name="username">账号</param>
        /// <param name="password">密码</param>
        private static void BasicAuthentication(HttpWebRequest request, string url, string username, string password)
        {
            string usernamePassword = username + ":" + password;
            CredentialCache mycache = new CredentialCache();
            mycache.Add(new Uri(url), "Basic", new NetworkCredential(username, password));
            request.Credentials = mycache;
            request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(usernamePassword)));
        }

        #endregion
    }
}
