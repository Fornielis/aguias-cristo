$(function () {
    // AVISO LOGIN
    $('#erro-login .btn').on('click', function () {
        $("#erro-login").removeClass("zoomInDown");
        $("#erro-login").addClass("bounceOut");
        $("input[name='Usuario']").val('');
        $("input[name='Senha']").val('');
    });

});