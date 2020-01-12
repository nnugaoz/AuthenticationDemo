using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RBACPractice.Handler
{
    public class RBACUser
    {
        string UserName = "";
        //List<T_Permission> PermissionList = new List<T_Permission>();
        public RBACUser(String pUserName)
        {
            UserName = pUserName;
        }

        public Boolean HasPermission(String Permission)
        {
            Boolean lRet = false;


            return lRet;
        }
    }
}