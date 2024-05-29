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
    public class ContaBancariaController : Controller
    {
        // GET: ContaBancaria
        public ActionResult Index()
        {
            string vView = UsuarioBLL.Permissao(Session["session_Usuario"], (((UsuarioBEAN)Session["session_Usuario"]).Codigo.ToString()), "5", (((UsuarioBEAN)Session["session_Usuario"]).Nivel));

            if (vView.Length > 0)
                return View(vView);

            ContaBancariaViewModel model = new ContaBancariaViewModel();
            return View("~/Views/Financeiro/ContaBancaria/Index.cshtml", model);
        }      

        [HttpPost]
        public PartialViewResult Buscar(ContaBancariaViewModel pModel)
        {
            ContaBancariaListaViewModel model = new ContaBancariaListaViewModel();
            ContaBancariaBLL objBll = new ContaBancariaBLL();
                        
            model.Contas = objBll.PopulaGrid(pModel);

            return PartialView("~/Views/Financeiro/ContaBancaria/Grid.cshtml", model);
        }

        [HttpPost]
        public JsonResult PreencherCampos(string pCodigo)
        {
            ContaBancariaBLL objBll = new ContaBancariaBLL();
            ContaBancariaViewModel model = new ContaBancariaViewModel();
                        
            model = objBll.Registro(pCodigo);
            model.Valor = objBll.Saldo(pCodigo);

            return Json(model);
        }

        [HttpPost]
        public PartialViewResult BuscarSaldos(ContaBancariaViewModel pModel)
        {
            
            ContaBancariaSaldoListaViewModel saldos = new ContaBancariaSaldoListaViewModel();            
            ContaBancariaBLL objBll = new ContaBancariaBLL();

            saldos.ContaBancaria = pModel.Codigo;
            saldos.Saldos = objBll.PopulaGrid(saldos);

            return PartialView("~/Views/Financeiro/ContaBancaria/GridSaldo.cshtml", saldos);
        }

        [HttpPost]
        public PartialViewResult BuscarExtrato(ContaBancariaViewModel pModel)
        {
            ContaBancariaExtratoListaViewModel extratos = new ContaBancariaExtratoListaViewModel();
            ContaBancariaBLL objBll = new ContaBancariaBLL();

            extratos.ContaBancaria = pModel.Codigo;
            extratos.Extratos = objBll.PopulaGridExtrato(extratos.ContaBancaria.ToString());

            return PartialView("~/Views/Financeiro/ContaBancaria/GridExtrato.cshtml", extratos);
        }
    }
}