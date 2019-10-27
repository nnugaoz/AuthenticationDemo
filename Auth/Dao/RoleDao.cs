using Auth.DBHelper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Auth.Dao
{
    public class RoleDao
    {
        public DataTable GetByID(string pID)
        {
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();

            DataTable lDTRole = null;

            string lSQL = "";
            lSQL += "SELECT";
            lSQL += " ID";
            lSQL += ", RName";
            lSQL += ", FIDS";
            lSQL += " FROM T_Role";
            lSQL += " WHERE ID=@ID";

            SqlParameter[] lParams = new SqlParameter[] {
                new SqlParameter("@ID",pID)
            };

            DataSet lDS = null;

            lDS = lMsSqlHelper.GetData(lSQL, lParams);

            if (lDS != null && lDS.Tables.Count > 0)
            {
                lDTRole = lDS.Tables[0];
                lDTRole.TableName = "SingleRole";
            }


            return lDTRole;
        }

        public void Update(string lID, string lRName, string lFIDS)
        {
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();
            string lSQL = "";

            lSQL += "UPDATE T_Role SET";
            lSQL += " RName=@RName";
            lSQL += ", FIDS=@FIDS";
            lSQL += " WHERE ID=@ID";

            SqlParameter[] lParams = new SqlParameter[] {
                new SqlParameter("@RName",lRName)
                ,new SqlParameter("@FIDS",lFIDS)
                ,new SqlParameter("@ID",lID)
            };

            lMsSqlHelper.ExecuteSQL(lSQL, lParams);

        }

        public DataTable GetRoleList()
        {
            DataTable lDTRole = new DataTable();
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();
            string lSQL = "";

            lSQL = "SELECT";
            lSQL += " ID";
            lSQL += ", RName";
            lSQL += " FROM";
            lSQL += " T_Role";

            DataSet lDS = new DataSet();

            lDS = lMsSqlHelper.GetData(lSQL);

            if (lDS != null && lDS.Tables.Count > 0)
            {
                lDTRole = lDS.Tables[0];
                lDTRole.TableName = "RoleList";
            }

            return lDTRole;
        }

        public void Insert(string pRName, string pFIDS)
        {
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();
            string lSQL = "";

            lSQL = "INSERT INTO T_Role(";
            lSQL += "ID";
            lSQL += ", RName";
            lSQL += ", FIDS";
            lSQL += ")VALUES(";
            lSQL += "@ID";
            lSQL += ", @RName";
            lSQL += ", @FIDS";
            lSQL += ")";

            SqlParameter[] lSqlParams = new SqlParameter[] {
                new SqlParameter("@RName", pRName)
            ,   new SqlParameter("@ID", Guid.NewGuid().ToString())
             ,   new SqlParameter("@FIDS", pFIDS)
            };

            lMsSqlHelper.ExecuteSQL(lSQL, lSqlParams);
        }
    }
}