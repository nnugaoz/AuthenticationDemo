using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace RGPTSimulator
{
    /// <summary>
    /// 调度模拟器
    /// 监视线路发车时刻表，到点发车
    /// </summary>
    class SchedulingSimulator
    {
        public void ScheduleMinitor()
        {
            DataTable lDT = null;
            T_Line_Departure_ScheduleDao lLine_Departure_ScheduleDao = new T_Line_Departure_ScheduleDao();
            //循环今日发车时刻表
            while (true)
            {
                string lDate = DateTime.Now.ToString("yyyy-MM-dd");
                string lTime = DateTime.Now.ToString("HH:mm");
                string lCarID = "";
                string lLineID = "";

                lDT = lLine_Departure_ScheduleDao.SelectByDatetime(lDate, lTime);

                if (lDT != null)
                {
                    for (int i = 0; i < lDT.Rows.Count; i++)
                    {
                        lCarID = lDT.Rows[i]["CarID"].ToString();
                        lLineID = lDT.Rows[i]["LID"].ToString();
                         CarTrackSimulator lCarTrackSimulator = new CarTrackSimulator();
                        lCarTrackSimulator.CarID = lCarID;
                        lCarTrackSimulator.LineID = lLineID;
                        lCarTrackSimulator.Simulator();
                    }
                }

                System.Threading.Thread.Sleep(60000);
            }
        }

    }
}
