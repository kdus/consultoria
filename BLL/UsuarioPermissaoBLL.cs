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
    public class UsuarioPermissaoBLL
    {     

        public UsuarioPermissaoViewModel Registro(string pUsuario, String pTela)
        {
            UsuarioPermissaoDaoMySql objDao = new UsuarioPermissaoDaoMySql();
            return objDao.Registro(pUsuario, pTela);
        }      
    }
}
