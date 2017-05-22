using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Zane.Common.WebBrowser.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //WebBrowser browser = new WebBrowser();
            ////var response = browser.DownloadString("https://trends.google.com.hk/trends/hotvideos");
            ////var response = browser.DownloadString("https://www.facebook.com/");
            var wc = new WebClient();
            ////var proxy = new WebProxy(new Uri("http://duotai:7TAFyxWwh7s@team.h.xduotai.com:16662"));



            //wc.Proxy = WebRequest.GetSystemWebProxy();
            var a = wc.DownloadString("https://www.facebook.com/");





            // 要访问的目标页面
            string targetUrl = "https://www.facebook.com/";

            // 代理服务器
            string proxyHost = "http://team.h.xduotai.com";
            string proxyPort = "16662";

            // 代理隧道验证信息
            string proxyUser = "duotai";
            string proxyPass = "7TAFyxWwh7s";

            // 设置代理服务器
            var proxy = new WebProxy();
            proxy.Address = new Uri(string.Format("{0}:{1}", proxyHost, proxyPort));
            proxy.Credentials = new NetworkCredential(proxyUser, proxyPass);

            ServicePointManager.Expect100Continue = false;

            var request = WebRequest.Create(targetUrl) as HttpWebRequest;

            request.AllowAutoRedirect = true;
            request.KeepAlive = true;
            request.Method = "GET";
            request.Proxy = proxy;

            //request.Timeout = 20000;
            //request.ServicePoint.ConnectionLimit = 512;
            //request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/48.0.2564.82 Safari/537.36";
            //request.Headers.Add("Cache-Control", "max-age=0");
            //request.Headers.Add("DNT", "1");

            using (var response = request.GetResponse() as HttpWebResponse)
            using (var sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                string htmlStr = sr.ReadToEnd();
            }

            Console.ReadLine();

        }
    }
}
