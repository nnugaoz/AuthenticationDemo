//标签页类
function CTabPage() {
    //唯一标识，用户唯一标识标签和页面
    //标签div的id='tab'+id
    //页面div的id='page'+id
    this.Id = "";

    //页面地址
    this.Addr = "";

    //标签名称
    this.Name = "";

    //标签页面管理类实例
    this.TabPageController = null;
}

//标签页类方法 — 创建标签及页面HTML
CTabPage.prototype.CreateTabPageHtml = function () {

    //创建标签HTML
    var tabDiv = $('<div style="float:left;border-right:1px solid #bbbbbb;border-top-left-radius:5px;border-top-right-radius:5px" >');
    var tabNameDiv = $('<div style="float:left;">');
    var tabCloseIcon = $('<img src="/JSLIB/TabPageController/theme/default/close.png">');

    //设置标签HTML相关属性及事件处理函数

    var tabNameDivWidth = parseInt((this.TabPageController.TabContainer.css('width').replace('px', '') - 60) / 7 - 40) + 'px';
    tabNameDiv.css('width', tabNameDivWidth);

    tabDiv.attr('id', 'tab' + this.Id);
    tabDiv.css('padding', '2px 10px');
    tabDiv.css('cursor', 'pointer');

    tabNameDiv.text(this.Name);

    tabNameDiv.on('click', { TabObj: this }, this.NameDiv_OnClick);
    tabCloseIcon.on('click', { TabObj: this }, this.CloseIcon_OnClick);
    tabCloseIcon.on('mouseover', function () { $(this).attr('src', '/JSLIB/TabPageController/theme/default/closeMouseover.png'); });
    tabCloseIcon.on('mouseout', function () { $(this).attr('src', '/JSLIB/TabPageController/theme/default/close.png'); });

    //设置标签内HTML关联关系
    tabDiv.append(tabNameDiv);
    tabDiv.append(tabCloseIcon);

    //创建页面HTML
    var iframeDiv = $('<div>');
    var lFrame = $('<iframe>');

    //设置页面HTML相关属性
    iframeDiv.attr('id', 'page' + this.Id);
    lFrame.attr('src', this.Addr);
    lFrame.attr('frameborder', 'no');
    lFrame.css('width', '100%');
    lFrame.css('height', '100%');

    //设置标签内HTML关联关系
    iframeDiv.append(lFrame);

    //新建的标签和页面，默认不展示
    tabDiv.css('display', 'none');
    iframeDiv.css('display', 'none');

    //返回标签jquery对象及页面jquery对象
    return { TabHtml: tabDiv, PageHtml: iframeDiv }
}

//标签关闭事件
CTabPage.prototype.CloseIcon_OnClick = function (event) {
    var lTabObj = event.data.TabObj;
    var lTabPageController = lTabObj.TabPageController;
    lTabPageController.RemoveTabPageById(lTabObj.Id);
}

//标签单击事件
CTabPage.prototype.NameDiv_OnClick = function (event) {
    var lTabObj = event.data.TabObj;
    var lTabPageController = lTabObj.TabPageController;

    lTabPageController.SetCurrentTabPageById(lTabObj.Id);
}

//标签页管理类
function CTabPageController() {
    //标签容器，jquery对象
    this.TabContainer = null;

    //页面容器，jquery对象
    this.PageContainer = null;

    //标签页类实例数组
    this.TabPages = new Array();

    //当前标签页下标
    this.CurrentTagePageIndex = -1;
}

