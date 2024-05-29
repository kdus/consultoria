using System;
using System.Collections.Generic;
using System.Text;


namespace MODELS
{
    public class ContaBancariaExtratoViewModel : DmlViewModel
    {
        public int? ContaBancaria { get; set; }
        public double Valor { get; set; }        
        public string Historico { get; set; }
        public string Favorecido { get; set; }
    }
}
