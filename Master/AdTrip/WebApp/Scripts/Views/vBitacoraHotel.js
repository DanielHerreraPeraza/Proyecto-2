function vBitacoraHotel() {

    this.tblBitacora = 'tblBitacoraHotel';
    this.service = 'bitacora';
    this.ctrlActions = new ControlActions();

    this.RetrieveAll = function () {
        var id = document.getElementById("txtIdHotel").value;
        this.ctrlActions.FillTable(this.service +"/bitacoraHotel/"+id , this.tblBitacora, false);
    }

    this.ReloadTable = function () {
        this.ctrlActions.FillTable(this.service, this.tblBitacora, true);
    }

    this.BindFields = function (data) {

    }
    this.volverPerfil = function () {
        var id = document.getElementById("txtIdHotel").value;
        window.location.href = '/Home/vPerfilHotel/' + id;
    };
}
$(document).ready(function () {
    var vbitacora = new vBitacoraHotel();

    var element = document.getElementById("Hotel");
    element.classList.remove("d-none");
    var elementC = document.getElementById("HotelC");
    elementC.classList.remove("d-none");
});