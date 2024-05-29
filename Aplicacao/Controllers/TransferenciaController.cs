using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MODELS;

namespace Aplicacao.Controllers
{
    public class TransferenciaController : Controller
    {
        // GET: Transferencia     
        public ActionResult Index()
        {
            TransferenciaViewModel model = new TransferenciaViewModel();
            return View("~/Views/Financeiro/Transferencia/Index.cshtml", model);
        }
    }
}