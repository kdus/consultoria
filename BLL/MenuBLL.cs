using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEAN;
using System.Data;
using DaoMySql;


using MODELS;

namespace BLL
{
    public class MenuBLL
    {
        public List<MenuViewModel> PopulaGrid(string pUsuario)
        {
            MenuDaoMySql objDao = new MenuDaoMySql();
            return objDao.PopulaGrid(pUsuario);
        }

        public List<TelaBEAN> Menus(string pUsuario, string pNivel)
        {
            MenuDaoMySql objDao = new MenuDaoMySql();
            List<TelaViewModel> Menus = new List<TelaViewModel>();
            Menus = objDao.PopulaGridMenus(pUsuario, pNivel);

            List<TelaBEAN> objMenusBean = new List<TelaBEAN>();
            TelaBEAN objMenu = new TelaBEAN();

            foreach (var item in Menus)
            {
                objMenu = new TelaBEAN();
                objMenu.Modulo = item.Menu;
                objMenu.Icone = item.Icone;
                objMenusBean.Add(objMenu);
            }

            return objMenusBean;
        }
        public List<TelaBEAN> Telas(string pUsuario, string pNivel)
        {
            MenuDaoMySql objDao = new MenuDaoMySql();
            List<TelaViewModel> Telas = new List<TelaViewModel>();
            Telas = objDao.PopulaGridTelas(pUsuario, pNivel);

            List<TelaBEAN> objTelasBean = new List<TelaBEAN>();
            TelaBEAN objTela = new TelaBEAN();

            foreach (var item in Telas)
            {
                objTela = new TelaBEAN();
                objTela.Tela = item.Tela;
                objTela.Modulo = item.Menu;
                objTela.Icone = item.Icone;
                objTela.Pagina = item.Pagina;
                objTelasBean.Add(objTela);
            }

            return objTelasBean;
        }
    }
}
