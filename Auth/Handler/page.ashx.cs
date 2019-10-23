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
    /// page 的摘要说明
    /// </summary>
    public class page : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string lRequestMethod = context.Request.Params["RequestMethod"].ToString();

            switch (lRequestMethod)
            {
                case "PAGE_ADD":
                    PageAdd(context);
                    break;

                case "PAGE_LIST":
                    PageList(context);
                    break;

                case "PAGE_EDIT":
                    PageEdit(context);
                    break;
            }
        }

        private void PageList(HttpContext context)
        {
            string lSQL = "";
            string lRetJson = "";
            using (SqlConnection lSqlConn = new SqlConnection())
            {
                lSqlConn.ConnectionString = "Data Source=.;Initial Catalog=Auth;User=sa;Password=1;";
                lSqlConn.Open();

                lSQL = "SELECT ";
                lSQL += "ID";
                lSQL += ",PageName";
                lSQL += ",PID";
                lSQL += ",Addr";
                lSQL += " FROM T_Page";

                SqlCommand lSqlCmd = new SqlCommand(lSQL, lSqlConn);
                SqlDataAdapter lSqlAdpt = new SqlDataAdapter(lSqlCmd);
                DataSet lDS = new DataSet();

                lSqlAdpt.Fill(lDS);

                lRetJson = JsonConvert.SerializeObject(lDS);

                context.Response.Write(lRetJson);
            }

        }

        private void PageAdd(HttpContext context)
        {
            string lParentID = context.Request.Form["txtParentPageID"].ToString();
            string lParentName = context.Request.Form["txtParentPageName"].ToString();
            string lNewPageName = context.Request.Form["txtNewPageName"].ToString();
            string lNewPageAddr = context.Request.Form["txtNewPageAddr"].ToString();

            string lSQL = "";

            using (SqlConnection lSqlConn = new SqlConnection())
            {
                lSqlConn.ConnectionString = "Data Source=.;Initial Catalog=Auth;User=sa;Password=1;";
                lSqlConn.Open();

                lSQL = "INSERT INTO T_Page(";
                lSQL += "ID";
                lSQL += ", PageName";
                lSQL += ", PID";
                lSQL += ", Addr";
                lSQL += ")VALUES(";
                lSQL += "NEWID()";
                lSQL += ",@PageName";
                lSQL += ",@PID";
                lSQL += ",@Addr";
                lSQL += ")";

                SqlCommand lSqlCmd = new SqlCommand(lSQL, lSqlConn);

                SqlParameter[] lSqlParams = new SqlParameter[] {
                    new SqlParameter("@PageName",lNewPageName)
                    ,new SqlParameter("@PID",lParentID)
                    ,new SqlParameter("@Addr",lNewPageAddr)
                };

                lSqlCmd.Parameters.AddRange(lSqlParams);
                lSqlCmd.ExecuteNonQuery();

                context.Response.Redirect("../Html/Page/pageList.html");
            }
        }

        private void PageEdit(HttpContext context)
        {
            string lID = context.Request.Form["txtID"].ToString();
            string lName = context.Request.Form["txtPageName"].ToString();
            string lAddr = context.Request.Form["txtAddr"].ToString();

            string lSQL = "";

            using (SqlConnection lSqlConn = new SqlConnection())
            {
                lSqlConn.ConnectionString = "Data Source=.;Initial Catalog=Auth;User=sa;Password=1;";
                lSqlConn.Open();

                lSQL = "UPDATE T_Page SET";
                lSQL += " PageName=@PageName";
                lSQL += ", Addr=@Addr";
                lSQL += " WHERE ID=@ID";

                SqlCommand lSqlCmd = new SqlCommand(lSQL, lSqlConn);

                SqlParameter[] lSqlParams = new SqlParameter[] {
                    new SqlParameter("@PageName",lName)
                    ,new SqlParameter("@Addr",lAddr)
                    ,new SqlParameter("@ID",lID)
                };

                lSqlCmd.Parameters.AddRange(lSqlParams);

                lSqlCmd.ExecuteNonQuery();

                context.Response.Redirect("../Html/Page/pageList.html");
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