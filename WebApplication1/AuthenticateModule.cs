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
            context.AuthenticateRequest += new EventHandler(this.Authenticate);
            context.PostAuthenticateRequest += MMMM;////
        }

        private void Authenticate(Object source , EventArgs e)
        {
            var app = (HttpApplication) source;
            var context = app.Context;
            User user;

            using (var db = new UserContext())
            {
                user = db.Users.ToArray()[0];
            }

            int asdfhg = 0;
        }

        private void MMMM(Object source, EventArgs e)
        {
            int asad = 0;
        }
    }
}