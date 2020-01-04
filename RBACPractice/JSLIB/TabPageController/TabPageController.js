//标签页面管理类
function CTabPageController() {
    //标签容器
    this.TabContainer = null;
    //页面容器
    this.PageContainer = null;
    //标签页数组
    this.TabPages = new Array();
    //当前标签页下标
    this.CurrentTagePageIndex = -1;
}

//创建Tab栏前后滑动导航按钮
CTabPageController.prototype.CreateNav = function () {
    var lBackwordDiv = $('<div>');
    lBackwordDiv.css('border-right', '1px solid blue');
    lBackwordDiv.css('float', 'left');
    lBackwordDiv.css('cursor', 'pointer')
    lBackwordDiv.css('width', '30px');
    lBackwordDiv.css('text-align', 'center');
    lBackwordDiv.on('click', { TabPageController: this }, BackwordDiv_OnClick);

    var lBackwordImg = $('<img>');
    lBackwordImg.attr('src', '/JSLIB/TabPageController/theme/default/backword.png');
    lBackwordImg.css('width', '16px');
    lBackwordImg.css('height', '16px');
    lBackwordDiv.append(lBackwordImg);
    this.TabContainer.append(lBackwordDiv);

    var lForwordDiv = $('<div>');
    lForwordDiv.css('border-left', '1px solid green');
    lForwordDiv.css('float', 'right');
    lForwordDiv.css('cursor', 'pointer')
    lForwordDiv.css('width', '30px');
    lForwordDiv.css('text-align', 'center');
    lForwordDiv.on('click', { TabPageController: this }, ForwordDiv_OnClick);

    var lForwordImg = $('<img>');
    lForwordImg.attr('src', '/JSLIB/TabPageController/theme/default/forword.png');
    lForwordImg.css('width', '16px');
    lForwordImg.css('height', '16px');
    lForwordDiv.append(lForwordImg);

    this.TabContainer.append(lForwordDiv);
}

//标签栏向后滑动单击事件
function BackwordDiv_OnClick(event) {
    var lTabPageController = event.data.TabPageController;
    var lLastHiddenIndexBegin = -1;
    var lFirstVisibleIndexEnd = -1;
    var lId = '';
    for (var i = 0; i <= lTabPageController.TabPages.length - 1; i++) {
        lId = lTabPageController.TabPages[i].Id;
        if (lTabPageController.TabContainer.find('div[id=' + lId + ']').css('display') == 'none') {
            lLastHiddenIndexBegin = i;
        } else {
            break;
        }
    }

    if (lLastHiddenIndexBegin != -1) {
        lId = lTabPageController.TabPages[lLastHiddenIndexBegin].Id;
        lTabPageController.TabContainer.find('div[id=' + lId + ']').css('display', 'block');
    } else {
        return;
    }

    for (var i = lLastHiddenIndexBegin; i < lTabPageController.TabPages.length; i++) {
        lId = lTabPageController.TabPages[i].Id;
        if (lTabPageController.TabContainer.find('div[id=' + lId + ']').css('display') == 'block') {
            lFirstVisibleIndexEnd = i;
        } else {
            break;
        }
    }

    if (lFirstVisibleIndexEnd != -1) {
        lId = lTabPageController.TabPages[lFirstVisibleIndexEnd].Id;
        lTabPageController.TabContainer.find('div[id=' + lId + ']').css('display', 'none');
    }
}

