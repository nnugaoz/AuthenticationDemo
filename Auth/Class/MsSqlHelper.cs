﻿using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Auth
{
    public class MsSqlHelper
    {
        ILog mLogger = LogManager.GetLogger("MsSqlAccess");

        private string mConnectionString = ConfigurationManager.AppSettings["MsSqlConnectionString"].ToString();

        public DataSet GetData(string pSQL)
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

        public DataSet GetData(string pSQL, SqlParameter[] pParams)
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
    }
}