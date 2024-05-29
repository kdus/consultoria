

function gravar() {
    var campo = document.getElementById("Codigo");
    campo.removeAttribute("disabled");
    habilitar("#TOTALFATPECA")
    habilitar("#TOTALRECEITA")

    if (validarCadastro())
    {        
        $('#divMensagemErro').hide();

        var registro = $('#frmPessoa').serializeObject();
         //registro.RECEITASERVICO = registro.RECEITASERVICO.replace("R$", "").replace(".", "").replace(",", ".").trim();
        //registro.CUSTOVARIAVELPECA = registro.CUSTOVARIAVELPECA.replace("R$", "").replace(".", "").replace(",", ".").trim();
        //registro.CUSTOVARIAVELPNEU = registro.CUSTOVARIAVELPNEU.replace("R$", "").replace(".", "").replace(",", ".").trim();
        //registro.INDICETICKETMEDIO = registro.INDICETICKETMEDIO.replace("R$", "").replace(".", "").replace(",", ".").trim();
      
        
        ChamarControler('/Pessoa/Gravar', 'POST', JSON.stringify({ pessoa: registro }), function (data)
        {
            if (data.status == 200)
            {
                $('#divMensagemErro').removeClass("alert-error");
                $('#divMensagemErro').addClass("alert-success");
                $('#msgErroSucesso').html("Registro gravado com sucesso.");
                $('#divMensagemErro').show();
            }
            else
            {
                
            }
        });
    }
    campo.disabled = true;
    desabilitar("#TOTALFATPECA")
    desabilitar("#TOTALRECEITA")
}


function Gravarusuario() {
    var campo = document.getElementById("Codigo");
    campo.removeAttribute("disabled");
   
    var registro = $('#frmUsuario').serializeObject();
    var registroPessoa = $('#frmPessoa').serializeObject();
    registro.pessoa = registroPessoa.Codigo

        ChamarControler('/Pessoa/Gravarusuario', 'POST', JSON.stringify({ pessoa: registro }), function (data) {
            if (data.status == 200) {
                $('#divMensagemErro').removeClass("alert-error");
                $('#divMensagemErro').addClass("alert-success");
                $('#msgErroSucesso').html("Registro gravado com sucesso.");
                $('#divMensagemErro').show();
            }
            else {

            }
        });
    campo.disabled = true;
}

