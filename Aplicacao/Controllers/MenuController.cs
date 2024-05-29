using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MODELS;
using BLL;

namespace Aplicacao.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult Menu(string pUsuario)
        {
            MenuViewModel model = new MenuViewModel();
            MenuBLL objBll = new MenuBLL();

            model.Menus = objBll.PopulaGrid(pUsuario);

            return PartialView("~/Views/Cadastros/Menu/grdMenu.cshtml", model);
        }
    }
}