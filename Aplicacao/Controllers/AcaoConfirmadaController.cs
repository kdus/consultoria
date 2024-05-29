using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BEAN;
using BLL;
using MODELS;
using Comum;

namespace Aplicacao.Controllers
{
    public class AcaoConfirmadaController : Controller
    {
        // GET: AcaoConfirmada
        public ActionResult Index()
        {
            AcaoConfirmadaViewModel model = new AcaoConfirmadaViewModel();
            return View("~/Views/Cadastros/AcaoConfirmada/Index.cshtml", model);
        }

        [HttpPost]
        public JsonResult EncerrarConfirmacao(AcaoConfirmadaViewModel pModel)
        {
            AcaoConfirmadaViewModel model = new AcaoConfirmadaViewModel();
            AcaoConfirmadaBLL objBll = new AcaoConfirmadaBLL();

            objBll.EncerrarConfirmacao(pModel);

            AcaoBLL objAcaoBll = new AcaoBLL();
            objAcaoBll.AtualizarAssessoria(pModel.Codigo.ToString());

            return Json(model);
        }

        public ActionResult AcaoConfirmar()
        {

            AcaoConfirmadaListaViewModel model = new AcaoConfirmadaListaViewModel();
            AcaoBLL objBll = new AcaoBLL();

            string vUltimaAcao = objBll.UltimaAcaoAhConfirmar((((UsuarioBEAN)Session["session_Usuario"]).Assessoria.ToString()));

            AcaoConfirmadaDetalheBLL objAconfirmadaDetalheBll = new AcaoConfirmadaDetalheBLL();

            model.Acao = objAconfirmadaDetalheBll.PopulaGrid(vUltimaAcao);

            return PartialView("~/Views/Cadastros/AcaoConfirmada/GridItemAssessoria.cshtml", model);
        }

        [HttpPost]
        public PartialViewResult Buscar(AcaoConfirmadaViewModel pModel)
        {
            AcaoConfirmadaListaViewModel model = new AcaoConfirmadaListaViewModel();
            AcaoBLL objBll = new AcaoBLL();

            //if ((((UsuarioBEAN)Session["session_Usuario"]).Nivel).Equals("A"))
            
            pModel.BuscaConfirmada    = pModel.BuscaConfirmada == "on" ? "S" : String.Empty;
            pModel.BuscaNaoConfirmada = pModel.BuscaNaoConfirmada == "on" ? "S" : String.Empty;

            model.Acao = objBll.PopulaGridAcaoConfirmada(pModel);

            return PartialView("~/Views/Cadastros/AcaoConfirmada/Grid.cshtml", model);
        }

        [HttpPost]
        public JsonResult PreencherCampos(AcaoConfirmadaViewModel pModel)
        {
            AcaoConfirmadaListaViewModel lista = new AcaoConfirmadaListaViewModel();
            AcaoConfirmadaViewModel model = new AcaoConfirmadaViewModel();
            AcaoBLL objBll = new AcaoBLL();

            lista.Acao = objBll.PopulaGridAcaoConfirmada(pModel);//usei o metodo de busca, pois nao preciso de outra busca, porem o retorno do busca é lista

            model = lista.Acao[0]; // aqui eu pego o indice 0, pois ele retorna uma linha só

            return Json(model);
            
        }

        [HttpPost]
        public PartialViewResult BuscarDetalhe(string pCodigo)
        {
            AcaoConfirmadaListaViewModel model = new AcaoConfirmadaListaViewModel();
            AcaoConfirmadaDetalheBLL objBll = new AcaoConfirmadaDetalheBLL();

            model.Acao = objBll.PopulaGrid(pCodigo);

            return PartialView("~/Views/Cadastros/AcaoConfirmada/GridItem.cshtml", model);
        }
    }
}