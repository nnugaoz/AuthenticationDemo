using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Auth.Handler
{
    /// <summary>
    /// user 的摘要说明
    /// </summary>
    public class user : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            using (SqlConnection lSqlConn = new SqlConnection())
            {
                lSqlConn.ConnectionString = "Data Source=127.0.0.1;Initial Catalog=Auth;User=sa;Password=1";
                lSqlConn.Open();

                SqlCommand lSqlCmd = new SqlCommand();
                lSqlCmd.Connection = lSqlConn;

                lSqlCmd.CommandText = "SELECT UserName,Password FROM T_User";

                SqlDataAdapter lSqlAdpt = new SqlDataAdapter(lSqlCmd);

                DataSet lDS = new DataSet();

                lSqlAdpt.Fill(lDS);

                context.Response.Write(JsonConvert.SerializeObject(lDS));
                
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