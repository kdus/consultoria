using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BEAN
{
    public class CidadeBEAN : DmlBEAN
    {       
        private String nome;
        public String Nome
        {
            get { return nome; }
            set { nome = value; }
        }      
    }
}
