$(function () {

    // APOS CARREGAMENTO PÁGINA
    $(document).ready(function ($) {

        // TORNA VISIVEL DIV LOADER
        $("#progress").css('display', 'block');

        //REQUISIÇÃO ASSINCRONA
        //CARROSSEL 
        $.get('/Banner/ListarBannerAtivo', function (resultado) {
            $('#resultadoCarrossel').html(resultado);
        });

        //UNIDADE 
        $.get('/Unidade/ListarUnidadeAtiva', function (resultado) {
            $('#resultadoUnidade').html(resultado);
        });

        //CONTATO 
        $.get('/Home/Contato', function (resultado) {
            $('#resultadoContato').html(resultado);
        });
    });
});
