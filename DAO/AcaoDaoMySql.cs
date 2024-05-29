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
    public class AcaoDaoMySql
    {
        FuncionalidadeDaoMySql Funcionalidade = new FuncionalidadeDaoMySql();
        void AtribuirValores(MySqlCommand cmd, AcaoViewModel pObjBean)
        {                       

            Funcionalidade.AtribuiValorCampo(cmd, "@CODEMP", pObjBean.MatrizFilial);

            Funcionalidade.AtribuiValorCampo(cmd, "@VLRACA", pObjBean.Valor);
            Funcionalidade.AtribuiValorCampo(cmd, "@NUMACA", pObjBean.Numeracao);
            Funcionalidade.AtribuiValorCampo(cmd, "@DTACA", string.Format("{0:yyyy-MM-dd}", pObjBean.Data));

            Funcionalidade.AtribuiValorCampo(cmd, "@CODREQ", pObjBean.Requerente);
            Funcionalidade.AtribuiValorCampo(cmd, "@CODRESFIN", pObjBean.ResponsavelFinanceiro);
            Funcionalidade.AtribuiValorCampo(cmd, "@CODASS", pObjBean.Assessoria);
            Funcionalidade.AtribuiValorCampo(cmd, "@CODRES", pObjBean.Responsavel);
            Funcionalidade.AtribuiValorCampo(cmd, "@CODACATIP", pObjBean.Tipo);

            Funcionalidade.AtribuiValorCampo(cmd, "@USUARIO", pObjBean.CodigoUsuario);
            Funcionalidade.AtribuiValorCampo(cmd, "@CODACA", pObjBean.Codigo);

        }

        public string Gravar(AcaoViewModel pObjBean)
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
                        strSql.AppendLine("Insert Into ACAO (");

                        strSql.AppendLine("    CODEMP");
                        strSql.AppendLine("  , VLRACA ");
                        strSql.AppendLine("  , NUMACA ");
                        strSql.AppendLine("  , CODACASEQ ");
                        strSql.AppendLine("  , DTACA ");

                        strSql.AppendLine("  , CODREQ     ");                        
                        strSql.AppendLine("  , CODRESFIN  ");                        
                        strSql.AppendLine("  , CODASS     ");
                        strSql.AppendLine("  , CODRES     ");
                        strSql.AppendLine("  , CODACATIP  ");

                        strSql.AppendLine("  , STAREG");
                        strSql.AppendLine("  , DTINC");
                        strSql.AppendLine("  , DTALT");
                        strSql.AppendLine("  , CODUSUINC");
                        strSql.AppendLine("  , CODUSUALT");


                        strSql.AppendLine(")  values (");

                        strSql.AppendLine("    @CODEMP");
                        strSql.AppendLine("  , @VLRACA ");
                        strSql.AppendLine("  , @NUMACA");

                        strSql.AppendLine("  , NEXTVAL('SEQ_ACAO" + pObjBean.MatrizFilial + "') ");

                        strSql.AppendLine("  , @DTACA ");

                        strSql.AppendLine("  , @CODREQ     ");
                        strSql.AppendLine("  , @CODRESFIN  ");
                        strSql.AppendLine("  , @CODASS     ");
                        strSql.AppendLine("  , @CODRES     ");

                        strSql.AppendLine("  , @CODACATIP  ");

                        strSql.Append("  , 'I'");
                        strSql.Append("  , NOW()");
                        strSql.Append("  , NOW()");
                        strSql.Append("  , @USUARIO");
                        strSql.Append("  , @USUARIO");


                        strSql.Append(")");

                        cmd = new MySqlCommand(strSql.ToString(), con);

                        AtribuirValores(cmd, pObjBean);

                        con.Open();

                        cmd.ExecuteNonQuery();

                        return Funcionalidade.Sequence(con);
                    }
                    else
                    {
                        strSql.AppendLine("UPDATE ACAO  ");
                        strSql.AppendLine("   SET NUMACA = @NUMACA");

                        strSql.AppendLine("  , CODEMP = @CODEMP");
                        strSql.AppendLine("  , VLRACA = @VLRACA");

                        strSql.AppendLine("  , DTACA     = @DTACA ");
                        strSql.AppendLine("  , CODREQ    = @CODREQ     ");
                        strSql.AppendLine("  , CODRESFIN = @CODRESFIN  ");
                        strSql.AppendLine("  , CODASS    = @CODASS     ");
                        strSql.AppendLine("  , CODRES    = @CODRES     ");

                        strSql.AppendLine("  , CODACATIP = @CODACATIP  ");

                        strSql.AppendLine("     , DTALT = NOW()");
                        strSql.AppendLine("     , STAREG = 'U'");

                        strSql.AppendLine(" WHERE CODACA = @CODACA");

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

        public string AlterarAssessoria(string pAcao, string pAssessoria)
        {
            using (MySqlConnection con = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {
                #region Incluir
                MySqlCommand cmd = null;
                StringBuilder strSql = new StringBuilder();
                string strRetorno = string.Empty;

                try
                {                    
                    strSql.AppendLine("UPDATE ACAO  ");
                    strSql.AppendLine("   SET       ");
                    strSql.AppendLine("       CODASS = @CODASS  ");
                    strSql.AppendLine("     , DTALT  = NOW()        ");
                    strSql.AppendLine("     , STAREG = 'U'         ");
                    strSql.AppendLine(" WHERE CODACA = @CODIGO");

                    cmd = new MySqlCommand(strSql.ToString(), con);

                    cmd.Parameters.Add(new MySqlParameter("@CODASS", pAssessoria));
                    cmd.Parameters.Add(new MySqlParameter("@CODIGO", pAcao));

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return "0";                   
                    
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

        public AcaoViewModel Registro(string pCodigo)
        {
            IEnumerable<AcaoViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                try
                {
                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine("SELECT A.CODACA CODIGO ");

                    strSql.AppendLine("     , A.CODACASEQ  AS SEQUENCIA ");
                    strSql.AppendLine("     , A.DTACA      AS DATA ");
                    strSql.AppendLine("     , A.NUMACA     AS NUMERACAO ");
                    strSql.AppendLine("     , A.VLRACA     AS VALOR ");

                    strSql.AppendLine("     , A.CODREQ    AS REQUERENTE             ");
                    strSql.AppendLine("     , R.NOMPES    AS REQUERENTENOME         ");

                    strSql.AppendLine("     , A.CODRESFIN AS RESPONSAVELFINANCEIRO ");
                    strSql.AppendLine("     , RF.NOMPES    AS RESPONSAVELFINANCEIRONOME         ");

                    strSql.AppendLine("     , A.CODASS    AS ASSESSORIA             ");
                    strSql.AppendLine("     , S.NOMPES    AS REQUERENTENOME         ");

                    strSql.AppendLine("     , A.CODRES    AS RESPONSAVEL            ");
                    strSql.AppendLine("     , RA.NOMPES    AS RESPONSAVELNOME         ");

                    strSql.AppendLine("     , A.CODACATIP  AS Tipo");
                    strSql.AppendLine("     , AT.DSCACATIP AS TipoDescricao");

                    strSql.AppendLine("     , A.CODEMP          AS MatrizFilial");

                    strSql.AppendLine("  FROM ACAO         AS A ");
                    strSql.AppendLine("  LEFT JOIN PESSOA  AS R  ON A.CODREQ = R.CODPES");   //REQUERENTE
                    strSql.AppendLine("  LEFT JOIN PESSOA  AS RF ON A.CODRESFIN = RF.CODPES"); // RESPONSAVEL FINANCEIRO
                    strSql.AppendLine("  LEFT JOIN PESSOA  AS S  ON A.CODASS = S.CODPES");  // ASSESSORIA
                    strSql.AppendLine("  LEFT JOIN PESSOA  AS RA ON A.CODRES = RA.CODPES"); // RESPONSAVEL
                    strSql.AppendLine("  LEFT JOIN ACAO_TIPO AS AT ON A.CODACATIP = AT.CODACATIP ");// TIPO

                    strSql.AppendLine(" where A.STAREG <> 'D' AND A.CODACA = " + pCodigo);
                 

                    strSql.AppendLine("  order by A.NUMACA ");

                    model = conexao.Query<AcaoViewModel>(strSql.ToString());
                }
                catch (Exception ex)
                {
                }

            }

            return model.FirstOrDefault();
        }

        public List<AcaoViewModel> PopulaGrid(AcaoViewModel pModel, string pOrderBy)
        {
            IEnumerable<AcaoViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {                
                
                StringBuilder strSql = new StringBuilder();

                strSql.AppendLine("SELECT A.CODACA     AS CODIGO    ");
                strSql.AppendLine("     , A.CODACASEQ  AS SEQUENCIA ");
                strSql.AppendLine("     , A.DTACA      AS DATA      ");
                strSql.AppendLine("     , A.NUMACA     AS NUMERACAO ");

                strSql.AppendLine("     , A.CODREQ     AS REQUERENTE      ");
                strSql.AppendLine("     , R.NOMPES     AS REQUERENTENOME  ");
                strSql.AppendLine("     , R.CPFCNPJPES AS RequerenteCpf   ");

                strSql.AppendLine("     , AT.DSCACATIP AS TipoDescricao   ");
                strSql.AppendLine("     , S.NOMPES     AS AssessoriaNome  ");

                strSql.AppendLine("  FROM ACAO         AS A ");
                strSql.AppendLine("  LEFT JOIN PESSOA  AS R  ON A.CODREQ = R.CODPES");   //REQUERENTE
                strSql.AppendLine("  LEFT JOIN PESSOA  AS RF ON A.CODRESFIN = RF.CODPES"); // RESPONSAVEL FINANCEIRO
                strSql.AppendLine("  LEFT JOIN PESSOA  AS S  ON A.CODASS = S.CODPES");  // ASSESSORIA
                strSql.AppendLine("  LEFT JOIN PESSOA  AS RA ON A.CODRES = RA.CODPES"); // RESPONSAVEL
                strSql.AppendLine("  LEFT JOIN ACAO_TIPO AS AT ON A.CODACATIP = AT.CODACATIP ");// TIPO

                strSql.AppendLine(" where A.STAREG <> 'D' ");

                if (pModel.BuscaCodigo != null)
                    strSql.AppendLine(" AND A.CODACA = " + pModel.BuscaCodigo);

                if (pModel.BuscaNumeracao != null)
                    strSql.AppendLine(" AND A.NUMACA LIKE '%" + pModel.BuscaNumeracao + "%'");

                if (pModel.BuscaAnoNumeracao != null)
                    strSql.AppendLine(" AND substring(A.NUMACA,12,4) = '" + pModel.BuscaAnoNumeracao + "'");

                if (pModel.BuscaRequerente != null)
                    strSql.AppendLine(" AND R.NOMPES LIKE '%" + pModel.BuscaRequerente + "%'");

                if (pModel.BuscaAssessoria != null)
                    strSql.AppendLine(" AND S.NOMPES LIKE '%" + pModel.BuscaAssessoria + "%'");

                if (pModel.BuscaDataDe != null && pModel.BuscaDataAte == null)
                    strSql.AppendLine(" AND A.DTACA = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataDe) + "'");

                if (pModel.BuscaDataDe != null && pModel.BuscaDataAte != null)
                    strSql.AppendLine(" AND A.DTACA BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataAte) + "'");

                if (pModel.BuscaTipoAcao != null)
                    strSql.AppendLine(" AND AT.DSCACATIP LIKE '%" + pModel.BuscaTipoAcao.Trim() + "%'");


                if (pModel.BuscaPorUsuarioPermitido != null)
                    strSql.AppendLine("AND EXISTS (SELECT 1 FROM USUARIO_ASSESSORIA UA WHERE UA.CODASS = A.CODASS AND UA.CODUSU = " + pModel.BuscaPorUsuarioPermitido + ")");


                if (pModel.BuscaEhRecebido.Equals("S"))
                    strSql.AppendLine("  AND EXISTS (SELECT 1 FROM CONTAS_RECEBER CONTA WHERE CONTA.INDCONREC = 'S' AND A.CODACA = CONTA.CODORIGEM AND CONTA.CODORICAD = 1) ");

                if (pModel.BuscaNaoRecebido.Equals("S"))
                    strSql.AppendLine("  AND EXISTS (SELECT 1 FROM CONTAS_RECEBER CONTA WHERE (CONTA.INDCONREC = 'N' OR CONTA.INDCONREC IS NULL) AND A.CODACA = CONTA.CODORIGEM AND CONTA.CODORICAD = 1) ");

                if (pModel.BuscaSemAssessoria.Equals("S"))
                    strSql.AppendLine("  AND A.CODASS IS NULL ");

                if (pModel.BuscaMatrizFilial != null && pModel.BuscaMatrizFilial != "0")
                    strSql.AppendLine(" AND A.CODEMP = " + pModel.BuscaMatrizFilial);


                strSql.AppendLine("  order by " + pOrderBy);

                model = conexao.Query<AcaoViewModel>(strSql.ToString());              

            }

            return model.ToList();
        }

        public DataTable PopulaGrid(string pFiltro)
        {
            DataTable dtTabela = new DataTable();
            StringBuilder strSql = new StringBuilder();

            try
            {
                strSql.AppendLine("SELECT A.CODACA AS CODIGO    ");
                strSql.AppendLine("     , A.DTACA  AS DATA ");
                strSql.AppendLine("     , A.NUMACA AS NUMERACAO ");
                strSql.AppendLine("     , A.VLRACA AS VALOR ");

                strSql.AppendLine("     , A.CODREQ    AS COD_REQUERENTE             ");
                strSql.AppendLine("     , R.NOMPES    AS REQUERENTE                 ");

                strSql.AppendLine("     , A.CODRESFIN AS COD_RESPONSAVEL_FINANCEIRO ");
                strSql.AppendLine("     , RF.NOMPES   AS RESPONSAVEL_FINANCEIRO     ");

                strSql.AppendLine("     , A.CODASS    AS COD_ASSESSORIA             ");
                strSql.AppendLine("     , S.NOMPES    AS ASSESSORIA                 ");

                strSql.AppendLine("     , A.CODRES    AS COD_RESPONSAVEL            ");
                strSql.AppendLine("     , RA.NOMPES   AS RESPONSAVEL                ");

                strSql.AppendLine("  FROM ACAO         AS A ");
                strSql.AppendLine("  LEFT JOIN PESSOA  AS R  ON A.CODREQ = R.CODPES");   //REQUERENTE
                strSql.AppendLine("  LEFT JOIN PESSOA  AS RF ON A.CODRESFIN = RF.CODPES"); // RESPONSAVEL FINANCEIRO
                strSql.AppendLine("  LEFT JOIN PESSOA  AS S  ON A.CODASS = S.CODPES");  // ASSESSORIA
                strSql.AppendLine("  LEFT JOIN PESSOA  AS RA ON A.CODRES = RA.CODPES"); // RESPONSAVEL

                strSql.AppendLine(" where A.STAREG <> 'D' ");

                strSql.AppendLine(pFiltro);

                strSql.AppendLine("  order by A.NUMACA ");

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


        public void Excluir(AcaoViewModel pObjBean)
        {
            using (MySqlConnection con = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {
                #region Incluir
                MySqlCommand cmd = null;
                StringBuilder strSql = new StringBuilder();
                string strRetorno = string.Empty;


                try
                {
                    strSql.Append("UPDATE ACAO  ");
                    strSql.Append("   SET STAREG = 'D'");

                    strSql.Append(" WHERE CODACA = @CODIGO");

                    cmd = new MySqlCommand(strSql.ToString(), con);

                    cmd.Parameters.Add(new MySqlParameter("@CODIGO", pObjBean.Codigo));

                    con.Open();

                    cmd.ExecuteNonQuery();

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

        public string GravarAcaoConfirmada(string pUsuario, string pAssessoria)
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
                    strSql.AppendLine("INSERT INTO ACAO_CONFIRMADA ");

                    strSql.AppendLine("      (        ");
                    
                    strSql.AppendLine("        CODUSUINC  ");
                    strSql.AppendLine("      , CODASS     ");
                    strSql.AppendLine("      , DTINC      ");
                    strSql.AppendLine("      , STAREG     ");
                    strSql.AppendLine("      , DTEXPIRA   ");

                    strSql.AppendLine("      )  values (  ");
                    
                    strSql.AppendLine("        @CODUSUINC ");
                    strSql.AppendLine("      , @CODASS    ");
                    strSql.AppendLine("      , NOW()      ");
                    strSql.AppendLine("      , 'I'        ");
                    strSql.AppendLine("      , ADDDATE(NOW(), INTERVAL 3 DAY)");
                                             
                    strSql.AppendLine("      ) ");

                    cmd = new MySqlCommand(strSql.ToString(), con);

                    
                    cmd.Parameters.Add(new MySqlParameter("@CODUSUINC", pUsuario));
                    cmd.Parameters.Add(new MySqlParameter("@CODASS", pAssessoria));

                    con.Open();
                    cmd.ExecuteNonQuery();                    

                    return Funcionalidade.Sequence(con);
                    
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

        
                
        public List<AcaoConfirmadaViewModel> PopulaGridAcaoConfirmada(AcaoConfirmadaViewModel pModel)
        {
            IEnumerable<AcaoConfirmadaViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                StringBuilder strSql = new StringBuilder();

                strSql.AppendLine("SELECT AC.CODACACON AS Codigo    ");                
                strSql.AppendLine("     , AC.DTINC     AS Data      ");               

                strSql.AppendLine("     , AC.DTEXPIRA  AS ExpiraEm   ");
                //strSql.AppendLine("     , R.NOMPES     AS REQUERENTENOME  ");
                //strSql.AppendLine("     , R.CPFCNPJPES AS RequerenteCpf   ");

                //strSql.AppendLine("     , AT.DSCACATIP AS TipoDescricao   ");
                strSql.AppendLine("     , S.NOMPES     AS AssessoriaNome  ");
                strSql.AppendLine("     , S.EMAPES     AS AssessoriaEmail");
                strSql.AppendLine("     , AC.INDENCERRADA AS Encerrada");

                strSql.AppendLine("  FROM ACAO_CONFIRMADA AS AC ");
                strSql.AppendLine("  LEFT JOIN PESSOA  AS S  ON AC.CODASS = S.CODPES");  // ASSESSORIA


                strSql.AppendLine(" where AC.STAREG <> 'D' ");                

                if (pModel.Codigo > 0)
                    strSql.AppendLine(" AND AC.CODACACON = " + pModel.Codigo);
                
                if (pModel.BuscaDataDe != null && pModel.BuscaDataAte != null)
                    strSql.AppendLine(" AND AC.DTINC BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataAte) + "'");

                if (pModel.BuscaConfirmada != null & pModel.BuscaConfirmada == "S" && pModel.BuscaNaoConfirmada.Equals(""))
                    strSql.AppendLine(" AND AC.INDENCERRADA = 'S' ");

                if (pModel.BuscaNaoConfirmada != null && pModel.BuscaNaoConfirmada.Equals("S") && pModel.BuscaConfirmada.Equals(""))
                    strSql.AppendLine(" AND ( AC.INDENCERRADA = 'N'  OR AC.INDENCERRADA IS NULL) ");


                strSql.AppendLine("  order by AC.DTINC ");

                model = conexao.Query<AcaoConfirmadaViewModel>(strSql.ToString());

            }

            return model.ToList();
        }        

        public string UltimaAcaoAhConfirmar(string pCodigo)
        {
            IEnumerable<string> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                try
                {
                    StringBuilder strSql = new StringBuilder();

                    strSql.AppendLine("SELECT A.CODACACON CODIGO ");                

                    strSql.AppendLine("  FROM ACAO_CONFIRMADA AS A ");

                    strSql.AppendLine(" WHERE A.CODASS  = " + pCodigo);
                    strSql.AppendLine("   AND (A.INDENCERRADA = 'N' OR A.INDENCERRADA IS NULL )");

                    strSql.AppendLine("  AND A.CODACACON = (SELECT MAX(AI.CODACACON) FROM ACAO_CONFIRMADA AI WHERE AI.CODASS = A.CODASS AND AI.INDENCERRADA = 'N' OR AI.INDENCERRADA IS NULL) ");

                    model = conexao.Query<string>(strSql.ToString());
                }
                catch (Exception ex)
                {
                }

            }

            return model.FirstOrDefault();
        }

        public string AtualizarAssessoria(string pCodigo)
        {
            using (MySqlConnection con = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {
                #region Incluir
                MySqlCommand cmd = null;
                StringBuilder strSql = new StringBuilder();
                string strRetorno = string.Empty;

                try
                {

                    strSql.AppendLine(" UPDATE ACAO A                                                  ");
                    strSql.AppendLine("    SET A.CODASS = (SELECT AD.CODASS                            ");
                    strSql.AppendLine("                      FROM ACAO_CONFIRMADA_DETALHE AD           ");
                    strSql.AppendLine("                     WHERE AD.CODACA = A.CODACA                 ");
                    strSql.AppendLine("                       AND AD.INDCONFIRMADA = 'S'               ");
                    strSql.AppendLine("                       AND AD.CODACACON = @CODIGOACAOCONFIRMADA ");
                    strSql.AppendLine("  				   )                                            ");
                    strSql.AppendLine("  WHERE EXISTS  (SELECT AD.CODACA                               ");
                    strSql.AppendLine("                   FROM ACAO_CONFIRMADA_DETALHE AD              ");
                    strSql.AppendLine("                                                                ");
                    strSql.AppendLine("                  WHERE AD.CODACA = A.CODACA                    ");
                    strSql.AppendLine("                    AND AD.INDCONFIRMADA = 'S'                  ");
                    strSql.AppendLine("                    AND AD.CODACACON = @CODIGOACAOCONFIRMADA    ");
                    strSql.AppendLine("  			   )                                                ");
                    strSql.AppendLine("    AND A.CODACA > 0                                             ");
                    strSql.AppendLine("    AND A.CODASS IS NULL                                         ");


                    cmd = new MySqlCommand(strSql.ToString(), con);

                    cmd.Parameters.Add(new MySqlParameter("@CODIGOACAOCONFIRMADA", pCodigo));

                    con.Open();
                    cmd.ExecuteNonQuery();

                    return "0";

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


    }
}
