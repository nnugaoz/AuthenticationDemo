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
    /// home 的摘要说明
    /// </summary>
    public class home : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();

            string lSQL = "SELECT ID, FName, PID, Addr FROM T_Feature WHERE Type='0' ORDER BY Sort";

            DataSet lDS = lMsSqlHelper.GetData(lSQL);

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