function excluir() {
    
        $('#divMensagemErro').hide();

        var registro = $('#frmPessoa').serializeObject();

        ChamarControler('/Pessoa/Excluir', 'POST', JSON.stringify({ pessoa: registro }), function (data) {
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

function validarCadastro() {
    var eValido = true;

    if ($("#Nome").val() == "")
    {    
        eValido = false;
        $('#divMensagemErro').removeClass("alert-success");
        $('#divMensagemErro').addClass("alert-error");
        $('#msgErroSucesso').html("Nome");
        $('#divMensagemErro').show();

    }

    if ($("#CpfCnpj").val().replace("-", "").replace("-", "").replace("-", "").replace("_", "").replace("_", "").replace("_", "").replace("_", "").replace("_", "").replace("_", "").replace("_", "").replace("_", "").replace("_", "").replace("_", "").replace("_", "") == "")
    {       
        eValido = false;
        $('#divMensagemErro').removeClass("alert-success");
        $('#divMensagemErro').addClass("alert-error");
        $('#msgErroSucesso').html("CPF/CNPJ");
        $('#divMensagemErro').show();
    }

    return eValido;
}

function novo()
{
    $("#Codigo").val("");
    $("#Sequencia").val("");
    $("#CpfCnpj").val("");
    $("#Nome").val("");
    $("#Telefone").val("");
    $('input[name=EhRequerente]').prop('checked', false);
    $('input[name=EhAssessoria]').prop('checked', false);
    $('input[name=EhFornecedor]').prop('checked', false);
    $('input[name=EhFuncionario]').prop('checked', false);
    $("#RECEITAPECA").val("");
    $("#RECEITASERVICO").val("");
    $("#RECEITAPNEU").val("");
    $("#PORCENTAGEMPECA").val("");
    $("#PORCENTAGEMPSERVICO").val("");
    $("#PORCENTAGEMPNEU").val("");
    $("#CUSTOVARIAVELPECA").val("");
    $("#CUSTOVARIAVELPNEU").val("");
    $("#INDICEVENDAS").val("");
    $("#INDICETICKETMEDIO").val("");

    $('#divMensagemErro').hide();
    $('#grdBusca').hide();
}




function detalhar(pCodigo)
{
    var vRegistro = $("#tr" + pCodigo).data("registro");
      

    $("#Codigo").val(vRegistro.Codigo);
    $("#Sequencia").val(vRegistro.Sequencia);
    $("#Nome").val(vRegistro.Nome);
    $("#CpfCnpj").val(vRegistro.CpfCnpj);
    $("#Email").val(vRegistro.Email);
    $("#Telefone").val(vRegistro.Telefone);

    $("#RECEITAPECA").val(vRegistro.RECEITAPECA);
  

   
    $("#RECEITASERVICO").val(vRegistro.RECEITASERVICO);
    $("#RECEITAPNEU").val(vRegistro.RECEITAPNEU);
    $("#PORCENTAGEMPECA").val(vRegistro.PORCENTAGEMPECA);
    $("#PORCENTAGEMPSERVICO").val(vRegistro.PORCENTAGEMPSERVICO);
    $("#PORCENTAGEMPNEU").val(vRegistro.PORCENTAGEMPNEU);
    $("#CUSTOVARIAVELPECA").val(vRegistro.CUSTOVARIAVELPECA);
    $("#CUSTOVARIAVELPNEU").val(vRegistro.CUSTOVARIAVELPNEU);
    $("#INDICEVENDAS").val(vRegistro.INDICEVENDAS);
    $("#INDICETICKETMEDIO").val(vRegistro.INDICETICKETMEDIO);
    $("#TOTALFATPECA").val(vRegistro.TOTALFATPECA);
    $("#TOTALRECEITA").val(vRegistro.TOTALRECEITA);



    $('#grdBusca').hide();
    calcular();
}

function buscar()
{    
    var busca = $('#frmPessoa').serializeObject();
    var dataJson = JSON.stringify({ pessoa: busca });
    
    $('#divMensagemErro').hide();

    ChamarControler('/Pessoa/Buscar', 'POST', dataJson, function (data) {
        if (data.status == 200)
        {            
            $('#grdBusca').html(data.responseText);
            $('#grdBusca').show();
            
        }
        else
        {
            $('#grdBusca').hide();
            alert("Ocorreu erro ao buscar.")
        }
    });
   
    
}

function buscarusuario() {

    var campo = document.getElementById("Codigo");
    campo.removeAttribute("disabled");

    if (validarCadastro()) {
        $('#divMensagemErro').hide();

        var busca = $('#frmPessoa').serializeObject();
        var dataJson = JSON.stringify({ pessoa: busca });

        $('#divMensagemErro').hide();

        ChamarControler('/Pessoa/Buscarusuarios', 'POST', dataJson, function (data) {
            if (data.status == 200) {
                $('#grdusuarios').html(data.responseText);
                $('#grdusuarios').show();

            }
            else {
                $('#grdusuarios').hide();
                alert("Ocorreu erro ao buscar.")
            }
        });

    }
    campo.disabled = true;


}

function imprimir()
{
    var busca = $('#frmPessoa').serializeObject();
    var dataJson = JSON.stringify({ pessoa: busca });    

    ChamarControler('/Pessoa/Imprimir', 'POST', dataJson, function (data) {
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
           
        }
    });


}


function calcular() {
    var n1 = parseInt(document.getElementById('RECEITAPECA').value, 10);
    if (isNaN(n1)) {
        n1 = 0;
    }
    var n2 = parseInt(document.getElementById('RECEITASERVICO').value, 10);
    if (isNaN(n2)) {
        n2 = 0;
    }
    var n3 = parseInt(document.getElementById('RECEITAPNEU').value, 10);
    if (isNaN(n3)) {
        n3 = 0;
    }
    var totalFat = n1 + n2;
    var totalreceita = n1 + n2 + n3;
    $("#TOTALFATPECA").val(n1 + n3);
    $("#TOTALRECEITA").val(n1+n2+n3);

}



