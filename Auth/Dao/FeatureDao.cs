using Auth.DBHelper;
using System;
using System.Collections.Generic;
using System.Data;
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

            lDS = lMsSqlHelper.GetData(lSQL);

            if (lDS != null && lDS.Tables.Count > 0)
            {
                lDT = lDS.Tables[0];
            }
            return lDT;
        }
    }
}