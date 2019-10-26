using Auth.DBHelper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

            string lUserName = context.Request.Form["txtUserName"].ToString();
            string lPassword = context.Request.Form["txtPassword"].ToString();

            string lSQL = "";
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();

            lSQL = "INSERT INTO T_User(UserName,Pwd)VALUES(@UserName,@Pwd)";

            SqlParameter[] lSqlParams = new SqlParameter[]{
                new SqlParameter("@UserName",lUserName)
                ,new SqlParameter("@Pwd",lPassword)
            };

            lMsSqlHelper.ExecuteSQL(lSQL, lSqlParams);

            context.Response.Redirect("/Html/login.html");
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