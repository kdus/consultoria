using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEAN;
using DaoMySql;
using System.Data;

using MODELS;

namespace BLL
{
    public class PedidoVendaBLL
    {
        FuncionalidadeBLL Funcionalidade = new FuncionalidadeBLL();
        public string Gravar(PedidoVendaViewModel pObjBean)
        {
            PedidoVendaDaoMySql objDao = new PedidoVendaDaoMySql();


            return objDao.Gravar(pObjBean);
        }

        //public void Excluir(PessoaViewModel pObjBean)
        //{
        //    PessoaDaoMySql objDao = new PessoaDaoMySql();
        //    objDao.Excluir(pObjBean);
        //}

        //public static List<PessoaViewModel> PopulaCombo()
        //{
        //    PessoaDaoMySql objDao = new PessoaDaoMySql();
        //    return objDao.PopulaCombo();
        //}

        public List<PedidoVendaViewModel> PopulaGrid(PedidoVendaViewModel PedidoVenda)
        {
            PedidoVendaDaoMySql objDao = new PedidoVendaDaoMySql();

             PedidoVenda.CODPES = PedidoVenda.CODPES;

			return objDao.PopulaGrid(PedidoVenda);
        }

        public DataTable PopulaGrid(string pFiltro)
        {
            PedidoVendaDaoMySql objDaoMySql = new PedidoVendaDaoMySql();
            return objDaoMySql.PopulaGrid(pFiltro);
        }
    }
}
