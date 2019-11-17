using Auth.DBHelper;
using Lib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Auth.Dao
{
    public class UserDao
    {

        public void AddUser(string pUserName, string pPwd, string pRIDS)
        {
            string lSQL = "";
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();

            lSQL = "INSERT INTO T_User(";
            lSQL += "ID";
            lSQL += ", UserName";
            lSQL += ", Pwd";
            lSQL += ", RIDS";
            lSQL += ")VALUES(";
            lSQL += "@ID";
            lSQL += ", @UserName";
            lSQL += ", @Pwd";
            lSQL += ", @RIDS";
            lSQL += ")";

            SqlParameter[] lSqlParams = new SqlParameter[]{
                new SqlParameter("@UserName",pUserName)
                ,new SqlParameter("@Pwd",pPwd)
                ,new SqlParameter("@RIDS",pRIDS)
                ,new  SqlParameter("@ID",Guid.NewGuid().ToString())
            };

            lMsSqlHelper.ExecuteSQL(lSQL, lSqlParams);
        }

        public void EditUser(string pID, string pUserName, string pRIDS)
        {
            string lSQL = "";
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();

            lSQL = "UPDATE T_User SET";
            lSQL += " UserName=@UserName";
            lSQL += ", RIDS=@RIDS";
            lSQL += " WHERE ID=@ID";

            SqlParameter[] lSqlParams = new SqlParameter[]{
                new SqlParameter("@UserName",pUserName)
                ,new SqlParameter("@RIDS",pRIDS)
                ,new  SqlParameter("@ID",pID)
            };

            lMsSqlHelper.ExecuteSQL(lSQL, lSqlParams);
        }

        public DataTable GetUserById(string lID)
        {
            DataTable lDTUserSingle = null;

            string lSQL = "";
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();

            lSQL += "SELECT ";
            lSQL += " ID";
            lSQL += ", UserName";
            lSQL += ", RIDS";
            lSQL += " FROM T_User";
            lSQL += " WHERE ID=@ID";

            SqlParameter[] lParams = new SqlParameter[]{
                new SqlParameter("@ID",lID)
            };

            DataSet lDS = lMsSqlHelper.GetDataSet(lSQL, lParams);

            if (lDS != null && lDS.Tables.Count > 0)
            {
                lDTUserSingle = lDS.Tables[0];
                lDTUserSingle.TableName = "UserSingle";
            }

            return lDTUserSingle;
        }

        internal int Import(string lFileName)
        {
            int lRet = -1;
            DataSet lDS = null;
            DataTable lDT = null;
            NPOIExcelHelper lExcelHelper = new NPOIExcelHelper();
            Dictionary<string, SqlParameter[]> lSqlDic = new Dictionary<string, SqlParameter[]>();
            string lSQL = "";
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();
            lDS = lExcelHelper.LoadToDataSet(lFileName);

            if (lDS != null && lDS.Tables.Count > 0)
            {
                lDT = lDS.Tables[0];
                for (var i = 0; i < lDT.Rows.Count; i++)
                {
                    lSQL = "INSERT INTO T_User(";
                    lSQL += "ID";
                    lSQL += ", UserName";
                    lSQL += ", RIDS";
                    lSQL += ")VALUES(";
                    lSQL += "@ID";
                    lSQL += ", @UserName";
                    lSQL += ", @RIDS";
                    lSQL += ")";

                    SqlParameter[] lParams = new SqlParameter[]
                    {
                        new SqlParameter("@ID",Guid.NewGuid().ToString())
                        ,new SqlParameter("@UserName",lDT.Rows[i]["UserName"].ToString())
                        ,new SqlParameter("@RIDS","cdfe4d65-5244-478f-afd6-ce8ab2740894")
                    };
                    lSqlDic.Add(lSQL + new string(' ', i), lParams);
                }

                lRet = lMsSqlHelper.ExecuteSQLWithTransaction(lSqlDic);
            }

            return lRet;
        }

        public DataTable GetUserList()
        {
            DataTable lDTUser = null;
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();

            string lSQL = "SELECT ";
            lSQL += " ID";
            lSQL += ", UserName";
            lSQL += ", RIDS";
            lSQL += " FROM T_User";

            DataSet lDS = new DataSet();

            lDS = lMsSqlHelper.GetDataSet(lSQL);

            if (lDS != null && lDS.Tables.Count > 0)
            {
                lDTUser = lDS.Tables[0];
                lDTUser.TableName = "UserList";
            }

            return lDTUser;
        }

        public DataTable GetUserByUserNamePwd(string pUserName, string pPassword)
        {
            DataTable lDTUserSingle = null;

            String lSQL = "SELECT ID";
            lSQL += ", UserName";
            lSQL += ", RIDS";
            lSQL += " FROM T_User";
            lSQL += " WHERE UserName=@UserName AND Pwd=@Pwd";

            SqlParameter[] lParams = new SqlParameter[]{
                new SqlParameter("@UserName",pUserName)
                ,new SqlParameter("@Pwd",pPassword)
            };

            MsSqlHelper lMsSql = new MsSqlHelper();

            DataSet lDS = lMsSql.GetDataSet(lSQL, lParams);

            if (lDS != null && lDS.Tables.Count > 0)
            {
                lDTUserSingle = lDS.Tables[0];
                lDTUserSingle.TableName = "UserSingle";
            }

            return lDTUserSingle;
        }

        internal DataTable GetListPagination(int pBeginIndex, int pEndIndex)
        {
            DataTable lDT = null;
            lDT = CommonDao.MsSqlQueryDataPagination("T_User", "ID", pBeginIndex, pEndIndex);
            return lDT;
        }
    }
}