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
    public class role : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string lRequestMethod = context.Request.Params["RequestMethod"].ToString();

            switch (lRequestMethod)
            {
                case "ROLE_LIST":
                    GetRoleList(context);
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
            }
        }

        private void EditRole(HttpContext context)
        {
            string lID = context.Request.Params["txtID"].ToString();
            string lRName = context.Request.Params["txtRName"].ToString();
            string lFIDS = context.Request.Params["txtFIDS"].ToString();

            RoleDao lRoleDao = new RoleDao();
            lRoleDao.Update(lID, lRName, lFIDS);

            context.Response.Redirect("/Html/Role/RoleList.html");
        }

        private void EditRoleInit(HttpContext context)
        {
            string lID = context.Request.Params["ID"].ToString();
            RoleDao lRoleDao = new RoleDao();
            FeatureDao lFeatureDao = new FeatureDao();

            DataTable lDTRole = lRoleDao.GetByID(lID);
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

            string lID = Guid.NewGuid().ToString();

            MsSqlHelper lMsSqlHelper = new MsSqlHelper();
            string lSQL = "";

            lSQL = "INSERT INTO T_Role(";
            lSQL += "ID";
            lSQL += ", RName";
            lSQL += ", FIDS";
            lSQL += ")VALUES(";
            lSQL += "@ID";
            lSQL += ", @RName";
            lSQL += ", @FIDS";
            lSQL += ")";

            SqlParameter[] lSqlParams = new SqlParameter[] {
                new SqlParameter("@RName", lRoleName)
            ,   new SqlParameter("@ID", lID)
             ,   new SqlParameter("@FIDS", lFIDs)
            };

            if (lMsSqlHelper.ExecuteSQL(lSQL, lSqlParams) > 0)
            {
                context.Response.Redirect("/Html/Role/roleList.html");
            }
        }

        private void GetRoleList(HttpContext context)
        {
            MsSqlHelper lMsSqlHelper = new MsSqlHelper();
            string lSQL = "";

            lSQL = "SELECT";
            lSQL += " ID";
            lSQL += ", RName";
            lSQL += " FROM";
            lSQL += " T_Role";

            DataSet lDS = new DataSet();

            lDS = lMsSqlHelper.GetData(lSQL);

            context.Response.Write(JsonConvert.SerializeObject(lDS));

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