function CTabPageController() {

}

function CTabController() {
    this.TabContainer = null;
    this.Tabs = new Array();
}

CTabController.prototype.Create = function (tabId) {

    for (var i = 0; i < this.Tabs.length; i++) {
        if (this.Tabs[i].TabId == tabId) {
            //设置为可见

            break;
        }
    }
}


function CTab() {
    this.TabId = "";
    this.TabName = "";
}

CTab.prototype.createTab = function () {
    var tabDiv = $('<div>');
    var tabNameSpan = $('<span>');
    var tabCloseIcon = $('<img src="/Image/deleteSelectedItem.png">');

    tabDiv.attr('id', this.TabId);
    tabNameSpan.text(this.TabName);

    tabDiv.append(tabNameSpan);
    tabDiv.append(tabCloseIcon);

}