using System.Collections.Generic;

public class V_RoleModel
{
    //主键
    //MaxLength=50
    public string ID { get; set; }
    //角色名称
    //MaxLength=50
    public string Name { get; set; }
    //编辑人
    //MaxLength=50
    public string EditMan { get; set; }
    //编辑时间
    //MaxLength=3
    public string EditDate { get; set; }

    public List<T_Role_PermissionModel> RoleMenuList { get; set; }
}
