//$(document).ready(function (){buscar()});


function gravar()
{
    if (validarCadastro())
    {
        $('#divMensagemErro').hide();

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

    $("#Descricao").val("");
    $("#Fixa").val("");
    $("#Pago").val("");

    $("#Desconto").val("");
    $("#Juros").val("");
    $("#ValorPago").val("");

    $("select[name='Favorecido']").val(0);
    $("select[name='MesReferencia']").val(0);
    $("select[name='AnoReferencia']").val(0);

    $("select[name='ContaBancaria']").val(0);
    $("select[name='FormaPagamentoRecebimento']").val(0);

    $('#divMensagemErro').hide();

    $('#grdBusca').hide();
}


function buscar()
{
    var busca = $('#frmContasPagar').serializeObject();
    var acao = JSON.stringify({ pModel: busca });

    $('#divMensagemErro').hide();

    ChamarControler('/ContaBancaria/Buscar', 'POST', acao, function (data)
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

    ChamarControler('/ContaBancaria/PreencherCampos', 'POST', JSON.stringify({ pCodigo }), function (data)
    {
        if (data.status == 200)
        {
            var resultado = data.responseJSON;
            
            $("#Codigo").val(resultado.Codigo);
            $("#Sequencia").val(resultado.Codigo);
            $("#Nome").val(resultado.Nome);
            $("#Agencia").val(resultado.Agencia);
            $("#Numero").val(resultado.Numero);
            $("#Digito").val(resultado.Digito);            
            $("#Valor").val(FormatoMoeda(resultado.Valor));
                        

            var busca = $('#frmContaBancaria').serializeObject();
            var dataJson = JSON.stringify({ pModel: busca });

            $('#divMensagemErro').hide();
            $('#Abas').hide();

            ChamarControler('/ContaBancaria/BuscarSaldos', 'POST', dataJson, function (data) {
                if (data.status == 200) {
                    $('#grdSaldos').html(data.responseText);
                    $('#Abas').show();
                    $('#grdSaldos').show();
                }
                else {
                    $('#grdSaldos').hide();
                    MostrarMensagem("Erro", "Erro ao buscar saldos.");
                }
            });

            ChamarControler('/ContaBancaria/BuscarExtrato', 'POST', dataJson, function (data) {
                if (data.status == 200) {
                    $('#grdExtrato').html(data.responseText);
                    $('#Abas').show();
                    $('#grdExtrato').show();
                }
                else {
                    $('#grdExtrato').hide();
                    MostrarMensagem("Erro", "Erro ao buscar extrato.");
                }
            });
            
        }
        else
        {
            MostrarMensagem("Erro", "Erro ao detalhar registro.");
        }
    });
}