//创建Tab栏前后滑动导航按钮
CTabPageController.prototype.CreateNav = function () {

    this.TabContainer.css('border', '1px solid #dddddd');

    var lBackwordDiv = $('<div>');
    lBackwordDiv.css('border-right', '1px solid #dddddd');
    lBackwordDiv.css('float', 'left');
    lBackwordDiv.css('cursor', 'pointer')
    lBackwordDiv.css('width', '30px');
    lBackwordDiv.css('text-align', 'center');
    lBackwordDiv.on('click', { TabPageController: this }, BackwordDiv_OnClick);

    var lBackwordImg = $('<img>');
    lBackwordImg.attr('src', '/JSLIB/TabPageController/theme/default/backword.png');
    lBackwordImg.css('width', '20px');
    lBackwordImg.css('height', '20px');
    lBackwordDiv.append(lBackwordImg);
    this.TabContainer.append(lBackwordDiv);

    var lForwordDiv = $('<div>');
    lForwordDiv.css('border-left', '1px solid #dddddd');
    lForwordDiv.css('float', 'right');
    lForwordDiv.css('cursor', 'pointer')
    lForwordDiv.css('width', '30px');
    lForwordDiv.css('text-align', 'center');
    lForwordDiv.on('click', { TabPageController: this }, ForwordDiv_OnClick);

    var lForwordImg = $('<img>');
    lForwordImg.attr('src', '/JSLIB/TabPageController/theme/default/forword.png');
    lForwordImg.css('width', '20px');
    lForwordImg.css('height', '20px');
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
        if (lTabPageController.TabContainer.find('div[id=tab' + lId + ']').css('display') == 'none') {
            lLastHiddenIndexBegin = i;
        } else {
            break;
        }
    }

    if (lLastHiddenIndexBegin != -1) {
        lId = lTabPageController.TabPages[lLastHiddenIndexBegin].Id;
        lTabPageController.TabContainer.find('div[id=tab' + lId + ']').css('display', 'block');
    } else {
        return;
    }

    for (var i = lLastHiddenIndexBegin; i < lTabPageController.TabPages.length; i++) {
        lId = lTabPageController.TabPages[i].Id;
        if (lTabPageController.TabContainer.find('div[id=tab' + lId + ']').css('display') == 'block') {
            lFirstVisibleIndexEnd = i;
        } else {
            break;
        }
    }

    if (lFirstVisibleIndexEnd != -1) {
        lId = lTabPageController.TabPages[lFirstVisibleIndexEnd].Id;
        lTabPageController.TabContainer.find('div[id=tab' + lId + ']').css('display', 'none');
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
        if (lTabPageController.TabContainer.find('div[id=tab' + lId + ']').css('display') == 'none') {
            lFirstHiddenIndexEnd = i;
        } else {
            break;
        }
    }

    if (lFirstHiddenIndexEnd != -1) {
        lId = lTabPageController.TabPages[lFirstHiddenIndexEnd].Id;
        lTabPageController.TabContainer.find('div[id=tab' + lId + ']').css('display', 'block');
    } else {
        return;
    }

    for (var i = lFirstHiddenIndexEnd; i >= 0; i--) {
        lId = lTabPageController.TabPages[i].Id;
        if (lTabPageController.TabContainer.find('div[id=tab' + lId + ']').css('display') == 'block') {
            lLastVisibleIndexBegin = i;
        } else {
            break;
        }
    }

    if (lLastVisibleIndexBegin != -1) {
        lId = lTabPageController.TabPages[lLastVisibleIndexBegin].Id;
        lTabPageController.TabContainer.find('div[id=tab' + lId + ']').css('display', 'none');
    }

}

//展示指定标签页
CTabPageController.prototype.Show = function (paraId, paraName, paraUrl) {
    var isNew = true;
    for (var i = 0; i < this.TabPages.length; i++) {
        if (this.TabPages[i].Id == paraId) {
            isNew = false;
            break;
        }
    }

    if (isNew) {
        var lTabPage = new CTabPage();
        lTabPage.Id = paraId;
        lTabPage.Addr = paraUrl;
        lTabPage.Name = paraName;
        lTabPage.TabPageController = this;
        var lTabPageHtmlObj = lTabPage.CreateTabPageHtml();
        this.TabPages.push(lTabPage);
        this.TabContainer.append(lTabPageHtmlObj.TabHtml);
        this.PageContainer.append(lTabPageHtmlObj.PageHtml);
    }
    this.SetCurrentTabPageById(paraId);
}

