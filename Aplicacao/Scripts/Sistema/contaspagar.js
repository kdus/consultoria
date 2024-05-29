function gravar()
{
    if (validarCadastro())
    {
        $('#divMensagemErro').hide();

        habilitar('#Codigo');

        var registro = $('#frmContasPagar').serializeObject();
        
        registro.Valor = parseFloat(registro.Valor.replace(/\./g, "").replace(',', '.'));
        registro.Desconto = parseFloat(registro.Desconto.replace(/\./g, "").replace(',', '.'));
        registro.Juros = parseFloat(registro.Juros.replace(/\./g, "").replace(',', '.'));
        registro.ValorPago = parseFloat(registro.ValorPago.replace(/\./g, "").replace(',', '.'));

        ChamarControler('/ContasPagar/Gravar', 'POST', JSON.stringify({ pModel: registro }), function (data)
        {
            if (data.status == 200)
            {                
                var resultado = data.responseJSON;

                $("#Codigo").val(resultado.Codigo);                               
                
                mostraDialogo('<strong>Sucesso</strong><br>Registro gravado com sucesso.', "success", 3500);

                
            }
            else
            {                
                mostraDialogo('<strong>Erro</strong><br>Erro ao gravar registro.', "info", 3500);
            }
        });

        desabilitar('#Codigo');
    }
}


function validarCadastro()
{
    var eValido = true;
    var vPreenchimento = "";

    if ($("#MatrizFilial").val() == "")
    {
        vPreenchimento = " Empresa/Pessoa";
    }

    if ($("#Favorecido").val() == "")
    {
        vPreenchimento = " Favorecido";
    }

    if ($("#Vencimento").val() == "")
    {
        vPreenchimento = " Vencimento";
    }

    if ($("#Valor").val() == "")
    {
        vPreenchimento = " Valor";
    }

    if ($("#ParcelaX").val() == "")
    {
        vPreenchimento = " Parcela De";
    }

    if ($("#ParcelaY").val() == "")
    {
        vPreenchimento = " Parcela Até";
    }

    if (vPreenchimento.length > 0)
    {
        eValido = false;        
        mostraDialogo('<strong>Erro</strong><br>Preencha o(s) campos(s):' + vPreenchimento + ".", "info", 3500);
    }

    return eValido;
}

function novo()
{
    $("#Codigo").val("");
    $("#CodigoOrigem").val("");
    $("#Sequencia").val("");

    $("#ParcelaX").val("");
    $("#ParcelaY").val("");

    $("#Valor").val("");
    $("#Vencimento").val("");
    $("#DataPagamento").val("");

    $("#Descricao").val("");
    $("#Fixa").val("");
    $("#Pago").val("");

    $("#Desconto").val("");
    $("#Juros").val("");
    $("#ValorPago").val("");
    $("#Observacao").val("");
    $("#CodigoOrigemSequencial").val("");    

    $("select[name='Favorecido']").val(0);
    $("select[name='MesReferencia']").val(0);
    $("select[name='AnoReferencia']").val(0);

    $("select[name='ContaBancaria']").val(0);
    $("select[name='FormaPagamentoRecebimento']").val(0);
    $("select[name='OrigemCadastro']").val(0);
    $("select[name='MatrizFilial']").val(0);

    $("select[name='TipoCobrancaRecebimento']").val(0);
    $("select[name='CentroCusto']").val(0);
    $("select[name='ContaContabil']").val(0);
    $("select[name='TipoRecebimentoPagamento']").val(0);

    limparCombos('Favorecido','0','');
    limparCombos('MesReferencia','0','');
    limparCombos('AnoReferencia','0','');
    limparCombos('ContaBancaria','0','');
    limparCombos('FormaPagamentoRecebimento','0','');
    limparCombos('OrigemCadastro', '0', '');
    limparCombos('MatrizFilial', '0', '');

    limparCombos('TipoCobrancaRecebimento', '0', '');
    limparCombos('CentroCusto', '0', '');
    limparCombos('ContaContabil', '0', '');
    limparCombos('TipoRecebimentoPagamento', '0', '');


    $("#Sucumbencia").val("");

    $('#divMensagemErro').hide();

    $('#grdBusca').hide();
       
}


