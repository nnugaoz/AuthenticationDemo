using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
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
                case "Login":
                    LoginCheck(context);
                    break;
                case "Logout":
                    LogOut(context);
                    break;
            }
        }

        private void LogOut(HttpContext context)
        {
            RequestResult lRR = new RequestResult();
            FormsAuthentication.SignOut();
            lRR.Status = "1";
            context.Response.Write(JsonConvert.SerializeObject(lRR));
        }

        private void LoginCheck(HttpContext context)
        {
            string lUserName = context.Request.Params["UserName"].ToString();
            string lPassword = context.Request.Params["Password"].ToString();
            T_UserDao lUserDao = new T_UserDao();
            Boolean lRet = lUserDao.Check(lUserName, lPassword);
            RequestResult lRR = new RequestResult();
            if (lRet)
            {
                lRR.Status = "1";
                lRR.Msg = "";
                FormsAuthentication.SetAuthCookie(lUserName, true);
            }
            else
            {
                lRR.Status = "0";
                lRR.Msg = "用户名或密码错误！";
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write(JsonConvert.SerializeObject(lRR));
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