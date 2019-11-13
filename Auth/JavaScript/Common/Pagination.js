﻿function PaginationConfiguration() {
    this.RequestUrl = "Url for Request Data";
    this.PageContainerID = "Div ID for Display Record List";
    this.Columns = [{
        HeaderText: "Header Text"
        , DataSource: [{
            Name: "DataSource Name"
        }]
    }];
}

function Pagination(pListConfig) {
    this.RequestUrl = pListConfig.RequestUrl;
    this.PageContainerID = pListConfig.PageContainerID;
    this.Columns = pListConfig.Columns;
    this.CurrentPageIndex = 0;
    this.RowCnt = 0;
    this.PageCnt = 0;
    this.TableObj = null;
    this.THeadObj = null;
    this.TBodyObj = null;
    this.PageNavContainer = null;

    this.TableObj = $("<table>");
    this.TableObj.prop("class", "table table-striped table-bordered");
    this.THeadObj = $("<thead>");
    var lTr = $("<tr>");
    for (var i = 0; i < this.Columns.length; i++) {
        var lTh = $("<th>");
        lTh.text(this.Columns[i].HeaderText);
        lTr.append(lTh);
    }
    this.THeadObj.append(lTr);
    this.TableObj.append(this.THeadObj);
    this.TBodyObj = $("<tbody>");
    this.TableObj.append(this.TBodyObj);

    $("#" + this.PageContainerID).append(this.TableObj);
    this.PageNavContainer = $("<div>");
    $("#" + this.PageContainerID).append(this.PageNavContainer);
}

Pagination.prototype.PageSize = 10;

Pagination.prototype.RequestData = function (RequestPageIndex) {
    var lBeginIndex = (RequestPageIndex - 1) * this.PageSize + 1;
    var lEndIndex = RequestPageIndex * this.PageSize;
    this.CurrentPageIndex = RequestPageIndex;
    var that = this;

    $.ajax({
        type: "post"
        , url: this.RequestUrl
        , data: { BeginIndex: lBeginIndex, EndIndex: lEndIndex }
        , success: function (pData) {
            $("#" + that.PageContainerID + " tbody").empty();
            var lData = JSON.parse(pData);
            for (var i = 0; i < that.PageSize; i++) {
                var lTr = $("<tr>");
                for (var j = 0; j < that.Columns.length; j++) {
                    var lTd = $("<td>");
                    for (var k = 0; k < that.Columns[j].DataSource.length; k++) {
                        if (i < lData.DataList.length) {
                            that.RowCnt = lData.DataList[i].RowCnt;
                            switch (that.Columns[j].DataSource[k].Style) {
                                case "TXT":
                                    var lLbl = $("<label>");
                                    lLbl.text(lData.DataList[i][that.Columns[j].DataSource[k].Name]);
                                    lTd.append(lLbl);
                                    break;

                                case "BTN":
                                    var lBtn = $("<input type='button'>");
                                    lBtn.prop("class", that.Columns[j].DataSource[k].CssClass);
                                    lBtn.prop("style", "margin-left:5px;");
                                    lBtn.prop("value", that.Columns[j].DataSource[k].Name);
                                    lBtn.on("click", { RowData: lData.DataList[i] }, that.Columns[j].DataSource[k].OnClickCallBack);
                                    lTd.append(lBtn);
                                    break;

                            }
                        } else {
                            lTd.append("<label>&nbsp;</label>");
                        }
                    }
                    lTr.append(lTd)
                }
                that.TBodyObj.append(lTr);
            }
            that.CreatePageNav();
        }
    });
}

