public class MenuModel{
//主键
//MaxLength=50
public string ID{get;set;}
//父级菜单ID
//MaxLength=50
public string PID{get;set;}
//菜单名称
//MaxLength=50
public string Name{get;set;}
//排序
//MaxLength=4
public string Sort{get;set;}
//页面地址
//MaxLength=50
public string Url{get;set;}
}
