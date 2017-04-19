using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zane.Common.WebBrowser.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            WebBrowser browser = new WebBrowser();
            var response = browser.DownloadString("http://z.cn");
        }
    }
}
