using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Zane.StandardCommen.WebBrowser
{
    public class Proxy
    {
        public Proxy(string uri, string username, string password)
        {
            if (!Regex.IsMatch(uri, @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}:\d+"))
            {
                throw new Exception("错误的代理地址格式：" + uri);
            }
            this.Address = uri.Substring(0, uri.IndexOf(':'));
            this.Port = Convert.ToInt32(uri.Substring(uri.IndexOf(':') + 1));
            this.Username = username;
            this.Password = password;
        }
        public void SetCredential(string username, string password)
        {
            this.Username = username;
            this.Password = password;
            if (!string.IsNullOrWhiteSpace(this.Username) && !string.IsNullOrWhiteSpace(this.Password))
            {
                this.Credential = new NetworkCredential(this.Username, this.Password);
            }
        }
        public Proxy(string uri):this(uri, null, null){ }
        public string Address { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool HasCredential { get { return !string.IsNullOrWhiteSpace(this.Username) && !string.IsNullOrEmpty(Password); } }
        public NetworkCredential Credential { get; private set; }
        public override int GetHashCode()
        {
            return (this.Address + this.Port + this.Username + this.Password).GetHashCode();
        }
    }
}
