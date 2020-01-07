using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace RBACPractice.Handler
{
    /// <summary>
    /// Login 的摘要说明
    /// </summary>
    public class Login : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string lRequestMethod = context.Request.Params["RequestMethod"].ToString();

            switch (lRequestMethod)
            {
                case "login":
                    LoginCheck(context);
                    break;
            }
        }

        private void LoginCheck(HttpContext context)
        {
            string lUserName = context.Request.Params["UserName"].ToString();
            string lPassword = context.Request.Params["Password"].ToString();
            //context.Session = new HttpSessionState();
            context.Session.Add("UserID", lUserName);
            context.Response.ContentType = "text/plain";
            context.Response.Write("{Status:1,Msg:''}");
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