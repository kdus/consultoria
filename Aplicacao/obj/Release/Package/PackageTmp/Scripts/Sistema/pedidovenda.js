function gravar() {
    if (validarCadastro()) {
        $('#divMensagemErro').hide();

        var registro = $('#frmpedidovenda').serializeObject();

        ChamarControler('/PedidoVenda/Gravar', 'POST', JSON.stringify({ pedidovenda: registro }), function (data) {
            if (data.status == 200) {
                $('#divMensagemErro').removeClass("alert-error");
                $('#divMensagemErro').addClass("alert-success");
                $('#msgErroSucesso').html("Registro gravado com sucesso.");
                $('#divMensagemErro').show();
            }
            else {

            }
        });
    }
}

//function excluir() {

//        $('#divMensagemErro').hide();

//        var registro = $('#frmPessoa').serializeObject();

//        ChamarControler('/Pessoa/Excluir', 'POST', JSON.stringify({ pessoa: registro }), function (data) {
//            if (data.status == 200) {
//                $('#divMensagemErro').removeClass("alert-error");
//                $('#divMensagemErro').addClass("alert-success");
//                $('#msgErroSucesso').html("Registro excluido com sucesso.");
//                $('#divMensagemErro').show();
//            }
//            else {

//            }
//        });

//        novo();

//}

function validarCadastro() {
    var eValido = true;
    var vPreenchimento = 0;

    if ($("#Data").val() == "") {
        eValido = false;
        $('#divMensagemErro').removeClass("alert-success");
        $('#divMensagemErro').addClass("alert-error");
        $('#msgErroSucesso').html("Data inválida ou em branco");
        $('#divMensagemErro').show();

    }


    if ($("#CODPES").val() == 0) {
        vPreenchimento = "Empresa/Filial"
        eValido = false;
        $('#divMensagemErro').removeClass("alert-success");
        $('#divMensagemErro').addClass("alert-error");
        $('#msgErroSucesso').html(" Por favor selecione uma Empresa/Filial");
        $('#divMensagemErro').show();;
    }

    return eValido;
}

function novo()
{
    $("#Codigo").val("");
    $("#Sequencia").val("");
    $("#CODPES").val("");
    $("#Data").val("");
    $("#caixa_n").val("");
    $("#cmv_peca").val("");
    $("#cmv_pneu").val("");
    $("#servico_terceiro").val("");
    $("#faturamento_peca").val("");
    $("#faturamento_pneu").val("");
    $("#faturamento_maoobra").val("");
    $("#qtd_veiculo").val("");
    $("#Email").val("");

    $('#divMensagemErro').hide();
    $('#grdBusca').hide();
}




function detalhar(pvCodigo)
{

    var vRegistro = $("#tr" + pvCodigo).data("registro");

   
    $("#Codigo").val(vRegistro.Id);
    $("select[name='CODPES']").val(vRegistro.CODPES);
    $("#Id").val(vRegistro.Id);
    $("#CODPES").val(vRegistro.CODPES);
    $("#Data").val(vRegistro.Data);
    $("#caixa_n").val(vRegistro.caixa_n);
    $("#cmv_peca").val(vRegistro.cmv_peca);
    $("#cmv_pneu").val(vRegistro.cmv_pneu);
    $("#servico_terceiro").val(vRegistro.servico_terceiro);
    $("#faturamento_peca").val(vRegistro.faturamento_peca);
    $("#faturamento_pneu").val(vRegistro.faturamento_pneu);
    $("#faturamento_maoobra").val(vRegistro.faturamento_maoobra);
    $("#qtd_veiculo").val(vRegistro.qtd_veiculo);


    $('#grdBusca').hide();
}


function buscar() {
    var busca = $('#frmpedidovenda').serializeObject();
    var dataJson = JSON.stringify({ pedidovenda: busca });

    $('#divMensagemErro').hide();

    ChamarControler('/PedidoVenda/Buscar', 'POST', dataJson, function (data) {
        if (data.status == 200) {
            $('#grdBusca').html(data.responseText);
            $('#grdBusca').show();

        }
        else {
            $('#grdBusca').hide();
            alert("Ocorreu erro ao buscar.")
        }
    });


}

//function imprimir()
//{
//    var busca = $('#frmPessoa').serializeObject();
//    var dataJson = JSON.stringify({ pessoa: busca });

//    ChamarControler('/Pessoa/Imprimir', 'POST', dataJson, function (data) {
//        if (data.status == 200)
//        {

//            var left = ($(window).width() / 2) - (1000 / 2);
//            var top = ($(window).height() / 2) - (800 / 2);
//            var mywindow = window.open('', 'Impressão', "width=1510,height=720,top=" + top + ",left=" + left);

//            mywindow.document.write(data.responseText);

//            mywindow.document.close();
//            mywindow.focus();

//            mywindow.print();

//        }
//        else
//        {

//        }
//    });


//}



