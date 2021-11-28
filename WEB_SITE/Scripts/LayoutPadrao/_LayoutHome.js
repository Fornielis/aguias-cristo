$(function () {

    // APOS CARREGAMENTO PÁGINA
    $(document).ready(function ($) {

        // EFEITO SCROLL PÁGINA
        $(".scroll").click(function (event) {
            event.preventDefault();
            $('html,body').animate({ scrollTop: $(this.hash).offset().top }, 800);
        });
    });

    // FUNÇÕES MENU LATERAL
    function menuLateral() {
        var lista = document.getElementById('menu-lista');
        var janela = document.getElementById('menu-janela');
        var logo = document.getElementById('menu-logo');
        var logo_lateral = document.getElementById('logo-lateral');
        var titulo = document.getElementById('menu-titulo-xs');

        if (lista.style.left == "-300px") {
            lista.style.left = "0px";
            janela.style.left = "300px";
            janela.style.opacity = "1";
            janela.style.visibility = "visible";
            logo.style.top = "-300px";
            logo_lateral.style.left = "0px";
            titulo.style.margin = "38px 0px 0px 15px"
        }
        else {
            lista.style.left = "-300px";
            janela.style.left = "0px";
            janela.style.opacity = "0";
            janela.style.visibility = "hidden";
            logo.style.top = "0px";
            logo_lateral.style.left = "-300px";
            titulo.style.margin = "38px 0px 0px 125px"
        };
    };

    // GUARDA ALTURA DAS SEÇÕES
    var alturas = {};
    $('.section').each(function () {
        alturas[$(this).prop('id')] = $(this).offset().top;
    });

    // QUANDO É FEITO O SCROLL PERCORRE AS SEÇÕES
    $(window).on('scroll', function () {
        for (var seccao in alturas) {
            if ($(window).scrollTop() >= alturas[seccao]) {
                $('li').removeClass('li-ativa');
                $('li[data-section="' + seccao + '"]').addClass('li-ativa');
            }
        }
    });

    // EXECUÇÃO MENU LATERAL
    menuLateral();
    $('.menu-icone').on('click', menuLateral);
    $('#menu-janela').on('click', menuLateral);
    $('#menu-lista li').on('click', menuLateral);

    // FUNÇÕES MENU ATIVO
    // HOME
    $('#li-home').on('click', function () {
        setTimeout(function () {
            $('li').removeClass('li-ativa');
            $('#li-home').addClass('li-ativa');
        }, 850);
    });

    // UNIDADES
    $('#li-unidades').on('click', function () {
        setTimeout(function () {
            $('li').removeClass('li-ativa');
            $('#li-unidades').addClass('li-ativa');
        }, 850);
    });

    // HISTÓRIA
    $('#li-historia').on('click', function () {
        setTimeout(function () {
            $('li').removeClass('li-ativa');
            $('#li-historia').addClass('li-ativa');
        }, 850);
    });

    // CONTATO
    $('#li-contato').on('click', function () {
        setTimeout(function () {
            $('li').removeClass('li-ativa');
            $('#li-contato').addClass('li-ativa');
        }, 850);
    });

    // ANIMAÇÕES SITE
    // RESSET ANIMAÇÕES - DESCKTOP
    function ressetAnimacoes() {
        // QUANDO SCROLL ESTIVER EM 0px
        if ($(window).scrollTop() == 0) {
            // TELAS MAIORES QUE 925px 
            var scre = $("body").width();
            if (scre >= 925) {
                $('.unidade-animacao').removeClass('animated fadeInUp');
                $('.unidade-animacao').addClass('animated fadeOutDown');
                $('.historia-animacao').removeClass('animated fadeInUp');
                $('.historia-animacao').addClass('animated fadeOutDown');
                $('.contato-animacao').removeClass('animated fadeInUp');
                $('.contato-animacao').addClass('animated fadeOutDown');
            }
        }   
    };

    // RESSET ANIMAÇÕES - CELULAR
    function animacaoCelular() {
        $('.unidade-animacao').addClass('animated fadeInUp');
        $('.unidade-animacao').removeClass('animated fadeOutDown');
        $('.historia-animacao').addClass('animated fadeInUp');
        $('.historia-animacao').removeClass('animated fadeOutDown');
        $('.contato-animacao').addClass('animated fadeInUp');
        $('.contato-animacao').removeClass('animated fadeOutDown');
    };

    // ANIMAÇÕES -DESCKTOP
    function animacaodesckTop() {
        // UNIDADES
        if ($(window).scrollTop() > 50) {
            $('.unidade-animacao').removeClass('animated fadeOutDown');
            $('.unidade-animacao').addClass('animated fadeInUp');
        }
        // HISTORIA
        if ($(window).scrollTop() > 900) {
            $('.historia-animacao').removeClass('animated fadeOutDown');
            $('.historia-animacao').addClass('animated fadeInUp');
        }
        // CONTATO
        if ($(window).scrollTop() > 1500) {
            $('.contato-animacao').removeClass('animated fadeOutDown');
            $('.contato-animacao').addClass('animated fadeInUp');
        }
    };

    // FUNÇÃO QUE CONTROLA APLICAÇÃO DA ANIMAÇÃO PELO TAMNHO DA TELA
    function aplicaAnimacoes() {
        $(".interface").each(function () {
            var scre = $("body").width();
            if (scre >= 925) {
                ressetAnimacoes();
                animacaodesckTop();
            }
            else {
                animacaoCelular();
            }
        });
    };

    $(document).ready(function () {

        // ADICIONA CLASSES AO CARREGAR DOCUMENTO
        aplicaAnimacoes();

        $(window).resize(function () {

            // APLICA FUNÇÃO QUANDO ELEMENTO ITERFACE É AJUSTADO
            aplicaAnimacoes();
        });
    });

    // ANIMAÇÕES PELO SCROLL --> TELAS MAIORES QUE 925px
    $(window).on('scroll', function () {
        ressetAnimacoes();
        animacaodesckTop();
    });

    // FECHAR RETORNO
    $('#btn-ok').on('click', function () {
        $('#retorno').addClass('animated bounceOut');

        // ESPERA 1 SEGUNDO PARA EXECUTAR PROXIMO PASSO
        var delayInMilliseconds = 1000;

        setTimeout(function () {
            $('#retorno').css('display', 'none');
        }, delayInMilliseconds);
    });
});