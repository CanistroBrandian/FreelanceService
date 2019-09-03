﻿using System.Web;
using System.Net;

namespace FreelanceService.BLL.Helpers
{
    public class ServerInfo
    {
        public string GetIPAddress()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); 
            IPAddress ipAddress = ipHostInfo.AddressList[0];

            return ipAddress.ToString();
        }

        public static string FullyQualifiedApplicationPath(HttpRequestBase httpRequestBase)
        {
            string appPath = string.Empty;

            if (httpRequestBase != null)
            {
                //Formatting the fully qualified website url/name
                appPath = string.Format("{0}://{1}{2}{3}",
                            httpRequestBase.Url.Scheme,
                            httpRequestBase.Url.Host,
                            httpRequestBase.Url.Port == 80 ? string.Empty : ":" + httpRequestBase.Url.Port,
                            httpRequestBase.ApplicationPath);
            }

            if (!appPath.EndsWith("/"))
            {
                appPath += "/";
            }

            return appPath;
        }
    }
}