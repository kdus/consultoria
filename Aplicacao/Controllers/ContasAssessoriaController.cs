using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Aplicacao.Controllers
{
    public class ContasAssessoriaController : Controller
    {
        // GET: ContasAssessoria
        public ActionResult Index()
        {
            if (Session["session_Usuario"] == null)
                return View("~/Views/Usuario/Login.cshtml");          

            return View("~/Views/Financeiro/ContasAssessoria/Index.cshtml");
        }

        public ActionResult Menu()
        {
            return View("~/Views/Financeiro/ContasAssessoria/Menu.cshtml");
        }
    }
}