//标签栏向前滑动单击事件
function ForwordDiv_OnClick(event) {
    var lTabPageController = event.data.TabPageController;
    var lFirstHiddenIndexEnd = -1;
    var lLastVisibleIndexBegin = -1;
    var lId = '';
    for (var i = lTabPageController.TabPages.length - 1; i >= 0; i--) {
        lId = lTabPageController.TabPages[i].Id;
        if (lTabPageController.TabContainer.find('div[id=' + lId + ']').css('display') == 'none') {
            lFirstHiddenIndexEnd = i;
        } else {
            break;
        }
    }

    if (lFirstHiddenIndexEnd != -1) {
        lId = lTabPageController.TabPages[lFirstHiddenIndexEnd].Id;
        lTabPageController.TabContainer.find('div[id=' + lId + ']').css('display', 'block');
    } else {
        return;
    }

    for (var i = lFirstHiddenIndexEnd; i >= 0; i--) {
        lId = lTabPageController.TabPages[i].Id;
        if (lTabPageController.TabContainer.find('div[id=' + lId + ']').css('display') == 'block') {
            lLastVisibleIndexBegin = i;
        } else {
            break;
        }
    }

    if (lLastVisibleIndexBegin != -1) {
        lId = lTabPageController.TabPages[lLastVisibleIndexBegin].Id;
        lTabPageController.TabContainer.find('div[id=' + lId + ']').css('display', 'none');
    }

}

//展示指定标签页
CTabPageController.prototype.Show = function (paraId, paraName, paraUrl) {
    var isNew = true;
    var lTotalWidth = this.TabContainer.css('width').replace('px', '');
    var lOccupiedWidth = 60;

    for (var i = 0; i < this.TabPages.length; i++) {
        if (this.TabPages[i].Id == paraId) {
            isNew = false;
            this.TabContainer.find('div[id=' + paraId + '] div').css('background-color', 'blue');
            this.PageContainer.find('iframe[id=' + paraId + ']').css('display', 'block');
            if (this.TabContainer.find('div[id=' + paraId + ']').css('display') == 'none') {

                for (var j = i; j < this.TabPages.length; j++) {
                    lOccupiedWidth += parseInt(this.TabContainer.find('div[id=' + this.TabPages[j].Id + ']').css('width').replace('px', ''));
                    if (lOccupiedWidth < lTotalWidth) {
                        this.TabContainer.find('div[id=' + this.TabPages[j].Id + ']').css('display', 'block');
                    } else {
                        this.TabContainer.find('div[id=' + this.TabPages[j].Id + ']').css('display', 'none');
                    }
                }

                for (var j = i - 1; j >= 0; j--) {
                    lOccupiedWidth += parseInt(this.TabContainer.find('div[id=' + this.TabPages[j].Id + ']').css('width').replace('px', ''));
                    if (lOccupiedWidth < lTotalWidth) {
                        this.TabContainer.find('div[id=' + this.TabPages[j].Id + ']').css('display', 'block');
                    } else {
                        this.TabContainer.find('div[id=' + this.TabPages[j].Id + ']').css('display', 'none');
                    }
                }
            }
        }
        else {
            this.TabContainer.find('div[id=' + this.TabPages[i].Id + '] div').css('background-color', 'grey');
            this.PageContainer.find('iframe[id=' + this.TabPages[i].Id + ']').css('display', 'none');
        }
    }

    if (isNew) {
        var lTabPage = new CTabPage();
        lTabPage.Id = paraId;
        lTabPage.Addr = paraUrl;
        lTabPage.Name = paraName;
        lTabPage.TabPageController = this;
        lTabPage.Show();
        this.TabPages.push(lTabPage);
        this.CurrentTagePageIndex = this.TabPages.length - 1;

        var i = this.CurrentTagePageIndex;
        for (; i >= 0; i--) {
            var lId = this.TabPages[i].Id;
            if (this.TabContainer.find('div[id=' + lId + ']').css('display') == 'block') {
                lOccupiedWidth += parseInt(this.TabContainer.find('div[id=' + lId + ']').css('width').replace('px', ''));
            }
            if (lOccupiedWidth > lTotalWidth) {
                break;
            }
        }

        for (var j = i; j >= 0; j--) {
            var lId = this.TabPages[j].Id;
            if (this.TabContainer.find('div[id=' + lId + ']').css('display') == 'block') {
                this.TabContainer.find('div[id=' + lId + ']').css('display', 'none');
            } else {
                break;
            }
        }
    }
}

