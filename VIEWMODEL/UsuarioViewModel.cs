using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class UsuarioViewModel : DmlViewModel
    {
        public String Email { get; set; }
        public String Senha { get; set; }
        public String Foto { get; set; }

        public List<MenuViewModel> Menu { get; set; }
    }
}