//根据id,设置标签为当前标签，设置页面可见
CTabPageController.prototype.SetCurrentTabPageById = function (id) {
    for (var i = 0; i < this.TabPages.length; i++) {
        if (this.TabPages[i].Id == id) {
            this.CurrentTagePageIndex = i;
            break;
        }
    }

    var lTotalWidth = this.TabContainer.css('width').replace('px', '');
    var lOccupiedWidth = 60;

    //查看标签是否可见
    if (this.TabContainer.find('div[id=tab' + id + ']').css('display') == 'none') {
        //如果标签不可见，则将标签设置为可见
        //并且在标签容器宽度范围内，将可见标签的后继标签设置为可见；在标签容器宽度范围之外的标签设置为不可见。
        for (var i = this.CurrentTagePageIndex; i < this.TabPages.length; i++) {
            lOccupiedWidth += parseInt(this.TabContainer.find('div[id=tab' + this.TabPages[i].Id + ']').css('width').replace('px', '')) + 20;
            if (lOccupiedWidth < lTotalWidth) {
                this.TabContainer.find('div[id=tab' + this.TabPages[i].Id + ']').css('display', 'block');
            } else {
                this.TabContainer.find('div[id=tab' + this.TabPages[i].Id + ']').css('display', 'none');
            }
        }

        //如果在标签容器宽度范围内，将可见标签的后记标签设置为可见，标签容器中仍有空余位置。
        //则仍在标签容器宽度范围内，将可见标签之前的标签设置为可见；在标签容器宽度范围之外的标签设置为不可见。
        for (var i = this.CurrentTagePageIndex - 1; i >= 0; i--) {
            lOccupiedWidth += parseInt(this.TabContainer.find('div[id=tab' + this.TabPages[i].Id + ']').css('width').replace('px', '')) + 20;
            if (lOccupiedWidth < lTotalWidth) {
                this.TabContainer.find('div[id=tab' + this.TabPages[i].Id + ']').css('display', 'block');
            } else {
                this.TabContainer.find('div[id=tab' + this.TabPages[i].Id + ']').css('display', 'none');
            }
        }
    }

    //将当前标签设置为选中标签，将对应的页面设置为可见
    //将其他标签设置为未选中状态，其他页面设置为不可见
    for (var i = 0; i < this.TabPages.length; i++) {
        if (i == this.CurrentTagePageIndex) {
            this.TabContainer.find('div[id=tab' + this.TabPages[i].Id + ']').css('background-color', '#ffffff');
            this.PageContainer.find('div[id=page' + this.TabPages[i].Id + ']').css('display', 'block');
        } else {
            this.TabContainer.find('div[id=tab' + this.TabPages[i].Id + ']').css('background-color', '#dddddd');
            this.PageContainer.find('div[id=page' + this.TabPages[i].Id + ']').css('display', 'none');
        }
    }
}

//根据id,设置标签为当前标签，设置页面可见
CTabPageController.prototype.RemoveTabPageById = function (id) {
    this.TabContainer.find('div[id=tab' + id + ']').remove();
    this.PageContainer.find('div[id=page' + id + ']').remove();

    var i = -1;
    for (i = 0; i < this.TabPages.length; i++) {
        if (this.TabPages[i].Id == id) {
            break;
        }
    }

    if (i != -1) {
        this.TabPages.splice(i, 1);
    }

    if (this.CurrentTagePageIndex == i) {
        if (i < this.TabPages.length) {
            //设置后一页为当前页
            this.SetCurrentTabPageById(this.TabPages[i].Id);
        } else if (i - 1 >= 0 && i - 1 < lTabPageController.TabPages.length) {
            //设置前一页为当前页
            this.SetCurrentTabPageById(this.TabPages[i - 1].Id);
        } else {
            lTabPageController.CurrentTagePageIndex = -1;
        }
    }
}
