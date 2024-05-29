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
    public class AcaoBLL
    {

        public string Gravar(AcaoViewModel pObjBean)
        {
            AcaoDaoMySql objDao = new AcaoDaoMySql();
            return objDao.Gravar(pObjBean);
        }

        public string AlterarAssessoria(string pAcao, string pAssessoria)
        {
            AcaoDaoMySql objDao = new AcaoDaoMySql();
            return objDao.AlterarAssessoria(pAcao, pAssessoria);
        }

        public List<AcaoViewModel> PopulaGrid(AcaoViewModel acao, string pOrderBy)
        {
            AcaoDaoMySql objDao = new AcaoDaoMySql();
            return objDao.PopulaGrid(acao, pOrderBy);
        }

        public AcaoViewModel Registro(string pCodigo)
        {
            AcaoDaoMySql objDao = new AcaoDaoMySql();
            return objDao.Registro(pCodigo);
        }

        public DataTable PopulaGrid(string pFiltro)
        {
            AcaoDaoMySql objDao = new AcaoDaoMySql();
            return objDao.PopulaGrid(pFiltro);
        }

        public void Excluir(AcaoViewModel pObjBean)
        {
            AcaoDaoMySql objDao = new AcaoDaoMySql();
            objDao.Excluir(pObjBean);
        }

        public string GravarAcaoConfirmada(string pUsuario, string pAssessoria)
        {
            AcaoDaoMySql objDao = new AcaoDaoMySql();
            return objDao.GravarAcaoConfirmada( pUsuario, pAssessoria);
        }               

        public List<AcaoConfirmadaViewModel> PopulaGridAcaoConfirmada(AcaoConfirmadaViewModel pAcao)
        {
            AcaoDaoMySql objDao = new AcaoDaoMySql();
            return objDao.PopulaGridAcaoConfirmada(pAcao);
        }
        public string UltimaAcaoAhConfirmar(string pCodigo)
        {
            AcaoDaoMySql objDao = new AcaoDaoMySql();
            return objDao.UltimaAcaoAhConfirmar(pCodigo);
        }

        public string AtualizarAssessoria(string pCodigo)
        {
            AcaoDaoMySql objDao = new AcaoDaoMySql();
            return objDao.AtualizarAssessoria(pCodigo);
        }
    }
}
