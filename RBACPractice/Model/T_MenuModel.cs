public class T_PermissionModel{
//主键
//MaxLength=50
public string ID{get;set;}
//权限名称
//MaxLength=50
public string Name{get;set;}
//父权限ID
//MaxLength=50
public string PID{get;set;}
//排序
//MaxLength=4
public string Sort{get;set;}
//地址
//MaxLength=200
public string Addr{get;set;}
//编辑人
//MaxLength=50
public string EditMan{get;set;}
//编辑时间
//MaxLength=3
public string EditDate{get;set;}
}
