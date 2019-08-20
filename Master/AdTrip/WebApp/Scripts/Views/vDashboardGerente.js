function vDashboardGerente() {

    this.service = 'reporte';
    this.ctrlActions = new ControlActions();
    this.tblGananciasXDia = 'tblGananciasXDia';
    this.tblCantHabHoteles = 'tblCantHabHoteles';
    this.columns = 'FechaRealizado,MontoGanancia';
  

    this.ReporteGananciasTotales = function () {
        var data = {}
        var total = 0;
        data.IdUsuario = sessionStorage.getItem('idUsuario');
        this.ctrlActions.ReporteGananciasGerente('reporte', '5', sessionStorage.getItem('idUsuario'), function (reporte) {
            for (var i in reporte) {

                if (reporte[i].MontoGanancia == -1) {
                    total = 0
                } else {
                    total = total + reporte[i].MontoGanancia;
                }

            }
            $('#totalGanancias').append(formatter.format(total));

        });


    };

  


    this.ReporteGananciasXDia = function () {
        var data = {}
        
        this.ctrlActions.FillTableId('reporte/6', this.tblGananciasXDia, sessionStorage.getItem('idUsuario'), false);
    };


    this.ReporteCantHabXhotel = function () {
        var data = {}
        data.IdUsuario = sessionStorage.getItem('idUsuario');
        this.ctrlActions.FillTableId('reporte/7', this.tblCantHabHoteles, sessionStorage.getItem('idUsuario') , false);
    };



    this.GetReporteGraficoXDia = function (data) {
        this.ctrlActions.GetById(this.service + '/6/' + sessionStorage.getItem('idUsuario'), data);
    };


    this.GetReporteCantHabitacionHotel = function (data) {
        this.ctrlActions.GetById(this.service + '/7/' + sessionStorage.getItem('idUsuario'), data);
    };


    this.MostrarHoteles = function () {
        this.ctrlActions.GetById('hotel/usuario/' + sessionStorage.getItem('idUsuario'), function (data) {

            $(data).each(function (index, value) {
                $("#ReportesPorHoteles").append('<div  style="display:inline; margin:20px 40px;"><button name="'
                    + value.Nombre + '"  OnClick="IrReporteHotel(this);" style="margin:20px; padding:30px 60px; font-size:20px;" class="btn btn-color boton" value="' + value.CedulaJuridica + '" >' + value.Nombre + '</button></div>');
            });

            

        });
    }


   

}


$(document).ready(function () {
    var vdashboard = new vDashboardGerente();
    vdashboard.ReporteGananciasTotales();
    vdashboard.ReporteGananciasXDia();
    vdashboard.MostrarHoteles();
});



function IrReporteHotel (data) {

    var valor = data.value;
    var nombre = data.name;
    sessionStorage.setItem('idHotelLogeado', valor);
    sessionStorage.setItem('nombreHotel', nombre);


    window.location = '/Home/vDashboardHotel';
}


const formatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
    minimumFractionDigits: 0
})