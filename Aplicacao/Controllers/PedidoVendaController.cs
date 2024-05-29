using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BEAN;
using BLL;

using MODELS;

namespace Aplicacao.Controllers
{
    public class PedidoVendaController: Controller
    {
        // GET: PedidoVendas
        public ActionResult Index()
        {
            if (Session["session_Usuario"] == null)
                return View("~/Views/Usuario/Login.cshtml");

			PedidoVendaViewModel model = new PedidoVendaViewModel();

			PopulaCombos(model);
			
            return View("~/Views/Comercial/PedidoVenda/Index.cshtml", model);
        }


		private void PopulaCombos(PedidoVendaViewModel model)
		{
			if (!((UsuarioBEAN)Session["session_Usuario"]).Nivel.Equals("S")) //M DE MASTER
			{
				model.Clientes = FuncionalidadeBLL.PopulaCombos<PessoaViewModel>("PESSOA P", "P.CODPES", "P.NOMPES", "P.NOMPES", "AND STAREG <> 'D' and exists (select 1 from PESSOA_USUARIO PU where P.CODPES=PU.CODPES and PU.CODUSU=" + ((UsuarioBEAN)Session["session_Usuario"]).Codigo.ToString()+ ")").ToList();

			}
			else
			{
				model.Clientes = FuncionalidadeBLL.PopulaCombos<PessoaViewModel>("PESSOA", "CODPES", "NOMPES", "NOMPES", "AND STAREG <> 'D'").ToList();

			}

		}



		//[HttpPost]
		//      public ActionResult Gravar(PedidoVendaViewModel pedidovenda)
		//      {
		//	PedidoVendaBLL objBll = new PedidoVendaBLL();

		//	PedidoVendaViewModel model = new PedidoVendaViewModel();

		//          objBll.Gravar(pedidovenda);

		//	return View("~/Views/Comercial/PedidoVenda/Index.cshtml");
		//}


		public JsonResult Gravar(PedidoVendaViewModel pedidovenda)
		{

			PedidoVendaViewModel model = new PedidoVendaViewModel();
			PedidoVendaBLL objBll = new PedidoVendaBLL();

			pedidovenda.TelaBotaoQueChamou = 1;
			pedidovenda.CodigoUsuario = ((UsuarioBEAN)Session["session_Usuario"]).Codigo;

			model.Codigo = int.Parse(objBll.Gravar(pedidovenda));

			//PopulaCombos(model);

			return Json(model);
		}

		//[HttpPost]
		//public ActionResult Excluir(PessoaViewModel pessoa)
		//{
		//    PessoaBLL objBll = new PessoaBLL();

		//    PessoaViewModel model = new PessoaViewModel();
		//    pessoa.MatrizFilial = 2;

		//    pessoa.EhRequerente = pessoa.EhRequerente == "on" ? "S" : String.Empty;
		//    pessoa.EhAssessoria = pessoa.EhAssessoria == "on" ? "S" : String.Empty;

		//    objBll.Excluir(pessoa);

		//    return View("~/Views/Cadastros/Pessoa/Index.cshtml");
		//}

		[HttpPost]
        public PartialViewResult Buscar(PedidoVendaViewModel pedidovenda)
        {
			PedidoVendaViewModel model = new PedidoVendaViewModel();
			PedidoVendaBLL objBll = new PedidoVendaBLL();

            model.PedidoVenda = objBll.PopulaGrid(pedidovenda);

            return PartialView("~/Views/Comercial/PedidoVenda/Grid.cshtml", model);
        }

        //[HttpPost]
        //public PartialViewResult Imprimir(PessoaViewModel pessoa)
        //{
        //    PessoaViewModel model = new PessoaViewModel();
        //    PessoaBLL objBll = new PessoaBLL();

        //    model.Pessoa = objBll.PopulaGrid(pessoa);


        //    model.FiltrosUtilizados = "<br /><br />Filtros Utilizados";

        //    if (pessoa.BuscaNome != null)
        //        model.FiltrosUtilizados += "<br />NOME: " + pessoa.BuscaNome;

        //    if (pessoa.BuscaEhAssessoria != null)
        //        model.FiltrosUtilizados += "<br />Assessoria: SIM";

        //    if (pessoa.BuscaEhRequerente != null)
        //        model.FiltrosUtilizados += "<br />Requerente: SIM";

        //    return PartialView("~/Views/Cadastros/Pessoa/Imprimir.cshtml", model);
        //}


    }
}