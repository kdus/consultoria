using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;

using BEAN;
using DaoMySql;

using MODELS;

namespace BLL
{
    public class RelatorioGerencialBLL
    {
        #region PopulaGrid
        public DataTable PopulaGrid(string pFiltro)
        {
            RelatorioGerencialDaoMySql objDao = new RelatorioGerencialDaoMySql();
            return objDao.PopulaGrid(pFiltro); 
        }

        public DataTable RelatorioGerencial(string pFiltro, string pDtInicialVencto, string pDtFinalVencto, string pDtInicialPagto, string pDtFinalPagto, string pDtInicialRecto, string pDtFinalRecto)
        {
            RelatorioGerencialDaoMySql objDao = new RelatorioGerencialDaoMySql();
            return objDao.RelatorioGerencial(pFiltro, pDtInicialVencto, pDtFinalVencto, pDtInicialPagto, pDtFinalPagto, pDtInicialRecto, pDtFinalRecto);
        }
        #endregion

        public List<ResumoViewModel> PopulaGrid(ResumoViewModel conta)
        {
            RelatorioGerencialDaoMySql objDao = new RelatorioGerencialDaoMySql();
            return objDao.PopulaGrid(conta);
        }

        public List<ResumoViewModel> PopulaGridDetalhado(ResumoViewModel conta)
        {
            RelatorioGerencialDaoMySql objDao = new RelatorioGerencialDaoMySql();
            return objDao.PopulaGridDetalhado(conta);
        }

        public List<ResumoViewModel> PopulaGridDetalhadoII(ResumoViewModel conta)
        {
            RelatorioGerencialDaoMySql objDao = new RelatorioGerencialDaoMySql();
            return objDao.PopulaGridDetalhadoII(conta);
        }

    }
}
