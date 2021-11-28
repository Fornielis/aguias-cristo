// UTILIZANDO FORMULARIOS
//ABRIR
function abrirFormulario() {
    $("#menu-inativo").css("display", "block");
    $("#conteudo").addClass("animated zoomInDown");
};

//FECHAR
function fecharFormulario() {
    $("#menu-inativo").css("display", "none");
    $("#conteudo").removeClass("zoomInDown");
    $("#conteudo").addClass("bounceOut");
};

//LIMPAR TEXTOS
function limparTextos() {
    $(".txt").val("");
};

//MASCARAS CAMPOS TEXTOS
function mascarasTestos() {
    $(".mask-telefone").mask("(99)99999-9999");
    $(".mask-cpf").mask("999.999.999-99");
    $(".mask-data").mask("99/99/9999");
    $(".mask-cep").mask("99999-999");
};

//REQUISIÇÃO ASSINCRONA
//LINHA DA TABELA FUNCIONA COMO LINK
function linkLinha(url) {
    $.get(url, function (resultado) {
        $("#conteudo").html(resultado);
    })
};

// ABRIR - FECHAR TELA PROCESS
function abrirTelaProcess() {
    $("#progress").css('display', 'block');
};

function fecharTelaProcess() {
    $("#progress").css('display', 'none');
};

//AVISO DELETAR
function avisoDeletarAbrir() {
    $("#aviso-deletar").css("display", "block");
    $("#aviso-deletar").removeClass("bounceOut");
    $("#aviso-deletar").addClass("zoomInDown");
};

function avisoDeletarFechar() {
    $("#aviso-deletar").removeClass("zoomInDown");
    $("#aviso-deletar").addClass("bounceOut");
};

//FECHAR AVISO
function conteudoRetornoFechar() {
    $(".conteudo-retorno").css("display", "none");
};

// CARRAGAR IMAGEM
function carregarImagem() {
    // VERIFICA SE IMAGEM FOI SELECIONADA
    if (event.target.files != null && event.target.files.length != 0) {

        // CAPTURA ELEMENTOS A SEREM MANIPULADOS
        var imagem = document.getElementById('imagem');
        var arquivo = document.getElementById('arquivo').files[0];
        var base64imagem = document.getElementById('base64imagem');

        // ZERAR VARIAVEL
        base64imagem.value = "";

        // LER ARQUIVO
        var reader = new FileReader();
        reader.onloadend = function () {
            imagem.src = reader.result;
            base64imagem.value = reader.result;
        };

        // VERIFICA SE EXISTE ARUIVO 
        // COLOCA VALOR DA IMAGEM NO BOX TEXTO
        // SETA IMAGEM QUANDO ARQUIVO FOR VAZIO
        if (arquivo) {
            reader.readAsDataURL(arquivo);          
        }
        else {
            imagem.src = "/Imagens/FUNDO-NO-IMG.png";
        }
    }
};

// FUNÇÃO OCULTA CAMPO UF PARA OUTROS PAISES
function ocultarUF() {
    var valor = $("#campoPais option:selected").text();
    var UF = document.getElementById('campoUF');

    if (valor == 'Brasil') {
        UF.style.display = 'block';
        $("#campoCidade").removeClass("campo-50");
        $("#campoCidade").addClass("campo-25");
    }
    else {
        UF.style.display = 'none';
        $("#campoCidade").removeClass("campo-25");
        $("#campoCidade").addClass("campo-50");
    }
};




