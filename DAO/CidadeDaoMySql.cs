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

    public class CidadeDaoMySql
    {
        FuncionalidadeDaoMySql Funcionalidade = new FuncionalidadeDaoMySql();

        #region Atribuir Valores
        void AtribuirValores(MySqlCommand cmd, CidadeBEAN pObjBean)
        {                        

            Funcionalidade.AtribuiValorCampo(cmd, "@NOMCID", pObjBean.Nome);
            Funcionalidade.AtribuiValorCampo(cmd, "@CODIGO", pObjBean.Codigo);

        }
        #endregion

        #region Dml
        public string Dml(CidadeBEAN pObjBean)
        {
            using (MySqlConnection con = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {
                #region Incluir
                MySqlCommand cmd = null;
                StringBuilder strSql = new StringBuilder();
                string strRetorno = string.Empty;


                try
                {
                    if (pObjBean.Codigo == 0)
                    {
                        strSql.Append("Insert Into CIDADE (");                        
                     
                        strSql.Append("    NOMCID ");
                        
                        strSql.Append(")  values (");
                        strSql.Append("    @NOMCID ");

                        strSql.Append(")");

                        cmd = new MySqlCommand(strSql.ToString(), con);

                        AtribuirValores(cmd, pObjBean);

                        con.Open();

                        cmd.ExecuteNonQuery();

                        return Funcionalidade.Sequence(con);
                    }
                    else
                    {
                        strSql.Append("UPDATE CIDADE  ");
                        strSql.Append("   SET NOMCID = @NOMCID");


                        strSql.Append(" WHERE CODCID = @CODIGO");

                        cmd = new MySqlCommand(strSql.ToString(), con);

                        AtribuirValores(cmd, pObjBean);

                        con.Open();
                        cmd.ExecuteNonQuery();

                        return pObjBean.Codigo.ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
                #endregion Incluir
            }
        }
        #endregion        

        #region Popula DataGrid
        public DataTable PopulaGrid(string pFiltro)
        {
            DataTable dtTabela = new DataTable();
            StringBuilder strSql = new StringBuilder();

            try
            {

                strSql.Append("select P.CODPRO    AS CODIGO ");
                strSql.Append("     , P.NOMPRO    AS NOME ");
                strSql.Append("     , P.NOMPROECF AS ECF ");
                
                strSql.Append("     , P.REFPRO    AS REFERENCIA ");

                strSql.Append("     , P.VLRVENPRO AS VENDA ");

                strSql.Append("     , P.CODNCM    AS COD_NCM ");
                strSql.Append("     , P.CODUNIMED AS COD_UNIMED ");

                strSql.Append("     , P.STAREG    AS STAREG ");

                strSql.Append("  FROM PRODUTO AS P  ");
                strSql.Append("  LEFT JOIN UNIMED AS U ON P.CODUNIMED = U.CODUNIMED  ");
                strSql.Append("  LEFT JOIN NCM    AS N ON P.CODNCM    = N.CODNCM  ");

                strSql.Append(" WHERE P.STAREG <> 'D' ");

                strSql.Append(pFiltro);

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

        #region Pesquisa Produto
        public DataTable Pesquisa(string pFiltro, string pEmpresa)
        {
            MySqlCommand cmd = null;
            DataTable dtTabela = new DataTable();
            MySqlConnection conn = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN();

            try
            {
                String strSql = "SELECT P.CODPRO, P.NOMPRO FROM PRODUTO P WHERE P.CODEMP = @EMPRESA AND P.NOMPRO LIKE CONCAT('%',@NOME, '%');";
                conn.Open();
                cmd = new MySqlCommand(strSql.ToString(), conn);

                Funcionalidade.AtribuiValorCampo(cmd, "@EMPRESA", pEmpresa);
                Funcionalidade.AtribuiValorCampo(cmd, "@NOME", pFiltro.ToUpper());

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                da.Fill(dtTabela);

                return dtTabela;
            }
            catch (MySqlException ex)
            {
                conn.Close();
                throw new Exception(ex.Message);
            }
            catch (Exception err0)
            {
                conn.Close();
                err0.GetHashCode();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        public DataTable Registro(string pCodigo)
        {
            MySqlCommand cmd = null;
            DataTable dtTabela = new DataTable();
            MySqlConnection conn = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN();

            try
            {
                String strSql = "SELECT P.* FROM CIDADE P WHERE P.CODCID = @CODIGO";
                conn.Open();
                cmd = new MySqlCommand(strSql.ToString(), conn);

                Funcionalidade.AtribuiValorCampo(cmd, "@CODIGO", pCodigo);


                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                da.Fill(dtTabela);

                return dtTabela;
            }
            catch (MySqlException ex)
            {
                conn.Close();
                throw new Exception(ex.Message);
            }
            catch (Exception err0)
            {
                conn.Close();
                err0.GetHashCode();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable RegistroPorReferencia(string pEmpresa, string pReferencia)
        {
            MySqlCommand cmd = null;
            DataTable dtTabela = new DataTable();
            MySqlConnection conn = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN();

            try
            {
                String strSql = "SELECT P.* FROM PRODUTO P WHERE P.REFPRO = @CODIGO AND P.CODEMP = @EMPRESA ";
                conn.Open();
                cmd = new MySqlCommand(strSql.ToString(), conn);

                Funcionalidade.AtribuiValorCampo(cmd, "@CODIGO", pReferencia);
                Funcionalidade.AtribuiValorCampo(cmd, "@EMPRESA", pEmpresa);


                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                da.Fill(dtTabela);

                return dtTabela;
            }
            catch (MySqlException ex)
            {
                conn.Close();
                throw new Exception(ex.Message);
            }
            catch (Exception err0)
            {
                conn.Close();
                err0.GetHashCode();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable RegistroPorNome(string pNome)
        {
            MySqlCommand cmd = null;
            DataTable dtTabela = new DataTable();
            MySqlConnection conn = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN();

            try
            {
                String strSql = "SELECT P.* FROM CIDADE P WHERE P.DSCCID = @NOME ";
                conn.Open();
                cmd = new MySqlCommand(strSql.ToString(), conn);

                Funcionalidade.AtribuiValorCampo(cmd, "@NOME", pNome);                

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);

                da.Fill(dtTabela);

                return dtTabela;
            }
            catch (MySqlException ex)
            {
                conn.Close();
                throw new Exception(ex.Message);
            }
            catch (Exception err0)
            {
                conn.Close();
                err0.GetHashCode();
                throw;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Excluir(string pCodigo)
        {
            using (MySqlConnection con = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {
                try
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();

                    sql.Append("UPDATE CIDADE SET STAREG = 'D' ");
                    sql.Append(" WHERE CODCID = " + pCodigo);

                    MySqlCommand cmd = new MySqlCommand(sql.ToString(), con);

                    cmd.ExecuteNonQuery();


                }
                catch (Exception ex)
                {
                    ex.GetHashCode();
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }

        }

        #region Método que retorna o nivel do usuario, parametro codigo
        public string GetNome(string pCodigo)
        {
            string strSql = null;            


            using (MySqlConnection conexaoOracleDSN = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {                               
                try
                {
                    conexaoOracleDSN.Open();

                    strSql = "SELECT NOMPRO FROM CIDADE WHERE CODCID = @CODIGO";

                    MySqlCommand cmd = new MySqlCommand(strSql, conexaoOracleDSN);

                    cmd.Parameters.Add(new MySqlParameter("@CODIGO", pCodigo));

                    return cmd.ExecuteScalar().ToString();


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

    }
}
