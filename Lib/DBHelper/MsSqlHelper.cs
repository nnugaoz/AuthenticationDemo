using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Lib.DBHelper
{
    public class MsSqlHelper
    {
        ILog mLogger = LogManager.GetLogger("MsSqlAccess");

        private string mConnectionString = ConfigurationManager.AppSettings["MsSqlConnectionString"].ToString();

        public DataSet GetDataSet(string pSQL)
        {
            DataSet lDS = new DataSet();
            try
            {
                using (SqlConnection lSqlConnection = new SqlConnection(mConnectionString))
                {
                    SqlCommand lSqlCommand = new SqlCommand(pSQL, lSqlConnection);

                    SqlDataAdapter lSqlAdpt = new SqlDataAdapter(lSqlCommand);

                    lSqlAdpt.Fill(lDS);
                }
            }
            catch (Exception ex)
            {
                mLogger.Error(ex);
            }
            return lDS;
        }

        public DataSet GetDataSet(string pSQL, SqlParameter[] pParams)
        {
            DataSet lDS = new DataSet();
            try
            {
                using (SqlConnection lSqlConnection = new SqlConnection(mConnectionString))
                {
                    SqlCommand lSqlCommand = new SqlCommand(pSQL, lSqlConnection);

                    if (pParams != null)
                    {
                        lSqlCommand.Parameters.AddRange(pParams);
                    }
                    SqlDataAdapter lSqlAdpt = new SqlDataAdapter(lSqlCommand);

                    lSqlAdpt.Fill(lDS);
                }
            }
            catch (Exception ex)
            {
                mLogger.Error(ex);
            }
            return lDS;
        }
        public DataTable GetDataTable(string pSQL)
        {
            DataTable lDT = null;
            try
            {
                using (SqlConnection lSqlConnection = new SqlConnection(mConnectionString))
                {
                    SqlCommand lSqlCommand = new SqlCommand(pSQL, lSqlConnection);

                    SqlDataAdapter lSqlAdpt = new SqlDataAdapter(lSqlCommand);

                    DataSet lDS = new DataSet();

                    lSqlAdpt.Fill(lDS);

                    if (lDS != null && lDS.Tables.Count > 0)
                    {
                        lDT = lDS.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                mLogger.Error(ex);
            }
            return lDT;
        }

        public DataTable GetDataTable(string pSQL, SqlParameter[] pParams)
        {
            DataTable lDT = null;

            try
            {
                using (SqlConnection lSqlConnection = new SqlConnection(mConnectionString))
                {
                    SqlCommand lSqlCommand = new SqlCommand(pSQL, lSqlConnection);

                    if (pParams != null)
                    {
                        lSqlCommand.Parameters.AddRange(pParams);
                    }
                    SqlDataAdapter lSqlAdpt = new SqlDataAdapter(lSqlCommand);

                    DataSet lDS = new DataSet();

                    lSqlAdpt.Fill(lDS);


                    if (lDS != null && lDS.Tables.Count > 0)
                    {
                        lDT = lDS.Tables[0];
                    }
                }
            }
            catch (Exception ex)
            {
                mLogger.Error(ex);
            }
            return lDT;
        }

        public Int32 ExecuteSQL(string pSQL)
        {
            Int32 lRet = -1;
            try
            {
                using (SqlConnection lSqlConnection = new SqlConnection(mConnectionString))
                {
                    lSqlConnection.Open();

                    SqlCommand lSqlCommand = new SqlCommand(pSQL, lSqlConnection);

                    lRet = lSqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                mLogger.Error(ex);
            }
            return lRet;
        }

        public Int32 ExecuteSQL(string pSQL, SqlParameter[] pParams)
        {
            Int32 lRet = -1;
            try
            {
                using (SqlConnection lSqlConnection = new SqlConnection(mConnectionString))
                {
                    lSqlConnection.Open();

                    SqlCommand lSqlCommand = new SqlCommand(pSQL, lSqlConnection);

                    if (pParams != null)
                    {
                        lSqlCommand.Parameters.AddRange(pParams);
                    }

                    lRet = lSqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                mLogger.Error(ex);
            }
            return lRet;
        }

        public Int32 ExecuteSQLWithTransaction(Dictionary<string, SqlParameter[]> pSQLDic)
        {
            Int32 lRet = -1;
            try
            {
                using (SqlConnection lSqlConnection = new SqlConnection(mConnectionString))
                {
                    lSqlConnection.Open();
                    SqlTransaction lSqlTransaction = lSqlConnection.BeginTransaction();

                    SqlCommand lSqlCommand = new SqlCommand();
                    lSqlCommand.Connection = lSqlConnection;
                    lSqlCommand.Transaction = lSqlTransaction;

                    try
                    {
                        foreach (KeyValuePair<string, SqlParameter[]> lPair in pSQLDic)
                        {
                            lSqlCommand.CommandText = lPair.Key;
                            if (lPair.Value != null)
                            {
                                lSqlCommand.Parameters.AddRange(lPair.Value);
                            }
                            lSqlCommand.ExecuteNonQuery();
                            lSqlCommand.Parameters.Clear();
                        }

                        lSqlTransaction.Commit();
                        lRet = 1;
                    }
                    catch (Exception ex)
                    {
                        mLogger.Error(ex);
                        lSqlTransaction.Rollback();
                    }
                }
            }
            catch (Exception ex)
            {
                mLogger.Error(ex);
            }
            return lRet;
        }
    }
}