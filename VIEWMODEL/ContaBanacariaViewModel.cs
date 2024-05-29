using System;
using System.Collections.Generic;
using System.Text;


namespace MODELS
{
    public class ContaBancariaViewModel
    {
        public int Codigo { get; set; }
        public String Nome { get; set; }
        public String Agencia { get; set; }
        public String Numero { get; set; }
        public String Digito { get; set; }
        public double Valor { get; set; }
    }
}

