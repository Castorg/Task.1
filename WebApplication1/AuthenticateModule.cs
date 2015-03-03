using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Web;
using WebApplication1.EFContext;

namespace WebApplication1
{
    public class AuthenticateModule : IHttpModule
    {

        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += new EventHandler(this.Authenticate);
        }

        private void Authenticate(Object source , EventArgs e)
        {
            var app = (HttpApplication) source;
            var context = app.Context;

            var cookie = context.Request.Cookies["user"];

            if (cookie != null)
            {
                string user;
                using (var db = new UserContext())
                {
                    user = db.Users.ToArray()[0].Login;
                }
                var userCook = cookie.Values["1"];

                if (context.Request.Cookies["isAuthorise"] != null)
                {
                    if (context.Request.Cookies["isAuthorise"].Value == "keep")
                    {
                        var _cookie = new HttpCookie("user");
                        _cookie.Expires = DateTime.Now.AddMinutes(10);
                        context.Response.SetCookie(_cookie);
                    }
                    else
                    {
                        if (string.Compare(user, userCook) == 0)
                        {
                            var _cookie = new HttpCookie("user");
                            _cookie.Expires = DateTime.Now.AddDays(-1d);
                            context.Response.SetCookie(_cookie);
                        }
                    }
                }
            }
        }

    }
}