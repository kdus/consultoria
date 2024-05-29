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
    public class AcaoConfirmadaDaoMySql
    {
        FuncionalidadeDaoMySql Funcionalidade = new FuncionalidadeDaoMySql();

        public string Gravar(AcaoConfirmadaViewModel pObjBean)
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
                    if (pObjBean.Codigo == 0)
                    {
                        strSql.AppendLine("INSERT INTO ACAO_CONFIRMADA ");

                        strSql.AppendLine("      (        ");

                        strSql.AppendLine("        CODUSUINC  ");
                        strSql.AppendLine("      , CODASS     ");
                        strSql.AppendLine("      , DTINC      ");
                        strSql.AppendLine("      , STAREG     ");
                        strSql.AppendLine("      , DTEXPIRA   ");
                        strSql.AppendLine("      , INDENCERRADA   ");

                        strSql.AppendLine("      )  values (  ");

                        strSql.AppendLine("        @CODUSUINC ");
                        strSql.AppendLine("      , @CODASS    ");
                        strSql.AppendLine("      , NOW()      ");
                        strSql.AppendLine("      , 'I'        ");
                        strSql.AppendLine("      , ADDDATE(NOW(), INTERVAL 3 DAY)");
                        strSql.AppendLine("      , 'N' ");

                        strSql.AppendLine("      ) ");

                        cmd = new MySqlCommand(strSql.ToString(), con);


                        cmd.Parameters.Add(new MySqlParameter("@CODUSUINC", pObjBean.CodigoUsuario));
                        cmd.Parameters.Add(new MySqlParameter("@CODASS", pObjBean.Assessoria));

                        con.Open();
                        cmd.ExecuteNonQuery();

                        return Funcionalidade.Sequence(con);

                    }
                    else
                    {
                        strSql.AppendLine("UPDATE ACAO_CONFIRMADA SET ");

                        strSql.AppendLine("       INDENCERRADA  = @INDENCERRADA   ");

                        strSql.AppendLine(" WHERE CODACACON = @CODIGO ");

                        cmd = new MySqlCommand(strSql.ToString(), con);

                        cmd.Parameters.Add(new MySqlParameter("@INDENCERRADA", pObjBean.Encerrada));
                        cmd.Parameters.Add(new MySqlParameter("@CODIGO", pObjBean.Codigo));

                        con.Open();
                        cmd.ExecuteNonQuery();

                        return pObjBean.Codigo.ToString();
                    }

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

        public string EncerrarConfirmacao(AcaoConfirmadaViewModel pObjBean)
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
                    strSql.AppendLine("UPDATE ACAO_CONFIRMADA      ");
                    strSql.AppendLine("   SET INDENCERRADA  = 'S'  ");
                    strSql.AppendLine(" WHERE CODACACON = @CODIGO  ");

                    cmd = new MySqlCommand(strSql.ToString(), con);

                    cmd.Parameters.Add(new MySqlParameter("@CODIGO", pObjBean.Codigo));

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return pObjBean.Codigo.ToString();
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

        public List<AcaoConfirmadaViewModel> PopulaGrid(AcaoConfirmadaViewModel pModel)
        {
            IEnumerable<AcaoConfirmadaViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                StringBuilder strSql = new StringBuilder();

                strSql.AppendLine("SELECT AC.CODACACON AS Codigo    ");
                strSql.AppendLine("     , AC.DTINC     AS Data      ");

                strSql.AppendLine("     , AC.DTEXPIRA  AS ExpiraEm   ");
                strSql.AppendLine("     , S.NOMPES     AS AssessoriaNome  ");

                strSql.AppendLine("  FROM ACAO_CONFIRMADA AS AC ");
                strSql.AppendLine("  LEFT JOIN PESSOA  AS S  ON AC.CODASS = S.CODPES");  // ASSESSORIA


                strSql.AppendLine(" where AC.STAREG <> 'D' ");

                if (pModel.Codigo > 0)
                    strSql.AppendLine(" AND AC.CODACACON = " + pModel.Codigo);

                if (pModel.BuscaDataDe != null && pModel.BuscaDataAte != null)
                    strSql.AppendLine(" AND AC.DTINC BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataAte) + "'");

                strSql.AppendLine("  order by AC.DTINC ");

                model = conexao.Query<AcaoConfirmadaViewModel>(strSql.ToString());

            }

            return model.ToList();
        }
    }
}
