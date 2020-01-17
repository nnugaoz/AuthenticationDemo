using Newtonsoft.Json;
using RGPTDemo.Dao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace RGPTDemo.Handler
{
    /// <summary>
    /// TrackData 的摘要说明
    /// </summary>
    public class TrackData : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string lLine = context.Request.QueryString["Line"].ToString();
            string lSeq = context.Request.QueryString["Seq"].ToString();
            DataTable lDT = null;

            TrackDataDao lDao = new TrackDataDao();

            lDT = lDao.GetTrackData(lLine, Convert.ToInt32(lSeq));

            RequestResponse lRR = new RequestResponse();
            lRR.Data.Add(lDT);

            context.Response.ContentType = "text/plain";
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