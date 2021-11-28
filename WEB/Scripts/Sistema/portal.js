$(function () {


    // RESSETS ELEMENTOS - UL
    function ressetElementos() {
        $(".menu-lateral-lista").css("display", "none");
        $(".btn-menu h4").css("transform", "rotate(0deg)");
        $(".btn-menu p").css("padding", "10px 5px 3px 20px");
        $(".btn-menu").removeClass("menu-ativo");
    };

    // LISTA - MARKETING
    function mk() {

        var lista = document.getElementById('ul-mk');

        if (lista.style.display == "none") {
            ressetElementos();
            $("#btn-mk").addClass("menu-ativo");
            $("#ul-mk").css("display", "block");
            $("#ul-mk").addClass("animated bounceInLeft");
            $("#btn-mk h4").css("transform", "rotate(180deg)");
            $("#btn-mk p").css("padding", "10px 5px 3px 80px");
        }
        else {
            $("#btn-mk").removeClass("menu-ativo");
            $("#ul-mk").css("display", "none");
            $("#btn-mk h4").css("transform", "rotate(0deg)");
            $("#btn-mk p").css("padding", "10px 5px 3px 20px");
        };
    };
    $('#btn-mk').on('click', mk);

    // LISTA - UNIDADES
    function unidades() {

        var lista = document.getElementById('ul-unidades');

        if (lista.style.display == "none") {
            ressetElementos();
            $("#btn-unidades").addClass("menu-ativo");
            $("#ul-unidades").css("display", "block");
            $("#ul-unidades").addClass("animated bounceInLeft");
            $("#btn-unidades h4").css("transform", "rotate(180deg)");
            $("#btn-unidades p").css("padding", "10px 5px 3px 80px");
        }
        else {
            $("#btn-unidades").removeClass("menu-ativo");
            $("#ul-unidades").css("display", "none");
            $("#btn-unidades h4").css("transform", "rotate(0deg)");
            $("#btn-unidades p").css("padding", "10px 5px 3px 20px");
        };
    };
    $('#btn-unidades').on('click', unidades);

    // LISTA - MEMBROS
    function membros() {

        var lista = document.getElementById('ul-membros');

        if (lista.style.display == "none") {
            ressetElementos();
            $("#btn-membros").addClass("menu-ativo");
            $("#ul-membros").css("display", "block");
            $("#ul-membros").addClass("animated bounceInLeft");
            $("#btn-membros h4").css("transform", "rotate(180deg)");
            $("#btn-membros p").css("padding", "10px 5px 3px 80px");
        }
        else {
            $("#btn-membros").removeClass("menu-ativo");
            $("#ul-membros").css("display", "none");
            $("#btn-membros h4").css("transform", "rotate(0deg)");
            $("#btn-membros p").css("padding", "10px 5px 3px 20px");
        };
    };
    $('#btn-membros').on('click', membros);

    // LISTA - EVENTOS
    function eventos() {

        var lista = document.getElementById('ul-eventos');

        if (lista.style.display == "none") {
            ressetElementos();
            $("#btn-eventos").addClass("menu-ativo");
            $("#ul-eventos").css("display", "block");
            $("#ul-eventos").addClass("animated bounceInLeft");
            $("#btn-eventos h4").css("transform", "rotate(180deg)");
            $("#btn-eventos p").css("padding", "10px 5px 3px 80px");
        }
        else {
            $("#btn-eventos").removeClass("menu-ativo");
            $("#ul-eventos").css("display", "none");
            $("#btn-eventos h4").css("transform", "rotate(0deg)");
            $("#btn-eventos p").css("padding", "10px 5px 3px 20px");
        };
    };
    $('#btn-eventos').on('click', eventos);
});













