function vDashboardAdministrador() {

    this.service = 'reporte';
    this.ctrlActions = new ControlActions();
    this.chartId = 'chartGananciasXMes';
    this.chartId2 = 'chartGananciasComsionXDia';
    this.tblGananciasXMes = 'tblGananciasXMes';
    this.tblGananciasComisionesXDia = 'tblGananciasComisionesXDia';
    this.tblGananciasMembresiasXMes = 'tblGananciasMembresiasXMes';
    this.columns = 'FechaRealizado,MontoGanancia';


    this.ReporteGananciasTotales = function () {
        this.ctrlActions.ReporteGananciasAdmin('reporte', '1', function (reporte) {

            if (reporte.MontoGanancia == -1) {
                $('#totalGanancias').append(formatter.format(0));

            } else {
                $('#totalGanancias').append(formatter.format(reporte.MontoGanancia));
            }

        });

    };



    this.ReporteGananciasComisiones = function () {
        this.ctrlActions.ReporteGananciasAdmin('reporte', '3', function (reportes) {
            var total = 0;
            $(reportes).each(function (index, value) {
                total = total + value.MontoGanancia;
            })
            $('#gananciasComisiones').append(formatter.format(total));
        });
    };


    this.ReporteGananciasMembresias = function () {
        this.ctrlActions.ReporteGananciasAdmin('reporte', '4', function (reportes) {
            var total = 0;
            $(reportes).each(function (index, value) {
                total = total + value.MontoGanancia;
            })
            $('#gananciasMembresias').append(formatter.format(total));
        });
    };



    this.ReporteGananciasXMes = function () {
        this.ctrlActions.FillTable(this.service + '/2', this.tblGananciasXMes, false);

    }


    this.ReporteGananciasComisionesXDia = function () {
        this.ctrlActions.FillTable(this.service + '/3', this.tblGananciasComisionesXDia, false);
    };



    this.ReporteGananciasMembresiasXMes = function () {
        this.ctrlActions.FillTable(this.service + '/4', this.tblGananciasMembresiasXMes, false);
    };



    this.GetReporteGraficoXMesAdmin = function (data) {

        this.ctrlActions.GetById(this.service + '/2', data);
    };

    this.GetReporteGraficoComisionesXDiaAdmin = function (data) {

        this.ctrlActions.GetById(this.service + '/3', data);
    };


    this.GetReporteGraficoMembresiasXMesAdmin = function (data) {

        this.ctrlActions.GetById(this.service + '/4', data);
    };

  

}

 
$(document).ready(function () {

    var vdashboard = new vDashboardAdministrador();

    vdashboard.ReporteGananciasTotales();
    vdashboard.ReporteGananciasComisiones();
    vdashboard.ReporteGananciasMembresias();
    vdashboard.ReporteGananciasXMes();
    vdashboard.ReporteGananciasComisionesXDia();
    vdashboard.ReporteGananciasMembresiasXMes();

});



const formatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
    minimumFractionDigits: 0
})