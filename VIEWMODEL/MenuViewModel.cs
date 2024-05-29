using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class MenuViewModel
    { 

        public String Icone { get; set; }
        public int Codigo { get; set; }
        public String Menu { get; set; }
        public List<MenuViewModel> Menus { get; set; }
    }
}
