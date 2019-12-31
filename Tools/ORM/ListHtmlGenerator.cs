using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tools
{
    class ListHtmlGenerator
    {
        private TableInfo mTable = null;
        private String mHtmlPath = "Html/";

        public ListHtmlGenerator(TableInfo pTable)
        {
            mTable = pTable;
        }

        public void GenerateListHtml()
        {
            String lTableName = "";
            String lLine = "";

            if (mTable == null) return;

            lTableName = mTable.Name;

            if (!Directory.Exists(mHtmlPath + @"/" + lTableName))
            {
                Directory.CreateDirectory(mHtmlPath + @"/" + lTableName);
            }

            FileStream lFileStream = File.Create(mHtmlPath + @"/" + lTableName + @"/" + lTableName + "List.html");
            lLine = "<!DOCTYPE html>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<html xmlns=\"http://www.w3.org/1999/xhtml\"> ";
            FileHelper.AppendLine(lFileStream, lLine);

            Generate_Header(lFileStream);

            Generate_Body(lFileStream);

            lLine = "</html> ";
            FileHelper.AppendLine(lFileStream, lLine);

            lFileStream.Close();
        }

        private void Generate_Body(FileStream lFileStream)
        {
            String lLine = "";

            lLine = "<body> ";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<div class='form-inline' style='padding:10px;'>";
            FileHelper.AppendLine(lFileStream, lLine);

            for (int i = 0; i < mTable.Columns.Count; i++)
            {
                if (!mTable.Columns[i].SearchFlg) continue;

                lLine = "<div class='form-group'>";
                FileHelper.AppendLine(lFileStream, lLine);

                lLine = "<label for='" + mTable.Columns[i].Name + "'>" + mTable.Columns[i].Comment + "</label>";
                FileHelper.AppendLine(lFileStream, lLine);

                lLine = "<input type='text' id='" + mTable.Columns[i].Name + "' name='" + mTable.Columns[i].Name + "' class='form-control' />";
                FileHelper.AppendLine(lFileStream, lLine);

                lLine = "</div>";
                FileHelper.AppendLine(lFileStream, lLine);
            }

            lLine = "<input type='button' class='btn btn-success' value='检索' id='btnSearch' />";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<input type='button' class='btn btn-primary' value='清空' id='btnClear' />";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "</div>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<div style='padding:10px'>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<input type='button' id='btnNew' class='btn btn-success' value='新增'/>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<input type = 'button' id = 'btnImport' class='btn btn-success' value='导入' />";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<input type = 'button' id='btnExport' class='btn btn-success' value='导出' />";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<input type = 'file' id='fileImport' name ='fileImport' accept ='.xls,.xlsx' style ='display:none;' />";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<div id='" + mTable.Name + "List' style='margin-top:20px;'></div>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "</div>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "</body> ";
            FileHelper.AppendLine(lFileStream, lLine);
        }

        private void Generate_Header(FileStream lFileStream)
        {
            String lLine = "";
            lLine = "<head > ";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<title>" + mTable.Name + "列表 </title>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<link href='bootstrap.min.css' rel='stylesheet'/>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<script src='jquery-3.4.1.min.js'></script>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<script src='layer.js'></script>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<script src = 'Pagination.js'></script>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "<script type='text/javascript'>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "$(function () {";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "Init();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "$('#btnSearch').on('click', btnSearch_OnClick);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "$('#btnClear').on('click', btnClear_OnClick);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "$('#btnNew').on('click', btnNew_OnClick);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = " $('#btnImport').on('click', btnImport_OnClick);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = " $('#fileImport').on('change', fileImport_OnChange);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "$('#btnExport').on('click', btnExport_OnClick);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = " })";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "function btnSearch_OnClick() {";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "Init();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "function btnClear_OnClick() {";
            FileHelper.AppendLine(lFileStream, lLine);

            for (int i = 0; i < mTable.Columns.Count; i++)
            {
                if (!mTable.Columns[i].SearchFlg) continue;

                lLine = "$('#" + mTable.Columns[i].Name + "').val('');";
                FileHelper.AppendLine(lFileStream, lLine);
            }

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "function Init() {";
            FileHelper.AppendLine(lFileStream, lLine);

            for (int i = 0; i < mTable.Columns.Count; i++)
            {
                if (!mTable.Columns[i].SearchFlg) continue;

                lLine = "var " + mTable.Columns[i].Name + " = $('#" + mTable.Columns[i].Name + "').val();";
                FileHelper.AppendLine(lFileStream, lLine);
            }

            lLine = "$('#" + mTable.Name + "List').empty();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "var lPConfig = new PaginationConfiguration();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "lPConfig.RequestUrl = '/Handler/" + mTable.Name + ".ashx?RequestMethod=SelectPage'";
            FileHelper.AppendLine(lFileStream, lLine);

            for (int i = 0; i < mTable.Columns.Count; i++)
            {
                if (!mTable.Columns[i].SearchFlg) continue;

                lLine = "lPConfig.RequestUrl += '&" + mTable.Columns[i].Name + "='+" + mTable.Columns[i].Name + ";";
                FileHelper.AppendLine(lFileStream, lLine);
            }

            lLine = "lPConfig.PageContainerID = '" + mTable.Name + "List';";
            FileHelper.AppendLine(lFileStream, lLine);

            for (int i = 0; i < mTable.Columns.Count; i++)
            {
                if (!mTable.Columns[i].ListVisible) continue;

                lLine = "var " + mTable.Columns[i].Name + "Col = new PaginationColumnConfiguration();";
                FileHelper.AppendLine(lFileStream, lLine);

                lLine = mTable.Columns[i].Name + "Col.HeaderText = '" + mTable.Columns[i].Comment + "';";
                FileHelper.AppendLine(lFileStream, lLine);

                lLine = mTable.Columns[i].Name + "Col.Type = 'TXT';";
                FileHelper.AppendLine(lFileStream, lLine);

                lLine = mTable.Columns[i].Name + "Col.DataField = '" + mTable.Columns[i].Name + "';";
                FileHelper.AppendLine(lFileStream, lLine);

                lLine = "lPConfig.Columns.push(" + mTable.Columns[i].Name + "Col);";
                FileHelper.AppendLine(lFileStream, lLine);
            }

            lLine = "var EditCol = new PaginationColumnConfiguration();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "EditCol.HeaderText = '编辑';";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "EditCol.Type = 'BTN';";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "EditCol.DataField = '编辑';";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "EditCol.CssClass = 'btn btn-success';";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "EditCol.OnClickEventHandler = btnEdit_OnClick;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "lPConfig.Columns.push(EditCol);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "var DelCol = new PaginationColumnConfiguration();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "DelCol.HeaderText = '删除';";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "DelCol.Type = 'BTN';";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "DelCol.DataField = '删除';";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "DelCol.CssClass = 'btn btn-success';";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "DelCol.OnClickEventHandler = btnDel_OnClick;";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "lPConfig.Columns.push(DelCol);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "var " + mTable.Name + "ListPagination = new Pagination(lPConfig);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = mTable.Name + "ListPagination.RequestData(1);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "function btnEdit_OnClick(event) {";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "layer.open({";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "type: 2";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", title: '编辑'";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", content: '/Html/" + mTable.Name + "/" + mTable.Name + "Edit.html?ID=' + event.data.RowData.ID";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", area: ['800px', '600px']";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "});";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "function btnDel_OnClick(event) {";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "layer.confirm('您确认要删除此条记录吗？', { icon: 3, title: '提示' }, function (index) {";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "$.ajax({";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "type: 'get'";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", url: '/Handler/" + mTable.Name + ".ashx?RequestMethod=Delete&ID=' + event.data.RowData.ID";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", success: function (pData) {";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "if (pData == '1') {";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "layer.alert('删除成功！', { icon: 1 });";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "Init();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "} else {";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "layer.alert('删除失败！', { icon: 2 });";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "});";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "});";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "function btnNew_OnClick() {";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "layer.open({";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "type: 2";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", title: '新增'";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", content: '/Html/" + mTable.Name + "/" + mTable.Name + "New.html'";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", area: ['800px', '600px']";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "});";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "function fileImport_OnChange() {";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "var formData = new FormData();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "formData.append('fileImport', $('#fileImport')[0].files[0]);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "$.ajax({";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "type: 'post'";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", url: '/Handler/" + mTable.Name + ".ashx?RequestMethod=Import'";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", data: formData";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", contentType: false";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", cache: false";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", processData: false";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", success: function(pData) {";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "if (pData === '1')";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "layer.alert('导入成功！', { icon: 1 });";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "Init();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "else";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "layer.alert('导入失败！', { icon: 1 });";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "Init();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "});";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "function btnImport_OnClick() {";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "$('#fileImport').click();";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "function btnExport_OnClick(event) {";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "$.ajax({";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "async: false";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", type: 'GET'";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", url: '/Handler/" + mTable.Name + ".ashx?RequestMethod=Export'";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = ", success: function(pData) {";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "var lData = JSON.parse(pData);";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "if (lData.Status === '1')";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "window.location.href = lData.Msg";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "else";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "{";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "layer.alert('导出失败', { icon: 2 });";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "});";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "}";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = " </script>";
            FileHelper.AppendLine(lFileStream, lLine);

            lLine = "</head> ";
            FileHelper.AppendLine(lFileStream, lLine);
        }
    }
}
