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
            FeatureDao lFeatureDao = new FeatureDao();
            DataTable lDTFeature = null;

            lDTFeature = lFeatureDao.GetFeatureById(lID);

            DataSet lDS = new DataSet();
            lDS.Tables.Add(lDTFeature.Copy());
            context.Response.Write(JsonConvert.SerializeObject(lDS));
        }

        private void FeatureList(HttpContext context)
        {
            string lRetJson = "";
            FeatureDao lFeatureDao = new FeatureDao();

            DataSet lDS = new DataSet();
            DataTable lDTFeatureList = lFeatureDao.GetFeatureList();

            lDS.Tables.Add(lDTFeatureList.Copy());
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

            FeatureDao lFeatureDao = new FeatureDao();
            lFeatureDao.Add(lPID, lFName, lAddr, lType, lSort);

            RequestResult lRR = new RequestResult();
            context.Response.Write(JsonConvert.SerializeObject(lRR));

        }

        private void FeatureEdit(HttpContext context)
        {
            string lID = context.Request.Form["txtID"].ToString();
            string lName = context.Request.Form["txtFName"].ToString();
            string lAddr = context.Request.Form["txtAddr"].ToString();
            string lType = context.Request.Form["radType"].ToString();
            string lSort = context.Request.Form["txtSort"].ToString();

            FeatureDao lFeatureDao = new FeatureDao();

            lFeatureDao.Update(lID, lName, lAddr, lType, lSort);
            RequestResult lRR = new Handler.RequestResult();

            context.Response.Write(JsonConvert.SerializeObject(lRR));
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