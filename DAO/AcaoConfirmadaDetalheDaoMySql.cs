using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;

using MODELS;


namespace DaoMySql
{
    public class AcaoConfirmadaDetalheDaoMySql
    {
        FuncionalidadeDaoMySql Funcionalidade = new FuncionalidadeDaoMySql();

        public string Gravar(string pAcao, string pUsuario, string pAssessoria, string pConfirmacao)
        {
            using (MySqlConnection con = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {
                #region Incluir
                MySqlCommand cmd = null;
                StringBuilder strSql = new StringBuilder();
                string strRetorno = string.Empty;

                try
                {

                    #region Update
                    strSql.AppendLine("INSERT INTO ACAO_CONFIRMADA_DETALHE ");

                    strSql.AppendLine("      (        ");

                    strSql.AppendLine("        CODACACON ");
                    strSql.AppendLine("      , CODACA ");
                    strSql.AppendLine("      , CODUSUINC ");
                    strSql.AppendLine("      , CODASS ");
                    strSql.AppendLine("      , DTINC      ");
                    strSql.AppendLine("      , STAREG ");

                    strSql.AppendLine("      )  values (    ");

                    strSql.AppendLine("       @CODACACON ");
                    strSql.AppendLine("     , @CODACA ");
                    strSql.AppendLine("     , @CODUSUINC ");
                    strSql.AppendLine("     , @CODASS ");
                    strSql.AppendLine("     , NOW() ");
                    strSql.AppendLine("     , 'I'  ");
                                           
                    strSql.AppendLine("     ) ");

                    cmd = new MySqlCommand(strSql.ToString(), con);

                    cmd.Parameters.Add(new MySqlParameter("@CODACACON", pConfirmacao));
                    cmd.Parameters.Add(new MySqlParameter("@CODACA",    pAcao));
                    cmd.Parameters.Add(new MySqlParameter("@CODUSUINC", pUsuario));
                    cmd.Parameters.Add(new MySqlParameter("@CODASS",    pAssessoria));

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return "0";

                    #endregion

                }
                catch (MySqlException ex)
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

        public string Confirmar(string pAcao)
        {
            using (MySqlConnection con = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {
                #region Update
                MySqlCommand cmd = null;
                StringBuilder strSql = new StringBuilder();
                string strRetorno = string.Empty;

                try
                {

                    #region Update
                    strSql.AppendLine("UPDATE ACAO_CONFIRMADA_DETALHE ");

                    strSql.AppendLine("  SET   INDCONFIRMADA = 'S' ");
                    strSql.AppendLine("      , DTCONFIRMACAO = NOW() ");                          

                    strSql.AppendLine("  WHERE CODACACONDET = @CODIGO ");

                    cmd = new MySqlCommand(strSql.ToString(), con);

                    cmd.Parameters.Add(new MySqlParameter("@CODIGO", pAcao));

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return "0";

                    #endregion

                }
                catch (MySqlException ex)
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
        

        public List<AcaoConfirmadaViewModel> PopulaGrid(string pCodigo)
        {
            IEnumerable<AcaoConfirmadaViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                StringBuilder strSql = new StringBuilder();

                strSql.AppendLine("SELECT ACD.CODACACONDET  AS Codigo    ");
                strSql.AppendLine("     , A.NUMACA          AS Numeracao ");
                strSql.AppendLine("     , ACD.INDCONFIRMADA AS Ativo     ");//Confirmada

                strSql.AppendLine("     , R.NOMPES         AS RequerenteNome  ");
                strSql.AppendLine("     , R.CPFCNPJPES     AS RequerenteCpf   ");
                strSql.AppendLine("     , AC.INDENCERRADA  AS Encerrada       ");


                strSql.AppendLine("  FROM ACAO_CONFIRMADA_DETALHE AS ACD ");
                strSql.AppendLine(" INNER JOIN ACAO_CONFIRMADA    AS AC ON AC.CODACACON = ACD.CODACACON ");              
                strSql.AppendLine("  LEFT JOIN ACAO               AS A  ON ACD.CODACA = A.CODACA");
                strSql.AppendLine("  LEFT JOIN PESSOA             AS R  ON A.CODREQ  = R.CODPES");   //REQUERENTE 


                strSql.AppendLine(" where ACD.STAREG <> 'D' ");

                if (pCodigo != null)
                    strSql.AppendLine(" AND ACD.CODACACON = " + pCodigo);
                else
                    strSql.AppendLine(" AND ACD.CODACACON = 0 "); // administradores

                strSql.AppendLine("  order by ACD.CODACA ");

                model = conexao.Query<AcaoConfirmadaViewModel>(strSql.ToString());

            }

            return model.ToList();
        }

        
    }
}
