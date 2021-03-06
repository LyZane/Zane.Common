﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Routing;

namespace Zane.Common.WebBrowser
{
    public abstract class IWebBrowser : IDisposable
    {
        public IWebBrowser()
        {
            this.Config = new RequestConfig();
            try
            {
                Init(this.Config);
            }
            catch (Exception ex)
            {
                this.Dispose();
                throw new TypeInitializationException("IWebBrowser", ex);
            }
        }

        /// <summary>
        /// 当修改Config之后，需要调用此方法以应用新的配置。
        /// </summary>
        public void RefreshConfig()
        {
            try
            {
                Init(this.Config);
            }
            catch (Exception ex)
            {
                this.Dispose();
                throw new TypeInitializationException("IWebBrowser", ex);
            }
        }

        public IWebBrowser(RequestConfig config)
        {
            this.Config = config;
            try
            {
                Init(this.Config);
            }
            catch (Exception ex)
            {
                this.Dispose();
                throw new TypeInitializationException("IWebBrowser", ex);
            }
        }

        protected abstract void Init(RequestConfig config);

        public RequestConfig Config { get; private set; }

        public abstract string GetCookie(Uri uri, string name);
        public abstract Dictionary<string, string> GetCookies(Uri uri);

        public abstract void SetCookie(Cookie cookie);

        /// <summary>
        /// 下载文本内容
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract IResponsePack DownloadString(Uri uri, IDictionary<string, object> data = null);
        public IResponsePack DownloadString(string url, IDictionary<string, object> data = null)
        {
            return DownloadString(new Uri(url), data);
        }

        public IResponsePack DownloadString(string url, dynamic data)
        {
            return DownloadString(new Uri(url),new RouteValueDictionary(data));
        }
        public IResponsePack DownloadString(Uri uri, dynamic data)
        {
            return DownloadString(uri, new RouteValueDictionary(data));
        }

        /// <summary>
        /// 下载json形式的内容（由于有些浏览器默认会将json内容包装成html，所以可能需要额外处理，以提取出其中的json部分。）
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public abstract IResponsePack DownloadJson(Uri uri, IDictionary<string, object> data = null);
        public IResponsePack DownloadJson(string url, IDictionary<string, object> data = null)
        {
            return DownloadJson(new Uri(url), data);
        }
        public IResponsePack DownloadJson(string url, dynamic data)
        {
            return DownloadJson(new Uri(url), new RouteValueDictionary(data));
        }
        public IResponsePack DownloadJson(Uri uri, dynamic data)
        {
            return DownloadJson(uri, new RouteValueDictionary(data));
        }

        public abstract void Dispose();
    }
}