using Lib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Lib.DBHelper;

public class T_Line_Departure_ScheduleDao
{
    public static string InsertSQL()
    {
        string lSQL = "";
        lSQL += "INSERT INTO T_Line_Departure_Schedule(";
        lSQL += "ID";
        lSQL += ",LID";
        lSQL += ",DDate";
        lSQL += ",DTime";
        lSQL += ",CarID";
        lSQL += ",EditMan";
        lSQL += ",EditDate";
        lSQL += ")VALUES(";
        lSQL += "@ID";
        lSQL += ",@LID";
        lSQL += ",@DDate";
        lSQL += ",@DTime";
        lSQL += ",@CarID";
        lSQL += ",@EditMan";
        lSQL += ",@EditDate";
        lSQL += ")";
        return lSQL;
    }
    public static string DeleteSQL()
    {
        string lSQL = "";
        lSQL += "DELETE FROM T_Line_Departure_Schedule ";
        lSQL += " WHERE ID=@ID";
        return lSQL;
    }
    public static string UpdateSQL()
    {
        string lSQL = "";
        lSQL += "UPDATE T_Line_Departure_Schedule SET ";
        lSQL += "ID=@ID";
        lSQL += ",LID=@LID";
        lSQL += ",DDate=@DDate";
        lSQL += ",DTime=@DTime";
        lSQL += ",CarID=@CarID";
        lSQL += ",EditMan=@EditMan";
        lSQL += ",EditDate=@EditDate";
        lSQL += " WHERE ID=@ID";
        return lSQL;
    }
    public static string SelectSQL()
    {
        string lSQL = "";
        lSQL += "SELECT ";
        lSQL += "ID";
        lSQL += ",LID";
        lSQL += ",DDate";
        lSQL += ",DTime";
        lSQL += ",CarID";
        lSQL += ",EditMan";
        lSQL += ",EditDate";
        lSQL += " FROM T_Line_Departure_Schedule";
        return lSQL;
    }
    public static string SelectByIDSQL()
    {
        string lSQL = "";
        lSQL += "SELECT ";
        lSQL += "ID";
        lSQL += ",LID";
        lSQL += ",DDate";
        lSQL += ",DTime";
        lSQL += ",CarID";
        lSQL += ",EditMan";
        lSQL += ",EditDate";
        lSQL += " FROM T_Line_Departure_Schedule";
        lSQL += " WHERE ID=@ID";
        return lSQL;
    }

