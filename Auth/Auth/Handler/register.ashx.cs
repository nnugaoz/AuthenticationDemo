using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auth
{
    /// <summary>
    /// register 的摘要说明
    /// </summary>
    public class register : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string lUserName = context.Request.Form["txtUserName"].ToString();
            string lPassword = context.Request.Form["txtPassword"].ToString();


            context.Response.Write("Hello World");
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