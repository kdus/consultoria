using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;

using BEAN;

namespace DaoMySql
{
    public class SelectDaoMySql
    {

        #region Metodo que retorna uma coluna de uma tabela
        //Devo excluir o pFiltro ou mudar a string completa
        public string Coluna(string pTabela, string pColuna, string pWhere)
        {
            StringBuilder strSql = new StringBuilder();
            string vRetorno = string.Empty;

            using (MySqlConnection con = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {
                try
                {
                    con.Open();


                    strSql.Append("SELECT " + pColuna + " as COLUNA FROM " + pTabela + " WHERE 1 = 1 " + pWhere);


                    MySqlCommand cmd = new MySqlCommand(strSql.ToString(), con);

                    try
                    {
                        vRetorno = cmd.ExecuteScalar().ToString();

                        if (vRetorno.Equals("Null"))
                            return "";
                        else
                            return vRetorno;
                    }
                    catch
                    {
                        return "";
                    }


                }
                catch (MySqlException erro0)
                {
                    throw new Exception(erro0.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }
        #endregion

        #region Metodo que retorna uma coluna de uma tabela
        //Devo excluir o pFiltro ou mudar a string completa
        public string Coluna(string pTabela, string pColuna, string pWhere, int pCodigo, string pTipoDado, string pFiltro)
        {
            StringBuilder strSql = new StringBuilder();
            string vRetorno = string.Empty;

            using (MySqlConnection conexaoOracleDSN = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {
                try
                {
                    conexaoOracleDSN.Open();

                    if (pTipoDado == "NUMERO")
                    {
                        strSql.Append("SELECT " + pColuna + " as COLUNA FROM " + pTabela + " WHERE " + pWhere + " = " + pCodigo);
                    }
                    else if (!pWhere.Equals(" "))
                    {
                        if (pWhere.Length > 0)
                            strSql.Append("SELECT " + pColuna + " as COLUNA FROM " + pTabela + " WHERE " + pWhere + " = '" + pFiltro + "'");
                        else
                            strSql.Append("SELECT " + pColuna + " as COLUNA FROM " + pTabela);
                    }
                    else
                    {
                        strSql.Append("SELECT " + pColuna + " as COLUNA FROM " + pTabela);
                    }

                    MySqlCommand cmd = new MySqlCommand(strSql.ToString(), conexaoOracleDSN);
                    

                    try
                    {
                        vRetorno = cmd.ExecuteScalar().ToString();

                        if (vRetorno.Equals("Null"))
                            return "";
                        else
                            return vRetorno;
                    }
                    catch
                    {
                        return "";
                    }


                }
                catch (MySqlException erro0)
                {
                    throw new Exception(erro0.Message);
                }
                finally
                {
                    conexaoOracleDSN.Close();
                }
            }
        }
        #endregion

        #region Método que retorna o Codigo do Usuario
        public int GetCodigoUsuario(string pFiltro)
        {
            StringBuilder strSql = new StringBuilder();

            int vRetorno = 0;

            using (MySqlConnection conexaoOracleDSN = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {
                try
                {
                    strSql.Append("SELECT CODUSU ");
                    strSql.Append("  FROM USUARIO ");
                    strSql.Append(" where 1 = 1 AND STAREG <> 'D'");

                    strSql.Append(pFiltro);

                    conexaoOracleDSN.Open();

                    MySqlCommand cmd = new MySqlCommand(strSql.ToString(), conexaoOracleDSN);

                    vRetorno = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                    return vRetorno;

                }
                catch
                {
                    return 0;
                }
                finally
                {
                    conexaoOracleDSN.Close();
                }
            }
        }
        #endregion

        #region Tabela
        public DataTable Tabela(string pTabela, string pWhere)
        {
            try
            {
                DataTable tabela = new DataTable();
                StringBuilder strSql = new StringBuilder();

                strSql.Append("SELECT * FROM " + pTabela);

                if (pWhere.Length > 0)
                    strSql.Append(" WHERE " + pWhere);

                MySqlDataAdapter orcDa = new MySqlDataAdapter(strSql.ToString(), ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN());
                orcDa.Fill(tabela);
                return tabela;
            }
            catch (OleDbException orcE)
            {

                throw new Exception(orcE.Message + " " + orcE.GetHashCode());
            }
        }

        public DataTable Tabela(string pTabela)
        {
            try
            {
                DataTable tabela = new DataTable();
                
                MySqlDataAdapter orcDa = new MySqlDataAdapter(pTabela, ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN());
                orcDa.Fill(tabela);
                return tabela;
            }
            catch (OleDbException orcE)
            {

                throw new Exception(orcE.Message + " " + orcE.GetHashCode());
            }
        }


        public DataTable Tabela(string pTabela, string pColuna, string pWhere, string pOrder)
        {
            try
            {
                DataTable tabela = new DataTable();
                StringBuilder strSql = new StringBuilder();

                strSql.Append("SELECT " + pColuna + " from " + pTabela + " WHERE 1 = 1 " + pWhere + " order by " + pOrder);

                MySqlDataAdapter orcDa = new MySqlDataAdapter(strSql.ToString(), ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN());
                orcDa.Fill(tabela);
                return tabela;
            }
            catch (OleDbException orcE)
            {

                throw new Exception(orcE.Message + " " + orcE.GetHashCode());
            }
        }


        public DataTable TabelaColunas(string pColunas, string pTabela, string pWhere, string pOrderBy)
        {
            try
            {
                DataTable tabela = new DataTable();
                StringBuilder strSql = new StringBuilder();

                strSql.Append("SELECT " + pColunas + " FROM " + pTabela);

                if (pWhere.Length > 0)
                    strSql.Append(" WHERE " + pWhere);

                strSql.Append(pOrderBy);

                MySqlDataAdapter orcDa = new MySqlDataAdapter(strSql.ToString(), ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN());
                orcDa.Fill(tabela);
                return tabela;
            }
            catch (OleDbException orcE)
            {

                throw new Exception(orcE.Message + " " + orcE.GetHashCode());
            }
        }

        #endregion

        public string ExisteRegistro(string pTabela, string pWhere)
        {
            StringBuilder strSql = new StringBuilder();
            string vRetorno = string.Empty;

            using (MySqlConnection con = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {
                try
                {
                    con.Open();


                    strSql.Append("SELECT COUNT(*) AS QTDE FROM " + pTabela + " WHERE 1 = 1 " + pWhere);


                    MySqlCommand cmd = new MySqlCommand(strSql.ToString(), con);

                    try
                    {
                        vRetorno = cmd.ExecuteScalar().ToString();

                        return vRetorno;
                    }
                    catch 
                    {
                        return "";
                    }

                }
                catch (MySqlException erro0)
                {
                    throw new Exception(erro0.Message);
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public DataTable Comando(string pComando)
        {
            try
            {
                DataTable tabela = new DataTable();
         
                MySqlDataAdapter orcDa = new MySqlDataAdapter(pComando, ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN());
                orcDa.Fill(tabela);
                return tabela;
            }
            catch (OleDbException orcE)
            {

                throw new Exception(orcE.Message + " " + orcE.GetHashCode());
            }
        }
    }
}
