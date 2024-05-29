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
    public class PessoaController : Controller
    {
        // GET: Pessoa
        public ActionResult Index()
        {
            if (Session["session_Usuario"] == null)
                return View("~/Views/Usuario/Login.cshtml");

			PessoaViewModel model = new PessoaViewModel();

			PopulaCombos(model);
			return View("~/Views/Cadastros/Pessoa/Index.cshtml", model);
        }

		private void PopulaCombos(PessoaViewModel model)
		{

			model.Usuarios = FuncionalidadeBLL.PopulaCombos<PessoaViewModel>("USUARIO", "CODUSU", "NOMUSU", "NOMUSU", string.Empty).ToList();

        }



        [HttpPost]
		public JsonResult Gravarusuario(PessoaUsuarioViewModel pessoa)
        {
            PessoaBLL objBll = new PessoaBLL();

			PessoaUsuarioViewModel model = new PessoaUsuarioViewModel();
            
            objBll.Gravarusuario(pessoa);

			return Json(model);
		}

		public ActionResult Gravar(PessoaViewModel pessoa)
		{
			PessoaBLL objBll = new PessoaBLL();

			PessoaViewModel model = new PessoaViewModel();
			PopulaCombos(model);

			//pessoa.EhRequerente = pessoa.EhRequerente == "on" ? "S" : String.Empty;
			//pessoa.EhAssessoria = pessoa.EhAssessoria == "on" ? "S" : String.Empty;
			//pessoa.EhFornecedor = pessoa.EhFornecedor == "on" ? "S" : String.Empty;

			objBll.Gravar(pessoa);

			return View("~/Views/Cadastros/Pessoa/Index.cshtml", model);
		}

		[HttpPost]
        public ActionResult Excluir(PessoaViewModel pessoa)
        {
            PessoaBLL objBll = new PessoaBLL();

            PessoaViewModel model = new PessoaViewModel();
            pessoa.MatrizFilial = 2;

            pessoa.EhRequerente = pessoa.EhRequerente == "on" ? "S" : String.Empty;
            pessoa.EhAssessoria = pessoa.EhAssessoria == "on" ? "S" : String.Empty;

            objBll.Excluir(pessoa);

            return View("~/Views/Cadastros/Pessoa/Index.cshtml");
        }

        [HttpPost]
        public PartialViewResult Buscar(PessoaViewModel pessoa)
        {
            PessoaViewModel model = new PessoaViewModel();
            PessoaBLL objBll = new PessoaBLL();

            model.Pessoa = objBll.PopulaGrid(pessoa);

            return PartialView("~/Views/Cadastros/Pessoa/Grid.cshtml", model);
        }

        public PartialViewResult Buscarusuarios(PessoaViewModel pessoa)
        {
            PessoaViewModel model = new PessoaViewModel();
            PessoaBLL objBll = new PessoaBLL();

            model.Pessoa = objBll.BuscaUsuarios(pessoa);

            return PartialView("~/Views/Cadastros/Pessoa/GridUsuario.cshtml", model);
        }

        

        [HttpPost]
        public PartialViewResult Imprimir(PessoaViewModel pessoa)
        {
            PessoaViewModel model = new PessoaViewModel();
            PessoaBLL objBll = new PessoaBLL();

            model.Pessoa = objBll.PopulaGrid(pessoa);


            model.FiltrosUtilizados = "<br /><br />Filtros Utilizados";

            if (pessoa.BuscaNome != null)
                model.FiltrosUtilizados += "<br />NOME: " + pessoa.BuscaNome;

            if (pessoa.BuscaEhAssessoria != null)
                model.FiltrosUtilizados += "<br />Assessoria: SIM";

            if (pessoa.BuscaEhRequerente != null)
                model.FiltrosUtilizados += "<br />Requerente: SIM";

            return PartialView("~/Views/Cadastros/Pessoa/Imprimir.cshtml", model);
        }


    }
}