function buscar()
{
    var busca = $('#frmContasPagar').serializeObject();
    var acao = JSON.stringify({ pModel: busca });

    $('#divMensagemErro').hide();

    ChamarControler('/ContasPagar/Buscar', 'POST', acao, function (data)
    {
        if (data.status == 200)
        {
            $('#grdBusca').html(data.responseText);
            $('#grdBusca').show();
        }
        else
        {
            $('#grdBusca').hide();            
            mostraDialogo('<strong>Erro</strong><br>Erro ao buscar', "info", 3500);
        }
    });


}

function detalhar(pCodigo)
{

    ChamarControler('/ContasPagar/PreencherCampos', 'POST', JSON.stringify({ pCodigo }), function (data) {
        if (data.status == 200)
        {
            var resultado = data.responseJSON;


            $("#Codigo").val(resultado.Codigo);
            $("#Sequencia").val(resultado.Sequencia);
                        
            
            $("#Valor").val(FormatoMoeda(resultado.Valor));
            $("#ValorPago").val(FormatoMoeda(resultado.ValorPago));

            $("#ParcelaX").val(resultado.ParcelaX);
            $("#ParcelaY").val(resultado.ParcelaY);


            $("#Vencimento").val(formatarData(resultado.Vencimento));
            $("#DataPagamento").val(formatarData(resultado.DataPagamento));

            $("#Descricao").val(resultado.Descricao);
            $("#Fixa").val(resultado.Fixa);
            $("#Pago").val(resultado.Pago);

            $("#Desconto").val(FormatoMoeda(resultado.Desconto));
            $("#Juros").val(FormatoMoeda(resultado.Juros));
            $("#Observacao").val(resultado.Observacao);
            

            $("select[name='Favorecido']").val(resultado.Favorecido);
            $("select[name='MesReferencia']").val(resultado.MesReferencia);
            $("select[name='AnoReferencia']").val(resultado.AnoReferencia);

            $("select[name='ContaBancaria']").val(resultado.ContaBancaria);
            $("select[name='FormaPagamentoRecebimento']").val(resultado.FormaPagamentoRecebimento);

            $("select[name='OrigemCadastro']").val(resultado.OrigemCadastro);
            $("select[name='MatrizFilial']").val(resultado.MatrizFilial);

            $("select[name='CentroCusto']").val(resultado.CentroCusto);
            $("select[name='ContaContabil']").val(resultado.ContaContabil);
            $("select[name='TipoCobrancaRecebimento']").val(resultado.TipoCobrancaRecebimento);
            $("select[name='TipoRecebimentoPagamento']").val(resultado.TipoRecebimentoPagamento);


            limparCombos('Favorecido', resultado.Favorecido, '');
            limparCombos('MesReferencia', resultado.MesReferencia, '');
            limparCombos('AnoReferencia', resultado.AnoReferencia, '');
            limparCombos('ContaBancaria', resultado.ContaBancaria, '');
            limparCombos('FormaPagamentoRecebimento', resultado.FormaPagamentoRecebimento, '');
            limparCombos('OrigemCadastro', resultado.OrigemCadastro, '');
            limparCombos('MatrizFilial', resultado.MatrizFilial, '');

            limparCombos('ContaContabil', resultado.ContaContabil, '');
            limparCombos('TipoCobrancaRecebimento', resultado.TipoCobrancaRecebimento, '');
            limparCombos('CentroCusto', resultado.CentroCusto, '');
            limparCombos('TipoRecebimentoPagamento', resultado.TipoRecebimentoPagamento, '');


            $("#CodigoOrigem").val(resultado.CodigoOrigem);
            $("#CodigoOrigemSequencial").val(resultado.CodigoOrigemSequencial);

            $("#Sucumbencia").val(resultado.Sucumbencia);

            $("#linkcomprovante").val(resultado.Codigo);

            $('#grdBusca').hide();
            
        }
        else
        {            
            mostraDialogo('<strong>Erro</strong><br>Erro ao detalhar registro.', "error", 3500);
        }
    });
}


//function imprimir(divID) {
//    var conteudo = document.getElementById(divID).innerHTML;
//    var win = window.open();
//    win.document.write(conteudo);
//    win.print();
//    //win.close();//Fecha após a impressão.  
//}



