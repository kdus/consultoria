using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class ContasPagarListaViewModel : ContasPagarViewModel
    {
        public List<ContasPagarViewModel> Contas { get; set; }
    }
}
