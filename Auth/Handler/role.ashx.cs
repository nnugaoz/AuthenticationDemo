using Auth.Dao;
using Auth.DBHelper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Auth.Handler
{
    /// <summary>
    /// role 的摘要说明
    /// </summary>
    public class Role : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string lRequestMethod = context.Request.Params["RequestMethod"].ToString();

            switch (lRequestMethod)
            {
                case "ROLE_LIST_PAGE":
                    GetRoleListPage(context);
                    break;

                case "ROLE_ADD":
                    AddRole(context);
                    break;

                case "ROLE_EDIT_INIT":
                    EditRoleInit(context);
                    break;

                case "ROLE_EDIT":
                    EditRole(context);
                    break;

                case "ROLE_DEL":
                    DelRole(context);
                    break;

                case "PageFeatureInit":
                    PageFeatureInit(context);
                    break;
            }
        }

        private void PageFeatureInit(HttpContext context)
        {
            FeatureDao lFeatureDao = new FeatureDao();
            string lPageName = context.Request.Params["PageName"].ToString();
            string lFeatureID = lFeatureDao.GetFeatureIDByFID(lPageName);
            DataTable lDT = null;
            string lRetJson = "";

            if (lFeatureID != "")
            {
                lDT = lFeatureDao.GetChildFeatureList(lFeatureID);

                if (lDT != null)
                {
                    lRetJson = JsonConvert.SerializeObject(lDT);
                }
            }

            context.Response.Write(lRetJson);
        }

        private void DelRole(HttpContext context)
        {
            string lID = context.Request.Params["ID"].ToString();

            RoleDao lRoleDao = new RoleDao();
            lRoleDao.Delete(lID);

            RequestResult lRR = new RequestResult();
            context.Response.Write(JsonConvert.SerializeObject(lRR));
        }

        private void EditRole(HttpContext context)
        {
            string lID = context.Request.Params["txtID"].ToString();
            string lRName = context.Request.Params["txtRName"].ToString();
            string lFIDS = context.Request.Params["txtFIDS"].ToString();
            string lUserID = context.Request.Cookies["UserID"].Value;

            RoleDao lRoleDao = new RoleDao();
            lRoleDao.Update(lID, lRName, lFIDS, lUserID);
            RequestResult lRR = new RequestResult();

            context.Response.Write(JsonConvert.SerializeObject(lRR));

        }

        private void EditRoleInit(HttpContext context)
        {
            string lID = context.Request.Params["ID"].ToString();
            RoleDao lRoleDao = new RoleDao();
            FeatureDao lFeatureDao = new FeatureDao();

            DataTable lDTRole = lRoleDao.GetRoleByID(lID);
            lDTRole.TableName = "Role";
            DataTable lDTFeature = lFeatureDao.GetFeatureList();
            lDTFeature.TableName = "Feature";

            DataSet lDS = new DataSet();

            lDS.Tables.Add(lDTRole.Copy());
            lDS.Tables.Add(lDTFeature.Copy());

            context.Response.Write(JsonConvert.SerializeObject(lDS));

        }


        private void AddRole(HttpContext context)
        {
            string lRoleName = context.Request.Form["Role_Name"].ToString();
            string lFIDs = context.Request.Form["txtSelectedFID"].ToString();
            string lUserID = context.Request.Cookies["UserID"].Value;

            RoleDao lRoleDao = new RoleDao();
            lRoleDao.Insert(lRoleName, lFIDs, lUserID);
            RequestResult lRR = new Handler.RequestResult();

            context.Response.Write(JsonConvert.SerializeObject(lRR));

        }

        private void GetRoleListPage(HttpContext context)
        {
            int lBeginIndex = Convert.ToInt32(context.Request.Params["BeginIndex"].ToString());
            int lEndIndex = Convert.ToInt32(context.Request.Params["EndIndex"].ToString());

            RoleDao lRoleDao = new RoleDao();
            DataTable lDTRole = lRoleDao.GetRoleList(lBeginIndex, lEndIndex);

            context.Response.Write(JsonConvert.SerializeObject(lDTRole));

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}