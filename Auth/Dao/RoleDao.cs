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
        public DataTable GetRoleByID(string pID)
        {
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();
            DataTable lDTRole = null;

            string lSQL = "";
            lSQL += "SELECT";
            lSQL += " ID";
            lSQL += ", RName";
            lSQL += ", FIDS";
            lSQL += ", EditMan";
            lSQL += ", EditDate";
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

        public void Update(string lID, string lRName, string lFIDS, string pUserID)
        {
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();
            string lSQL = "";

            lSQL += "UPDATE T_Role SET";
            lSQL += " RName=@RName";
            lSQL += ", FIDS=@FIDS";
            lSQL += ", EditMan=@EditMan";
            lSQL += ", EditDate=@EditDate";
            lSQL += " WHERE ID=@ID";

            SqlParameter[] lParams = new SqlParameter[] {
                new SqlParameter("@RName",lRName)
                ,new SqlParameter("@FIDS",lFIDS)
                ,new SqlParameter("@ID",lID)
                ,new SqlParameter("@EditMan",pUserID)
                ,new SqlParameter("@EditDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            };

            lMsSqlHelper.ExecuteSQL(lSQL, lParams);

        }

        internal void Delete(string lID)
        {
            string lSQL = "";
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();

            lSQL += "DELETE FROM T_Role";
            lSQL += " WHERE ID=@ID";

            SqlParameter[] lParams = new SqlParameter[]
            {
                new SqlParameter("@ID",lID)
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
            lSQL += ", EditMan";
            lSQL += ", EditDate";
            lSQL += " FROM";
            lSQL += " T_Role";
            lSQL += " ORDER BY EditDate DESC";

            DataSet lDS = new DataSet();

            lDS = lMsSqlHelper.GetData(lSQL);

            if (lDS != null && lDS.Tables.Count > 0)
            {
                lDTRole = lDS.Tables[0];
                lDTRole.TableName = "RoleList";
            }

            return lDTRole;
        }

        public void Insert(string pRName, string pFIDS, string pUserID)
        {
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();
            string lSQL = "";

            lSQL = "INSERT INTO T_Role(";
            lSQL += "ID";
            lSQL += ", RName";
            lSQL += ", FIDS";
            lSQL += ", EditMan";
            lSQL += ", EditDate";
            lSQL += ")VALUES(";
            lSQL += "@ID";
            lSQL += ", @RName";
            lSQL += ", @FIDS";
            lSQL += ", @EditMan";
            lSQL += ", @EditDate";
            lSQL += ")";

            SqlParameter[] lSqlParams = new SqlParameter[] {
                new SqlParameter("@RName", pRName)
            ,   new SqlParameter("@ID", Guid.NewGuid().ToString())
             ,   new SqlParameter("@FIDS", pFIDS)
            ,   new SqlParameter("@EditMan", pUserID)
             ,   new SqlParameter("@EditDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))
            };

            lMsSqlHelper.ExecuteSQL(lSQL, lSqlParams);
        }

        internal DataTable GetRoleList(int lBeginIndex, int lEndIndex)
        {
            DataTable lDT = null;

            string lSQL = "";
            lSQL += "SELECT";
            lSQL += " ID";
            lSQL += ", RName";
            lSQL += ", FIDS";
            lSQL += ", EditMan";
            lSQL += ", EditDate";
            lSQL += ", Idx";
            lSQL += " FROM(";
            lSQL += "SELECT";
            lSQL += " ID";
            lSQL += ", RName";
            lSQL += ", FIDS";
            lSQL += ", EditMan";
            lSQL += ", EditDate";
            lSQL += ", ROW_NUMBER()OVER(ORDER BY EditDate DESC) Idx";
            lSQL += " FROM T_Role";
            lSQL += ")T";
            lSQL += " WHERE T.Idx>=@BeginIndex AND T.Idx <=@EndIndex";

            SqlParameter[] lParams = new SqlParameter[]{
                new SqlParameter("@BeginIndex",lBeginIndex)
                ,new SqlParameter("@EndIndex",lEndIndex)
            };

            MsSqlHelper lMsSqlHelper = new MsSqlHelper();

            DataSet lDS = lMsSqlHelper.GetData(lSQL, lParams);

            if (lDS != null && lDS.Tables.Count > 0)
            {
                lDS.Tables[0].TableName = "DataList";
                lDT = lDS.Tables[0];
            }

            return lDT;
        }
    }
}