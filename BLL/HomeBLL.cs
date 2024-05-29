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
    public class HomeBLL
    {
        public HomeViewModel Contas(string pEmpresa, string pAno, string pMes)
        {
            HomeDaoMySql objDao = new HomeDaoMySql();
            return objDao.Contas(pEmpresa, pAno, pMes);
        }

		public HomeViewModel Vendas(string pEmpresa, string pAno, string pMes)
		{
			HomeDaoMySql objDao = new HomeDaoMySql();
			return objDao.Vendas(pEmpresa, pAno, pMes);
		}
	}
}