    public static string SelectByDatetimeSQL()
    {
        string lSQL = "";
        lSQL += "SELECT ";
        lSQL += "T1.ID";
        lSQL += ",T1.LID";
        lSQL += ",T1.DDate";
        lSQL += ",T1.DTime";
        lSQL += ",T1.CarID";
        lSQL += ",T1.EditMan";
        lSQL += ",T1.EditDate";
        lSQL += ",T2.Name AS LineName";
        lSQL += ",T3.CarNO";
        lSQL += " FROM T_Line_Departure_Schedule T1";
        lSQL += " LEFT JOIN T_Line T2 ON T1.LID=T2.ID";
        lSQL += " LEFT JOIN T_Car T3 ON T1.CarID=T3.ID";
        lSQL += " WHERE T1.DDate=@DDate";
        lSQL += " AND T1.DTime=@DTime";
        return lSQL;
    }
    public static string SelectPageSQL()
    {
        string lSQL = "";
        lSQL += "EXEC T_Line_Departure_Schedule_Page @ID,@LID,@DDate,@DTime,@CarID,@EditMan,@EditDate,@BeginIndex,@EndIndex";
        return lSQL;
    }
    public static string ImportSQL()
    {
        string lSQL = "";
        lSQL += "INSERT INTO T_Line_Departure_Schedule(";
        lSQL += "ID";
        lSQL += ",LID";
        lSQL += ",DDate";
        lSQL += ",DTime";
        lSQL += ",CarID";
        lSQL += ",EditMan";
        lSQL += ",EditDate";
        lSQL += ")VALUES(";
        lSQL += "@ID";
        lSQL += ",@LID";
        lSQL += ",@DDate";
        lSQL += ",@DTime";
        lSQL += ",@CarID";
        lSQL += ",@EditMan";
        lSQL += ",@EditDate";
        lSQL += ")";
        return lSQL;
    }
    public int Insert(T_Line_Departure_ScheduleModel T_Line_Departure_ScheduleModel)
    {
        string lSQL = InsertSQL();
        List<SqlParameter> lParams = new List<SqlParameter>();
        lParams.Add(new SqlParameter("@ID", T_Line_Departure_ScheduleModel.ID));
        lParams.Add(new SqlParameter("@LID", T_Line_Departure_ScheduleModel.LID));
        lParams.Add(new SqlParameter("@DDate", T_Line_Departure_ScheduleModel.DDate));
        lParams.Add(new SqlParameter("@DTime", T_Line_Departure_ScheduleModel.DTime));
        lParams.Add(new SqlParameter("@CarID", T_Line_Departure_ScheduleModel.CarID));
        lParams.Add(new SqlParameter("@EditMan", T_Line_Departure_ScheduleModel.EditMan));
        lParams.Add(new SqlParameter("@EditDate", T_Line_Departure_ScheduleModel.EditDate));
        MsSqlHelper lMSSqlHelper = new MsSqlHelper();
        return lMSSqlHelper.ExecuteSQL(lSQL, lParams.ToArray());
    }
    public int Update(T_Line_Departure_ScheduleModel T_Line_Departure_ScheduleModel)
    {
        string lSQL = UpdateSQL();
        List<SqlParameter> lParams = new List<SqlParameter>();
        lParams.Add(new SqlParameter("@ID", T_Line_Departure_ScheduleModel.ID));
        lParams.Add(new SqlParameter("@LID", T_Line_Departure_ScheduleModel.LID));
        lParams.Add(new SqlParameter("@DDate", T_Line_Departure_ScheduleModel.DDate));
        lParams.Add(new SqlParameter("@DTime", T_Line_Departure_ScheduleModel.DTime));
        lParams.Add(new SqlParameter("@CarID", T_Line_Departure_ScheduleModel.CarID));
        lParams.Add(new SqlParameter("@EditMan", T_Line_Departure_ScheduleModel.EditMan));
        lParams.Add(new SqlParameter("@EditDate", T_Line_Departure_ScheduleModel.EditDate));
        MsSqlHelper lMSSqlHelper = new MsSqlHelper();
        return lMSSqlHelper.ExecuteSQL(lSQL, lParams.ToArray());
    }
    public int Delete(string pID)
    {
        string lSQL = DeleteSQL();
        List<SqlParameter> lParams = new List<SqlParameter>();
        lParams.Add(new SqlParameter("@ID", pID));
        MsSqlHelper lMSSqlHelper = new MsSqlHelper();
        return lMSSqlHelper.ExecuteSQL(lSQL, lParams.ToArray());
    }
    public DataTable Select()
    {
        string lSQL = SelectSQL();
        DataTable lDT = null;
        MsSqlHelper lMSSqlHelper = new MsSqlHelper();
        lDT = lMSSqlHelper.GetDataTable(lSQL);
        return lDT;
    }
    public DataTable SelectPage(string ID, string LID, string DDate, string DTime, string CarID, string EditMan, string EditDate, int BeginIndex, int EndIndex)
    {
        string lSQL = SelectPageSQL();
        DataTable lDT = null;
        List<SqlParameter> lParams = new List<SqlParameter>();
        lParams.Add(new SqlParameter("@ID", ID));
        lParams.Add(new SqlParameter("@LID", LID));
        lParams.Add(new SqlParameter("@DDate", DDate));
        lParams.Add(new SqlParameter("@DTime", DTime));
        lParams.Add(new SqlParameter("@CarID", CarID));
        lParams.Add(new SqlParameter("@EditMan", EditMan));
        lParams.Add(new SqlParameter("@EditDate", EditDate));
        lParams.Add(new SqlParameter("@BeginIndex", BeginIndex));
        lParams.Add(new SqlParameter("@EndIndex", EndIndex));
        MsSqlHelper lMSSqlHelper = new MsSqlHelper();
        lDT = lMSSqlHelper.GetDataTable(lSQL, lParams.ToArray());
        return lDT;
    }
    public DataTable SelectByID(string ID)
    {
        string lSQL = SelectByIDSQL();
        DataTable lDT = null;
        List<SqlParameter> lParams = new List<SqlParameter>();
        lParams.Add(new SqlParameter("@ID", ID));
        MsSqlHelper lMSSqlHelper = new MsSqlHelper();
        lDT = lMSSqlHelper.GetDataTable(lSQL, lParams.ToArray());
        return lDT;
    }

