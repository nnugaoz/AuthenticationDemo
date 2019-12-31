function CMenu() {
    this.menuItems = new Array();
    this.menuItemsSorted = new Array();
}

function CMenuItem() {
    this.Caption = "";
    this.Url = "";
    this.ID = "";
    this.ParentID = "";
    this.Order = 0;
    this.Level = 0;
}

CMenu.prototype.SortMenuItems = function (pMenuID) {
    var tempArray = new Array();

    for (var i = 0; i < this.menuItems.length; i++) {
        if (this.menuItems[i].ParentID == pMenuID) {
            tempArray.push(this.menuItems[i]);
        }
    }

    tempArray.sort(function (a, b) { return a.Order - b.Order });

    for (var i = 0; i < tempArray.length; i++) {
        this.menuItemsSorted.push(tempArray[i]);
        this.SortMenuItems(tempArray[i].ID)
    }
}

CMenu.prototype.GenerateMenuHtml = function (menuContainer) {

    for (var i = 0; i < this.menuItemsSorted.length; i++) {

        var lMenuItemContainer = $('<div>');
        var lMenuItemSpan = $('<span>');

        lMenuItemContainer.attr('id', this.menuItemsSorted[i].ID);
        lMenuItemContainer.addClass('menu');
        if (this.menuItemsSorted[i].Level == 1) {
            lMenuItemContainer.addClass('menu-l1');
        } else if (this.menuItemsSorted[i].Level == 2) {
            lMenuItemContainer.addClass('menu-l2');
        }

        lMenuItemSpan.text(this.menuItemsSorted[i].Caption);

        lMenuItemContainer.append(lMenuItemSpan);

        lMenuItemContainer.on('click', { 'menuIns': this, 'selectedItem': lMenuItemContainer }, this.MenuItem_OnClick);

        menuContainer.append(lMenuItemContainer);
    }
}

CMenu.prototype.CollapsedAll = function () {
    for (var i = 0; i < this.menuItemsSorted.length; i++) {
        if (this.menuItemsSorted[i].ParentID != '') {
            $('div[id=' + this.menuItemsSorted[i].ID + ']').hide('normal');
        }
    }
}

CMenu.prototype.ExpandAll = function () {
    for (var i = 0; i < this.menuItemsSorted.length; i++) {
        if (this.menuItemsSorted[i].ParentID != '') {
            $('div[id=' + this.menuItemsSorted[i].ID + ']').show('normal');
        }
    }
}

CMenu.prototype.Expand = function (ID) {
    for (var i = 0; i < this.menuItemsSorted.length; i++) {
        if (this.menuItemsSorted[i].ParentID == ID) {
            $('div[id=' + this.menuItemsSorted[i].ID + ']').show('normal');
        }
    }
}

CMenu.prototype.Collapsed = function (ID) {
    for (var i = 0; i < this.menuItemsSorted.length; i++) {
        if (this.menuItemsSorted[i].ParentID == ID) {
            $('div[id=' + this.menuItemsSorted[i].ID + ']').hide('normal');
        }
    }
}

CMenu.prototype.MenuItem_OnClick = function (e) {
    var lSelectedMenuItem = e.data.selectedItem;
    var lCMenu = e.data.menuIns;
    var lLeaf = true;

    for (var i = 0; i < lCMenu.menuItemsSorted.length; i++) {
        if (lCMenu.menuItemsSorted[i].ParentID == lSelectedMenuItem.attr('id')) {
            $('div[id=' + lCMenu.menuItemsSorted[i].ID + ']').toggle('normal');
            lLeaf = false;
        }
    }

    if (lLeaf) {
        $('div[class*=menu]').removeClass('selected');
        $(lSelectedMenuItem).addClass('selected');
    }

    var lUrl = '';
    for (var i = 0; i < lCMenu.menuItemsSorted.length; i++) {
        if (lCMenu.menuItemsSorted[i].ID == lSelectedMenuItem.attr('id')) {
            lUrl = lCMenu.menuItemsSorted[i].Url;
        }
    }
    if (lUrl != '') {
        $('#CenterRight').load(lUrl);
    }
}

