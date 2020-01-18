using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace RGPTSimulator
{
    /// <summary>
    /// 车辆运行轨迹模拟器
    /// 生成指定车辆、指定线路中的经度、纬度及速度数据
    /// 用于模拟车辆实时位置。
    /// </summary>
    class CarTrackSimulator
    {
        public string CarID { get; set; }

        public string LineID { get; set; }

        public void Simulator()
        {
            int seq = 1;
            string lng = "";
            string lat = "";
            DataTable lDT = null;
            T_TrackDataDao lTrackDataDao = new T_TrackDataDao();
            T_CarTrackModel lModel = new T_CarTrackModel();
            T_CarTrackDao lCarTrackDao = new T_CarTrackDao();

            lDT = lTrackDataDao.SelectByLineIDAndSeq(LineID, seq);
            while (lDT.Rows.Count > 0)
            {
                lng = lDT.Rows[0]["Lng"].ToString();
                lat = lDT.Rows[0]["Lat"].ToString();

                lModel.Lng = lng;
                lModel.Lat = lat;
                lModel.CarID = CarID;
                lModel.CollectTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff");
                lModel.Speed = "20";
                lCarTrackDao.Insert(lModel);
                System.Threading.Thread.Sleep(1000);
                seq += 1;
                lDT = lTrackDataDao.SelectByLineIDAndSeq(LineID, seq);
            }
        }

    }
}
