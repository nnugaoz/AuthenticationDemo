using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Auth.Dao
{
    public class HomeDao
    {
        public DataTable GetFeatureListByUserID(string pUserID)
        {
            UserDao lUserDao = new UserDao();
            RoleDao lRoleDao = new RoleDao();
            FeatureDao lFeatureDao = new FeatureDao();

            DataTable lDTUser = null;
            DataTable lDTRole = null;
            DataTable lDTFeature = null;

            string[] lRIDArr = null;
            string[] lFIDArr = null;
            List<string> lFIDList = new List<string>();

            lDTUser = lUserDao.GetUserById(pUserID);
            lRIDArr = lDTUser.Rows[0]["RIDS"].ToString().Split(new char[] { ';' });

            for (int i = 0; i < lRIDArr.Length && lRIDArr[i] != ""; i++)
            {
                lDTRole = lRoleDao.GetRoleByID(lRIDArr[i]);
                lFIDArr = lDTRole.Rows[0]["FIDS"].ToString().Split(new char[] { ';' });

                for (int j = 0; j < lFIDArr.Length && lFIDArr[j] != ""; j++)
                {
                    if (!lFIDList.Contains(lFIDArr[j]))
                    {
                        lFIDList.Add(lFIDArr[j]);
                    }
                }
            }

            lDTFeature = lFeatureDao.GetFeatureListByIds(lFIDList.ToArray());

            return lDTFeature;
        }
    }
}