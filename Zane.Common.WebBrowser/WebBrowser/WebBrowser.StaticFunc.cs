using System;
using System.Collections.Generic;
using System.Text;

namespace Zane.Common.WebBrowser
{
    public partial class WebBrowser
    {
        #region static

        public static ResponsePack_WebBrowser Get(string url, Dictionary<string, object> data = null)
        {
            return Get(new Uri(url), data);
        }

        public static ResponsePack_WebBrowser Get(Uri uri, Dictionary<string,object> data = null)
        {
            ClientCore browser = new ClientCore();
            return browser.DownloadString(uri, data);
        }

        public static ResponsePack_WebBrowser Post(string url, Dictionary<string, object> data = null, Encoding requestEncoding = null)
        {
            return Post(new Uri(url), data, requestEncoding);
        }

        public static ResponsePack_WebBrowser Post(Uri uri, Dictionary<string, object> data = null, Encoding requestEncoding = null)
        {
            ClientCore browser = new ClientCore();
            browser.Config.Method = "POST";
            return browser.DownloadString(uri, data, requestEncoding);
        }

        #endregion static
    }
}