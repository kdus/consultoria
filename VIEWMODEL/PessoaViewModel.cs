using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace MODELS
{

	public class PessoaViewModel : DmlViewModel
	{

		public string Fantasia { get; set; }
		public string Telefone { get; set; }
		public string Email { get; set; }
		public string CpfCnpj { get; set; }
		public string Ie { get; set; }
		public string EhCliente { get; set; }
		public string EhFornecedor { get; set; }
		public int? Vendedor { get; set; }
		public string EhFabricante { get; set; }
		public string EhFuncionario { get; set; }
		public string Endereco { get; set; }
		public string Numero { get; set; }
		public string Complemento { get; set; }
		public string Bairro { get; set; }
		public int? Cidade { get; set; }
		public int? Estado { get; set; }
		public string Cep { get; set; }
		public string Celular { get; set; }

		public string EhRequerente { get; set; }
		public string EhAssessoria { get; set; }

		public string BuscaEhRequerente { get; set; }
		public string BuscaEhAssessoria { get; set; }
		public string BuscaEhFuncionario { get; set; }
		public string BuscaCpf { get; set; }

		public string CODUSU { get; set; }

		public double RECEITAPECA { get; set; }
		public double RECEITASERVICO { get; set; }
		public double RECEITAPNEU { get; set; }
		public double PORCENTAGEMPECA { get; set; }
		public double PORCENTAGEMPSERVICO { get; set; }
		public double PORCENTAGEMPNEU { get; set; }
		public double CUSTOVARIAVELPECA { get; set; }
		public double CUSTOVARIAVELPNEU { get; set; }
		public double INDICEVENDAS { get; set; }
		public double INDICETICKETMEDIO { get; set; }
		public double TOTALFATPECA { get; set; }
		public double TOTALRECEITA { get; set; }



		public List<PessoaViewModel> Pessoa { get; set; }

		public List<PessoaViewModel> Usuarios { get; set; }
	}
}