$(document).ready(function () {
    var usuario = {};
    usuario = JSON.parse(window.sessionStorage.getItem("usuario"));
    nombreUsuario = window.sessionStorage.getItem("nombreUsuario");
    if (usuario != null) {
        MostrarBarLogueado();
        EsconderBarVisitante();
        document.getElementById("nombreUsuario").innerText = nombreUsuario + "!";
    } else {
        MostrarBarVisitante();
        EsconderBarLogueado();
    }

    $("#btnLogout").click(function () {
        LogOut();
    });
});

function MostrarBarLogueado() {
    $("#barLogueado").removeClass("d-none");
    $("#barLogueado").addClass("d-inline");
}

function EsconderBarLogueado() {
    $("#barLogueado").removeClass("d-inline");
    $("#barLogueado").addClass("d-none");
}

function MostrarBarVisitante() {
    $("#barVisitante").removeClass("d-none");
    $("#barVisitante").addClass("d-inline");
}

function EsconderBarVisitante() {
    $("#barVisitante").removeClass("d-inline");
    $("#barVisitante").addClass("d-none");
}

function LogOut() {
    sessionStorage.clear();
    
    $.ajax({
        type: "POST",
        url: '/home/RemoveUser',
        dataType: 'json'
    }).done(function () {
        alert('Usuario eliminado');
        });
}
