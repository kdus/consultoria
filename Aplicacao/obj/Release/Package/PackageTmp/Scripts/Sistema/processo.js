function gravar()
{
    if (validarCadastro())
    {
        habilitar("#Codigo");

        $('#divMensagemErro').hide();

        var registro = $('#frmProcesso').serializeObject();
       

        registro.Valor = parseFloat(registro.Valor.replace(/\./g, "").replace(',', '.'));

        ChamarControler('/Processo/Gravar', 'POST', JSON.stringify({ acao: registro }), function (data)
        {
            if (data.status == 200)
            {
                var resultado = data.responseJSON;

                $("#Codigo").val(resultado.Codigo);

                habilitar("#btnAbreCp");
                habilitar("#btnAbreCr");

                mostraDialogo('<strong>Sucesso</strong><br>Registro gravado com sucesso.', "success", 3500);
            }
            else
            {
                mostraDialogo('<strong>Erro</strong><br>Erro ao gravar registro.', "error", 3500);
            }
        });

        desabilitar("#Codigo");
    }
}

function gravarcp()
{
    habilitar("#Codigo");

    if (validarLancamentoCp())
    {        

        $('#divMensagemErro').hide();

        var registro = $('#frmProcesso').serializeObject();

        ChamarControler('/Processo/GravarContasPagar', 'POST', JSON.stringify({ acao: registro }), function (data) {
            if (data.status == 200)
            {
                $("#Favorecido").val("");
                $("#ParcelaDeContasPagar").val("");
                $("#ParcelaAteContasPagar").val("");
                $("#ValorContasPagar").val("");
                $("#VencimentoContasPagar").val("");                

                mostraDialogo('<strong>Sucesso</strong><br>Conta gerada com sucesso.', "success", 3500);
            }
            else {
                mostraDialogo('<strong>Erro</strong><br>Erro ao gravar conta a pagar.', "error", 3500);
            }
        });

        var busca = $('#frmProcesso').serializeObject();
        var dataJson = JSON.stringify({ acao: busca });

        ChamarControler('/Processo/BuscarContasPagar', 'POST', dataJson, function (data) {
            if (data.status == 200) {
                $('#grdContasPagar').html(data.responseText);
                $('#Abas').show();
                $('#grdContasPagar').show();
            }
            else {
                $('#grdBusca').hide();
                mostraDialogo('<strong>Erro</strong><br>Erro ao listar contas a pagar.', "error", 3500);
            }
        });

        desabilitar("#Codigo");
    }
}

function gravarcr()
{
    habilitar("#Codigo");

    if (validarLancamentoCr())
    {
        

        $('#divMensagemErro').hide();

        var registro = $('#frmProcesso').serializeObject();

        ChamarControler('/Processo/GravarContasReceber', 'POST', JSON.stringify({ acao: registro }), function (data) {
            if (data.status == 200)
            {
                $("#Cliente").val("");
                $("#ParcelaDeContasReceber").val("");
                $("#ParcelaAteContasReceber").val("");
                $("#ValorContasReceber").val("");
                $("#ValorRecebidoContasReceber").val("");
                $("#VencimentoContasReceber").val("");

                mostraDialogo('<strong>Sucesso</strong><br>Conta gerada com sucesso.', "success", 3500);
            }
            else
            {
                MostrarMensagem("Erro", "Erro ao gravar registro.");
            }
        });

        var busca = $('#frmProcesso').serializeObject();
        var dataJson = JSON.stringify({ acao: busca });

        ChamarControler('/Processo/BuscarContasReceber', 'POST', dataJson, function (data) {
            if (data.status == 200) {
                $('#grdContasReceber').html(data.responseText);

                $('#grdContasReceber').show();
            }
            else {
                $('#grdBusca').hide();
                mostraDialogo('<strong>Erro</strong><br>Erro ao listar contas a receber.', "error", 3500);
            }
        });

        desabilitar("#Codigo");
    }
}


