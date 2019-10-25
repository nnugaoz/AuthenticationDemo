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
    /// Feature 的摘要说明
    /// </summary>
    public class Feature : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string lRequestMethod = context.Request.Params["RequestMethod"].ToString();

            switch (lRequestMethod)
            {
                case "FEATURE_ADD":
                    FeatureAdd(context);
                    break;

                case "FEATURE_LIST":
                    FeatureList(context);
                    break;

                case "FEATURE_EDIT":
                    FeatureEdit(context);
                    break;
            }
        }

        private void FeatureList(HttpContext context)
        {
            string lSQL = "";
            string lRetJson = "";
            using (SqlConnection lSqlConn = new SqlConnection())
            {
                lSqlConn.ConnectionString = "Data Source=.;Initial Catalog=Auth;User=sa;Password=1;";
                lSqlConn.Open();

                lSQL = "SELECT ";
                lSQL += "ID";
                lSQL += ",FeatureName";
                lSQL += ",PID";
                lSQL += ",Addr";
                lSQL += " FROM T_Feature";

                SqlCommand lSqlCmd = new SqlCommand(lSQL, lSqlConn);
                SqlDataAdapter lSqlAdpt = new SqlDataAdapter(lSqlCmd);
                DataSet lDS = new DataSet();

                lSqlAdpt.Fill(lDS);

                lRetJson = JsonConvert.SerializeObject(lDS);

                context.Response.Write(lRetJson);
            }

        }

        private void FeatureAdd(HttpContext context)
        {
            string lParentID = context.Request.Form["txtParentFeatureID"].ToString();
            string lParentName = context.Request.Form["txtParentFeatureName"].ToString();
            string lNewFeatureName = context.Request.Form["txtNewFeatureName"].ToString();
            string lNewFeatureAddr = context.Request.Form["txtNewFeatureAddr"].ToString();

            string lSQL = "";

            using (SqlConnection lSqlConn = new SqlConnection())
            {
                lSqlConn.ConnectionString = "Data Source=.;Initial Catalog=Auth;User=sa;Password=1;";
                lSqlConn.Open();

                lSQL = "INSERT INTO T_Feature(";
                lSQL += "ID";
                lSQL += ", FeatureName";
                lSQL += ", PID";
                lSQL += ", Addr";
                lSQL += ")VALUES(";
                lSQL += "NEWID()";
                lSQL += ",@FeatureName";
                lSQL += ",@PID";
                lSQL += ",@Addr";
                lSQL += ")";

                SqlCommand lSqlCmd = new SqlCommand(lSQL, lSqlConn);

                SqlParameter[] lSqlParams = new SqlParameter[] {
                    new SqlParameter("@FeatureName",lNewFeatureName)
                    ,new SqlParameter("@PID",lParentID)
                    ,new SqlParameter("@Addr",lNewFeatureAddr)
                };

                lSqlCmd.Parameters.AddRange(lSqlParams);
                lSqlCmd.ExecuteNonQuery();

                context.Response.Redirect("../Html/Feature/FeatureList.html");
            }
        }

        private void FeatureEdit(HttpContext context)
        {
            string lID = context.Request.Form["txtID"].ToString();
            string lName = context.Request.Form["txtFeatureName"].ToString();
            string lAddr = context.Request.Form["txtAddr"].ToString();

            string lSQL = "";

            using (SqlConnection lSqlConn = new SqlConnection())
            {
                lSqlConn.ConnectionString = "Data Source=.;Initial Catalog=Auth;User=sa;Password=1;";
                lSqlConn.Open();

                lSQL = "UPDATE T_Feature SET";
                lSQL += " FeatureName=@FeatureName";
                lSQL += ", Addr=@Addr";
                lSQL += " WHERE ID=@ID";

                SqlCommand lSqlCmd = new SqlCommand(lSQL, lSqlConn);

                SqlParameter[] lSqlParams = new SqlParameter[] {
                    new SqlParameter("@FeatureName",lName)
                    ,new SqlParameter("@Addr",lAddr)
                    ,new SqlParameter("@ID",lID)
                };

                lSqlCmd.Parameters.AddRange(lSqlParams);

                lSqlCmd.ExecuteNonQuery();

                context.Response.Redirect("../Html/Feature/FeatureList.html");
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