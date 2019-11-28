using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Auth.DBHelper;
using Auth.Dao;
using System.IO;
using Lib;

namespace Auth.Handler
{
    /// <summary>
    /// user 的摘要说明
    /// </summary>
    public class User : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string lRequestMethod = context.Request.Params["RequestMethod"].ToString();
            switch (lRequestMethod)
            {
                case "USER_ADD_INIT":
                    AddUserInit(context);
                    break;

                case "USER_ADD":
                    AddUser(context);
                    break;

                case "USER_EDIT":
                    EditUser(context);
                    break;

                case "USER_LIST":
                    GetUserList(context);
                    break;

                case "USER_EDIT_INIT":
                    EditUserInit(context);
                    break;

                case "LOGIN_CHECK":
                    LoginCheck(context);
                    break;

                case "GET_LIST_PAGINATION":
                    GetListPagination(context);
                    break;

                case "IMPORT":
                    Import(context);
                    break;

                case "EXPORT":
                    Export(context);
                    break;
            }
        }

        private void Export(HttpContext context)
        {
            RequestResult lRR = new RequestResult();
            NPOIExcelHelper lExcelHelper = new NPOIExcelHelper();
            String lExcelFilePath = "";
            DataSet lDS = new DataSet();
            DataTable lDT = new DataTable();

            UserDao lUserDao = new UserDao();
            lDT = lUserDao.GetUserList();
            lDS.Tables.Add(lDT.Copy());

            lExcelHelper.DataSetExport(lDS, ref lExcelFilePath);
            lRR.Msg = @"\TempFile\" + lExcelFilePath;

            context.Response.Write(JsonConvert.SerializeObject(lRR));

        }

        private void Import(HttpContext context)
        {
            RequestResult lRR = new RequestResult();

            if (context.Request.Files.Count > 0)
            {
                if (context.Request.Files[0].ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                {
                    string lFileName = @"E:\00_Temp\" + context.Request.Files[0].FileName;
                    context.Request.Files[0].SaveAs(lFileName);

                    UserDao lUserDao = new UserDao();
                    if (lUserDao.Import(lFileName) == 1)
                    {
                        lRR.Status = "1";
                    }
                    else
                    {
                        lRR.Status = "0";
                        lRR.Msg = "保存失败！";
                    }
                }
                else
                {
                    lRR.Status = "0";
                    lRR.Msg = "文件格式错误！";
                }
            }


            context.Response.Write(JsonConvert.SerializeObject(lRR));
        }

        private void GetListPagination(HttpContext context)
        {
            DataTable lDT = null;
            UserDao lUserDao = new UserDao();

            string lUserName = context.Request.QueryString["UserName"].ToString();
            int lBeginIndex = Convert.ToInt32(context.Request.Params["BeginIndex"].ToString());
            int lEndIndex = Convert.ToInt32(context.Request.Params["EndIndex"].ToString());

            lDT = lUserDao.GetListPagination(lUserName, lBeginIndex, lEndIndex);

            context.Response.Write(JsonConvert.SerializeObject(lDT));
        }

        private void AddUserInit(HttpContext context)
        {
            DataSet lDS = new DataSet();
            DataTable lDTRoleList = null;
            RoleDao lRoleDao = new RoleDao();

            lDTRoleList = lRoleDao.GetRoleList();
            lDS.Tables.Add(lDTRoleList.Copy());
            context.Response.Write(JsonConvert.SerializeObject(lDS));

        }

        private void LoginCheck(HttpContext context)
        {
            string lUserName = context.Request.Form["txtUserName"].ToString();
            string lPassword = context.Request.Form["txtPwd"].ToString();
            UserDao lUserDao = new UserDao();
            DataTable lUser = lUserDao.GetUserByUserNamePwd(lUserName, lPassword);

            if (lUser.Rows.Count > 0)
            {
                HttpCookie lCookie = new HttpCookie("UserID", lUser.Rows[0]["ID"].ToString());
                lCookie.Expires = DateTime.Now.AddMinutes(10);
                lCookie.Path = "/";
                context.Response.Cookies.Add(lCookie);

                lCookie = new HttpCookie("UserName", lUser.Rows[0]["UserName"].ToString());
                lCookie.Expires = DateTime.Now.AddMinutes(10);
                lCookie.Path = "/";
                context.Response.Cookies.Add(lCookie);

                context.Response.Redirect("/Html/Home/Home.html");
            }
            else
            {
                context.Response.Redirect("/Html/login.html");
            }
        }

        private void EditUserInit(HttpContext context)
        {
            string lID = context.Request.Params["ID"].ToString();

            UserDao lUserDao = new UserDao();
            DataTable lDTUserSingle = lUserDao.GetUserById(lID);

            RoleDao lRoleDao = new RoleDao();
            DataTable lDTRoleList = lRoleDao.GetRoleListByUserID(lID);

            DataSet lDS = new DataSet();

            lDS.Tables.Add(lDTUserSingle.Copy());
            lDS.Tables.Add(lDTRoleList.Copy());

            context.Response.Write(JsonConvert.SerializeObject(lDS));

        }

        private void GetUserList(HttpContext context)
        {
            UserDao lUserDao = new UserDao();
            DataTable lDTUser = lUserDao.GetUserList();
            DataSet lDS = new DataSet();
            lDS.Tables.Add(lDTUser.Copy());

            context.Response.Write(JsonConvert.SerializeObject(lDS));
        }

        private void EditUser(HttpContext context)
        {
            string lID = context.Request.Form["ID"].ToString();
            string lUserName = context.Request.Form["UserName"].ToString();
            string lRIDS = context.Request.Form["RIDS"].ToString();

            UserDao lUserDao = new UserDao();

            lUserDao.EditUser(lID, lUserName, lRIDS);
            RequestResult lRR = new RequestResult();
            context.Response.Write(JsonConvert.SerializeObject(lRR));
        }

        private void AddUser(HttpContext context)
        {
            string lUserName = context.Request.Params["txtUserName"].ToString();
            string lPwd = context.Request.Params["txtPwd"].ToString();
            string lRIDS = context.Request.Params["txtRIDS"].ToString();

            UserDao lUserDao = new UserDao();

            lUserDao.AddUser(lUserName, lPwd, lRIDS);

            context.Response.Redirect("/Html/User/UserList.html");

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