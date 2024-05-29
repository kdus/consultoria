function gravar()
{
    if (validarCadastro())
    {
        $('#divMensagemErro').hide();

        habilitar("#Codigo");

        var registro = $('#frmContasReceber').serializeObject();

        registro.Valor = parseFloat(registro.Valor.replace(/\./g, "").replace(',', '.'));
        registro.Desconto = parseFloat(registro.Desconto.replace(/\./g, "").replace(',', '.'));
        registro.Juros = parseFloat(registro.Juros.replace(/\./g, "").replace(',', '.'));
        registro.ValorPago = parseFloat(registro.ValorPago.replace(/\./g, "").replace(',', '.'));

        ChamarControler('/ContasReceber/Gravar', 'POST', JSON.stringify({ pModel: registro }), function (data)
        {
            if (data.status == 200)
            {
                var resultado = data.responseJSON;

                $("#Codigo").val(resultado.Codigo);

                desabilitar("#Codigo");

                mostraDialogo('<strong>Sucesso</strong><br>Registro gravado com sucesso.', "success", 3500);
            }
            else
            {
                mostraDialogo('<strong>Erro</strong><br>Erro ao gravar registro.', "error", 3500);
            }
        });
    }
}


function validarCadastro()
{
    var eValido = true;
    var vPreenchimento = "";

    if ($("#Favorecido").val() == "")
    {
        vPreenchimento = " Favorecido";
    }

    if ($("#Vencimento").val() == "")
    {
        vPreenchimento = " Vencimento";
    }

    if ($("#Valor").val() == "") {
        vPreenchimento = " Valor";
    }


    if (vPreenchimento.length > 0)
    {
        eValido = false;
        MostrarMensagem("Erro", "Preencha o(s) campos(s):" + vPreenchimento);
    }

    return eValido;
}

function novo()
{
    $("#Codigo").val("");
    $("#Sequencia").val("");        
    $("#Valor").val("");
    $("#Vencimento").val("");
    $("#DataPagamento").val("");

    $("#ParcelaX").val("");
    $("#ParcelaY").val("");

    $("#Descricao").val("");
    $("#Fixa").val("");
    $("#Pago").val("");

    $("#Desconto").val("");
    $("#Juros").val("");
    $("#ValorPago").val("");
    $("#Principal").val("");
    $("#Observacao").val("");
    $("#CodigoOrigemSequencial").val("");    

    $("select[name='Cliente']").val(0);
    $("select[name='MesReferencia']").val(0);
    $("select[name='AnoReferencia']").val(0);

    $("select[name='ContaBancaria']").val(0);
    $("select[name='FormaPagamentoRecebimento']").val(0);
    $("select[name='OrigemCadastro']").val(0);
    $("select[name='MatrizFilial']").val(0);
    $("select[name='TipoRecebimentoPagamento']").val(0);


    ////
    limparCombos('Cliente', '0', '');
    limparCombos('MesReferencia', '0', '');
    limparCombos('AnoReferencia', '0', '');    
    limparCombos('ContaBancaria', '0', '');
    limparCombos('FormaPagamentoRecebimento', '0', '');
    limparCombos('OrigemCadastro', '0', '');
    limparCombos('MatrizFilial', '0', '');
    limparCombos('TipoRecebimentoPagamento', '0', '');

    $('#divMensagemErro').hide();

    $('#grdBusca').hide();
}


function buscar()
{
    var busca = $('#frmContasReceber').serializeObject();
    var acao = JSON.stringify({ pModel: busca });

    $('#divMensagemErro').hide();

    ChamarControler('/ContasReceber/Buscar', 'POST', acao, function (data)
    {
        if (data.status == 200)
        {
            $('#grdBusca').html(data.responseText);
            $('#grdBusca').show();
        }
        else
        {
            $('#grdBusca').hide();
            MostrarMensagem("Erro", "Erro ao buscar.");        
        }
    });


}

