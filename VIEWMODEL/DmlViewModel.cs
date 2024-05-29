using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODELS
{
    public class DmlViewModel
    {
        public int Codigo { get; set; }
        public int Empresa { get; set; }
        public string EmpresaNome { get; set; }
        public int Sequencia { get; set; }
        public string CodigoLegado { get; set; }

        protected static int codigousuario;
        public int CodigoUsuario
        {
            get { return codigousuario; }
            set { codigousuario = value; }
        }
        
        public String Nome { get; set; }
		public String BuscaNome { get; set; }

		public String BuscaData { get; set; }
		public String BuscaCodigoDe { get; set; }
        public String BuscaCodigoAte { get; set; }
        public int MatrizFilial { get; set; }
        public string Dml { get; set; }
        public int OrigemCodigo { get; set; }
        public int? CodigoOrigem { get; set; }
        public string Origem { get; set; }
        public int OrigemCadastro { get; set; }
        public string Schema { get; set; }
        public DateTime DataRegistro { get; set; }
        public DateTime? Data { get; set; }
        public string Ativo { get; set; }
        public string Inativo { get; set; }
        public string Tabela { get; set; }
        public string CodigoAntigo { get; set; }
        public string SistemaOrigem { get; set; }
        public int? Tipo { get; set; }
        public string Sigla { get; set; }
        public int? CodigoOrigemSequencial { get; set; }

        public string Arquivo { get; set; }

        public string FiltrosUtilizados { get; set; }
        public int TelaBotaoQueChamou { get; set; }
    }
}
