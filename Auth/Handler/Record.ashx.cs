using Auth.Dao;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Auth.Handler
{
    /// <summary>
    /// Record 的摘要说明
    /// </summary>
    public class Record : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string lRequestMethod = context.Request.Params["RequestMethod"].ToString();

            switch (lRequestMethod)
            {
                case "GetListPagination":
                    GetListPagination(context);
                    break;
            }
        }

        private void GetListPagination(HttpContext context)
        {
            DataTable lDT = null;

            int lBeginIndex = Convert.ToInt32(context.Request.Params["BeginIndex"].ToString());
            int lEndIndex = Convert.ToInt32(context.Request.Params["EndIndex"].ToString());

            RecordDao lRecordDao = new RecordDao();

            lDT = lRecordDao.GetListPagination(lBeginIndex, lEndIndex);

            context.Response.Write(JsonConvert.SerializeObject(lDT));

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