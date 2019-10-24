using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Auth.Handler
{
    /// <summary>
    /// home 的摘要说明
    /// </summary>
    public class home : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            using (SqlConnection lSqlConn = new SqlConnection())
            {
                lSqlConn.ConnectionString = "Data Source=.;Initial Catalog=Auth;User=sa;Password=1;";
                lSqlConn.Open();

                SqlCommand lSqlCmd = new SqlCommand("SELECT ID, PageName, PID, Addr FROM T_Feature", lSqlConn);

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