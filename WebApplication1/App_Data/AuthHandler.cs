using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using WebApplication1.EFContext;

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

            var reg1 = new Regex("<");
            var reg2 = new Regex(">");

            var a = reg1.Match(loginPassString).Index;
            var b = reg2.Match(loginPassString).Index;

            var c = reg1.Match(loginPassString ,a + 1).Index;
            var d = reg2.Match(loginPassString, b + 1).Index;

            var log = new StringBuilder();
            var pass = new StringBuilder();

            log.Append(loginPassString, a + 1, c - a - 2);
            pass.Append(loginPassString, b + 2, d - b - 2);

            string user;
            using (var db = new UserContext())
            {
                user = db.Users.ToArray()[0].Login;
            }

            if (string.Compare(user, log.ToString()) == 0)
            {
                context.Response.Write("authorize");

                var cookie = new HttpCookie("user");
                cookie.Values["1"] = log.ToString();
                cookie.Values["2"] = pass.ToString();
                cookie.Expires = DateTime.Now.AddMinutes(10);
                context.Response.SetCookie(cookie);

                var keep = new HttpCookie("keep-alive");
                keep.Values["1"] = "keep";
                cookie.Expires = DateTime.Now.AddMinutes(10);
                context.Response.SetCookie(keep);


                context.User = new UserProvider(log.ToString(), pass.ToString());
            }
            else
            {
                context.Response.Write("notauthorize"); 
            }
        }
    }

    public class CustomIdentity : IIdentity
    {
        private User user;
        public CustomIdentity(string log , string pass)
        {
            user = new User();
            user.Login = log;
            user.Password = pass;
        }
        public string AuthenticationType
        {
            get { return typeof (UserContext).ToString(); }
        }

        public bool IsAuthenticated
        {
            get { return (user != null); }
        }

        public string Name
        {
            get
            {
                return user != null ? user.Login : "anonim";
            }
        }
    }

    public class UserProvider : IPrincipal
    {

        private CustomIdentity userIdentity;

        public UserProvider(string name , string pass)
        {
            userIdentity = new CustomIdentity(name , pass);
        }
        public IIdentity Identity
        {
            get { return userIdentity; }
        }
        public bool IsInRole(string role)
        {
            return (userIdentity != null);
        }
    }
}