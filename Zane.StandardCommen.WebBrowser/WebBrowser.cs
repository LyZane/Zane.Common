using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Linq;
namespace Zane.StandardCommen.WebBrowser
{
    public class WebBrowser : IWebBrowser
    {
        HttpClient _Client = new HttpClient();
        
        public override void Dispose()
        {
            _Client.Dispose();
        }

        public override IResponsePack DownloadJson(Uri uri, IDictionary<string, object> data = null)
        {
            throw new NotImplementedException();
        }

        public override IResponsePack DownloadString(Uri uri, IDictionary<string, object> data = null)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.Method = new HttpMethod(this.Config.Method);
            HttpResponseMessage response = _Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;

            return null;
        }

        

        public override string GetCookie(Uri uri, string name)
        {
            throw new NotImplementedException();
        }
Class1.cs
        public override Dictionary<string, string> GetCookies(Uri uri)
        {
            throw new NotImplementedException();
        }

        public override void SetCookie(Cookie cookie)
        {
            throw new NotImplementedException();
        }

        protected override void Init(RequestConfig config)
        {
            
        }
    }
}
