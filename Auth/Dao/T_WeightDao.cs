public class T_WeightDao{
public string Insert(){string lSQL="";
lSQL += "INSERT INTO T_Weight(";
lSQL += "ID";
lSQL += ",CarNO";
lSQL += ",Gross";
lSQL += ",Tare";
lSQL += ",Net";
lSQL += ",GrossTime";
lSQL += ",TareTime";
lSQL += ",NetTime";
lSQL += ",EditMan";
lSQL += ",EditDate";
lSQL += ")VALUES(";
lSQL += "@ID";
lSQL += ",@CarNO";
lSQL += ",@Gross";
lSQL += ",@Tare";
lSQL += ",@Net";
lSQL += ",@GrossTime";
lSQL += ",@TareTime";
lSQL += ",@NetTime";
lSQL += ",@EditMan";
lSQL += ",@EditDate";
lSQL += ")";
return lSQL;
}
public string Delete(){
string lSQL="";
lSQL += "DELETE FROM T_Weight ";
lSQL += " WHERE ID=@ID";
return lSQL;
}
public string Update(){
string lSQL="";
lSQL += "UPDATE T_Weight SET ";
lSQL += "ID=@ID";
lSQL += ",CarNO=@CarNO";
lSQL += ",Gross=@Gross";
lSQL += ",Tare=@Tare";
lSQL += ",Net=@Net";
lSQL += ",GrossTime=@GrossTime";
lSQL += ",TareTime=@TareTime";
lSQL += ",NetTime=@NetTime";
lSQL += ",EditMan=@EditMan";
lSQL += ",EditDate=@EditDate";
lSQL += " WHERE ID=@ID";
return lSQL;
}
public string Select(){
string lSQL="";
lSQL += "SELECT ";
lSQL += "ID";
lSQL += ",CarNO";
lSQL += ",Gross";
lSQL += ",Tare";
lSQL += ",Net";
lSQL += ",GrossTime";
lSQL += ",TareTime";
lSQL += ",NetTime";
lSQL += ",EditMan";
lSQL += ",EditDate";
lSQL += "FROM T_Weight";
lSQL += ")";
return lSQL;
}
public string SelectByID(){
string lSQL="";
lSQL += "SELECT ";
lSQL += "ID";
lSQL += ",CarNO";
lSQL += ",Gross";
lSQL += ",Tare";
lSQL += ",Net";
lSQL += ",GrossTime";
lSQL += ",TareTime";
lSQL += ",NetTime";
lSQL += ",EditMan";
lSQL += ",EditDate";
lSQL += "FROM T_Weight";
lSQL += ")";
lSQL += "WHERE ID=@ID";
return lSQL;
}
}
