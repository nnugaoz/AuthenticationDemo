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

            using (SqlConnection lSqlConn = new SqlConnection())
            {
                lSqlConn.ConnectionString = "Data Source=127.0.0.1;Initial Catalog=Auth;User ID=sa;Password=1";
                lSqlConn.Open();

                SqlCommand lSqlCmd = new SqlCommand("INSERT INTO T_User(UserName,Password)VALUES('" + lUserName + "','" + lPassword + "')");
                lSqlCmd.Connection = lSqlConn;

                lSqlCmd.ExecuteNonQuery();

            }

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