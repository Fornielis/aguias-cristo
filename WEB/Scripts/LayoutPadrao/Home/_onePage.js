// EFEITO SCROLL PÁGINA
$(function () {
    $(document).ready(function ($) {
        $(".scroll").click(function (event) {
            event.preventDefault();
            $('html,body').animate({ scrollTop: $(this.hash).offset().top }, 800);
        });
    });
});

// FUNÇÕES MENU LATERAL
function menuLateral() {
    var lista = document.getElementById('menu-lista');
    var janela = document.getElementById('menu-janela');

    if (lista.style.left == "-300px") {
        lista.style.left = "0px";
        janela.style.left = "300px";
        janela.style.opacity = "1";
        janela.style.visibility = "visible";
    }
    else {
        lista.style.left = "-300px";
        janela.style.left = "0px";
        janela.style.opacity = "0";
        janela.style.visibility = "hidden";
    };
};

$(function () {
    menuLateral();
    $('.menu-icone').on('click', menuLateral);
    $('#menu-janela').on('click', menuLateral);
    $('#menu-lista li').on('click', menuLateral);
});