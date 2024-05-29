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
    public class AcaoConfirmadaBLL
    {    
        public string Gravar(AcaoConfirmadaViewModel pModel)
        {
            AcaoConfirmadaDaoMySql objDao = new AcaoConfirmadaDaoMySql();
            return objDao.Gravar(pModel);
        }

        public string EncerrarConfirmacao(AcaoConfirmadaViewModel pModel)
        {
            AcaoConfirmadaDaoMySql objDao = new AcaoConfirmadaDaoMySql();
            return objDao.EncerrarConfirmacao(pModel);
        }
    }
}