//标签类
function CTabPage() {
    this.Id = "";
    this.Addr = "";
    this.Name = "";
    this.TabPageController = null;
}

//创建标签
CTabPage.prototype.Show = function () {

    //创建标签HTML
    var tabDiv = $('<div style="float:left;">');
    var tabNameDiv = $('<div style="float:left;width:120px;">');
    var tabCloseIcon = $('<img src="/JSLIB/TabPageController/theme/default/close.png">');

    tabDiv.attr('id', this.Id);

    tabNameDiv.text(this.Name);
    tabNameDiv.css('background-color', 'blue');
    tabNameDiv.css('cursor', 'pointer');
    tabNameDiv.on('click', { TabObj: this }, this.NameDiv_OnClick);
    tabCloseIcon.on('click', { TabObj: this }, this.CloseIcon_OnClick);

    tabDiv.append(tabNameDiv);
    tabDiv.append(tabCloseIcon);

    //将标签HTML加入标签容器
    this.TabPageController.TabContainer.append(tabDiv);

    //创建页面HTML
    var lFrame = $('<iframe>');
    lFrame.attr('id', this.Id);
    lFrame.attr('src', this.Addr);
    lFrame.attr('frameborder', 'no');
    lFrame.css('width', '800px');
    lFrame.css('height', '600px');
    lFrame.css('background-color', '#654321');

    //将页面HTML加入标签容器
    this.TabPageController.PageContainer.append(lFrame);
}

//标签关闭事件
CTabPage.prototype.CloseIcon_OnClick = function (event) {
    var lTabObj = event.data.TabObj;
    var lTabPageController = lTabObj.TabPageController;

    lTabPageController.TabContainer.find('div[id=' + lTabObj.Id + ']').remove();
    lTabPageController.PageContainer.find('iframe[id=' + lTabObj.Id + ']').remove();

    for (var i = 0; i < lTabPageController.TabPages.length; i++) {
        if (lTabPageController.TabPages[i].Id == lTabObj.Id) {
            lTabPageController.TabPages.splice(i, 1);
            if (lTabPageController.CurrentTagePageIndex == i) {
                if (i < lTabPageController.TabPages.length) {
                    //设置后一页为当前页
                    lTabPageController.TabPages[i].NameDiv_OnClick({ data: { TabObj: lTabPageController.TabPages[i] } });
                } else if (i - 1 >= 0 && i - 1 < lTabPageController.TabPages.length) {
                    //设置前一页为当前页
                    lTabPageController.TabPages[i - 1].NameDiv_OnClick({ data: { TabObj: lTabPageController.TabPages[i - 1] } });
                    lTabPageController.CurrentTagePageIndex = i - 1;
                } else {
                    lTabPageController.CurrentTagePageIndex = -1;
                }
            }
        }
    }
}

//标签单击事件
CTabPage.prototype.NameDiv_OnClick = function (event) {
    var lTabObj = event.data.TabObj;
    var lTabPageController = lTabObj.TabPageController;

    for (var i = 0; i < lTabPageController.TabPages.length; i++) {
        if (lTabPageController.TabPages[i].Id == lTabObj.Id) {
            lTabPageController.TabContainer.find('div[id=' + lTabPageController.TabPages[i].Id + '] div').css('background-color', 'blue');
            lTabPageController.PageContainer.find('iframe[id=' + lTabPageController.TabPages[i].Id + ']').css('display', 'block');
            lTabPageController.CurrentTagePageIndex = i;
        }
        else {
            lTabPageController.TabContainer.find('div[id=' + lTabPageController.TabPages[i].Id + '] div').css('background-color', 'grey');
            lTabPageController.PageContainer.find('iframe[id=' + lTabPageController.TabPages[i].Id + ']').css('display', 'none');
        }
    }
}