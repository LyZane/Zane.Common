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
        private HttpRequestMessage BuildRequest(Uri uri, IDictionary<string, object> data = null)
        {
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod(Config.Method), uri);
            request.Content = new FormUrlEncodedContent(data.ToDictionary(a=>a.Key,a=>a.Value.ToString()));
            if (!string.IsNullOrEmpty(Config.ContentType))
            {
                request.Content.Headers.ContentType.MediaType = Config.ContentType;
            }
            return request;
        }
        public override IResponsePack DownloadString(Uri uri, IDictionary<string, object> data = null)
        {
            HttpRequestMessage request = BuildRequest(uri,data);
            HttpResponseMessage response = _Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).Result;

            return null;
        }

        

        public override string GetCookie(Uri uri, string name)
        {
            throw new NotImplementedException();
        }

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
