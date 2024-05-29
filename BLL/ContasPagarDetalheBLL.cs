using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BEAN;
using DaoMySql;

using MODELS;

namespace BLL
{
    public class ContasPagarDetalheBLL
    {
        #region DML 
        public string Gravar(ContasPagarDetalheViewModel pObjBean)
        {
            ContasPagarDetalheDaoMySql objDao = new ContasPagarDetalheDaoMySql();
            return objDao.Gravar(pObjBean);
        }
        #endregion

        public List<ContasPagarDetalheViewModel> PopulaGrid(ContasPagarDetalheViewModel conta)
        {
            ContasPagarDetalheDaoMySql objDao = new ContasPagarDetalheDaoMySql();
            return objDao.PopulaGrid(conta);
        }
    }
}
