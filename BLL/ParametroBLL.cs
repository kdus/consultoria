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
    public class ParametroBLL
    {     

        public ParametroViewModel Parametro(string pNome)
        {
            ParametroDaoMySql objDao = new ParametroDaoMySql();
            return objDao.Registro(pNome);
        }      
    }
}
