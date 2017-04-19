using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Zane.Common.WebBrowser
{
    [Serializable]
    public abstract class IResponsePack
    {
        public IResponsePack(string downloadMethod,Uri requestUri)
        {
            this.Timer = Stopwatch.StartNew();
            this.DownloadMethod = downloadMethod;
            this.RequestUri = requestUri;
        }

        public void End()
        {
            Timer.Stop();
        }

        private Stopwatch Timer;
        public TimeSpan DownloadTime { get { return this.Timer.Elapsed; } }
        public abstract string PageSource { get; set; }

        public Uri RequestUri { get;private set; }
        public Uri ResponseUri { get; set; }
        public Exception Error { get; set; }

        public string DownloadMethod { get; set; }
    }
}