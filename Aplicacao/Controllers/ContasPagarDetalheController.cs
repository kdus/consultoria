using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BEAN;
using MODELS;
using BLL;

namespace Aplicacao.Controllers
{
    public class ContasPagarDetalheController : Controller
    {
        // GET: ContasPagarDetalhe
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Gravar(string pAcao, string pConta)
        {
            ContasPagarDetalheViewModel model = new ContasPagarDetalheViewModel();
            ContasPagarDetalheBLL objBll = new ContasPagarDetalheBLL();

            model.CodigoAcao = Convert.ToInt32(pAcao);
            model.CodigoContasPagar = Convert.ToInt32(pConta);
            model.CodigoUsuario = (((UsuarioBEAN)Session["session_Usuario"]).Codigo);

            model.Codigo = int.Parse(objBll.Gravar(model));            

            return Json(model);
        }
    }
}