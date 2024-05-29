using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;

using BLL;
using MODELS;


namespace Aplicacao.Controllers
{
    public class MesController : Controller
    {

        
        public IEnumerable<MesViewModel> GetEstados()
        {
            MesBLL objBll = new MesBLL();
            return objBll.GetMeses();
        }

        public ActionResult Index()
        {
            GetEstados();
            return View();
        }
    }
}