function imprimir2() {
   
    var busca = $('#frmContasPagar').serializeObject();
    var acao = JSON.stringify({ pModel: busca });

    $('#divMensagemErro').hide();

    ChamarControler('/ContasPagar/Imprimir', 'POST', acao, function (data) {
        if (data.status == 200)
        {
            
            var left = ($(window).width() / 2) - (1000 / 2);
            var top = ($(window).height() / 2) - (800 / 2);
            var mywindow = window.open('', 'Impressão', "width=1510,height=720,top=" + top + ",left=" + left);

            mywindow.document.write(data.responseText);

            mywindow.document.close();
            mywindow.focus();

            mywindow.print();

        }
        else
        {
         
            MostrarMensagem("Erro", "Erro ao Imprimir.");
        }
    });

}

function imprimir()
{
    var busca = $('#frmContasPagar').serializeObject();
    var acao = JSON.stringify({ pModel: busca });

    ChamarControler('/ContasPagar/Imprimir', 'POST', acao, function (data) {
        if (data.status == 200) {

            var left = ($(window).width() / 2) - (1000 / 2);
            var top = ($(window).height() / 2) - (800 / 2);
            var mywindow = window.open('', 'Impressão', "width=1510,height=720,top=" + top + ",left=" + left);

            mywindow.document.write(data.responseText);

            mywindow.document.close();
            mywindow.focus();

            mywindow.print();

        }
        else
        {
            MostrarMensagem("Erro", "Erro ao Imprimir.");
        }
    });
}

window.onload = function () {
    document.getElementById('frmContasPagar').onsubmit = function () {
        var fileInput = document.getElementById('fileInput');
        var vNome = document.getElementById('Codigo');


        if (vNome.value.length == 0) {            
            mostraDialogo('<strong>Erro</strong><br>Nome de arquivo inválido.', "info", 3500);
            return false;
        }

        
        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/ContasPagar/Upload');
        xhr.setRequestHeader('Content-type', 'multipart/form-data');
        //Appending file information in Http headers
        xhr.setRequestHeader('X-File-Name', fileInput.files[0].name);
        xhr.setRequestHeader('X-File-Type', fileInput.files[0].type);
        xhr.setRequestHeader('X-File-Size', fileInput.files[0].size);
        xhr.setRequestHeader('vNomeArquivo', vNome.value);        
       

        //Sending file in XMLHttpRequest
        xhr.send(fileInput.files[0]);
        xhr.onreadystatechange = function ()
        {
            if (xhr.readyState == 4 && xhr.status == 200)
            {                
                mostraDialogo('<strong>Upload</strong><br>Arquivo disponibilizado com sucesso.', "success", 3500);
            }
        }
        return false;
    }
}


function excluir() {

    $('#divMensagemErro').hide();

    var registro = $('#frmContasPagar').serializeObject();

    ChamarControler('/ContasPagar/Excluir', 'POST', JSON.stringify({ pCodigo: registro.Codigo }), function (data) {
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
                ChamarControler('/ContasPagar/BaixaLote', 'POST', JSON.stringify({ pCodigo: vConta }), function (data) {
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

function excluiLote() {
    var vQtdeFiltrado = $("#QtdeFiltrado").val();
    var vConta = 0;
    var vFazer = "";
    var vValida = true; //valida se poode baixar as contas por lote
    var vSucesso = true;
    var vQtdeExcluidos = 0;

    if (vValida) {
        for (var i = 0; i < vQtdeFiltrado; i++) {
            vConta = $("input[name=Codigo_" + i + "]").prop('value');
            vFazer = $("input[name=Codigo_" + i + "]").prop('checked');

            if (vFazer == true) {
               
                ChamarControler('/ContasPagar/Excluir', 'POST', JSON.stringify({ pCodigo: vConta }), function (data) {
                    if (data.status == 200) {
                        
                        vQtdeExcluidos++
                    }
                    else {
                        vSucesso = false;
                    }
                });

            }
        }

        if (vSucesso == true & vQtdeExcluidos > 0) {
            mostraDialogo('<strong>Aviso</strong><br>Exclusão feita com sucesso.', "info", 3500);
        }
        else {
            mostraDialogo('<strong>Aviso</strong><br>Houve algum erro ou você não selecinou registro.', "error", 3500);
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

function ImprimirDespesas() {
    var busca = $('#frmContasPagar').serializeObject();
    var acao = JSON.stringify({ pModel: busca });

    ChamarControler('/ContasPagar/ImprimirDespesas', 'POST', acao, function (data) {
        if (data.status == 200) {

            var left = ($(window).width() / 2) - (1000 / 2);
            var top = ($(window).height() / 2) - (800 / 2);
            var mywindow = window.open('', 'Impressão', "width=900,height=720,top=" + top + ",left=" + left);

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