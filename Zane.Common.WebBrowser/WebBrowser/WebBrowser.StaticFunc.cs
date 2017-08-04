using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Routing;

namespace Zane.Common.WebBrowser
{
    public partial class WebBrowser
    {
        #region static


        public static ResponsePack_WebBrowser Get(Uri uri, IDictionary<string, object> data = null)
        {
            ClientCore browser = new ClientCore();
            return browser.DownloadString(uri, data);
        }

        public static ResponsePack_WebBrowser Get(string url, dynamic data = null)
        {
            if (data == null)
            {
                return Get(new Uri(url));
            }
            else
            {
                return Get(new Uri(url), new RouteValueDictionary(data));
            }

        }





        public static ResponsePack_WebBrowser Post(Uri uri, IDictionary<string, object> data = null, Encoding requestEncoding = null)
        {
            ClientCore browser = new ClientCore();
            browser.Config.Method = "POST";
            return browser.DownloadString(uri, data, requestEncoding);
        }

        public static ResponsePack_WebBrowser Post(string url, dynamic data = null, Encoding requestEncoding = null)
        {
            if (data == null)
            {
                return Post(new Uri(url), null, requestEncoding);
            }
            else
            {
                return Post(new Uri(url), new RouteValueDictionary(data), requestEncoding);
            }
        }


        #endregion static
    }
}