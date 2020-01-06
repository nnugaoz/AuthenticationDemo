function CMenu() {
    this.menuItems = new Array();
    this.menuItemsSorted = new Array();
}

function CMenuItem() {
    this.Caption = "";
    this.Url = "";
    this.ID = "";
    this.ParentID = "";
    this.Sort = 0;
    this.Level = 0;
    this.OnClick = null;
}

CMenu.prototype.SortMenuItems = function (pMenuID) {
    var tempArray = new Array();

    for (var i = 0; i < this.menuItems.length; i++) {
        if (this.menuItems[i].ParentID == pMenuID) {
            tempArray.push(this.menuItems[i]);
        }
    }

    tempArray.sort(function (a, b) { return a.Sort - b.Sort });

    for (var i = 0; i < tempArray.length; i++) {
        this.menuItemsSorted.push(tempArray[i]);
        this.SortMenuItems(tempArray[i].ID)
    }
}

CMenu.prototype.GenerateMenuHtml = function (menuContainer) {

    if (this.menuItemsSorted.length == 0 && this.menuItems.length > 0) {
        this.menuItemsSorted = this.menuItems;
    }

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

        lMenuItemContainer.on('click', { 'menuIns': this, 'selectedItem': lMenuItemContainer }, MenuItem_OnClick);

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

function MenuItem_OnClick(e) {
    var lSelectedMenuItem = e.data.selectedItem;
    var lCMenu = e.data.menuIns;
    var lCMenuItem = null;
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

    for (var i = 0; i < lCMenu.menuItemsSorted.length; i++) {
        if (lCMenu.menuItemsSorted[i].ID == lSelectedMenuItem.attr('id')) {
            lCMenuItem = lCMenu.menuItemsSorted[i];
            break;
        }
    }

    if (lCMenuItem != null && lCMenuItem.OnClick != null) {
        lCMenuItem.OnClick({ data: { MenuItem: lCMenuItem } });
    }
}

