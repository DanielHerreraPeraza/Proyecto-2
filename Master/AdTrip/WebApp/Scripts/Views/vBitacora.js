
function vBitacora() {

    this.tblBitacora = 'tblBitacora';
    this.service = 'bitacora';
    this.ctrlActions = new ControlActions();

    this.RetrieveAll = function () {
        this.ctrlActions.FillTable(this.service, this.tblBitacora, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblBitacora, true);
    }

    this.BindFields = function (data) {

    }

}

$(document).ready(function () {

    var vbitac = new vBitacora();
    vbitac.RetrieveAll();

});
