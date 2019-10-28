using Auth.DBHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Auth.Dao
{
    public class FeatureDao
    {
        public DataTable GetFeatureList()
        {
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();
            DataSet lDS = null;
            DataTable lDT = null;
            string lSQL = "";

            lSQL += "SELECT";
            lSQL += " ID";
            lSQL += ", FName";
            lSQL += ", PID";
            lSQL += ", Addr";
            lSQL += ", Type";
            lSQL += ", Sort";
            lSQL += " FROM T_Feature";
            lSQL += " ORDER BY Sort";

            lDS = lMsSqlHelper.GetData(lSQL);

            if (lDS != null && lDS.Tables.Count > 0)
            {
                lDT = lDS.Tables[0];
                lDT.TableName = "FeatureList";
            }
            return lDT;
        }

        public DataTable GetFeatureListByIds(string[] pIDArr)
        {
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();
            DataSet lDS = null;
            DataTable lDT = null;
            List<SqlParameter> lParams = new List<SqlParameter>();

            string lSQL = "";

            lSQL += "SELECT";
            lSQL += " ID";
            lSQL += ", FName";
            lSQL += ", PID";
            lSQL += ", Addr";
            lSQL += ", Type";
            lSQL += ", Sort";
            lSQL += " FROM T_Feature";
            lSQL += " WHERE 1=1 AND ";
            if (pIDArr.Length > 0)
            {
                lSQL += "ID IN(";
                for (int i = 0; i < pIDArr.Length; i++)
                {
                    lSQL += "@ID" + i.ToString() + ",";
                    lParams.Add(new SqlParameter("@ID" + i.ToString(), pIDArr[i]));
                }
                lSQL = lSQL.Substring(0, lSQL.Length - 1);

                lSQL += ")";
            }
            lSQL += " ORDER BY Sort";

            lDS = lMsSqlHelper.GetData(lSQL, lParams.ToArray());

            if (lDS != null && lDS.Tables.Count > 0)
            {
                lDT = lDS.Tables[0];
                lDT.TableName = "FeatureList";
            }
            return lDT;
        }
    }
}