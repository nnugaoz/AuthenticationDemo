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
        }

        private void AddRole(HttpContext context)
        {
            string lRoleName = context.Request.Form["Role_Name"].ToString();

            MsSqlHelper lMsSqlHelper = new MsSqlHelper();
            string lSQL = "";

            lSQL = "INSERT INTO T_Role(RName)VALUES(@RName)";

            SqlParameter[] lSqlParams = new SqlParameter[] { new SqlParameter("@RName", lRoleName) };

            if (lMsSqlHelper.ExecuteSQL(lSQL, lSqlParams) > 0)
            {
                context.Response.Redirect("/Html/Role/roleList.html");
            }
        }

        private void GetRoleList(HttpContext context)
        {
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();
            string lSQL = "";

            lSQL = "SELECT RName FROM T_Role";

            DataSet lDS = new DataSet();

            lDS = lMsSqlHelper.GetData(lSQL);

            context.Response.Write(JsonConvert.SerializeObject(lDS));

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