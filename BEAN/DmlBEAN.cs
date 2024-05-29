using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEAN
{
    public class DmlBEAN
    {        
        public int Codigo { get; set; }
        public string CodigoLegado { get; set; }
        public int CodigoUsuario { get; set; }
        public int Empresa { get; set; }
        public string Dml { get; set; }     
        public int OrigemCodigo { get; set; }
        public string Origem { get; set; }
        public int OrigemCadastro { get; set; }
        public string Schema { get; set; }
        public DateTime DataRegistro { get; set; }
        public string Data { get; set; }
        public string Ativo { get; set; }
        public string Inativo { get; set; }
        public string Tabela { get; set; }
        public string CodigoAntigo { get; set; }
        public string SistemaOrigem { get; set; }
        public int? Tipo { get; set; }
        public string Sigla { get; set; }
    }
}
