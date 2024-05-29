using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class AcaoListaViewModel
    {
        public List<AcaoViewModel> Acao { get; set; }
        public List<PessoaViewModel> AssessoriasParaConfirmar { get; set; }
    }
}
