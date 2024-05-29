using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using BLL;
using MODELS;

namespace Aplicacao.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Enviar(string pPara, string pAssunto, string pCorpo)
        {
            FuncionalidadeBLL objBll = new FuncionalidadeBLL();
            EmailViewModel model = new EmailViewModel();           

            model.Status = objBll.Enviar(pPara, pAssunto, pCorpo, null, false);

            return Json(model);
        }
    }
}