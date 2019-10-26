using Auth.DBHelper;
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

                case "FEATURE_QUERY_BY_ID":
                    FeatureQueryById(context);
                    break;
            }
        }

        private void FeatureQueryById(HttpContext context)
        {
            string lID = context.Request.Params["ID"].ToString();
            string lSQL = "";
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();
            SqlParameter[] lParams = null;

            lSQL = "SELECT ID";
            lSQL += ", FName";
            lSQL += ", PID";
            lSQL += ", Addr";
            lSQL += ", Type";
            lSQL += ", Sort";
            lSQL += " FROM T_Feature";
            lSQL += " WHERE ID=@ID";

            lParams = new SqlParameter[]{
                new SqlParameter("@ID",lID)
            };

            DataSet lDS = null;

            lDS = lMsSqlHelper.GetData(lSQL, lParams);

            context.Response.Write(JsonConvert.SerializeObject(lDS));

        }

        private void FeatureList(HttpContext context)
        {
            string lSQL = "";
            string lRetJson = "";
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();

            lSQL = "SELECT ";
            lSQL += "ID";
            lSQL += ", FName";
            lSQL += ", PID";
            lSQL += ", Addr";
            lSQL += ", Type";
            lSQL += " FROM T_Feature";
            lSQL += " ORDER BY Sort";
            DataSet lDS = null;
            lDS = lMsSqlHelper.GetData(lSQL);

            lRetJson = JsonConvert.SerializeObject(lDS);

            context.Response.Write(lRetJson);
        }

        private void FeatureAdd(HttpContext context)
        {
            string lPID = context.Request.Form["txtPID"].ToString();
            string lFName = context.Request.Form["txtFName"].ToString();
            string lAddr = context.Request.Form["txtAddr"].ToString();
            string lType = context.Request.Form["radType"].ToString();
            string lSort = context.Request.Form["txtSort"].ToString();

            MsSqlHelper lMsSqlHelper = new MsSqlHelper();
            string lSQL = "";

            lSQL = "INSERT INTO T_Feature(";
            lSQL += "ID";
            lSQL += ", FName";
            lSQL += ", PID";
            lSQL += ", Addr";
            lSQL += ", Type";
            lSQL += ", Sort";
            lSQL += ")VALUES(";
            lSQL += "NEWID()";
            lSQL += ", @FName";
            lSQL += ", @PID";
            lSQL += ", @Addr";
            lSQL += ", @Type";
            lSQL += ", @Sort";
            lSQL += ")";

            SqlParameter[] lSqlParams = new SqlParameter[] {
                new SqlParameter("@FName",lFName)
                ,new SqlParameter("@PID",lPID)
                ,new SqlParameter("@Addr",lAddr)
                ,new SqlParameter("@Type",lType)
                ,new SqlParameter("@Sort",lSort)
            };

            lMsSqlHelper.ExecuteSQL(lSQL, lSqlParams);

            context.Response.Redirect("../Html/Feature/FeatureList.html");
        }

        private void FeatureEdit(HttpContext context)
        {
            string lID = context.Request.Form["txtID"].ToString();
            string lName = context.Request.Form["txtFName"].ToString();
            string lAddr = context.Request.Form["txtAddr"].ToString();
            string lType = context.Request.Form["radType"].ToString();
            string lSort = context.Request.Form["txtSort"].ToString();

            string lSQL = "";
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();

            lSQL = "UPDATE T_Feature SET";
            lSQL += " FName=@FName";
            lSQL += ", Addr=@Addr";
            lSQL += ", Type=@Type";
            lSQL += ", Sort=@Sort";
            lSQL += " WHERE ID=@ID";

            SqlParameter[] lSqlParams = new SqlParameter[] {
                    new SqlParameter("@FName",lName)
                    ,new SqlParameter("@Addr",lAddr)
                    ,new SqlParameter("@ID",lID)
                     ,new SqlParameter("@Type",lType)
                      ,new SqlParameter("@Sort",lSort)
                };

            lMsSqlHelper.ExecuteSQL(lSQL, lSqlParams);
            context.Response.Redirect("../Html/Feature/FeatureList.html");
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