CMenu.prototype.GenerateDemoData = function () {
    var menuItem = new CMenuItem();
    menuItem.Caption = "A";
    menuItem.Url = "";
    menuItem.ID = "A";
    menuItem.ParentID = "";
    menuItem.Order = 4;
    menuItem.Level = 1;
    this.menuItems.push(menuItem);

    var menuItem = new CMenuItem();
    menuItem.Caption = "A3";
    menuItem.Url = "";
    menuItem.ID = "A3";
    menuItem.ParentID = "A";
    menuItem.Order = 3;
    menuItem.Level = 2;
    this.menuItems.push(menuItem);

    var menuItem = new CMenuItem();
    menuItem.Caption = "A4";
    menuItem.Url = "";
    menuItem.ID = "A4";
    menuItem.ParentID = "A";
    menuItem.Order = 4;
    menuItem.Level = 2;
    this.menuItems.push(menuItem);

    var menuItem = new CMenuItem();
    menuItem.Caption = "B";
    menuItem.Url = "";
    menuItem.ID = "B";
    menuItem.ParentID = "";
    menuItem.Order = 3;
    menuItem.Level = 1;
    this.menuItems.push(menuItem);

    var menuItem = new CMenuItem();
    menuItem.Caption = "B1";
    menuItem.Url = "/Html/Menu/B1.html";
    menuItem.ID = "B1";
    menuItem.ParentID = "B";
    menuItem.Order = 1;
    menuItem.Level = 2;
    this.menuItems.push(menuItem);

    var menuItem = new CMenuItem();
    menuItem.Caption = "B2";
    menuItem.Url = "";
    menuItem.ID = "B2";
    menuItem.ParentID = "B";
    menuItem.Order = 2;
    menuItem.Level = 2;
    this.menuItems.push(menuItem);

    var menuItem = new CMenuItem();
    menuItem.Caption = "C";
    menuItem.Url = "";
    menuItem.ID = "C";
    menuItem.ParentID = "";
    menuItem.Order = 2;
    menuItem.Level = 1;
    this.menuItems.push(menuItem);

    var menuItem = new CMenuItem();
    menuItem.Caption = "C1";
    menuItem.Url = "/Html/Menu/C1.html";
    menuItem.ID = "C1";
    menuItem.ParentID = "C";
    menuItem.Order = 1;
    menuItem.Level = 2;
    this.menuItems.push(menuItem);

    var menuItem = new CMenuItem();
    menuItem.Caption = "C4";
    menuItem.Url = "";
    menuItem.ID = "C4";
    menuItem.ParentID = "C";
    menuItem.Order = 4;
    menuItem.Level = 2;
    this.menuItems.push(menuItem);

    var menuItem = new CMenuItem();
    menuItem.Caption = "D";
    menuItem.Url = "";
    menuItem.ID = "D";
    menuItem.ParentID = "";
    menuItem.Order = 1;
    menuItem.Level = 1;
    this.menuItems.push(menuItem);

    var menuItem = new CMenuItem();
    menuItem.Caption = "D2";
    menuItem.Url = "";
    menuItem.ID = "D2";
    menuItem.ParentID = "D";
    menuItem.Order = 2;
    menuItem.Level = 2;
    this.menuItems.push(menuItem);

    var menuItem = new CMenuItem();
    menuItem.Caption = "D4";
    menuItem.Url = "";
    menuItem.ID = "D4";
    menuItem.ParentID = "D";
    menuItem.Order = 4;
    menuItem.Level = 2;
    this.menuItems.push(menuItem);

    var menuItem = new CMenuItem();
    menuItem.Caption = "D3";
    menuItem.Url = "";
    menuItem.ID = "D3";
    menuItem.ParentID = "D";
    menuItem.Order = 3;
    menuItem.Level = 2;
    this.menuItems.push(menuItem);

    var menuItem = new CMenuItem();
    menuItem.Caption = "D1";
    menuItem.Url = "/Html/Menu/D1.html";
    menuItem.ID = "D1";
    menuItem.ParentID = "D";
    menuItem.Order = 1;
    menuItem.Level = 2;
    this.menuItems.push(menuItem);

    var menuItem = new CMenuItem();
    menuItem.Caption = "C2";
    menuItem.Url = "";
    menuItem.ID = "C2";
    menuItem.ParentID = "C";
    menuItem.Order = 2;
    menuItem.Level = 2;
    this.menuItems.push(menuItem);

    var menuItem = new CMenuItem();
    menuItem.Caption = "C3";
    menuItem.Url = "";
    menuItem.ID = "C3";
    menuItem.ParentID = "C";
    menuItem.Order = 3;
    menuItem.Level = 2;
    this.menuItems.push(menuItem);

    var menuItem = new CMenuItem();
    menuItem.Caption = "B3";
    menuItem.Url = "";
    menuItem.ID = "B3";
    menuItem.ParentID = "B";
    menuItem.Order = 3;
    menuItem.Level = 2;
    this.menuItems.push(menuItem);

    var menuItem = new CMenuItem();
    menuItem.Caption = "B4";
    menuItem.Url = "";
    menuItem.ID = "B4";
    menuItem.ParentID = "B";
    menuItem.Order = 4;
    menuItem.Level = 2;
    this.menuItems.push(menuItem);

    var menuItem = new CMenuItem();
    menuItem.Caption = "A1";
    menuItem.Url = "/Html/Menu/A1.html";
    menuItem.ID = "A1";
    menuItem.ParentID = "A";
    menuItem.Order = 1;
    menuItem.Level = 2;
    this.menuItems.push(menuItem);

    var menuItem = new CMenuItem();
    menuItem.Caption = "A2";
    menuItem.Url = "";
    menuItem.ID = "A2";
    menuItem.ParentID = "A";
    menuItem.Order = 2;
    menuItem.Level = 2;
    this.menuItems.push(menuItem);

}





