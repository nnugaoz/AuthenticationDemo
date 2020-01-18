using Lib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Lib.DBHelper;

public class T_TrackDataDao
{
    public static string InsertSQL()
    {
        string lSQL = "";
        lSQL += "INSERT INTO T_TrackData(";
        lSQL += "ID";
        lSQL += ",Line";
        lSQL += ",Lng";
        lSQL += ",Lat";
        lSQL += ",Seq";
        lSQL += ")VALUES(";
        lSQL += "@ID";
        lSQL += ",@Line";
        lSQL += ",@Lng";
        lSQL += ",@Lat";
        lSQL += ",@Seq";
        lSQL += ")";
        return lSQL;
    }
    public static string DeleteSQL()
    {
        string lSQL = "";
        lSQL += "DELETE FROM T_TrackData ";
        lSQL += " WHERE ID=@ID";
        return lSQL;
    }
    public static string UpdateSQL()
    {
        string lSQL = "";
        lSQL += "UPDATE T_TrackData SET ";
        lSQL += "ID=@ID";
        lSQL += ",Line=@Line";
        lSQL += ",Lng=@Lng";
        lSQL += ",Lat=@Lat";
        lSQL += ",Seq=@Seq";
        lSQL += " WHERE ID=@ID";
        return lSQL;
    }
    public static string SelectSQL()
    {
        string lSQL = "";
        lSQL += "SELECT ";
        lSQL += "ID";
        lSQL += ",Line";
        lSQL += ",Lng";
        lSQL += ",Lat";
        lSQL += ",Seq";
        lSQL += " FROM T_TrackData";
        return lSQL;
    }
    public static string SelectByIDSQL()
    {
        string lSQL = "";
        lSQL += "SELECT ";
        lSQL += "ID";
        lSQL += ",Line";
        lSQL += ",Lng";
        lSQL += ",Lat";
        lSQL += ",Seq";
        lSQL += " FROM T_TrackData";
        lSQL += " WHERE ID=@ID";
        return lSQL;
    }
    public static string SelectByLineIDAndSeqSQL()
    {
        string lSQL = "";
        lSQL += "SELECT ";
        lSQL += "ID";
        lSQL += ",Line";
        lSQL += ",Lng";
        lSQL += ",Lat";
        lSQL += ",Seq";
        lSQL += " FROM T_TrackData";
        lSQL += " WHERE Line=@Line";
        lSQL += " AND Seq=@Seq";
        return lSQL;
    }
    public static string SelectPageSQL()
    {
        string lSQL = "";
        lSQL += "EXEC T_TrackData_Page @ID,@Line,@Lng,@Lat,@Seq,@BeginIndex,@EndIndex";
        return lSQL;
    }
    public static string ImportSQL()
    {
        string lSQL = "";
        lSQL += "INSERT INTO T_TrackData(";
        lSQL += "ID";
        lSQL += ",Line";
        lSQL += ",Lng";
        lSQL += ",Lat";
        lSQL += ",Seq";
        lSQL += ")VALUES(";
        lSQL += "@ID";
        lSQL += ",@Line";
        lSQL += ",@Lng";
        lSQL += ",@Lat";
        lSQL += ",@Seq";
        lSQL += ")";
        return lSQL;
    }
    public int Insert(T_TrackDataModel T_TrackDataModel)
    {
        string lSQL = InsertSQL();
        List<SqlParameter> lParams = new List<SqlParameter>();
        lParams.Add(new SqlParameter("@ID", T_TrackDataModel.ID));
        lParams.Add(new SqlParameter("@Line", T_TrackDataModel.Line));
        lParams.Add(new SqlParameter("@Lng", T_TrackDataModel.Lng));
        lParams.Add(new SqlParameter("@Lat", T_TrackDataModel.Lat));
        lParams.Add(new SqlParameter("@Seq", T_TrackDataModel.Seq));
        MsSqlHelper lMSSqlHelper = new MsSqlHelper();
        return lMSSqlHelper.ExecuteSQL(lSQL, lParams.ToArray());
    }
    public int Update(T_TrackDataModel T_TrackDataModel)
    {
        string lSQL = UpdateSQL();
        List<SqlParameter> lParams = new List<SqlParameter>();
        lParams.Add(new SqlParameter("@ID", T_TrackDataModel.ID));
        lParams.Add(new SqlParameter("@Line", T_TrackDataModel.Line));
        lParams.Add(new SqlParameter("@Lng", T_TrackDataModel.Lng));
        lParams.Add(new SqlParameter("@Lat", T_TrackDataModel.Lat));
        lParams.Add(new SqlParameter("@Seq", T_TrackDataModel.Seq));
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
    public DataTable SelectPage(string ID, string Line, string Lng, string Lat, string Seq, int BeginIndex, int EndIndex)
    {
        string lSQL = SelectPageSQL();
        DataTable lDT = null;
        List<SqlParameter> lParams = new List<SqlParameter>();
        lParams.Add(new SqlParameter("@ID", ID));
        lParams.Add(new SqlParameter("@Line", Line));
        lParams.Add(new SqlParameter("@Lng", Lng));
        lParams.Add(new SqlParameter("@Lat", Lat));
        lParams.Add(new SqlParameter("@Seq", Seq));
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

    public DataTable SelectByLineIDAndSeq(string Line, int Seq)
    {
        string lSQL = SelectByLineIDAndSeqSQL();
        DataTable lDT = null;
        List<SqlParameter> lParams = new List<SqlParameter>();
        lParams.Add(new SqlParameter("@Line", Line));
        lParams.Add(new SqlParameter("@Seq", Seq));
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
      ,new SqlParameter("@Line",lDT.Rows[i]["Line"].ToString())
      ,new SqlParameter("@Lng",lDT.Rows[i]["Lng"].ToString())
      ,new SqlParameter("@Lat",lDT.Rows[i]["Lat"].ToString())
      ,new SqlParameter("@Seq",lDT.Rows[i]["Seq"].ToString())
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
