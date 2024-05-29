using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class ResumoListaViewModel : ResumoViewModel
    {
        public List<ResumoViewModel> Contas { get; set; }

        public List<PessoaViewModel> Favorecidos { get; set; }
    }
}
