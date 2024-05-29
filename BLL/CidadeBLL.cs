using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;

using BEAN;

using DaoMySql;

namespace BLL
{
    public class CidadeBLL
    {
        public string Dml(CidadeBEAN pObjBean)
        {
            if (pObjBean.Nome.Length == 0)
            {
                throw new Exception("Por favor, preencha o campo Nome.");                
            }

            CidadeDaoMySql objDaoMySql = new CidadeDaoMySql();
            return objDaoMySql.Dml(pObjBean);            
        }

        public void Excluir(string pCodigo)
        {
            CidadeDaoMySql objDaoMySql = new CidadeDaoMySql();
            objDaoMySql.Excluir(pCodigo);

        }

        public DataTable Registro(string pCodigo)
        {
            CidadeDaoMySql objDaoMySql = new CidadeDaoMySql();
            return objDaoMySql.Registro(pCodigo);
        }

        public DataTable RegistroPorReferencia(string pEmpresa, string pCodigo)
        {
            CidadeDaoMySql objDaoMySql = new CidadeDaoMySql();
            return objDaoMySql.RegistroPorReferencia(pEmpresa, pCodigo);
        }

        public DataTable PopulaGrid(string pFiltro)
        {
            CidadeDaoMySql objDaoMySql = new CidadeDaoMySql();
            return objDaoMySql.PopulaGrid(pFiltro);            
        }

        public static List<String> Pesquisa(string pFiltro, string pEmpresa)
        {

            CidadeDaoMySql objDaoMySql = new CidadeDaoMySql();

            DataTable dt = objDaoMySql.Pesquisa(pFiltro, pEmpresa);

            List<String> resultato = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                resultato.Add(string.Format("{0};{1}", row["NOMPRO"], row["CODPRO"]));
            }

            return resultato;
        }

        public string GetNome(string pCodigo)
        {
            CidadeDaoMySql objDaoMySql = new CidadeDaoMySql();
            return objDaoMySql.GetNome(pCodigo);
        }
    }
}
