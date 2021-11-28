// EVENTOS PELO MES
function eventosPeloMes() {

    // RECUPERA VALOR DO MES SELECIONADO
    var e = document.getElementById("mesPesquuisa");
    var itemSelecionado = e.options[e.selectedIndex].value;

    // TORNA VISIVEL DIV LOADER
    $("#progress").css('display', 'block');

    $.get('/Eventos/AgendaPorMes?mesEvento=' + itemSelecionado, function (resultado) {
        $('#retornoEventos').html(resultado);
    });
};