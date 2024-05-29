$(document).ready(function () {

   

    "use strict";

    // AREA CHART
    //var area = new Morris.Area({
    //    element: 'revenue-chart',
    //    resize: true,
    //    data: [
    //      { y: '2011 Q1', item1: 2666, item2: 2666 },
    //      { y: '2011 Q2', item1: 2778, item2: 2294 },
    //      { y: '2011 Q3', item1: 4912, item2: 1969 },
    //      { y: '2011 Q4', item1: 3767, item2: 3597 },
    //      { y: '2012 Q1', item1: 6810, item2: 1914 },
    //      { y: '2012 Q2', item1: 5670, item2: 4293 },
    //      { y: '2012 Q3', item1: 4820, item2: 3795 },
    //      { y: '2012 Q4', item1: 15073, item2: 5967 },
    //      { y: '2013 Q1', item1: 10687, item2: 4460 },
    //      { y: '2013 Q2', item1: 8432, item2: 5713 }
    //    ],
    //    xkey: 'y',
    //    ykeys: ['item1', 'item2'],
    //    labels: ['Item 1', 'Item 2'],
    //    lineColors: ['#a0d0e0', '#3c8dbc'],
    //    hideHover: 'auto'
    //});

    // LINE CHART
    //var line = new Morris.Line({
    //    element: 'line-chart',
    //    resize: true,
    //    data: [
    //      { y: '2011 Q1', item1: 2666 },
    //      { y: '2011 Q2', item1: 2778 },
    //      { y: '2011 Q3', item1: 4912 },
    //      { y: '2011 Q4', item1: 3767 },
    //      { y: '2012 Q1', item1: 6810 },
    //      { y: '2012 Q2', item1: 5670 },
    //      { y: '2012 Q3', item1: 4820 },
    //      { y: '2012 Q4', item1: 15073 },
    //      { y: '2013 Q1', item1: 10687 },
    //      { y: '2013 Q2', item1: 8432 }
    //    ],
    //    xkey: 'y',
    //    ykeys: ['item1'],
    //    labels: ['Item 1'],
    //    lineColors: ['#3c8dbc'],
    //    hideHover: 'auto'
    //});

    //DONUT CHART

  
    var donut = new Morris.Donut({
       
              
        element: 'sales-chart',
        resize: true,
        colors: ["#3c8dbc", "#f56954", "#00a65a", "#ffa500"],
        data: [
            { label: "Faturamento Peças", value: 10 },
            { label: "Faturamento Serviços", value: 20 },
            { label: "QTD Atendimentos", value: 30 },
            { label: "Ticket Médio", value: 40 }
          

        ],
        hideHover: 'auto'
    });
    //BAR CHART
    var bar = new Morris.Bar({
        element: 'bar-chart',
        resize: true,
        data: [
          { y: 'Janeiro', a: 100 },
          { y: 'Fevereiro', a: 75 },
          { y: 'Março', a: 50  },
          { y: 'Abril', a: 75  },
          { y: 'Maio', a: 50  },
          { y: 'Junho', a: 75  }
       
        ],
        barColors: ['#00a65a', '#f56954'],
        xkey: 'y',
        ykeys: ['a'],
        labels: ['%'],
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
            selectElement.appendChild(optionElement);o
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



//function detalhar() {

//    var frm = $('#frmHome').serializeObject();
//    var acao = JSON.stringify({ pModel: frm });
    

//    ChamarControler('/Home/Index', 'POST', acao, function (data) {
//        if (data.status == 200)
//        {
            
//        }
//        else
//        {
           
//        }
//    });
//}


function detalhar()
{

    if (validarCadastro())
    {
        var frm = $('#frmHome').serializeObject();
        var acao = JSON.stringify({ pModel: frm });



        ChamarControler('/Home/Buscar', 'POST', acao, function (data) {
            if (data.status == 200) {
                $('#gdrContas').html(data.responseText);
                $('#gdrContas').show();
                
            }
            else
            {
                $('#gdrContas').hide();
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

