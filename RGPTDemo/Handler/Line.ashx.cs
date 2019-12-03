using Newtonsoft.Json;
using RGPTDemo.Dao;
using System.Data;
using System.Web;

namespace RGPTDemo.Handler
{
    /// <summary>
    /// Line 的摘要说明
    /// </summary>
    public class Line : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string lLineID = context.Request.QueryString["LineID"].ToString();
            LineDao lLineDao = new LineDao();

            DataTable lDT = lLineDao.GetLineTrack(lLineID);

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