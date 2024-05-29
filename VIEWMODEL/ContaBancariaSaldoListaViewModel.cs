using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class ContaBancariaSaldoListaViewModel : ContaBancariaSaldoViewModel
    {
        public List<ContaBancariaSaldoViewModel> Saldos { get; set; }
    }
}
