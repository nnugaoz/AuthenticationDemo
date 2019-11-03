function Pagination(pListConfig) {
    this.RequestUrl = pListConfig.RequestUrl;
    this.PageContainerID = pListConfig.PageContainerID;
    this.Columns = pListConfig.Columns;

    this.CurrentPageIndex = 0;
    this.TableObj = null;
    this.THeadObj = null;
    this.TBodyObj = null;

    this.TableObj = $("<table>");
    this.TableObj.prop("class", "table");
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
}

Pagination.prototype.PageSize = 10;

Pagination.prototype.RequestData = function (RequestPageIndex) {
    var that = this;
    var lBeginIndex = (RequestPageIndex - 1) * this.PageSize + 1;
    var lEndIndex = RequestPageIndex * this.PageSize;
    $.ajax({
        type: "post"
        , url: this.RequestUrl
        , data: { BeginIndex: lBeginIndex, EndIndex: lEndIndex }
        , success: function (pData) {
            var lData = JSON.parse(pData);
            for (var i = 0; i < that.PageSize; i++) {                
                var lTr = $("<tr>");
                for (var j = 0; j < that.Columns.length; j++) {                    
                    var lTd = $("<td>");
                    for (var k = 0; k < that.Columns[j].DataSource.length; k++) {
                        if (i < lData.DataList.length) {
                            switch (that.Columns[j].DataSource[k].Style) {
                                case "TXT":
                                    var lLbl = $("<label>");
                                    lLbl.text(lData.DataList[i][that.Columns[j].DataSource[k].Name]);
                                    lTd.append(lLbl);
                                    lTd.append("&nbsp;")
                                    break;
                                case "BTN":
                                    var lBtn = $("<input type='button'>");
                                    lBtn.prop("class", that.Columns[j].DataSource[k].CssClass);
                                    lBtn.prop("value", that.Columns[j].DataSource[k].Name);
                                    lBtn.on("click", { RowData: lData.DataList[i] }, that.Columns[j].DataSource[k].OnClickCallBack);
                                    lTd.append(lBtn);
                                    lTd.append("&nbsp;")
                            }
                        }else {

                        }
                    }
                    lTr.append(lTd)
                }
                that.TBodyObj.append(lTr);
            } 
        }
    });

}



