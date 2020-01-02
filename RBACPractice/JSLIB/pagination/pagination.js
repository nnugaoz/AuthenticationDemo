/// <reference path="Pagination.js" />
function PaginationColumnConfiguration() {
    this.HeaderText = "";
    this.DataField = "";
    this.CssClass = "";
    this.Type = "";
    this.PWidth = "";
}
PaginationColumnConfiguration.prototype.OnClickEventHandler = function () { }
PaginationColumnConfiguration.prototype.OnColumnDataBinding = function () { }

function PaginationConfiguration() {
    this.RequestUrl = "";
    this.PageContainerID = "";
    this.Columns = new Array();
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
        lTh.css("width", this.Columns[i].PWidth);
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
    this.CurrentPageIndex = parseInt(RequestPageIndex);
    var that = this;
    $.ajax({
        async: false,
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
                    if (i < lData.length) {
                        that.RowCnt = lData[i].RowCnt;
                        switch (that.Columns[j].Type) {
                            case "TXT":
                                var lLbl = $("<label>");
                                lLbl.text(lData[i][that.Columns[j].DataField]);
                                lTd.append(lLbl);
                                break;

                            case "BTN":
                                var lBtn = $("<input type='button'>");
                                lBtn.prop("class", that.Columns[j].CssClass);
                                lBtn.prop("style", "margin-left:5px;");
                                lBtn.prop("value", that.Columns[j].DataField);
                                lBtn.on("click", { RowData: lData[i] }, that.Columns[j].OnClickEventHandler);
                                lTd.append(lBtn);
                                break;

                            case "CHK":
                                var lChk = $("<input type='checkbox' class='checkbox'>");                                
                                lChk.val(lData[i][that.Columns[j].DataField]);
                                lChk.on("change", { RowData: lData[i] }, that.Columns[j].OnClickEventHandler);
                                if (that.Columns[j].OnColumnDataBinding) {
                                    that.Columns[j].OnColumnDataBinding({ data: { ColControl: lChk, RowData: lData[i] } });
                                }
                                lTd.append(lChk);
                                break;
                        }
                    } else {
                        lTd.append("<label>&nbsp;</label>");
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
    lPageNav.on("click", { PaginationObj: this, PageIndex: 1 }, Goto);
    this.PageNavContainer.append(lPageNav);

    //上一页
    lPageNav = this.GenPageNav();
    lPageNav.append('<span class="glyphicon glyphicon-step-backward" aria-hidden="true"></span>');
    if (this.CurrentPageIndex == 1) {
        lPageNav.prop("disabled", true);
    }
    lPageNav.on("click", { PaginationObj: this, PageIndex: this.CurrentPageIndex - 1 }, Goto);
    this.PageNavContainer.append(lPageNav);

    //前省略号
    if (this.CurrentPageIndex - 2 > 1) {
        lPageNav = this.GenPageNav();
        lPageNav.text('...');
        this.PageNavContainer.append(lPageNav);
    }

    ////当前页前3页
    //if (this.CurrentPageIndex - 3 >= 1) {
    //    lPageNav = this.GenPageNav();
    //    lPageNav.text(this.CurrentPageIndex - 3);
    //    lPageNav.on("click", { PaginationObj: this, PageIndex: this.CurrentPageIndex - 3 }, Goto);
    //    this.PageNavContainer.append(lPageNav);
    //}

    //当前页前2页
    if (this.CurrentPageIndex - 2 >= 1) {
        lPageNav = this.GenPageNav();
        lPageNav.text(this.CurrentPageIndex - 2);
        lPageNav.on("click", { PaginationObj: this, PageIndex: this.CurrentPageIndex - 2 }, Goto);
        this.PageNavContainer.append(lPageNav);
    }

    //当前页前1页
    if (this.CurrentPageIndex - 1 >= 1) {
        lPageNav = this.GenPageNav();
        lPageNav.text(this.CurrentPageIndex - 1);
        lPageNav.on("click", { PaginationObj: this, PageIndex: this.CurrentPageIndex - 1 }, Goto);
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
        lPageNav.on("click", { PaginationObj: this, PageIndex: this.CurrentPageIndex + 1 }, Goto);
        this.PageNavContainer.append(lPageNav);
    }

    //当前页后2页
    if (this.CurrentPageIndex + 2 <= this.PageCnt) {
        lPageNav = this.GenPageNav();
        lPageNav.text(this.CurrentPageIndex + 2);
        lPageNav.on("click", { PaginationObj: this, PageIndex: this.CurrentPageIndex + 2 }, Goto);
        this.PageNavContainer.append(lPageNav);
    }

    ////当前页后3页
    //if (this.CurrentPageIndex + 3 <= this.PageCnt) {
    //    lPageNav = this.GenPageNav();
    //    lPageNav.text(this.CurrentPageIndex + 3);
    //    lPageNav.on("click", { PaginationObj: this, PageIndex: this.CurrentPageIndex + 3 }, Goto);
    //    this.PageNavContainer.append(lPageNav);
    //}

    //后省略号
    if (this.CurrentPageIndex + 2 < this.PageCnt) {
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
    lPageNav.on("click", { PaginationObj: this, PageIndex: this.CurrentPageIndex + 1 }, Goto);
    this.PageNavContainer.append(lPageNav);

    //末页
    lPageNav = this.GenPageNav();
    lPageNav.append('<span class="glyphicon glyphicon-fast-forward" aria-hidden="true"></span>');
    if (this.CurrentPageIndex == this.PageCnt) {
        lPageNav.prop("disabled", true);
    }
    lPageNav.on("click", { PaginationObj: this, PageIndex: this.PageCnt }, Goto);
    this.PageNavContainer.append(lPageNav);

    //展示记录总条数及总页数
    var lSummary = $("<span>");
    lSummary.text("共 " + this.RowCnt + " 条记录 / " + this.PageCnt + " 页");
    this.PageNavContainer.append(lSummary);

    //跳页
    var lJumpPageLabel = $("<label for='txtJumpTo' style='margin-left:10px;'>");
    lJumpPageLabel.text("跳至");
    this.PageNavContainer.append(lJumpPageLabel);
    var lJumpPageTxt = $("<input type='text' id='txtJumpTo' style='width:40px;'>")
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

function Goto(event) {
    event.data.PaginationObj.RequestData(event.data.PageIndex);
    return false;
}
