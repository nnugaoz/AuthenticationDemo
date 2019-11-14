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

            lDS = lMsSqlHelper.GetDataSet(lSQL);

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

            lDS = lMsSqlHelper.GetDataSet(lSQL, lParams.ToArray());

            if (lDS != null && lDS.Tables.Count > 0)
            {
                lDT = lDS.Tables[0];
                lDT.TableName = "FeatureList";
            }
            return lDT;
        }

        public DataTable GetFeatureById(string pID)
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
            lSQL += " WHERE ID=@ID ";
            lSQL += " ORDER BY Sort";

            lParams.Add(new SqlParameter("@ID", pID));

            lDS = lMsSqlHelper.GetDataSet(lSQL, lParams.ToArray());

            if (lDS != null && lDS.Tables.Count > 0)
            {
                lDT = lDS.Tables[0];
                lDT.TableName = "FeatureSingle";
            }
            return lDT;
        }

        public DataTable GetChildFeatureList(string pPID)
        {
            DataTable lDT = null;
            string lSQL = "";
            DataSet lDS = null;
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();

            lSQL = "SELECT";
            lSQL += " ID";
            lSQL += ", FName";
            lSQL += ", PID";
            lSQL += ", Addr";
            lSQL += ", Type";
            lSQL += ", Sort";
            lSQL += ", EditMan";
            lSQL += ", EditDate";
            lSQL += " FROM T_Feature";
            lSQL += " WHERE PID=@PID";
            lSQL += " ORDER BY Sort";

            SqlParameter[] lParams = new SqlParameter[]
            {
                new SqlParameter("@PID",pPID)
            };

            lDS = lMsSqlHelper.GetDataSet(lSQL, lParams);

            if (lDS != null && lDS.Tables.Count > 0)
            {
                lDT = lDS.Tables[0];
                lDT.TableName = "FeatureList";
            }

            return lDT;
        }

        public string GetFeatureIDByFID(string pFID)
        {
            string lID = "";
            string lSQL = "";
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();
            DataSet lDS = null;
            lSQL = "SELECT ID FROM T_Feature WHERE FID=@FID";
            SqlParameter[] lParams = new SqlParameter[]
            {
                new SqlParameter("@FID",pFID)
            };
            lDS = lMsSqlHelper.GetDataSet(lSQL, lParams);

            if (lDS != null && lDS.Tables.Count > 0 && lDS.Tables[0].Rows.Count > 0)
            {
                lID = lDS.Tables[0].Rows[0][0].ToString();
            }
            return lID;
        }

        internal void Update(string pID, string pName, string pAddr, string pType, string pSort)
        {

            string lSQL = "";
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();

            lSQL = "UPDATE T_Feature SET";
            lSQL += " FName=@FName";
            lSQL += ", Addr=@Addr";
            lSQL += ", Type=@Type";
            lSQL += ", Sort=@Sort";
            lSQL += " WHERE ID=@ID";

            SqlParameter[] lSqlParams = new SqlParameter[] {
                    new SqlParameter("@FName",pName)
                    ,new SqlParameter("@Addr",pAddr)
                    ,new SqlParameter("@ID",pID)
                     ,new SqlParameter("@Type",pType)
                      ,new SqlParameter("@Sort",pSort)
                };

            lMsSqlHelper.ExecuteSQL(lSQL, lSqlParams);
        }

        internal void Add(string pPID, string pFName, string pAddr, string pType, string pSort)
        {
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();
            string lSQL = "";

            lSQL = "INSERT INTO T_Feature(";
            lSQL += "ID";
            lSQL += ", FName";
            lSQL += ", PID";
            lSQL += ", Addr";
            lSQL += ", Type";
            lSQL += ", Sort";
            lSQL += ")VALUES(";
            lSQL += "NEWID()";
            lSQL += ", @FName";
            lSQL += ", @PID";
            lSQL += ", @Addr";
            lSQL += ", @Type";
            lSQL += ", @Sort";
            lSQL += ")";

            SqlParameter[] lSqlParams = new SqlParameter[] {
                new SqlParameter("@FName",pFName)
                ,new SqlParameter("@PID",pPID)
                ,new SqlParameter("@Addr",pAddr)
                ,new SqlParameter("@Type",pType)
                ,new SqlParameter("@Sort",pSort)
            };

            lMsSqlHelper.ExecuteSQL(lSQL, lSqlParams);
        }
    }
}