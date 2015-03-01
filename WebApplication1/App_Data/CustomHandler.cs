using System;
using System.IO;
using System.Net.Mime;
using System.Runtime.Remoting.Channels;
using System.Security.AccessControl;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;

namespace WebApplication1.App_Data
{
    public class CustomHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var path = context.Server.MapPath("contact.json");
            var b = File.ReadLines(path);
            context.Response.ContentType = "application/json";
            foreach (var v in b)
            {
                context.Response.Write(v);
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}