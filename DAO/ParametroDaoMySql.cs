using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Linq;
using Dapper;

using BEAN;
using MODELS;

namespace DaoMySql
{
    public class ParametroDaoMySql
    {
        FuncionalidadeDaoMySql Funcionalidade = new FuncionalidadeDaoMySql();

        public static string GetValorParametro(string pParametro, string pEmpresa)
        {
            StringBuilder strSql = new StringBuilder();
            string vRetorno = string.Empty;

            using (MySqlConnection con = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {
                try
                {
                    con.Open();

                    strSql.Append("SELECT CONPAR FROM PARAMETROS WHERE DSCPAR = '" + pParametro + "' AND CODEMP = " + pEmpresa);


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

        public static string GetValorParametro(string pParametro)
        {
            StringBuilder strSql = new StringBuilder();
            string vRetorno = string.Empty;

            using (MySqlConnection con = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {
                try
                {
                    con.Open();

                    strSql.Append("SELECT CONPAR FROM PARAMETROS WHERE DSCPAR = '" + pParametro + "'");


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

        #region PopulaGrid
        public DataTable PopulaGrid()
        {
            DataTable dtTabela = new DataTable();
            StringBuilder strSql = new StringBuilder();
            DmlBEAN pObjBean = new DmlBEAN();

            try
            {

                strSql.Append("SELECT * FROM PARAMETROS");
                              
                MySqlDataAdapter da = new MySqlDataAdapter(strSql.ToString(), ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN());

                da.Fill(dtTabela);

                return dtTabela;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception err0)
            {
                err0.GetHashCode();
                throw;
            }
        }
        #endregion

        public ParametroViewModel Registro(string pNome)
        {
            IEnumerable<ParametroViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                try
                {
                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine("SELECT A.CONPAR Conteudo ");
                    strSql.AppendLine("  FROM PARAMETROS   AS A ");

                    strSql.AppendLine(" where A.DSCPAR = '" + pNome + "'");

                    model = conexao.Query<ParametroViewModel>(strSql.ToString());
                }
                catch (Exception ex)
                {
                }
            }

            return model.FirstOrDefault();
        }

    }
}
