var user_id;
$(document).ready(function () {

    //data-tables incidencias
    $('#incidenciasList').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.8/i18n/Spanish.json"
        }
     
    });
    //data-tables personal
    $('#personal').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.8/i18n/Spanish.json"
        }
    });

    //data-tables personal
    $('#maquinaria').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.8/i18n/Spanish.json"
        }
    });
    //data-tables tipos
    $('#tipos').DataTable({
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.8/i18n/Spanish.json"
        }
    });

    //selector operarios

    $('#selector').multiSelect();
  
    //selector de archivos
    $('input[type=file]').bootstrapFileInput();
    $('.file-inputs').bootstrapFileInput();
   
    //obtencion de datos estadisticas

    function obtenerDatos() {
        $.ajax({
            type: "GET",
            url: "/api/apiEstadisticas",
            contentType: "application/Json; charset=utf-8",
            dataType: "json",
            asinc:true
        }).done(function(data) {
       

            $('#enEspera').html(data["espera"]);
            $('#proceso').html(data["proceso"]);
            $('#reparadas').html(data["reparadas"]);
            $('#detenidas').html(data["detenidas"]);
            $('#esperaTime').html(data["tiempoEspera"]);
            $('#intervencionTime').html(data["tiempointervencion"]);
            $('#totalTime').html(data["tiempoTotal"]);

            donut(data["porcentaje"]);
        });
        
    }
    $('.obtenerDatos').click(obtenerDatos());

    //setInterval(alerta, 5000);

    //cargar lista de archivos

    //function cargarFiles(IncidenciaId) {
    //    alert('entro');
    //    $.ajax({
    //        type: "GET",
    //        url: "/api/apiFile",
    //        data: { id: IncidenciaId },
    //        dataType: "json",
    //        success: function (data) {
    //            $.each(data, function (index) {
    //                $(".filesTable").append("<tr><td>" + data[index].file_id + "</td><td>" + data[index].file_name + "</td><td>" + data[index].file_ext + "</td><td>" + data[index].file_coment + "</td><td><button class='btn' onclick='descargar(" + data[index].file_id + ");'>Descargar</button></td></tr>")
    //            });
    //       }
    //    });
    //}

    //function alerta() {
    //    alert('entro');
    //}

    //$('.editar').click(cargarFiles(1008));


    //estadisticas morris
    //donut
    function donut(datos) {
        Morris.Donut({
            element: 'donut',
            data: datos
        });
    }
 
    //grafica
    Morris.Bar({
        element: 'bar-chart',
        data: [
          { y: 'Enero', a: 100, b: 90,c:30 },
          { y: 'Febrero', a: 75, b: 65, c:30 },
          { y: 'Marzo', a: 50, b: 40, c: 30 },
          { y: 'Abril', a: 75, b: 65, c: 30 },
          { y: 'Mayo', a: 50, b: 40, c: 30 },
          { y: 'Junio', a: 75, b: 65, c: 30 },
          { y: 'Julio', a: 100, b: 90, c: 30 },
          { y: 'Agosto', a: 50, b: 40, c: 30 },
          { y: 'Septiembre', a: 75, b: 65, c: 30 },
          { y: 'Octubre', a: 50, b: 40, c: 30 },
          { y: 'Noviembre', a: 75, b: 65, c: 30 },
          { y: 'Diciembre', a: 100, b: 90, c: 30 }
        ],
        xkey: 'y',
        ykeys: ['a', 'b','c'],
        labels: ['Series A', 'Series B','Series C']
    });
});
