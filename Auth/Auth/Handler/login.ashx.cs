using Auth.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auth
{
    /// <summary>
    /// login 的摘要说明
    /// </summary>
    public class login : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string lUserName = context.Request.Form["txtUserName"].ToString();
            string lPassword = context.Request.Form["txtPassword"].ToString();
            Login lLogin = new Login();
            if (lLogin.Check(lUserName, lPassword))
            {
                context.Response.Redirect("/Html/home.html");
            }
            else
            {
                context.Response.Redirect("/Html/login.html");
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}