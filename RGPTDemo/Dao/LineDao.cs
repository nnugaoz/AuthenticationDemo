using Lib.DBHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RGPTDemo.Dao
{
    public class LineDao
    {

        public DataTable GetLineTrack(string pLineID)
        {
            string lSQL = "";
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();
            DataTable lDT = null;

            lSQL += "SELECT ";
            lSQL += "ID";
            lSQL += ", LID";
            lSQL += ", Lng";
            lSQL += ", Lat";
            lSQL += ", Seq";
            lSQL += " FROM T_Line_Track";
            lSQL += " WHERE LID=@LID";
            lSQL += " ORDER BY Seq";

            SqlParameter[] lParams = new SqlParameter[]
            {
                new SqlParameter("@LID",pLineID)
            };

            lDT = lMsSqlHelper.GetDataTable(lSQL, lParams);

            return lDT;
        }

        public DataTable GetLineStation(string pLineID)
        {
            string lSQL = "";
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();
            DataTable lDT = null;

            lSQL += "SELECT ";
            lSQL += "T_Lin_Sta.ID";
            lSQL += ", T_Lin_Sta.LID";
            lSQL += ", T_Lin_Sta.SID";
            lSQL += ", T_Lin_Sta.Seq";
            lSQL += ", T_Sta.Name";
            lSQL += ", T_Sta.Lng";
            lSQL += ", T_Sta.Lat";
            lSQL += " FROM T_Line_Station T_Lin_Sta";
            lSQL += " LEFT JOIN T_Station T_Sta ON T_Lin_Sta.SID=T_Sta.ID";
            lSQL += " WHERE T_Lin_Sta.LID=@LID";
            lSQL += " ORDER BY T_Lin_Sta.Seq";

            SqlParameter[] lParams = new SqlParameter[]
            {
                new SqlParameter("@LID",pLineID)
            };

            lDT = lMsSqlHelper.GetDataTable(lSQL, lParams);

            return lDT;
        }
    }
}