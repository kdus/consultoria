function gravar()
{
    if (validarCadastro())
    {
        $('#divMensagemErro').hide();

        var registro = $('#frmAcaoConfirmada').serializeObject();

        ChamarControler('/AcaoConfirmada/Gravar', 'POST', JSON.stringify({ pModel: registro }), function (data)
        {
            if (data.status == 200)
            {
                MostrarMensagem("Sucesso", "Registro gravado com sucesso.");
            }
            else
            {
                MostrarMensagem("Erro", "Erro ao gravar registro.");
            }
        });
    }
}

function validarCadastro()
{
    var eValido = true;

    if ($("#Numeracao").val() == "")
    {
        eValido = false;
        MostrarMensagem("Erro", "Preencha o campo Numeração.");
    }   

    return eValido;
}

function EncerrarConfirmacao()
{
    var eValido = true;

    if ($("#Codigo").val() == "")
    {
        eValido = false;
        mostraDialogo('<strong>Erro</strong><br>Por favor, selecione uma Relação.', "error", 3500);
    }

    if (eValido)
    {
        habilitar("#Codigo");

        var registro = $('#frmAcaoConfirmada').serializeObject();

        ChamarControler('/AcaoConfirmada/EncerrarConfirmacao', 'POST', JSON.stringify({ pModel: registro }), function (data)
        {
            if (data.status == 200)
            {
                var resultado = data.responseJSON;

                mostraDialogo('<strong>Sucesso</strong><br>Encerrramento efetuado com sucesso.', "success", 3500);
            }
            else {
                mostraDialogo('<strong>Erro</strong><br>Erro ao encerrar.', "error", 3500);
            }
        });       

        desabilitar("#Codigo");
    }

    return eValido;
}





function novo()
{
    $("#Codigo").val("");
    $("#AssessoriaNome").val("");
    $("#Numeracao").val("");
    $("#ExpiraEm").val("");
    


    $('#divMensagemErro').hide();

    $('#grdBusca').hide();

    $('#Abas').hide();
    $('#grdContasPagar').hide();
    $('#grdAcaoProcesso').hide();
    
}


function buscar()
{
    var busca = $('#frmAcaoConfirmada').serializeObject();
    var dataJson = JSON.stringify({ pModel: busca });

    $('#divMensagemErro').hide();
    //$('#Abas').hide();
    $('#grdAcaoProcesso').hide();

    ChamarControler('/AcaoConfirmada/Buscar', 'POST', dataJson, function (data)
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
    habilitar("#Codigo");

    var busca = $('#frmAcaoConfirmada').serializeObject();

    busca.Codigo = pCodigo;

    var dataJson = JSON.stringify({ pModel: busca }); 
    

    $('#divMensagemErro').hide();
    //$('#Abas').hide();

    ChamarControler('/AcaoConfirmada/PreencherCampos', 'POST', dataJson, function (data)
    {
        if (data.status == 200)
        {
            var resultado = data.responseJSON;

            $("#Codigo").val(resultado.Codigo);
            $("#AssessoriaNome").val(resultado.AssessoriaNome);
            $("#ExpiraEm").val(formatarData(resultado.ExpiraEm));
            $("#EmailPara").val(resultado.AssessoriaEmail);

            if (resultado.Encerrada == "S")
                $('input[name=Encerrada]').prop('checked', true);
            else
                $('input[name=Encerrada]').prop('checked', false);

            $('#grdBusca').hide();

            $('#Abas').hide();

            ChamarControler('/AcaoConfirmada/BuscarDetalhe', 'POST', JSON.stringify({ pCodigo }), function (data)            
            {
                if (data.status == 200) {
                    $('#grdAcaoProcesso').html(data.responseText);
                    $('#Abas').show();
                    $('#grdAcaoProcesso').show();
                }
                else
                {
                    $('#grdBusca').hide();
                    MostrarMensagem("Erro", "Erro ao buscar.");
                }
            });
        }
        else
        {
            $('#grdBusca').hide();
            MostrarMensagem("Erro", "Erro ao buscar.");
        }
    });

    desabilitar("#Codigo");
}

function imprimir(divID) {
    var conteudo = document.getElementById(divID).innerHTML;
    var win = window.open();
    win.document.write(conteudo);
    win.print();
    //win.close();//Fecha após a impressão.  
}



function pesquisar() {

    //popup contas a receber
    $("#BuscaRequerente").val("");
    $("#BuscaAssessoria").val("");
    $("#BuscaNumeracao").val("");
    $("#BuscaDataDe").val("");
    $("#BuscaDataAte").val("");
    $("#BuscaTipoAcao").val("");
   

}

function selecionarTodos() {
    var vQtdeFiltrado = $("#QtdeFiltrado").val();

    var vValida = $("#SelecionarTodos").prop('checked');


    for (var i = 0; i < vQtdeFiltrado; i++)
    {
        if (vValida == true)
            $("input[name=Codigo_" + i + "]").prop('checked', true);
        else
            $("input[name=Codigo_" + i + "]").prop('checked', false);
    }

}


function selecionarTodos2() {
    var vQtdeFiltrado = $("#QtdeFiltrado").val();

    var vValida = $("#SelecionarTodos2").prop('checked');


    for (var i = 0; i < vQtdeFiltrado; i++)
    {
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


    for (var i = 0; i < vQtdeFiltrado; i++)
    {
        vAssessoria = registro.AssessoriaParaConfirmar;
        vConta = $("input[name=Codigo_" + i + "]").prop('value');
        vFazer = $("input[name=Codigo_" + i + "]").prop('checked');

        if (vFazer == true) 
        {
            ChamarControler('/Processo/EnviarRelacao', 'POST', JSON.stringify({ pCodigo: vConta, pAssessoria: vAssessoria }), function (data)
            {
                if (data.status == 200)
                {
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

function Confirmar() {

    var registro = $('#frmConfirmar').serializeObject();

    var vQtdeFiltrado = $("#QtdeFiltrado").val();
    var vConta = 0;
    var vAssessoria = 0;
    var vFazer = "";


    for (var i = 0; i < vQtdeFiltrado; i++)
    {
        vAssessoria = registro.AssessoriaParaConfirmar;
        vConta = $("input[name=Codigo_" + i + "]").prop('value');
        vFazer = $("input[name=Codigo_" + i + "]").prop('checked');

        if (vFazer == true)
        {
            ChamarControler('/Processo/Confirmar', 'POST', JSON.stringify({ pCodigo: vConta }), function (data) {
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
