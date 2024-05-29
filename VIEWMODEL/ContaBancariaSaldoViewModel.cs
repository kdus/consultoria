using System;
using System.Collections.Generic;
using System.Text;


namespace MODELS
{
    public class ContaBancariaSaldoViewModel : DmlViewModel
    {


        public int? ContaBancaria { get; set; }
        public double Valor { get; set; }
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }       

    }
}
