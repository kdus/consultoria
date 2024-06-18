$(document).ready(function () {
    $("#PorFatPeca").knob({

    });
    $("#PorFatServico").knob({

    });
    $("#PorIndicador").knob({

    });
    $("#Porticketmedio").knob({

    });

  
    
    $("#PorFat").knob({

        fgColor: "#000000"

    });

  

    
    

    //var data = [
    //    { y: 'Receita', a: 100 },
    //    { y: 'Despesas', a: 75 }
    //];

    // Criando o gráfico
    var bar = new Morris.Bar({
        element: 'bar-chart',
        resize: true,
        data: [
            { y: 'Receita', a: 100 },
            { y: 'Despesas', a: 75 }
        ],
        xkey: 'y',
        ykeys: ['a'],
        labels: ['%'],
        barColors: function (row, series, type) {
            if (row.label === 'Receita') return '#00a65a';
            else if (row.label === 'Despesas') return '#d3d3d3';
        },
        hideHover: 'auto'
    });

 
  

});

document.addEventListener('DOMContentLoaded', function () {
    const currentYear = new Date().getFullYear();
    const currentMonth = ("0" + (new Date().getMonth() + 1)).slice(-2); // Formata o mês para MM
    const yearSelectElement = document.getElementById('BuscaAno');
    const monthSelectElement = document.getElementById('BuscaMes');
    const Empresa = document.getElementById('MatrizFilial');

    // Função para definir automaticamente a opção selecionada
    function setSelectedOption(selectElement, value) {
        let optionExists = false;
        for (let i = 0; i < selectElement.options.length; i++) {
            if (selectElement.options[i].value === value.toString()) {
                selectElement.options[i].selected = true;
                optionExists = true;
                break;
            }
        }
        if (!optionExists) {
            const optionElement = document.createElement('option');
            optionElement.value = value;
            optionElement.text = value;
            optionElement.selected = true;
            selectElement.appendChild(optionElement);
        }


    }

    // Definir o ano corrente como selecionado
    setSelectedOption(yearSelectElement, currentYear);

    // Definir o mês corrente como selecionado
    setSelectedOption(monthSelectElement, currentMonth);

    if (Empresa.options.length > 1) {
        Empresa.options[1].selected = true;
    }

    detalhar();
});


function Cores(value) {
    var valor = value
    var color;

    if (valor < 0) {
        color = "#007bff"; // Azul
        $("#PorFat").val(0);

    } else if (valor < 20) {
        color = "#007bff"; // Azul

    } else if (valor < 40) {
        color = "#ffc107"; // Amarelo
    } else {
        color = "#28a745"; // Green for values 66 - 100
    }
    return color;


}

function detalhar()
{

    if (validarCadastro())
    {
        var frm = $('#frmHome').serializeObject();
        var acao = JSON.stringify({ pModel: frm });



        ChamarControler('/Home/Buscar', 'POST', acao, function (data) {
        
            if (data.status == 200) {

                $("#total-faturamento-peca").text(data.responseJSON.Totalfaturamento_peca.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }));
                $("#total-faturamento-peca2").text("Faturamento Peça " + data.responseJSON.Totalfaturamento_peca.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }));
                $("#por-fat-peca").text(data.responseJSON.PorFatPeca.toFixed(2)+ "%");
                $("#PorFatPeca").val(data.responseJSON.PorFatPeca.toFixed(2)).trigger('change');

                $("#Totalfaturamento_maoobra").text(data.responseJSON.Totalfaturamento_maoobra.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }));
                $("#Totalfaturamento_maoobra2").text("Faturamento Serviço " + data.responseJSON.Totalfaturamento_maoobra.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }));
                $("#Por-Fat-Servico").text(data.responseJSON.PorFatServico.toFixed(2) + "%");
                $("#PorFatServico").val(data.responseJSON.PorFatServico.toFixed(2)).trigger('change');

                $("#Totalqtd_veiculo").text(data.responseJSON.Totalqtd_veiculo.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }));
                $("#Totalqtd_veiculo2").text("QTD Atendimento " + data.responseJSON.Totalqtd_veiculo.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }));
                $("#Por-Indicador").text(data.responseJSON.PorIndicador.toFixed(2) + "%");
                $("#PorIndicador").val(data.responseJSON.PorIndicador.toFixed(2)).trigger('change');

                $("#Totalticketmedio").text(data.responseJSON.Totalticketmedio.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }));
                $("#Totalticketmedio2").text("Ticket Médio " + data.responseJSON.Totalticketmedio.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }));
                $("#Total-ticketmedio").text(data.responseJSON.Porticketmedio.toFixed(2) + "%");
                $("#Porticketmedio").val(data.responseJSON.Porticketmedio.toFixed(2)).trigger('change');

                $("#TOTALFATURAMENTO").text("Total Faturamento " + data.responseJSON.TOTALFATURAMENTO.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' }));
                $("#PorFat").val(data.responseJSON.PorFat.toFixed(2)).trigger('change').trigger('configure', { 'fgColor': Cores(data.responseJSON.PorFat) });
                
              
            }
            else
            {
              
                mostraDialogo('<strong>Erro</strong><br>Erro ao buscar', "error", 3500);
            }
        });
    }

    
}


function validarCadastro()
{
    var eValido = true;
    var vPreenchimento = "";

    if ($("#MatrizFilial").val() == "")
    {
        vPreenchimento += " Empresa";
    }

    if ($("#BuscaAno").val() == "")
    {
        vPreenchimento += " Ano";
    }

    if ($("#BuscaMes").val() == "")
    {
        vPreenchimento += " Mês";
    }

    if (vPreenchimento.length > 0)
    {
        eValido = false;
        //mostraDialogo('<strong>Erro</strong><br>Preencha o(s) campos(s):' + vPreenchimento + ".", "error", 3500);
    }

    return eValido;
}