function validarCadastro()
{
    var eValido = true;
    var vPreenchimento = "";

    if ($("#Numeracao").val() == "")
    {
        eValido = false;
        vPreenchimento += " Numeração";
    }

    if ($("#Data").val() == "")
    {
        eValido = false;
        vPreenchimento += " Data";
    }


    if (vPreenchimento.length > 0) {
        eValido = false;
        mostraDialogo('<strong>Erro</strong><br>Preencha o(s) campos(s):' + vPreenchimento + ".", "info", 3500);
    }

    return eValido;
}


function validarLancamentoCr()
{
    var eValido = true;

    if ($("#Codigo").val() == "" || $("#Codigo").val() == "0") {
        eValido = false;
        MostrarMensagem("Erro", "Primeiro grave a ação.");

        return eValido;
    }

    if ($("#VencimentoContasReceber").val() == "" || $("#ValorContasReceber").val() == "" || $("#ParcelaDeContasReceber").val() == "" || $("#ParcelaAteContasReceber").val() == "")
    {
        eValido = false;
        MostrarMensagem("Erro", "Preencha pelo menos os campos Valor, Vencimento e De/Até.");

        return eValido;
    }

    return eValido;
}


function validarLancamentoCp()
{
    var eValido = true;

    
    if ($("#Codigo").val() == "" || $("#Codigo").val() == "0") {
        eValido = false;
        MostrarMensagem("Erro", "Primeiro grave a ação.");

        return eValido;
    }

    if ($("#VencimentoContasPagar").val() == "" || $("#ValorContasPagar").val() == "" || $("#ParcelaDeContasPagar").val() == "" || $("#ParcelaAteContasPagar").val() == "")
    {
        eValido = false;
        MostrarMensagem("Erro", "Preencha pelo menos os campos Valor, Vencimento e De/Até.");

        return eValido;
    }

   

    return eValido;
}

function novo()
{
    $("#Codigo").val("");
    $("#Sequencia").val("");
    $("#Numeracao").val("");
    $("#Data").val("");
    $("#Valor").val("");
    $("#ValorRecebidoContasReceber").val("");

    $("select[name='MatrizFilial']").val(0);
    $("select[name='Tipo']").val(0);
    $("select[name='Requerente']").val(0);
    $("select[name='Responsavel']").val(0);
    $("select[name='Assessoria']").val(0);
    $("select[name='ResponsavelFinanceiro']").val(0);

    limparCombos('MatrizFilial', '0', '');
    limparCombos('Tipo', '0', '');
    limparCombos('Requerente', '0', '');
    limparCombos('Responsavel', '0', '');
    limparCombos('Assessoria', '0', '');
    limparCombos('ResponsavelFinanceiro', '0', '');

    //popup contas a pagar
    $("#ParcelaDeContasPagar").val("");
    $("#ParcelaAteContasPagar").val("");
    $("#ValorContasPagar").val("");
    $("#VencimentoContasPagar").val("");
    $("#Sucumbencia").val("");
    $("select[name='Favorecido']").val(0);

    limparCombos('Favorecido', '0', '');

    //popup contas a receber
    $("#ParcelaDeContasReceber").val("");
    $("#ParcelaAteContasReceber").val("");
    $("#ValorContasReceber").val("");
    $("#ValorRecebidoContasReceber").val("");
    $("#Principal").val("");
    $("#VencimentoContasReceber").val("");

    $("select[name='Cliente']").val(0);

    limparCombos('Cliente', '0', '');

    desabilitar("#btnAbreCp");
    desabilitar("#btnAbreCr");

    $('#divMensagemErro').hide();

    $('#grdBusca').hide();

    $('#Abas').hide();
    $('#grdContasPagar').hide();
}


