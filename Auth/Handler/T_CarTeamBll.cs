using Lib;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
public class T_CarTeamBll
{
    public int insert(T_CarTeamModel T_CarTeamModel)
    {
        string lSQL = T_CarTeamDAL.Insert();
        List<SqlParameter> lParams = new List<SqlParameter>();
        lParams.Add(new SqlParameter("@ID", T_CarTeamModel.ID));
        lParams.Add(new SqlParameter("@Name", T_CarTeamModel.Name));
        lParams.Add(new SqlParameter("@EditMan", T_CarTeamModel.EditMan));
        lParams.Add(new SqlParameter("@EditDate", T_CarTeamModel.EditDate));
        MSSqlHelper lMSSqlHelper = new MSSqlHelper();
        return lMSSqlHelper.ExecuteSQL(lSQL, lParams.ToArray());
    }
    public void update(T_CarTeamModel T_CarTeamModel)
    {
        string lSQL = T_CarTeamDAL.Update();
        List<SqlParameter> lParams = new List<SqlParameter>();
        lParams.Add(new SqlParameter("@ID", T_CarTeamModel.ID));
        lParams.Add(new SqlParameter("@Name", T_CarTeamModel.Name));
        lParams.Add(new SqlParameter("@EditMan", T_CarTeamModel.EditMan));
        lParams.Add(new SqlParameter("@EditDate", T_CarTeamModel.EditDate));
        MSSqlHelper lMSSqlHelper = new MSSqlHelper();
        return lMSSqlHelper.ExecuteSQL(lSQL, lParams.ToArray());
    }
    public void delete(string pID)
{
string lSQL = T_CarTeamDAL.Delete();
List<SqlParameter> lParams = new List<SqlParameter>();
lParams.Add(new SqlParameter("@ID", pID);
MSSqlHelper lMSSqlHelper = new MSSqlHelper();
return lMSSqlHelper.ExecuteSQL(lSQL, lParams.ToArray());
}
    public DataTable select()
{
string lSQL = T_CarTeamDAL.Select();
DataTable lDT = NULL;
List<SqlParameter> lParams = new List<SqlParameter>();
lParams.Add(new SqlParameter("@ID", pID);
MSSqlHelper lMSSqlHelper = new MSSqlHelper();
lDT = lMSSqlHelper.ExecuteSQL(lSQL, lParams.ToArray());
return lDT;
}
}
