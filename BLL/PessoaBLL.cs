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
    public class PessoaBLL
    {
        FuncionalidadeBLL Funcionalidade = new FuncionalidadeBLL();
        public string Gravar(PessoaViewModel pObjBean)
        {
            PessoaDaoMySql objDao = new PessoaDaoMySql();

            pObjBean.CpfCnpj = pObjBean.CpfCnpj != null ? Funcionalidade.LimpaCnpjCpf(pObjBean.CpfCnpj) : pObjBean.CpfCnpj;

            return objDao.Gravar(pObjBean);
        }

		public string Gravarusuario(PessoaUsuarioViewModel pObjBean)
		{
			PessoaDaoMySql objDao = new PessoaDaoMySql();

			return objDao.Gravarusuario(pObjBean);
		}

		public void Excluir(PessoaViewModel pObjBean)
        {
            PessoaDaoMySql objDao = new PessoaDaoMySql();
            objDao.Excluir(pObjBean);
        }

        public static List<PessoaViewModel> PopulaCombo()
        {
            PessoaDaoMySql objDao = new PessoaDaoMySql();
            return objDao.PopulaCombo();
        }

        public List<PessoaViewModel> PopulaGrid(PessoaViewModel pessoa)
        {
            PessoaDaoMySql objDao = new PessoaDaoMySql();

            pessoa.BuscaCpf = pessoa.BuscaCpf != null ? Funcionalidade.LimpaCnpjCpf(pessoa.BuscaCpf) : pessoa.BuscaCpf;

            return objDao.PopulaGrid(pessoa);
        }

		public List<PessoaViewModel> BuscaUsuarios(PessoaViewModel pessoa)
		{
			PessoaDaoMySql objDao = new PessoaDaoMySql();
		
			return objDao.BuscaUsuarios(pessoa);
		}

		public DataTable PopulaGrid(string pFiltro)
        {
            PessoaDaoMySql objDaoMySql = new PessoaDaoMySql();
            return objDaoMySql.PopulaGrid(pFiltro);
        }
    }
}
