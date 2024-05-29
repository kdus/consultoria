


function buscar()
{
    var busca = $('#frmRelatorioGerencial').serializeObject();
    var acao = JSON.stringify({ pModel: busca });

    $('#divMensagemErro').hide();

    ChamarControler('/RelatorioGerencial/Buscar', 'POST', acao, function (data)
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

function buscarDetalhado() {
    var busca = $('#frmRelatorioGerencial').serializeObject();
    var acao = JSON.stringify({ pModel: busca });

    $('#divMensagemErro').hide();

    ChamarControler('/RelatorioGerencial/BuscarDetalhado', 'POST', acao, function (data) {
        if (data.status == 200) {
            $('#grdBusca').html(data.responseText);
            $('#grdBusca').show();
        }
        else {
            $('#grdBusca').hide();
            MostrarMensagem("Erro", "Erro ao buscar.");
        }
    });


}

function buscarDetalhadoII() {
    var busca = $('#frmRelatorioGerencial').serializeObject();
    var acao = JSON.stringify({ pModel: busca });

    $('#divMensagemErro').hide();

    ChamarControler('/RelatorioGerencial/BuscarDetalhadoII', 'POST', acao, function (data) {
        if (data.status == 200) {
            $('#grdBusca').html(data.responseText);
            $('#grdBusca').show();
        }
        else {
            $('#grdBusca').hide();
            MostrarMensagem("Erro", "Erro ao buscar.");
        }
    });


}




function imprimir(divID)
{
    var conteudo = document.getElementById(divID).innerHTML;
    var win = window.open();
    win.document.write(conteudo);
    win.print();
    //win.close();//Fecha após a impressão.  
}



function selecionarTodos()
{
    var vQtdeFiltrado = $("#QtdeFiltrado").val();
    var vTotalSelecionado = $("#TotalSelecionado").val();

    var vValida = $('input[name=SelecionarTodos]').prop('checked');


    for (var i = 0; i < vQtdeFiltrado; i++)
    {
        if (vValida == true)
            $("input[name=Codigo_" + i + "]").prop('checked', true);
        else
            $("input[name=Codigo_" + i + "]").prop('checked', false);
    }

    calculaSelecionados();

}


function calculaSelecionados()
{
    var vPrincipal = 0;
    var vSucumbencia = 0;
    var vTotal = 0;
    var vQtdeFiltrado = $("#QtdeFiltrado").val();
    var vValida = false;

    for (var i = 0; i < vQtdeFiltrado; i++)
    {
        var vValida = $('input[name=Codigo_'+ i +']').prop('checked');

        if (vValida)
        {
            //vValor = Principal - Sucumbencia * 30% + sucumbencia / 2;
            vSucumbencia = parseFloat($("#Sucumbencia_" + i).val().replace(/\./g, "").replace(',', '.'));
            vPrincipal = parseFloat($("#Principal_" + i).val().replace(/\./g, "").replace(',', '.'));
            vTotal += (((vPrincipal - vSucumbencia) * 30 / 100) + vSucumbencia) / 2;
        }
    }

    $("#TotalSelecionado").val(FormatoMoeda(vTotal));
    $("#ValorContasPagar").val(FormatoMoeda(vTotal));   

}

function gravarcp() {
    if (validarLancamentoCp())
    {
        $('#divMensagemErro').hide();

        var registro = $('#frmRelatorioGerencial').serializeObject();

        registro.ValorContasPagar = parseFloat(registro.ValorContasPagar.replace(/\./g, "").replace(',', '.'));

        var vCodigoContasPagar = "";

        ChamarControler('/RelatorioGerencial/GravarContasPagar', 'POST', JSON.stringify({ acao: registro }), function (data)
        {
            if (data.status == 200)
            {
                var resultado = data.responseJSON;
               
                vCodigoContasPagar = resultado.Codigo;

                $("#Favorecido").val("");
                $("#ParcelaDeContasPagar").val("");
                $("#ParcelaAteContasPagar").val("");
                $("#ValorContasPagar").val("");
                $("#VencimentoContasPagar").val("");

                PrestarContas(vCodigoContasPagar);

                MostrarMensagem("Sucesso", "Conta gerada com sucesso.");
                
            }
            else
            {
                MostrarMensagem("Erro", "Erro ao gravar registro.");
            }
        });        
    }
}

function validarLancamentoCp()
{
    var eValido = true;


    if ($("#VencimentoContasPagar").val() == "" || $("#ValorContasPagar").val() == "" || $("#ParcelaDeContasPagar").val() == "" || $("#ParcelaAteContasPagar").val() == "") {
        eValido = false;
        MostrarMensagem("Erro", "Preencha pelo menos os campos Valor, Vencimento e De/Até.");
    }



    return eValido;
}

function PrestarContas(pContasPagar)
{

    var registro = $('#frmRelatorioGerencialGrid').serializeObject();

    var vQtdeFiltrado = $("#QtdeFiltrado").val();
    var vConta = 0;
    var vAssessoria = 0;
    var vFazer = "";


    for (var i = 0; i < vQtdeFiltrado; i++)
    {
        vAssessoria = registro.AssessoriaParaConfirmar;
        vConta = $("input[name=Codigo_" + i + "]").prop('value');
        vFazer = $("input[name=Codigo_" + i + "]").prop('checked');
        vAcao = $("input[name=CodigoAcao_" + i + "]").prop('value');

        if (vFazer == true)
        {
            ChamarControler('/ContasPagarDetalhe/Gravar', 'POST', JSON.stringify({ pAcao: vAcao, pConta: pContasPagar }), function (data) {
                if (data.status == 200)
                {
                    var resultado = data.responseJSON;                    
                }                
            });
        }
    }
}
