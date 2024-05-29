using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;

//using DAO;
using DaoMySql;

namespace BLL
{
    public class SelectBLL
    {
        #region Metodo que retorna a coluna de uma tabela
        public string Coluna(string pTabela, string pColuna, string pWhere)
        {          
            SelectDaoMySql objSelectDaoMy = new SelectDaoMySql();
            return objSelectDaoMy.Coluna(pTabela, pColuna, pWhere);            
        }
        #endregion

        #region Metodo que retorna a coluna de uma tabela
        public string Coluna(string pTabela, string pColuna, string pWhere, int pCodigo, string pTipoDado, string pFiltro)
        {
           
                SelectDaoMySql objSelectDaoMy = new SelectDaoMySql();
                return objSelectDaoMy.Coluna(pTabela, pColuna, pWhere, pCodigo, pTipoDado, pFiltro);
            

        }
        #endregion

        #region Metodo que retorna uma Tabela
        public DataTable Tabela(string pTabela, string pWhere)
        {            
            SelectDaoMySql objSelectDaoMy = new SelectDaoMySql();
            return objSelectDaoMy.Tabela(pTabela, pWhere);
            
        }

        public DataTable Tabela(string pComando)
        {
            SelectDaoMySql objSelectDaoMy = new SelectDaoMySql();
            return objSelectDaoMy.Tabela(pComando);
        }

        public DataTable Tabela(string pTabela, string pColuna, string pWhere, string pOrder)
        {
            SelectDaoMySql objSelectDaoMy = new SelectDaoMySql();
            return objSelectDaoMy.Tabela(pTabela, pColuna, pWhere, pOrder);
        }

        public DataTable TabelaColunas(string pColunas, string pTabela, string pWhere, string pOrderBy)
        {           
            SelectDaoMySql objSelectDaoMy = new SelectDaoMySql();
            return objSelectDaoMy.TabelaColunas(pColunas, pTabela, pWhere, pOrderBy);            
        }

        #endregion

        #region Metodo que retorna a coluna de uma tabela
        public int GetCodigoUsuario(string pFiltro)
        {          
            SelectDaoMySql objSelectMySqlDao = new SelectDaoMySql();
            return objSelectMySqlDao.GetCodigoUsuario(pFiltro);            
        }
        #endregion

        #region Metodo que retorna a coluna de uma tabela
        public string ExisteRegistro(string pTabela, string pWhere)
        {           
            SelectDaoMySql objSelectMySqlDao = new SelectDaoMySql();
            return objSelectMySqlDao.ExisteRegistro(pTabela, pWhere);            
        }
        #endregion

        #region Metodo que retorna uma Tabela
        public DataTable Comando(string pComando)
        {
            SelectDaoMySql objSelectMySqlDao = new SelectDaoMySql();
            return objSelectMySqlDao.Comando(pComando);
        }
        #endregion

    }
}
