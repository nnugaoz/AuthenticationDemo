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
    /// role 的摘要说明
    /// </summary>
    public class role : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string lRequestMethod = context.Request.Params["RequestMethod"].ToString();

            switch (lRequestMethod)
            {
                case "ROLE_LIST":
                    GetRoleList(context);
                    break;

                case "ROLE_ADD":
                    AddRole(context);
                    break;

            }
            using (SqlConnection lSqlConn = new SqlConnection())
            {

            }
        }

        private void AddRole(HttpContext context)
        {
            string lRoleName = context.Request.Form["Role_Name"].ToString();
            using (SqlConnection lSqlConn = new SqlConnection())
            {
                lSqlConn.ConnectionString = "Data Source=.;Initial Catalog=Auth;User=sa;Password=1;";
                lSqlConn.Open();

                SqlCommand lSqlCmd = new SqlCommand("INSERT INTO T_Role(RoleName)VALUES(@RoleName)", lSqlConn);
                SqlParameter lSqlParam = new SqlParameter("@RoleName", lRoleName);

                lSqlCmd.Parameters.Add(lSqlParam);

                if (lSqlCmd.ExecuteNonQuery() > 0)
                {
                    context.Response.Redirect("/Html/Role/roleList.html");
                }
            }
        }

        private void GetRoleList(HttpContext context)
        {
            using (SqlConnection lSqlConn = new SqlConnection())
            {
                lSqlConn.ConnectionString = "Data Source=.;Initial Catalog=Auth;User=sa;Password=1;";
                lSqlConn.Open();

                SqlCommand lSqlCmd = new SqlCommand();
                lSqlCmd.Connection = lSqlConn;
                lSqlCmd.CommandText = "SELECT RoleName FROM T_Role";

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