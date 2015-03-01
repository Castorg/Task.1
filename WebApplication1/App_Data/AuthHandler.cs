using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace WebApplication1
{
    public class AuthHandler : IHttpHandler
    {
        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var loginPassString = System.Text.Encoding.UTF8.GetString(
                (HttpContext.Current.Request.BinaryRead
                (HttpContext.Current.Request.TotalBytes)));

            Regex reg1 = new Regex("<");
            Regex reg2 = new Regex(">");

            var a = reg1.Match(loginPassString).Index;
            var b = reg2.Match(loginPassString).Index;

            var c = reg1.Match(loginPassString ,a + 1).Index;
            var d = reg2.Match(loginPassString, b + 1).Index;

            var log = new StringBuilder();
            var pass = new StringBuilder();

            log.Append(loginPassString, a + 1, c - a - 2);
            pass.Append(loginPassString, b + 2, d - b - 2);



            var cookie = new HttpCookie("user");
            cookie.Values["1"] = log.ToString();
            cookie.Values["2"] = pass.ToString();
            cookie.Expires = DateTime.Now.AddMinutes(10);
            context.Response.SetCookie(cookie);




            var req = context.Request.Cookies["user"];

            var qqq = req.Values["1"];
            var zzz = req.Values["2"];

            int pokjihuyg = 0;
        }
    }
}