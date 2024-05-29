using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace Aplicacao
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			#region Cadastro
			routes.MapRoute("Pessoa", "Cadastros/Pessoa", new { controller = "Pessoa", action = "Index"});
			routes.MapRoute("Acao", "Cadastros/Acao", new { controller = "Acao", action = "Index" });
			routes.MapRoute("Processo", "Cadastros/Processo", new { controller = "Processo", action = "Index" });
			routes.MapRoute("AcaoConfirmada", "Cadastros/AcaoConfirmada", new { controller = "AcaoConfirmada", action = "Index" });
			routes.MapRoute("AcaoConfirmadaAssessoria", "Cadastros/AcaoConfirmada/Assessoria", new { controller = "AcaoConfirmada", action = "AcaoConfirmar" });

			#endregion


			#region Financeiro
			routes.MapRoute("ContasPagar", "Financeiro/ContasPagar/{pCodigo}", new { controller = "ContasPagar", action = "Index", pCodigo = UrlParameter.Optional });
			routes.MapRoute("ContasReceber", "Financeiro/ContasReceber/{pCodigo}", new { controller = "ContasReceber", action = "Index", pCodigo = UrlParameter.Optional });
			//routes.MapRoute("ContasReceberDetalhe", "Financeiro/ContasReceber/{codigo}", new { controller = "ContasReceber", action = "Detalhe", codigo = UrlParameter.Optional });
			routes.MapRoute("ContaBancaria", "Financeiro/ContaBancaria", new { controller = "ContaBancaria", action = "Index" });
			routes.MapRoute("Transferencia", "Financeiro/Transferencia", new { controller = "Transferencia", action = "Index" });
			routes.MapRoute("RelatorioGerencial", "Financeiro/RelatorioGerencial", new { controller = "RelatorioGerencial", action = "Index" });
			routes.MapRoute("RelatorioGerencialDetalhado", "Financeiro/RelatorioGerencialDetalhado", new { controller = "RelatorioGerencial", action = "Detalhado" });
			routes.MapRoute("ContasPagarImprimir", "Financeiro/ContasPagar/imprimir", new { controller = "ContasPagar", action = "Imprimir" });
			#endregion

			#region Usuario
			routes.MapRoute("AcessoNaoPermitido", "Usuario/AcessoNaoPermitido", new { controller = "Usuario", action = "AcessoNaoPermitido" });
			routes.MapRoute("Usuario", "Login", new { controller = "Usuario", action = "Login" });
			routes.MapRoute("UsuarioIndex", "Usuario/Index", new { controller = "Usuario", action = "Index" });
			#endregion

			#region Comercial
			routes.MapRoute("PedidoVenda", "Comercial/PedidoVenda", new { controller = "PedidoVenda", action = "Index" });
			//routes.MapRoute("Acao", "Cadastros/Acao", new { controller = "Acao", action = "Index" });
			//routes.MapRoute("Processo", "Cadastros/Processo", new { controller = "Processo", action = "Index" });
			//routes.MapRoute("AcaoConfirmada", "Cadastros/AcaoConfirmada", new { controller = "AcaoConfirmada", action = "Index" });
			//routes.MapRoute("AcaoConfirmadaAssessoria", "Cadastros/AcaoConfirmada/Assessoria", new { controller = "AcaoConfirmada", action = "AcaoConfirmar" });

			#endregion

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
	}
}
