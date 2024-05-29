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
    public class AcaoConfirmadaDetalheBLL
    {

        public string Gravar(string pAcao, string pUsuario, string pAssessoria, string pConfirmacao)
        {
            AcaoConfirmadaDetalheDaoMySql objDao = new AcaoConfirmadaDetalheDaoMySql();
            return objDao.Gravar(pAcao, pUsuario, pAssessoria, pConfirmacao);
        }

        public List<AcaoConfirmadaViewModel> PopulaGrid(string pCodigo)
        {
            AcaoConfirmadaDetalheDaoMySql objDao = new AcaoConfirmadaDetalheDaoMySql();
            return objDao.PopulaGrid(pCodigo);
        }

        public string Confirmar(string pAcao)
        {
            AcaoConfirmadaDetalheDaoMySql objDao = new AcaoConfirmadaDetalheDaoMySql();
            return objDao.Confirmar(pAcao);
        } 
    }
}
