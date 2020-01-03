//标签页面管理类
function CTabPageController() {
    this.TabContainer = null;
    this.PageContainer = null;
    this.TabPages = new Array();
}

//展示指定标签页
CTabPageController.prototype.Show = function (paraId, paraName, paraUrl) {
    var isNew = true;

    for (var i = 0; i < this.TabPages.length; i++) {
        if (this.TabPages[i].Id == paraId) {
            isNew = false;
            this.TabContainer.find('div[id=' + paraId + '] span').css('background-color', 'blue');
        }
        else {
            this.TabContainer.find('div[id=' + this.Tabs[i].TabId + '] span').css('background-color', 'grey');
        }
    }

    if (isNew) {
        var lTabPage = new CTabPage();
        lTabPage.Id = paraId;
        lTabPage.Addr = paraUrl;
        lTabPage.Name = paraName;
        lTabPage.Controller = this;
        this.TabPages.push(lTab);
    }
}

//标签类
function CTabPage() {
    this.TabId = "";
    this.TabUrl = "";
    this.TabName = "";
    this.TabController = null;
}

//创建标签
CTabPage.prototype.Show = function () {
    var tabDiv = $('<div style="float:left;padding-right:20px;">');
    var tabNameSpan = $('<span>');
    var tabCloseIcon = $('<img src="/JSLIB/TabPageController/theme/default/close.png">');

    tabDiv.attr('id', this.TabId);

    tabNameSpan.text(this.TabName);
    tabNameSpan.css('background-color', 'blue');
    tabNameSpan.css('cursor', 'pointer');
    tabNameSpan.on('click', { TabObj: this }, this.NameSpan_OnClick);

    tabCloseIcon.on('click', { TabObj: this }, this.CloseIcon_OnClick);

    tabDiv.append(tabNameSpan);
    tabDiv.append(tabCloseIcon);

    tabDiv;

}

//标签关闭事件
CTabPage.prototype.CloseIcon_OnClick = function (event) {
    var lTabObj = event.data.TabObj;
    var lTabController = lTabObj.TabController;

    lTabController.TabContainer.find('div[id=' + lTabObj.TabId + ']').remove();
    for (var i = 0; i < lTabController.Tabs.length; i++) {
        if (lTabController.Tabs[i].TabId == lTabObj.TabId) {
            lTabController.Tabs.splice(i, 1);
        }
    }
}

//标签单击事件
CTabPage.prototype.NameSpan_OnClick = function (event) {
    var lTabObj = event.data.TabObj;
    var lTabController = lTabObj.TabController;

    for (var i = 0; i < lTabController.Tabs.length; i++) {
        if (lTabController.Tabs[i].TabId == lTabObj.TabId) {
            lTabController.TabContainer.find('div[id=' + lTabController.Tabs[i].TabId + '] span').css('background-color', 'blue');
        }
        else {
            lTabController.TabContainer.find('div[id=' + lTabController.Tabs[i].TabId + '] span').css('background-color', 'grey');
        }
    }
}