using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tools
{
    class NewHtmlGenerator
    {
        private TableInfo mTable = null;
        private String mHtmlPath = "Html/";

        public NewHtmlGenerator(TableInfo pTable)
        {
            mTable = pTable;
        }

        public void GenerateNewHtml()
        {

            String lTableName = "";
            String lLine = "";

            if (mTable == null) return;

            lTableName = mTable.Name;

            if (!Directory.Exists(mHtmlPath + @"/" + lTableName))
            {
                Directory.CreateDirectory(mHtmlPath + @"/" + lTableName);
            }

            FileStream lFileStream = new FileStream(mHtmlPath + @"\" + lTableName + @"\" + lTableName + "New.html", FileMode.OpenOrCreate, FileAccess.ReadWrite);

            lLine = "<!DOCTYPE html>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<html>";
            FileHelper.AppendLine(lFileStream, lLine);

            Generate_Header(lFileStream);

            Generate_Body(lFileStream);

            lLine = "</html>";
            FileHelper.AppendLine(lFileStream, lLine);

            lFileStream.Close();
        }

        private void Generate_Body(FileStream lFileStream)
        {
            String lLine = "";

            lLine = "<body>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<form method='POST' action='/Handler/" + mTable.Name + ".ashx?RequestMethod=Insert' class='form-horizontal'>";
            FileHelper.AppendLine(lFileStream, lLine);

            for (int i = 0; i < mTable.Columns.Count; i++)
            {
                lLine = "<div class='form-group'>";
                FileHelper.AppendLine(lFileStream, lLine);

                lLine = "<label for='" + mTable.Columns[i].Name + "' class='col-sm-2 control-label'>" + mTable.Columns[i].Name + "</label>";
                FileHelper.AppendLine(lFileStream, lLine);

                lLine = "<div class='col-sm-8'>";
                FileHelper.AppendLine(lFileStream, lLine);

                lLine = "<input type='text' id='" + mTable.Columns[i].Name + "' name='" + mTable.Columns[i].Name + "' class='form-control' />";
                FileHelper.AppendLine(lFileStream, lLine);

                lLine = "</div>";
                FileHelper.AppendLine(lFileStream, lLine);

                lLine = "</div>";
                FileHelper.AppendLine(lFileStream, lLine);

            }

            lLine = "<div class='form-group'>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<div class='col-sm-offset-2 col-sm-8'>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<input type='submit' value='提交' class='btn btn-success' id='btnSubmit'/>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "</div>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "</div>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "</form>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "</body>";
            FileHelper.AppendLine(lFileStream, lLine);
        }

        private void Generate_Header(FileStream lFileStream)
        {
            String lLine = "";

            lLine = "<head>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<meta http-equiv='Content-Type' content='text/html; charset=utf-8'/>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "    <title></title>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "	<meta charset='utf-8' />";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<link href='bootstrap.min.css' rel='stylesheet'/>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<script src='jquery-3.4.1.min.js'></script>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<script src='layer.js'></script>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<script type=\"text/javascript\">";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "$(function () {";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "$('#btnSubmit').on('click', btnSubmit_OnClick);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "});";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "function btnSubmit_OnClick(event) {";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "event.preventDefault();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "var formData = new FormData($('form')[0]);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "$.ajax({";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "type: 'post'";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", url: $('form').attr('action')";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", data: formData";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", processData: false";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", contentType: false";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", success: function (pData) {";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "if (pData === '1') {";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "layer.alert('保存成功!', { icon: 1 }, function (index) {";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "layer.close(index);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "index = parent.layer.getFrameIndex(window.name); //先得到当前iframe层的索引";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "parent.layer.close(index); //再执行关闭";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "parent.Init();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "})";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "} else {";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "layer.alert('保存失败!', { icon: 2 }, function (index) {";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "layer.close(index);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "})";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "});";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "return false;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "</script>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "</head>";
            FileHelper.AppendLine(lFileStream, lLine);
        }
    }
}
