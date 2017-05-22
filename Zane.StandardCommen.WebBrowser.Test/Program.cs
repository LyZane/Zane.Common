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
                //https://www.facebook.com/
                browser.DownloadString("https://www.facebook.com");
            }




            Console.ReadKey();
        }
    }
}