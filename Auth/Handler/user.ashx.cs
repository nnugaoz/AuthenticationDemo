﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Auth.DBHelper;
using Auth.Dao;

namespace Auth.Handler
{
    /// <summary>
    /// user 的摘要说明
    /// </summary>
    public class user : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string lRequestMethod = context.Request.Params["RequestMethod"].ToString();
            switch (lRequestMethod)
            {
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

            }
        }

        private void EditUserInit(HttpContext context)
        {
            string lID = context.Request.Params["ID"].ToString();

            UserDao lUserDao = new UserDao();
            DataTable lDTUserSingle = lUserDao.GetUserById(lID);

            RoleDao lRoleDao = new RoleDao();
            DataTable lDTRoleList = lRoleDao.GetRoleList();



        }

        private void GetUserList(HttpContext context)
        {
            UserDao lUserDao = new UserDao();
            DataTable lDTUser = lUserDao.GetUserList();
            DataSet lDS = new DataSet();
            lDS.Tables.Add(lDTUser.Copy());
        }

        private void EditUser(HttpContext context)
        {
            string lID = context.Request.Params["ID"].ToString();
            string lUserName = context.Request.Params["txtUserName"].ToString();
            string lRIDS = context.Request.Params["txtRIDS"].ToString();

            UserDao lUserDao = new UserDao();

            lUserDao.EditUser(lID, lUserName, lRIDS);

            context.Response.Redirect("/Html/User/UserList.html");
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