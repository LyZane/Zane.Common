using System;
using System.Net.Http;

namespace Zane.StandardCommen.WebBrowser.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            


            using (WebBrowser browser = new WebBrowser())
            {
                browser.DownloadString("http://g.cn");
            }




            Console.ReadKey();
        }
    }
}