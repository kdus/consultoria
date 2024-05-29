using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace MODELS
{

	public class PedidoVendaViewModel : DmlViewModel
	{


		public int Id { get; set; }

		public DateTime Data { get; set; }

		public string caixa_n { get; set; }


		[DisplayName("CMV Peça")]
		public int? cmv_peca { get; set; }

		[DisplayName("CMV Pneu")]
		public int cmv_pneu { get; set; }

		[DisplayName("Serviço Terceiro")]
		public int servico_terceiro { get; set; }

		[DisplayName("Faturamento Peça")]
		public int faturamento_peca { get; set; }

		[DisplayName("Faturamento Venda")]
		public int faturamento_pneu { get; set; }

		[DisplayName("Faturamento Mão de Obra")]
		public int faturamento_maoobra { get; set; }

		[DisplayName("QTD de Veículo")]
		public int qtd_veiculo { get; set; }


		[DisplayName("Nome do Cliente")]
		public String Nomecliente { get; set; }

		public int CODUSU { get; set; }
		public int CODPES { get; set; }

		public string BuscaCliente { get; set; }

		public List<PedidoVendaViewModel> PedidoVenda { get; set; }

		public List<PessoaViewModel> Clientes { get; set; }

		public List<PessoaViewModel> Usuarios { get; set; }
	}

}



