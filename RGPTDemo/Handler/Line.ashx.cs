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
            string lRequestMethod = context.Request.QueryString["RequestMethod"].ToString();

            switch (lRequestMethod)
            {
                case "GetLineInfo":
                    GetLineInfo(context);
                    break;

                case "GetLatestPosition":
                    GetLatestPosition(context);
                    break;
            }
        }

        private void GetLatestPosition(HttpContext context)
        {
            string lLineID = context.Request.QueryString["LineID"].ToString();
            T_Line_Departure_ScheduleDao lLine_Departure_ScheduleDao = new T_Line_Departure_ScheduleDao();

            //线路车辆位置
            DataTable lDTCarInfoList = lLine_Departure_ScheduleDao.SelectTodaysCarsNewestPositionListByLineID(lLineID);

            RequestResponse lRR = new RequestResponse();
            lRR.Data.Add(lDTCarInfoList);

            context.Response.ContentType = "text/plain";
            context.Response.Write(JsonConvert.SerializeObject(lRR));
        }

        private void GetLineInfo(HttpContext context)
        {
            string lLineID = context.Request.QueryString["LineID"].ToString();
            LineDao lLineDao = new LineDao();
            T_Line_Departure_ScheduleDao lLine_Departure_ScheduleDao = new T_Line_Departure_ScheduleDao();

            //线路轨迹
            DataTable lDTLineTrack = lLineDao.GetLineTrack(lLineID);

            //线路站点信息
            DataTable lDTLineStation = lLineDao.GetLineStation(lLineID);

            //线路车辆位置
            DataTable lDTCarInfoList = lLine_Departure_ScheduleDao.SelectTodaysCarsNewestPositionListByLineID(lLineID);

            RequestResponse lRR = new RequestResponse();
            lRR.Data.Add(lDTLineTrack);
            lRR.Data.Add(lDTLineStation);
            lRR.Data.Add(lDTCarInfoList);

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