using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEAN
{
    public class PessoaBEAN : DmlBEAN
    {        
        public string CodigoOld { get; set; }        
        public string CpfCnpj { get; set; }
        public string Ie { get; set; }
        public string Nome { get; set; }
        public string Fantasia { get; set; }
        public string Telefone { get; set; }        
        public string Celular { get; set; }        
        public string Email { get; set; }        
        public int? Operadora { get; set; }        
        public string Fax { get; set; }        
        public string Responsavel { get; set; }
        public int? ExecutivoConta { get; set; }        
        public string EhCliente { get; set; }        
        public string EhFornecedor { get; set; }        
        public string EhConstrutora { get; set; }        
        public string EhFuncionario { get; set; }       
        public string EhImobiliaria { get; set; }        
        public string EhAgencia { get; set; }        
        public string EhPromocao { get; set; }        
        public string EhIncorporadora { get; set; }        
        public string Endereco { get; set; }       
        public string Numero { get; set; }        
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public int? Cidade { get; set; }        
        public string Cep { get; set; }        
        public int? Estado { get; set; }        
        public int? Vendedor { get; set; }        
        public string EhFabricante { get; set; }
    }
}