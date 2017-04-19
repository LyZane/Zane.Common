using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Text;

namespace Zane.Common.WebBrowser
{
    [Serializable]
    public partial class WebBrowser : IWebBrowser, IDisposable
    {
        public override void Dispose()
        {
            _Client.Dispose();
        }

        private BybClient _Client;

        protected override void Init(RequestConfig config)
        {
            _Client = new BybClient(config);
        }

        public WebBrowser() : base(new RequestConfig())
        {
        }

        public WebBrowser(RequestConfig config) : base(config)
        {
        }
        

        /// <summary>
        /// 发起HTTP请求，得到一个文本响应，该方法会自动编解码相关文本。
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public override IResponsePack DownloadString(Uri uri, IDictionary<string, object> data = null)
        {
            return _Client.DownloadString(uri, data);
        }
        public IResponsePack DownloadString(Uri uri, IDictionary<string, object> data, Encoding requestEncoding)
        {
            return _Client.DownloadString(uri, data, requestEncoding);
        }
        public IResponsePack DownloadString(Uri uri, string dataStr,Encoding requestEncoding)
        {
            return _Client.DownloadString(uri, dataStr, requestEncoding);
        }

        public override IResponsePack DownloadJson(Uri uri, IDictionary<string, object> data = null)
        {
            return _Client.DownloadString(uri, data);
        }

        /// <summary>
        /// 发起请求，得到一张图片。
        /// </summary>
        /// <param name="url"></param>
        /// <param name="retryCount"></param>
        /// <returns></returns>
        public Bitmap DownLoadImage(string url, int retryCount = 3)
        {
            return _Client.DownLoadImage(url, retryCount);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public ResponsePack_WebBrowser UploadFile(string url, string filePath)
        {
            return _Client.UploadFile(url, filePath);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public ResponsePack_WebBrowser UploadFile(Uri uri, string filePath)
        {
            return _Client.UploadFile(uri, filePath);
        }

        /// <summary>
        /// 读取Cookie
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public override string GetCookie(Uri uri, string name)
        {
            return _Client.GetCookie(uri, name);
        }

        /// <summary>
        /// 写入Cookie
        /// </summary>
        /// <param name="cookie"></param>
        public override void SetCookie(Cookie cookie)
        {
            _Client.SetCookie(cookie);
        }

        public override Dictionary<string, string> GetCookies(Uri uri)
        {
            return _Client.GetCookies(uri); ;
        }
    }
}