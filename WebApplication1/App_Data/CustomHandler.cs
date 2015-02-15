using System;
using System.Web;
using System.Web.Script.Serialization;

namespace WebApplication1.App_Data
{
    public class CustomHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            const String htmlTemplate = "<html><head><title>{0}</title></head><body>" +
            "<h1>Hello I'm: " +
            "<span style='color:blue'>{1}</span></h1>" +
            "</body></html>";
            var response = String.Format(htmlTemplate,
            "HTTP Handlers", context.Request.Path);
            context.Response.Write(response);
        }
        public bool IsReusable
        {
            get { return false; }
        }


    }
}