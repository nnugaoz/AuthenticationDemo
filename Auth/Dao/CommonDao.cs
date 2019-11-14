using Auth.DBHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Auth.Dao
{
    public class CommonDao
    {
        public static DataTable MsSqlQueryDataPagination(String pTableName, String pOrderByFields, int pBeginIndex, int pEndIndex)
        {
            DataTable lDT = null;
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();
            string lSQL = "";
            int lRowCnt = 0;

            lSQL = "SELECT COUNT(*) FROM " + pTableName;
            lDT = lMsSqlHelper.GetDataTable(lSQL);

            if (lDT != null && lDT.Rows.Count > 0)
            {
                lRowCnt = Convert.ToInt32(lDT.Rows[0][0].ToString());
            }

            if (lRowCnt == 0)
            {
                return null;
            }

            lSQL = "";
            lSQL += "SELECT *";
            lSQL += ", " + lRowCnt + " AS RowCnt";
            lSQL += " FROM(";
            lSQL += " SELECT *";
            lSQL += ",ROW_NUMBER()OVER(ORDER BY " + pOrderByFields + ") RowIdx";
            lSQL += " FROM " + pTableName;
            lSQL += ")T";
            lSQL += " WHERE RowIdx>=" + pBeginIndex + " AND RowIdx<=" + pEndIndex; ;

            lDT = lMsSqlHelper.GetDataTable(lSQL);


            return lDT;

        }
    }
}