using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using MODELS;

using BEAN;
using BLL;
using System.Data;

namespace Aplicacao.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {

            //if (Session["session_Usuario"] != null)
            //{                
            //    return View();
            //}
            //else
            //{   
            Session["session_Usuario"] = null;             
            return RedirectToAction("Login");
            //}
        }
                
        public ActionResult Login(UsuarioViewModel usuario)
        {
            UsuarioBLL objBll = new UsuarioBLL();
            var pessoa = objBll.ListarPorNomeSenha(usuario.Nome, usuario.Senha);

            //if (pessoa == null)
            //    return HttpNotFound();
            
            if (pessoa == null)
            {            
                return View("~/Views/Usuario/Login.cshtml");
            }
            else
            {
                UsuarioBEAN objUsuarioViewModels = new UsuarioBEAN();
                objUsuarioViewModels.Codigo = pessoa.Codigo;
                objUsuarioViewModels.Nome = pessoa.Nome;
                objUsuarioViewModels.Foto = pessoa.Foto;
                objUsuarioViewModels.MatrizFilial = pessoa.MatrizFilial;
                objUsuarioViewModels.Nivel = pessoa.Nivel;
                objUsuarioViewModels.Assessoria = pessoa.Assessoria;

                MenuBLL objMenuBll = new MenuBLL();
                objUsuarioViewModels.Telas = objMenuBll.Telas(pessoa.Codigo.ToString(), pessoa.Nivel);
                objUsuarioViewModels.Menus = objMenuBll.Menus(pessoa.Codigo.ToString(), pessoa.Nivel);

                Session.Add("session_Usuario", objUsuarioViewModels);

                HomeViewModel model = new HomeViewModel();                
                HomeBLL objHomeBll = new HomeBLL();
                
                //model = objHomeBll.Contas(pessoa.MatrizFilial.ToString(), DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString());

                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult AcessoNaoPermitido()
        {
            //UsuarioBLL objBll = new UsuarioBLL();
            //var pessoa = objBll.ListarPorNomeSenha(usuario.Nome, usuario.Senha);

            return View("~/Views/Usuario/AcessoNaoPermitido.cshtml");
            
        }
    }
}