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

        fgColor: Cores()

    });
 
    function Cores() {
        var valor = parseFloat($('#PorFat').val());
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

    var bar = new Morris.Bar({
        element: 'bar-chart2',
        resize: true,
        data: [
            { y: 'Total', a: 100 },
            { y: 'Despesas', a: 75 }
          

        ],
        barColors: ['#00a65a', '#f56954'],
        xkey: 'y',
        ykeys: ['a'],
        labels: ['%'],
        hideHover: 'auto'
    });





   

});


// Script para criar o gráfico de barras



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

    //var Totalfaturamento_peca = parseFloat($('#Totalfaturamento_peca').val());
    //var Totalfaturamento_maoobra = parseFloat($('#Totalfaturamento_maoobra').val()) || 0;
    //var Totalqtd_veiculo = parseFloat($('#Totalqtd_veiculo').val()) || 0;
    //var Totalticketmedio = parseFloat($('#Totalticketmedio').val()) || 0;

    //var donut = new Morris.Donut({


    //    element: 'sales-chart',
    //    resize: true,
    //    colors: ["#3c8dbc", "#f56954", "#00a65a", "#ffa500"],
    //    data: [
    //        { label: "Faturamento Peças", value: Totalfaturamento_peca },
    //        { label: "Faturamento Serviços", value: Totalfaturamento_maoobra },
    //        { label: "QTD Atendimentos", value: Totalqtd_veiculo },
    //        { label: "Ticket Médio", value: Totalticketmedio }


    //    ],
    //    hideHover: 'auto'
    //});


