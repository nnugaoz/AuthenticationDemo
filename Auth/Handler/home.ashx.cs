using Auth.Dao;
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
    public class Home : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpCookie lCookie = context.Request.Cookies["UserID"];
            string lUserID = lCookie.Value;

            HomeDao lHomeDao = new HomeDao();

            DataSet lDS = new DataSet();
            DataTable lDTFeature = lHomeDao.GetFeatureListByUserID(lUserID);

            lDS.Tables.Add(lDTFeature.Copy());

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