function buscar(pOrderBy)
{
    var busca = $('#frmProcesso').serializeObject();
    var dataJson = JSON.stringify({ acao: busca, pPagina: 'Grid', pOrdem : pOrderBy });

    $('#divMensagemErro').hide();
    $('#Abas').hide();
   

    //$("#BuscaRequerente").val("");
    //$("#BuscaNumeracao").val("");

    ChamarControler('/Processo/Buscar', 'POST', dataJson, function (data)
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

function buscaSimples(div)
{
    var busca = $('#frmProcesso').serializeObject();
    var dataJson = JSON.stringify({ acao: busca, pPagina: 'GridImprimirSimples' });

    $('#divMensagemErro').hide();
    $('#Abas').hide();

    $("#BuscaRequerente").val("");
    $("#BuscaNumeracao").val("");

    ChamarControler('/Processo/Buscar', 'POST', dataJson, function (data) {
        if (data.status == 200) {
            $('#' + div).html(data.responseText);
            $('#' + div).show();
        }
        else {
            $('#' + div).hide();
            MostrarMensagem("Erro", "Erro ao buscar.");
        }
    });


}

function detalhar(pCodigo)
{

    ChamarControler('/Processo/PreencherCampos', 'POST', JSON.stringify({ pCodigo }), function (data) {
        if (data.status == 200)
        {
            var resultado = data.responseJSON;
                       

            $("#Codigo").val(resultado.Codigo);
            $("#Data").val(formatarData(resultado.Data));
            $("#Sequencia").val(resultado.Sequencia);
            $("#Numeracao").val(resultado.Numeracao);
            $("#Valor").val(resultado.Valor);

            $("select[name='MatrizFilial']").val(resultado.MatrizFilial);
            $("select[name='Tipo']").val(resultado.Tipo);
            $("select[name='Responsavel']").val(resultado.Responsavel);
            $("select[name='Requerente']").val(resultado.Requerente);            
            $("select[name='Assessoria']").val(resultado.Assessoria);
            $("select[name='ResponsavelFinanceiro']").val(resultado.ResponsavelFinanceiro);
            
            limparCombos('MatrizFilial', resultado.MatrizFilial, '');
            limparCombos('Tipo', resultado.Tipo, '');
            limparCombos('Requerente', resultado.Requerente, '');
            limparCombos('Responsavel', resultado.Responsavel, '');
            limparCombos('Assessoria', resultado.Assessoria, '');
            limparCombos('ResponsavelFinanceiro', resultado.ResponsavelFinanceiro, '');
            

            $('#grdBusca').hide();
            $('#Abas').show();

            habilitar("#Codigo");

            habilitar("#btnAbreCp");
            habilitar("#btnAbreCr");

            var busca = $('#frmProcesso').serializeObject();
            var dataJson = JSON.stringify({ acao: busca });

            $('#divMensagemErro').hide();
            $('#Abas').hide();

            ChamarControler('/Processo/BuscarContasPagar', 'POST', dataJson, function (data) {
                if (data.status == 200) {
                    $('#grdContasPagar').html(data.responseText);
                    $('#Abas').show();
                    $('#grdContasPagar').show();
                }
                else {
                    $('#grdBusca').hide();
                    MostrarMensagem("Erro", "Erro ao buscar.");
                }
            });


            ChamarControler('/Processo/BuscarContasReceber', 'POST', dataJson, function (data) {
                if (data.status == 200) {
                    $('#grdContasReceber').html(data.responseText);
                    
                    $('#grdContasReceber').show();
                }
                else {
                    $('#grdBusca').hide();
                    MostrarMensagem("Erro", "Erro ao buscar.");
                }
            });
          
            desabilitar("#Codigo");
            
        }
        else
        {
            MostrarMensagem("Erro", "Erro ao detalhar Processo/Ação.");
        }
    });
}

function excluir()
{

    $('#divMensagemErro').hide();

    var registro = $('#frmProcesso').serializeObject();

    ChamarControler('/Processo/Excluir', 'POST', JSON.stringify({ pessoa: registro }), function (data) {
        if (data.status == 200) {
            $('#divMensagemErro').removeClass("alert-error");
            $('#divMensagemErro').addClass("alert-success");
            $('#msgErroSucesso').html("Registro excluido com sucesso.");
            $('#divMensagemErro').show();
        }
        else {

        }
    });

    novo();

}

//function imprimir(div)
//{    
//    tela_impressao = document.getElementById(div).innerHTML;
//    tela_impressao.window.print();
//}



function imprimir(divID) {
    var conteudo = document.getElementById(divID).innerHTML;
    var win = window.open();
    win.document.write(conteudo);
    win.print();
    //win.close();//Fecha após a impressão.  
}

function LimparPopUpCp()
{

    //popup contas a pagar
    $("#ParcelaDeContasPagar").val("");
    $("#ParcelaAteContasPagar").val("");
    $("#ValorContasPagar").val("");
    $("#VencimentoContasPagar").val("");
    $("#Sucumbencia").val("");
    $("select[name='Favorecido']").val(0);

    limparCombos('Favorecido', '0', '');
      

}


function LimparPopUpCr()
{      

    //popup contas a receber
    $("#ParcelaDeContasReceber").val("");
    $("#ParcelaAteContasReceber").val("");
    $("#ValorContasReceber").val("");
    $("#ValorRecebidoContasReceber").val("");
    $("#Principal").val("");
    $("#VencimentoContasReceber").val("");

    $("select[name='Cliente']").val(0);

    limparCombos('Cliente', '0', '');

}


function pesquisar() {

    //popup contas a receber
    //$("#BuscaRequerente").val("");
    //$("#BuscaAssessoria").val("");
    //$("#BuscaNumeracao").val("");
    //$("#BuscaDataDe").val("");
    //$("#BuscaDataAte").val("");
    //$("#BuscaTipoAcao").val("");
   

}

function selecionarTodos() {
    var vQtdeFiltrado = $("#QtdeFiltrado").val();

    var vValida = $('input[name=SelecionarTodos]').prop('checked');


    for (var i = 0; i < vQtdeFiltrado; i++) {
        if (vValida == true)
            $("input[name=Codigo_" + i + "]").prop('checked', true);
        else
            $("input[name=Codigo_" + i + "]").prop('checked', false);
    }

}

function EnviarRelacao()
{

    var registro = $('#frmProcessoGrid').serializeObject();

    var vQtdeFiltrado = $("#QtdeFiltrado").val();
    var vConta = 0;
    var vAssessoria = 0;
    var vFazer = "";
    var vCodigoConfirmacao = 0;
    var vSucesso = true;
    var vSelecionarAoMenosUm = false;

    vAssessoria = registro.AssessoriaParaConfirmar;

    for (var i = 0; i < vQtdeFiltrado; i++)
    {
        vFazer = $("input[name=Codigo_" + i + "]").prop('checked');

        if (vFazer == true)
        {
            vSelecionarAoMenosUm = true;
        }
    }

    if (vSelecionarAoMenosUm == false)
    {
        mostraDialogo('<strong>Erro</strong><br>Selecione pelo menos uma ação.', "error", 3500);
        return;
    }

    ChamarControler('/Processo/GravarAcaoConfirmada', 'POST', JSON.stringify({ pAssessoria: vAssessoria }), function (data) {
        if (data.status == 200)
        {
            var resultado = data.responseJSON;
            vCodigoConfirmacao = resultado.Codigo;


            for (var i = 0; i < vQtdeFiltrado; i++)
            {
                vAssessoria = registro.AssessoriaParaConfirmar;
                vConta = $("input[name=Codigo_" + i + "]").prop('value');
                vFazer = $("input[name=Codigo_" + i + "]").prop('checked');

                if (vFazer == true) {
                    ChamarControler('/Processo/GravarAcaoConfirmadaDetalhe', 'POST', JSON.stringify({ pAcao: vConta, pAssessoria: vAssessoria, pConfirmacao: vCodigoConfirmacao }), function (data) {
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

            mostraDialogo('<strong>Sucesso</strong><br>Relação gerada com sucesso.', "success", 3500);

        }
        else
        {
            mostraDialogo('<strong>Sucesso</strong><br>Erro ao gerar Relação.', "info", 3500);
        }
    });    



    
}
