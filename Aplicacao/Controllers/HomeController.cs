using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BLL;
using MODELS;
using System.Data;

namespace Aplicacao.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(HomeViewModel pModel)
        {            
            HomeViewModel model = new HomeViewModel();

            if (Session["session_Usuario"] == null)
            {
                return View("~/Views/Usuario/Login.cshtml");
            }
            else
            {

                HomeBLL objHomeBll = new HomeBLL();
                //model = objHomeBll.Contas(pModel.MatrizFilial.ToString(), pModel.BuscaAno.ToString(), pModel.BuscaMes.ToString());

                PopulaCombos(model);

                return View("~/Views/Home/Index.cshtml", model);
            }
        }

        [HttpPost]
        public JsonResult Buscar(HomeViewModel pModel)
        {
            HomeViewModel model = new HomeViewModel();
            
            HomeBLL objHomeBll = new HomeBLL();
            model = objHomeBll.Vendas( pModel.MatrizFilial.ToString(), pModel.BuscaAno.ToString(), pModel.BuscaMes.ToString());

            PopulaCombos(model);

			return Json(model);
		}

        private void PopulaCombos(HomeViewModel model)
        {
            model.Meses = FuncionalidadeBLL.PopulaCombos<MesViewModel>("MES", "CODMES", "DSCMES", string.Empty, string.Empty).ToList();
            model.Ano = FuncionalidadeBLL.PopulaCombos<AnoViewModel>("ANO", "ANO", "ANO", "ANO DESC", string.Empty).ToList();
            model.Empresas = FuncionalidadeBLL.PopulaCombos<EmpresaViewModel>("EMPRESA", "CODEMP", "NOMEMP", string.Empty, string.Empty).ToList();
            model.Clientes = FuncionalidadeBLL.PopulaCombos<PessoaViewModel>("PESSOA", "CODPES", "NOMPES", "NOMPES", "AND STAREG <> 'D'").ToList();
	
		}
    }
}