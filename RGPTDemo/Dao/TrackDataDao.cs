using Lib.DBHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace RGPTDemo.Dao
{
    public class TrackDataDao
    {
        public DataTable GetTrackData(string pLine, int pSeq)
        {
            string lSQL = "";
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();
            DataTable lDT = null;

            lSQL += "SELECT ";
            lSQL += "ID";
            lSQL += ", Line";
            lSQL += ", Lng";
            lSQL += ", Lat";
            lSQL += ", Seq";
            lSQL += " FROM T_TrackData";
            lSQL += " WHERE Line=@Line";
            lSQL += " AND Seq=@Seq";
            lSQL += " ORDER BY Seq";

            SqlParameter[] lParams = new SqlParameter[]
            {
                new SqlParameter("@Line",pLine)
                ,new SqlParameter("@Seq",pSeq)
            };

            lDT = lMsSqlHelper.GetDataTable(lSQL, lParams);

            return lDT;
        }        

    }
}