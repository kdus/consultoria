
function MostrarMensagem(pTipo, pMensagem)
{
    if (pTipo == "Erro") {
        $('#divMensagemErro').removeClass("alert-success");
        $('#divMensagemErro').addClass("alert-error");
    }
    else {
        $('#divMensagemErro').removeClass("alert-error");
        $('#divMensagemErro').addClass("alert-success");
    }

    $('#msgErroSucesso').html(pMensagem);
    $('#divMensagemErro').show();

}


function ChamarControler(url, tipoDaChamada, jsonASerEnviado, funcaoDeTermino) {

    $("#barra").attr('aria-valuenow', 0);
    $("#barra").attr('style', "width: 0%");
    $("#progressModal").attr('style', "display: block");

    $.ajax({
        type: tipoDaChamada,
        url: url,
        contentType: "application/json; charset=utf-8",
        data: jsonASerEnviado,
        dataType: "json",
        complete: function (data) {

            funcaoDeTermino(data);


            $("#progressModal").attr('style', "display: none");
            $("#barra").attr('aria-valuenow', 0);
            $("#barra").attr('style', "width: 0%");
            $("#barramsg").text("");

        }

    });
}



function formatarData(data)
{
    try
    {
        var date = new Date(parseInt(data.substr(6)));
        return date.format("dd/mm/yyyy");
    }
    catch (err)
    {
        return "";
    }
}


function mascaraData(campo, e)
{
    var kC = (document.all) ? event.keyCode : e.keyCode;
    var data = campo.value;

    if (kC != 8 && kC != 46) {
        if (data.length == 2) {
            campo.value = data += '/';
        }
        else if (data.length == 5) {
            campo.value = data += '/';
        }
        else
            campo.value = data;
    }
}


function FormatoMoeda(valor)
{

    if (valor != null){

        var valor = valor.toFixed(2).split('.');
        valor[0] = valor[0].split(/(?=(?:...)*$)/).join('.');
        return valor.join(',');
    }
    else {
        return valor;
    }
}


function formatarMoeda(valor) {
    return parseFloat(valor).replace('R$ ','.', ',');
   
}

function redirecionar(url, ev)
{   

    window.location = url;
    if (ev.preventDefault) ev.preventDefault();

}



function enviarteste()
{   
        $('#divMensagemErro').hide();

        var registro = $('#frmAgenda').serializeObject();

        ChamarControler('/Agenda/EnviarTeste', 'POST', JSON.stringify({ pModel: registro }), function (data)
        {
            if (data.status == 200)
            {
                MostrarMensagem("Sucesso", "E-mail enviado com sucesso.");
            }
            else
            {
                MostrarMensagem("Erro", "Erro ao enviar e-mail.");
            }
        });
    
}


function EnviarEmailRelacao(pPara, pCorpo, pDiv)
{
    var vCorpo = $(pDiv).html().replace('#AquiAqui', $('#grdAcaoProcesso').html());

    enviarEmail(pPara, pCorpo, vCorpo);
}


function enviarEmail(Para, Assunto, Corpo)
{

    ChamarControler('/Email/Enviar', 'POST', JSON.stringify({ pPara: Para, pAssunto: Assunto, pCorpo : Corpo}), function (data) {
        if (data.status == 200)
        {
            var resultado = data.responseJSON;
            MostrarMensagem("Sucesso", "Email enviado com Sucesso.");
        }
        else
        {
            MostrarMensagem("Erro", "Erro ao enviar.");
        }
    });
}


function limparCombos(pCombo, pId, pTexto)
{
    var data = { id: pId, text: pTexto };

    var newOption = new Option(data.text, data.id, false, false);
    $('#' + pCombo).append(newOption).trigger('change');
}


function mostraDialogo(mensagem, tipo, tempo) {

    // se houver outro alert desse sendo exibido, cancela essa requisição
    if ($("#message").is(":visible"))
    {
        return false;
    }

    // se não setar o tempo, o padrão é 3 segundos
    if (!tempo)
    {
        var tempo = 3000;
    }

    // se não setar o tipo, o padrão é alert-info
    if (!tipo)
    {
        var tipo = "info";
    }

    // monta o css da mensagem para que fique flutuando na frente de todos elementos da página
    var cssMessage = "display: block; position: fixed; top: 0; left: 20%; right: 20%; width: 60%; padding-top: 10px; z-index: 9999";
    var cssInner = "margin: 0 auto; box-shadow: 1px 1px 5px black;";

    // monta o html da mensagem com Bootstrap
    var dialogo = "";
    dialogo += '<div id="message" style="' + cssMessage + '">';
    dialogo += '    <div class="alert alert-' + tipo + ' alert-dismissable" style="' + cssInner + '">';
    dialogo += '    <a href="#" class="close" data-dismiss="alert" aria-label="close">×</a>';
    dialogo += mensagem;
    dialogo += '    </div>';
    dialogo += '</div>';

    // adiciona ao body a mensagem com o efeito de fade
    $("body").append(dialogo);
    $("#message").hide();
    $("#message").fadeIn(200);

    // contador de tempo para a mensagem sumir
    setTimeout(function () {
        $('#message').fadeOut(300, function () {
            $(this).remove();
        });
    }, tempo); // milliseconds

}


function desabilitar(obj) {
    $(obj).prop("disabled", true);
    //$(".tooltip").tooltip("hide");
}
function habilitar(obj) {
    $(obj).prop("disabled", false);
}

function ChamarControlerNaoAsync(url, tipoDaChamada, jsonASerEnviado, funcaoDeTermino)
{

    $.ajax({
        type: tipoDaChamada,
        url: url,
        contentType: "application/json; charset=utf-8",
        data: jsonASerEnviado,
        dataType: "json",
        async: false,
        complete: function (data) {
            funcaoDeTermino(data);
        }

    });
}


function formatCurrency(value) {
    return value.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' });
}