    public DataTable SelectByDatetime(string DDate, string DTime)
    {
        string lSQL = SelectByDatetimeSQL();
        DataTable lDT = null;
        List<SqlParameter> lParams = new List<SqlParameter>();
        lParams.Add(new SqlParameter("@DDate", DDate));
        lParams.Add(new SqlParameter("@DTime", DTime));
        MsSqlHelper lMSSqlHelper = new MsSqlHelper();
        lDT = lMSSqlHelper.GetDataTable(lSQL, lParams.ToArray());
        return lDT;
    }

    public int Import(string lFileName)
    {
        int lRet = -1;
        DataSet lDS = null;
        DataTable lDT = null;
        NPOIExcelHelper lExcelHelper = new NPOIExcelHelper();
        Dictionary<string, SqlParameter[]> lSqlDic = new Dictionary<string, SqlParameter[]>();
        string lSQL = "";
        MsSqlHelper lMsSqlHelper = new MsSqlHelper();
        lDS = lExcelHelper.ImportToDataSet(lFileName);
        if (lDS != null && lDS.Tables.Count > 0)
        {
            lDT = lDS.Tables[0];
            for (var i = 0; i < lDT.Rows.Count; i++)
            {
                lSQL = ImportSQL();
                SqlParameter[] lParams = new SqlParameter[]
                {
    new SqlParameter("@ID",Guid.NewGuid().ToString())
      ,new SqlParameter("@ID",lDT.Rows[i]["ID"].ToString())
      ,new SqlParameter("@LID",lDT.Rows[i]["LID"].ToString())
      ,new SqlParameter("@DDate",lDT.Rows[i]["DDate"].ToString())
      ,new SqlParameter("@DTime",lDT.Rows[i]["DTime"].ToString())
      ,new SqlParameter("@CarID",lDT.Rows[i]["CarID"].ToString())
      ,new SqlParameter("@EditMan",lDT.Rows[i]["EditMan"].ToString())
      ,new SqlParameter("@EditDate",lDT.Rows[i]["EditDate"].ToString())
     ,new SqlParameter("@EditMan","Admin")
      ,new SqlParameter("@EditDate",DateTime.Now.ToString("yyyy - MM - dd HH: mm: ss"))
                };
                lSqlDic.Add(lSQL + new string(' ', i), lParams);
            }
            lRet = lMsSqlHelper.ExecuteSQLWithTransaction(lSqlDic);
        }
        return lRet;
    }
    internal void Export(ref string lExcelFilePath)
    {
        NPOIExcelHelper lExcelHelper = new NPOIExcelHelper();
        DataSet lDS = new DataSet();
        DataTable lDT = new DataTable();
        lDT = Select();
        lDS.Tables.Add(lDT.Copy());
        lExcelHelper.DataSetExport(lDS, ref lExcelFilePath);
    }
}
