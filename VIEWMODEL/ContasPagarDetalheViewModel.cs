using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class ContasPagarDetalheViewModel : DmlViewModel
    {
        public int? CodigoAcao { get; set; }
        public int? CodigoContasPagar { get; set; }
        public int? CodigoContasReceber { get; set; }
    }
}
