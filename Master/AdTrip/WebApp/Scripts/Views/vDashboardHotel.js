function vDashboardHotel() {

    this.service = 'reporte';
    this.ctrlActions = new ControlActions();
    this.tblGananciasXMes = 'tblGananciasXMes';
    this.columns = 'FechaRealizado,MontoGanancia';


    this.ReporteGananciasTotales = function () {
        var data = {}
        var total = 0;
        data.IdHotel = sessionStorage.getItem('idHotelLogeado');
        this.ctrlActions.ReporteGananciasGerente('reporte/reportehotel/', '8', sessionStorage.getItem('idHotelLogeado'), function (reporte) {
            for (var i in reporte) {
                if (reporte[i].MontoGanancia == -1) {
                    total = 0
                } else {
                    total = total + reporte[i].MontoGanancia;
                }
               
            }
            $('#totalGanancias').append(formatter.format(total));

        });
        $('#NomHotel').append(sessionStorage.getItem('nombreHotel'));
    };

    this.ReporteGananciasXMes = function () {
        var data = {}
        data.IdHotel = sessionStorage.getItem('idHotelLogeado');
        data.tipoReporte = '9';
        this.ctrlActions.FillTableId(this.service + '/reportehotel/9', this.tblGananciasXMes, sessionStorage.getItem('idHotelLogeado'), false);
    };


    this.GetReporteGraficoXMes = function (data) {
        this.ctrlActions.GetById(this.service + '/reportehotel/9/' + sessionStorage.getItem('idHotelLogeado'), data);
       
    };


    this.GetReporteCantHabitacionTipo = function (data) {
        this.ctrlActions.GetById(this.service + '/reportehotel/10/' + sessionStorage.getItem('idHotelLogeado'), data);
    };


    this.GetReporteGraficoDisponibilidad = function (data) {
        this.ctrlActions.GetById(this.service + '/reportehotel/11/' + sessionStorage.getItem('idHotelLogeado'), data);
    };
}


$(document).ready(function () {

    var vdashboard = new vDashboardHotel();

    vdashboard.ReporteGananciasTotales();
    vdashboard.ReporteGananciasXMes();

});


const formatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
    minimumFractionDigits: 0
})