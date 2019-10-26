using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Auth.DBHelper;

namespace Auth.Handler
{
    /// <summary>
    /// user 的摘要说明
    /// </summary>
    public class user : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();

            string lSQL = "SELECT UserName,Pwd FROM T_User";

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