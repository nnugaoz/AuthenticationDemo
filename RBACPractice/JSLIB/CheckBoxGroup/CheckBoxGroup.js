//CheckBoxGroup类
//将一组CheckBox封装展示在一个Group中
//可以指定展示列数
function CheckBoxGroup() {
    this.container = null;
    this.dataSource = null;
    this.colCnt = 0;
    this.displayMem = "";
    this.valueMem = "";
    this.checkboxName = "";
}

CheckBoxGroup.prototype.Init = function () {
    if (this.container == null
        || this.colCnt == 0
        || this.dataSource == null
        || this.displayMem == ""
        || this.valueMen == ""
        || this.checkboxName == "") {
        return;
    }

    var lGroup = $('<div class="container">');
    var lNewRow = null;
    var lColSpan = parseInt(12 / this.colCnt);

    for (var i = 0; i < this.dataSource.length; i++) {

        if (i % this.colCnt == 0) {
            lNewRow = $('<div class="row">');
            lGroup.append(lNewRow);
        }

        var itemContainer = $('<div class="col-sm-' + lColSpan + '" >');
        var checkboxContainer = $('<div class="checkbox" >');
        var label = $('<label>');
        var checkbox = $('<input type="checkbox">');

        checkbox.attr('value', this.dataSource[i][this.valueMem]);
        checkbox.attr('name', this.checkboxName);
        label.append(checkbox);
        label.append(this.dataSource[i][this.displayMem]);
        checkboxContainer.append(label);
        itemContainer.append(checkboxContainer);

        lNewRow.append(itemContainer);

    }
    this.container.append(lGroup);
}

CheckBoxGroup.prototype.SetCheckedByValue = function (vals) {

    var valArr = vals.split(',');

    this.container.find('input[type="checkbox"]').each(function (idx, elem) {
        for (var i = 0; i < valArr.length; i++) {
            if ($(elem).attr('value') == valArr[i]) {
                $(elem).attr('checked', 'checked');
                break;
            }
        }
    });

}
