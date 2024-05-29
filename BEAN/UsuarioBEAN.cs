using System;
using System.Collections.Generic;
using System.Text;

namespace BEAN
{
    public class UsuarioBEAN
    {
        protected static int codigo;
        public int Codigo
        {
            get { return codigo; }
            set { codigo = value; }
        }

        protected static int empresa;
        public int Empresa
        {
            get { return empresa; }
            set { empresa = value; }
        }

        public String Nome { get; set; }
        public String Email { get; set; }
        public String Senha { get; set; }
        public String Foto { get; set; }
        public int QuantidadeAcesso { get; set; }
        public String Nivel { get; set; }
        public int CodigoUsuarioAlt { get; set; }
        public String AutorizaPagamento { get; set; }
        public String RecebeAvisoCompra { get; set; }
        public String Administrador { get; set; }
        public String Cnpj { get; set; }
        public int MatrizFilial { get; set; }
        public int Assessoria { get; set; }
        public List<TelaBEAN> Telas { get; set; }
        public List<TelaBEAN> Menus { get; set; }
    }
}