Pagination.prototype.CreatePageNav = function () {

    this.PageNavContainer.empty();

    var lPageNav = null;
    this.PageCnt = Math.ceil(this.RowCnt / this.PageSize);

    //首页
    lPageNav = this.GenPageNav();
    lPageNav.append('<span class="glyphicon glyphicon-fast-backward" aria-hidden="true"></span>');
    if (this.CurrentPageIndex == 1) {
        lPageNav.prop("disabled", true);
    }
    lPageNav.on("click", { PaginationObj: this, PageIndex: 1 }, function (event) { event.data.PaginationObj.RequestData(event.data.PageIndex); });
    this.PageNavContainer.append(lPageNav);

    //上一页
    lPageNav = this.GenPageNav();
    lPageNav.append('<span class="glyphicon glyphicon-step-backward" aria-hidden="true"></span>');
    if (this.CurrentPageIndex == 1) {
        lPageNav.prop("disabled", true);
    }
    lPageNav.on("click", { PaginationObj: this, PageIndex: this.CurrentPageIndex - 1 }, function (event) { event.data.PaginationObj.RequestData(event.data.PageIndex); });
    this.PageNavContainer.append(lPageNav);

    //前省略号
    if (this.CurrentPageIndex - 3 > 1) {
        lPageNav = this.GenPageNav();
        lPageNav.text('...');
        this.PageNavContainer.append(lPageNav);
    }

    //当前页前3页
    if (this.CurrentPageIndex - 3 >= 1) {
        lPageNav = this.GenPageNav();
        lPageNav.text(this.CurrentPageIndex - 3);
        lPageNav.on("click", { PaginationObj: this, PageIndex: this.CurrentPageIndex - 3 }, function (event) { event.data.PaginationObj.RequestData(event.data.PageIndex); });
        this.PageNavContainer.append(lPageNav);
    }

    //当前页前2页
    if (this.CurrentPageIndex - 2 >= 1) {
        lPageNav = this.GenPageNav();
        lPageNav.text(this.CurrentPageIndex - 2);
        lPageNav.on("click", { PaginationObj: this, PageIndex: this.CurrentPageIndex - 2 }, function (event) { event.data.PaginationObj.RequestData(event.data.PageIndex); });
        this.PageNavContainer.append(lPageNav);
    }

    //当前页前1页
    if (this.CurrentPageIndex - 1 >= 1) {
        lPageNav = this.GenPageNav();
        lPageNav.text(this.CurrentPageIndex - 1);
        lPageNav.on("click", { PaginationObj: this, PageIndex: this.CurrentPageIndex - 1 }, function (event) { event.data.PaginationObj.RequestData(event.data.PageIndex); });
        this.PageNavContainer.append(lPageNav);
    }

    //当前页
    lPageNav = this.GenPageNav();
    lPageNav.text(this.CurrentPageIndex);
    lPageNav.addClass("btn-primary");
    this.PageNavContainer.append(lPageNav);

    //当前页后1页
    if (this.CurrentPageIndex + 1 <= this.PageCnt) {
        lPageNav = this.GenPageNav();
        lPageNav.text(this.CurrentPageIndex + 1);
        lPageNav.on("click", { PaginationObj: this, PageIndex: this.CurrentPageIndex + 1 }, function (event) { event.data.PaginationObj.RequestData(event.data.PageIndex); });
        this.PageNavContainer.append(lPageNav);
    }

    //当前页后2页
    if (this.CurrentPageIndex + 2 <= this.PageCnt) {
        lPageNav = this.GenPageNav();
        lPageNav.text(this.CurrentPageIndex + 2);
        lPageNav.on("click", { PaginationObj: this, PageIndex: this.CurrentPageIndex + 2 }, function (event) { event.data.PaginationObj.RequestData(event.data.PageIndex); });
        this.PageNavContainer.append(lPageNav);
    }

    //当前页后3页
    if (this.CurrentPageIndex + 3 <= this.PageCnt) {
        lPageNav = this.GenPageNav();
        lPageNav.text(this.CurrentPageIndex + 3);
        lPageNav.on("click", { PaginationObj: this, PageIndex: this.CurrentPageIndex + 3 }, function (event) { event.data.PaginationObj.RequestData(event.data.PageIndex); });
        this.PageNavContainer.append(lPageNav);
    }

    //后省略号
    if (this.CurrentPageIndex + 3 < this.PageCnt) {
        lPageNav = this.GenPageNav();
        lPageNav.text('...');
        this.PageNavContainer.append(lPageNav);
    }

    //下一页
    lPageNav = this.GenPageNav();
    lPageNav.append('<span class="glyphicon glyphicon-step-forward" aria-hidden="true"></span>');
    if (this.CurrentPageIndex == this.PageCnt) {
        lPageNav.prop("disabled", true);
    }
    lPageNav.on("click", { PaginationObj: this, PageIndex: this.CurrentPageIndex + 1 }, function (event) { event.data.PaginationObj.RequestData(event.data.PageIndex); });
    this.PageNavContainer.append(lPageNav);

    //末页
    lPageNav = this.GenPageNav();
    lPageNav.append('<span class="glyphicon glyphicon-fast-forward" aria-hidden="true"></span>');
    if (this.CurrentPageIndex == this.PageCnt) {
        lPageNav.prop("disabled", true);
    }
    lPageNav.on("click", { PaginationObj: this, PageIndex: this.PageCnt }, function (event) { event.data.PaginationObj.RequestData(event.data.PageIndex); });
    this.PageNavContainer.append(lPageNav);

    //展示记录总条数及总页数
    var lSummary = $("<span>");
    lSummary.text("共 " + this.RowCnt + " 条记录 / " + this.PageCnt + " 页");
    this.PageNavContainer.append(lSummary);

    //跳页
    var lJumpPageLabel = $("<label for='txtJumpTo' style='margin-left:10px;'>");
    lJumpPageLabel.text("跳至");
    this.PageNavContainer.append(lJumpPageLabel);
    var lJumpPageTxt = $("<input type='text' id='txtJumpTo' style='width:60px;'>")
    lJumpPageTxt.on('keydown'
        , { PaginationObj: this }
        , function (event) {
            if (event.keyCode == "13") {
                var lJumpTo = $(this).val();
                if (!isNaN(lJumpTo) && lJumpTo <= event.data.PaginationObj.PageCnt) {
                    event.data.PaginationObj.RequestData(lJumpTo);
                }
            }
        });
    this.PageNavContainer.append(lJumpPageTxt);
    this.PageNavContainer.append("<label for='txtJumpTo'>页</label> ");
}

Pagination.prototype.GenPageNav = function () {
    var lPageNav = null;
    lPageNav = $("<button>");
    lPageNav.addClass("btn btn-sm");
    lPageNav.prop("style", "margin-right:5px;border:1px solid grey;");
    return lPageNav;
}