function detalhar(pCodigo)
{

    ChamarControler('/ContasReceber/PreencherCampos', 'POST', JSON.stringify({ pCodigo }), function (data) {
        if (data.status == 200)
        {
            var resultado = data.responseJSON;           


            $("#Codigo").val(resultado.Codigo);            
            $("#Sequencia").val(resultado.Sequencia);

            $("#ParcelaX").val(resultado.ParcelaX);
            $("#ParcelaY").val(resultado.ParcelaY);
                        
            $("#Valor").val(FormatoMoeda(resultado.Valor));
            
            $("#Vencimento").val(formatarData(resultado.Vencimento));
            $("#DataPagamento").val(formatarData(resultado.DataPagamento));

            $("#Descricao").val(resultado.Descricao);
            $("#Fixa").val(resultado.Fixa);
            $("#Pago").val(resultado.Pago);

            $("#Desconto").val(FormatoMoeda(resultado.Desconto));
            $("#Juros").val(FormatoMoeda(resultado.Juros));
            $("#ValorPago").val(FormatoMoeda(resultado.ValorPago));
            $("#Observacao").val(resultado.Observacao);

            $("#Principal").val(resultado.Principal);

            $("select[name='Cliente']").val(resultado.Cliente);
            $("select[name='MesReferencia']").val(resultado.MesReferencia);
            $("select[name='AnoReferencia']").val(resultado.AnoReferencia);
            $("select[name='ContaBancaria']").val(resultado.ContaBancaria);
            $("select[name='FormaPagamentoRecebimento']").val(resultado.FormaPagamentoRecebimento);
            $("select[name='OrigemCadastro']").val(resultado.OrigemCadastro);
            $("select[name='MatrizFilial']").val(resultado.MatrizFilial);
            $("select[name='TipoRecebimentoPagamentoRec']").val(resultado.TipoRecebimentoPagamentoRec);

            limparCombos('Cliente', resultado.Cliente, '');
            limparCombos('MesReferencia', resultado.MesReferencia, '');
            limparCombos('AnoReferencia', resultado.AnoReferencia, '');
            limparCombos('ContaBancaria', resultado.ContaBancaria, '');
            limparCombos('FormaPagamentoRecebimento', resultado.FormaPagamentoRecebimento, '');
            limparCombos('OrigemCadastro', resultado.OrigemCadastro, '');
            limparCombos('MatrizFilial', resultado.MatrizFilial, '');
            limparCombos('TipoRecebimentoPagamentoRec', resultado.TipoRecebimentoPagamentoRec, '');

            $("#CodigoOrigem").val(resultado.CodigoOrigem);
            $("#CodigoOrigemSequencial").val(resultado.CodigoOrigemSequencial);


            $('#grdBusca').hide();
            
        }
        else
        {
            MostrarMensagem("Erro", "Erro ao detalhar registro.");
        }
    });
}



function excluir() {

    $('#divMensagemErro').hide();

    habilitar("#Codigo");

    var registro = $('#frmContasReceber').serializeObject();

    ChamarControler('/ContasReceber/Excluir', 'POST', JSON.stringify({ pessoa: registro }), function (data) {
        if (data.status == 200)
        {
            mostraDialogo('<strong>Sucesso</strong><br>Registro excluído com sucesso.', "success", 3500);

        }
        else
        {

        }
    });

    desabilitar("#Codigo");

    novo();

}

function baixaLote()
{      
    var vQtdeFiltrado = $("#QtdeFiltrado").val();
    var vConta = 0;
    var vFazer = "";
    var vValida = true; //valida se poode baixar as contas por lote

    if ($("#BuscaPagoSimNao").val() == "" || $("#BuscaPagoSimNao").val() == "S")
    {
        mostraDialogo('<strong>Aviso</strong><br>Por favor, filtre somente contas abertas.', "info", 3500);
        vValida = false;
    }

    if (vValida)
    {
        for (var i = 0; i < vQtdeFiltrado; i++)
        {        
            vConta = $("input[name=Codigo_" + i + "]").prop('value');
            vFazer = $("input[name=Codigo_" + i + "]").prop('checked');

            if (vFazer == true)
            {
                ChamarControler('/ContasReceber/BaixaLote', 'POST', JSON.stringify({ pCodigo: vConta }), function (data) {
                    if (data.status == 200) {
                        var resultado = data.responseJSON;

                        mostraDialogo('<strong>Sucesso</strong><br>Registro gravado com sucesso.', "success", 3500);
                    }
                    else {
                        mostraDialogo('<strong>Erro</strong><br>Erro ao gravar registro.', "error", 3500);
                    }
                });
            }
        }
    }
}

function selecionarTodos() {
    var vQtdeFiltrado = $("#QtdeFiltrado").val();
  
    var vValida = $('input[name=SelecionarTodos]').prop('checked');    

   
    for (var i = 0; i < vQtdeFiltrado; i++)
    {
        if (vValida  == true)
            $("input[name=Codigo_" + i + "]").prop('checked', true);
        else
            $("input[name=Codigo_" + i + "]").prop('checked', false);
    }
    
}

function ImprimirReceita() {
    var busca = $('#frmContasReceber').serializeObject();
    var acao = JSON.stringify({ pModel: busca });

    ChamarControler('/ContasReceber/ImprimirReceita', 'POST', acao, function (data) {
        if (data.status == 200) {

            var left = ($(window).width() / 2) - (1000 / 2);
            var top = ($(window).height() / 2) - (800 / 2);
            var mywindow = window.open('', 'Impressão', "width=1510,height=720,top=" + top + ",left=" + left);

            mywindow.document.write(data.responseText);

            mywindow.document.close();
            mywindow.focus();

            mywindow.print();

        }
        else {
            MostrarMensagem("Erro", "Erro ao Imprimir.");
        }
    });
}