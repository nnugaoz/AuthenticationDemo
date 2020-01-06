function DropDownList() {
    this.container = null;
    this.dataSource = null;
    this.dispMem = "";
    this.valMen = "";
    this.id = "";
}

DropDownList.prototype.Init = function () {
    if (this.container == null
        || this.dataSource == null
        || this.dispMem == ""
        || this.valMem == ""
        || this.id == "") {
        return;
    }

    var lSelect = $('<select>');
    lSelect.attr('id', this.id);
    lSelect.addClass('form-control');

    var lOption = $('<option>');
    lOption.attr('value', '');
    lOption.text('');
    lSelect.append(lOption);

    for (var i = 0; i < this.dataSource.length; i++) {
        lOption = $('<option>');
        lOption.attr('value', this.dataSource[i][this.valMen]);
        lOption.text(this.dataSource[i][this.dispMem]);
        lSelect.append(lOption);
    }

    this.container.append(lSelect);
}