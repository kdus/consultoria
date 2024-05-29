using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

using System.Data;

using MySql.Data.MySqlClient;


namespace DaoMySql
{
    public class UpdateDaoMySql
    {
        private MySqlCommand ocrComando;

        private MySqlConnection ocrConexao;
        string strSql = null;

        #region Update 1
        public void Alterar(string pTabela, string pCampo, string pValor, string pPk, string pCodigo, string pTipoDado)
        {
            try
            {
                
                ocrComando = new MySqlCommand();
                ocrConexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient);
                ocrComando.Connection = ocrConexao;

                ocrComando.Connection = ocrConexao;
                ocrComando.CommandType = CommandType.Text;

                strSql = "update " + pTabela;
                if (pTipoDado == "S") //tipo STRING
                {
                    strSql += "   set " + pCampo + " = '" + pValor + "'";
                }
                else if (pTipoDado.Equals("N")) //tipo numerico
                {
                    strSql += "   set " + pCampo + " = " + Convert.ToInt32(pValor);
                }
                else
                {
                    strSql += "   set " + pCampo + " = " + pValor;
                }

                strSql += " where " + pPk + " = " + pCodigo;

                ocrComando.CommandText = strSql;

                ocrConexao.Open();
                ocrComando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                ocrConexao.Close();
            }
        }
        #endregion

        #region Update 1
        public void Alterar(string pTabela, string pWhere, string pSet)
        {
            //try
            //{
            //    ocrComando = new MySqlCommand();
            //    ocrConexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient);                
            //    ocrComando.Connection = ocrConexao;

            //    ocrComando.Connection = ocrConexao;
            //    ocrComando.CommandType = CommandType.Text;

            //    strSql = "update " + pTabela;
            //    strSql += "   set " + pSet;
            //    strSql += " where 1 = 1 AND " + pWhere;

            //    ocrComando.CommandText = strSql;

            //    ocrConexao.Open();
            //    ocrComando.ExecuteNonQuery();
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
            //finally
            //{
            //    ocrConexao.Close();
            //}

            using (MySqlConnection con = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {
                try
                {
                    con.Open();

                    StringBuilder strSql = new StringBuilder();

                    strSql.Append("update " + pTabela);
                    strSql.Append("   set " + pSet);
                    strSql.Append(" where 1 = 1 AND " + pWhere);

                    //preenche o objeto command
                    MySqlCommand cmd = new MySqlCommand(strSql.ToString(), con);


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
        #endregion

        #region Update 2
        public void Alterar(string pComando)
        {
            try
            {
                ocrComando = new MySqlCommand();
                ocrConexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient);
                ocrComando.Connection = ocrConexao;

                ocrComando.CommandType = CommandType.Text;

                ocrComando.CommandText = pComando;

                ocrConexao.Open();
                ocrComando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                ocrConexao.Close();
            }
        }
        #endregion
       
    }
}
