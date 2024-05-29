using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;

using BEAN;
using MODELS;

namespace DaoMySql
{
    public class RelatorioGerencialDaoMySql
    {
        FuncionalidadeDaoMySql Funcionalidade = new FuncionalidadeDaoMySql();
              

        #region Popula DataGrid
        public DataTable PopulaGrid(string pFiltro)
        {
            DataTable dtTabela = new DataTable();
            StringBuilder strSql = new StringBuilder();

            try
            {
                strSql.AppendLine(" SELECT CONTAS.* FROM (  ");

                #region Para Sucunbencia por Saldo
                //strSql.AppendLine(" SELECT A.CODACA  AS CODIGO ");
                //strSql.AppendLine("      , A.NUMACA  AS DESCRICAO ");
                //strSql.AppendLine("      , R.NOMPES  AS FAVORECIDO  ");

                //strSql.AppendLine("      , NULL       AS PAGTO ");
                //strSql.AppendLine("      , NULL       AS VENCTO");
                //strSql.AppendLine("      , NULL       AS PAGAR");
                //strSql.AppendLine("      , NULL       AS DESCONTO");
                //strSql.AppendLine("      , NULL       AS JUROS");
                //strSql.AppendLine("      , (SELECT SUM(CR.VLRRECCONREC) FROM CONTAS_RECEBER CR WHERE CR.DSCORIGEM = 'ACAO' AND CR.CODORIGEM = A.CODACA) - (SELECT SUM(CP.VLRCONPAGA) FROM CONTAS_PAGAR CP WHERE CP.DSCORIGEM = 'ACAO' AND CP.CODORIGEM = A.CODACA)  AS PAGA");
                //strSql.AppendLine("      , NULL       AS DE");
                //strSql.AppendLine("      , NULL       AS ATE");
                //strSql.AppendLine("      , NULL       AS QUITADA");

                //strSql.AppendLine("   FROM ACAO A ");
                //strSql.AppendLine("   LEFT JOIN PESSOA  AS R  ON A.CODREQ = R.CODPES");   //REQUERENTE
                #endregion

                //strSql.AppendLine(" union all");

                #region CP
                strSql.AppendLine("SELECT CONTA.CODCONPAG AS CODIGO ");
                strSql.AppendLine("     , CONTA.DSCCONPAG AS DESCRICAO ");              
                
                strSql.AppendLine("     , P.NOMPES        AS FAVORECIDO");
                strSql.AppendLine("     , S.NOMPES        AS ASSESSORIA");

                strSql.AppendLine("     , CONTA.DTPAGTO   AS PAGTO ");
                strSql.AppendLine("     , CONTA.VENCONPAG AS VENCTO");

                strSql.AppendLine("     , CONTA.VLRCONPAG  AS PAGAR");
                strSql.AppendLine("     , CONTA.VLRDESCON  AS DESCONTO");
                strSql.AppendLine("     , CONTA.VLRJURCON  AS JUROS");
                strSql.AppendLine("     , CONTA.VLRCONPAGA AS PAGA");

                strSql.AppendLine("     , CONTA.PARCONPAGDE AS DE");
                strSql.AppendLine("     , CONTA.PARCONPAG   AS ATE");  
                             
                strSql.AppendLine("     , CONTA.INDCONPAG AS QUITADA");

                strSql.AppendLine("     , AI.NUMACA    AS NUMERACAO ");
                strSql.AppendLine("     , 'PAGAR'      AS TIPO  ");

                strSql.AppendLine("  FROM  CONTAS_PAGAR AS CONTA  ");
                //strSql.AppendLine("   LEFT JOIN TIPO_GRUPO_PAGAMENTO_RECEBIMENTO AS TCR ON CONTA.CODTIPGRUPAGREC = TCR.CODTIPGRUPAGREC");
                //strSql.AppendLine("   LEFT JOIN CENTRO_CUSTO                     AS CC  ON CONTA.CODCENCUS = CC.CODCENCUS ");
                //strSql.AppendLine("   LEFT JOIN CONTA_CONTABIL                   AS CT  ON CONTA.CODCONCON = CT.CODCONCON  ");
                strSql.AppendLine("  LEFT JOIN (SELECT * FROM ACAO A) AI ON AI.CODACA = CONTA.CODORIGEM AND CONTA.CODORICAD = 1 ");
                strSql.AppendLine("  LEFT JOIN PESSOA                           AS P   ON CONTA.CODPES = P.CODPES ");
                strSql.AppendLine("  LEFT JOIN PESSOA  AS S  ON AI.CODASS = S.CODPES");  // ASSESSORIA             


                strSql.Append(" where CONTA.STAREG <> 'D' ");

                //strSql.Append(pFiltro);

                //strSql.Append("  order by CONTA.VENCONPAG ");
                #endregion

                strSql.AppendLine(" union all");

                #region CR
                strSql.AppendLine("SELECT CONTA.CODCONREC    AS CODIGO ");
                strSql.AppendLine("     , CONTA.DSCCONREC    AS DESCRICAO");
                strSql.AppendLine("     , P.NOMPES           AS FAVORECIDO "); //CLIENTE

                strSql.AppendLine("     , S.NOMPES        AS ASSESSORIA");

                strSql.AppendLine("     , CONTA.DTRECCONREC  AS PAGTO"); //RECBTO
                strSql.AppendLine("     , CONTA.VENCONREC    AS VENCTO");
                

                strSql.AppendLine("     , CONTA.VLRCONREC    AS RECEBER ");
                strSql.AppendLine("     , CONTA.DESCONREC    AS DESCONTO");
                strSql.AppendLine("     , CONTA.JURCONREC    AS JUROS");
                strSql.AppendLine("     , CONTA.VLRRECCONREC AS RECEBIDO ");

                strSql.AppendLine("     , CONTA.PARCONRECDE  AS DE");
                strSql.AppendLine("     , CONTA.PARCONREC    AS ATE");

           
                strSql.AppendLine("     , INDCONREC    AS QUITADO");
                strSql.AppendLine("     , AI.NUMACA    AS NUMERACAO ");
                strSql.AppendLine("     , 'RECEBER'    AS TIPO  ");


                strSql.AppendLine("  FROM CONTAS_RECEBER AS CONTA  ");

                //strSql.AppendLine("  LEFT JOIN TIPO_GRUPO_PAGAMENTO_RECEBIMENTO AS TCR ON CONTA.CODTIPGRUPAGREC = TCR.CODTIPGRUPAGREC ");
                //strSql.AppendLine("  LEFT JOIN CENTRO_CUSTO                AS CC ON CONTA.CODCENCUS = CC.CODCENCUS ");
                //strSql.AppendLine("  LEFT JOIN CONTA_CONTABIL              AS CT ON CONTA.CODCONCON = CT.CODCONCON ");
                strSql.AppendLine("  LEFT JOIN PESSOA                      AS P  ON CONTA.CODPES = P.CODPES ");
                //strSql.AppendLine("  LEFT JOIN FORMA_PAGAMENTO_RECEBIMENTO AS FR ON CONTA.CODFORPAGREC = FR.CODFORPAGREC ");

                //strSql.AppendLine("  LEFT JOIN CONTA_BANCARIA CB ON CONTA.CODCONBAN = CB.CODCONBAN ");
                //strSql.AppendLine("  LEFT JOIN BANCO          B  ON CB.CODBAN = B.CODBAN");
                strSql.AppendLine("  LEFT JOIN (SELECT * FROM ACAO A) AI ON AI.CODACA = CONTA.CODORIGEM AND CONTA.CODORICAD = 1 ");

                strSql.AppendLine("  LEFT JOIN PESSOA  AS S  ON AI.CODASS = S.CODPES");  // ASSESSORIA  

                strSql.AppendLine(" where CONTA.STAREG <> 'D' ");
                #endregion

                strSql.AppendLine("    ) CONTAS ");

                strSql.AppendLine(" where 1 = 1 ");

                strSql.AppendLine(pFiltro);

                strSql.AppendLine(" ORDER BY CONTAS.VENCTO ");

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

        public DataTable RelatorioGerencial(string pFiltro, string pDtInicialVencto, string pDtFinalVencto, string pDtInicialPagto, string pDtFinalPagto, string pDtInicialRecto, string pDtFinalRecto)
        {
            DataTable dtTabela = new DataTable();
            StringBuilder strSql = new StringBuilder();

            try
            {

                strSql.AppendLine("       SELECT CONTAS.*                                                                                               ");
                strSql.AppendLine("         FROM (                                                                                                 ");
                strSql.AppendLine("                SELECT 'RECEBER'       ORIGEM                                                                   ");
                strSql.AppendLine("                     , CR.CODCONREC    CODIGO                                                                   ");
                strSql.AppendLine("                     , A.NUMACA        NUMERACAO                                                                ");
                strSql.AppendLine("                     , R.NOMPES        FAVORECIDO                                                               ");
                strSql.AppendLine("                     , R.CPFCNPJPES    CPF                                                                      ");
                strSql.AppendLine("                     , CR.VENCONREC    VENCTO                                                                   ");
                strSql.AppendLine("                     , CR.DTRECCONREC  RECBTO                                                                   ");
                strSql.AppendLine("                     , CR.VLRCONREC    PRINCIPAL                                                                ");
                strSql.AppendLine("                     , CR.VLRRECCONREC ATUALIZADO                                                               ");
                strSql.AppendLine("                     , S.NOMPES        ASSESSORIA                                                               ");
                strSql.AppendLine("                     , NULL            SUCUMBENCIA                                                              ");
                strSql.AppendLine("                     , CR.INDVLRPRI    PRCP                                                                     ");
                strSql.AppendLine("                  FROM systemson10.ACAO A                                                                                   ");
                strSql.AppendLine("                  LEFT JOIN systemson10.PESSOA R  ON A.CODREQ = R.CODPES                                        ");
                strSql.AppendLine("                  LEFT JOIN systemson10.PESSOA S  ON A.CODASS = S.CODPES                                        ");
                strSql.AppendLine("                  LEFT JOIN systemson10.CONTAS_RECEBER CR  ON A.CODACA = CR.CODORIGEM AND CR.CODORICAD = 1      ");
                strSql.AppendLine("                                                                                                                ");
                strSql.AppendLine("                UNION ALL                                                                                       ");
                strSql.AppendLine("                                                                                                                ");
                strSql.AppendLine("                SELECT 'PAGAR'         ORIGEM                                                                   ");
                strSql.AppendLine("                     , CP.CODCONPAG    CODIGO                                                                   ");
                strSql.AppendLine("                     , A.NUMACA        NUMERACAO                                                                ");
                strSql.AppendLine("                     , R.NOMPES        FAVORECIDO                                                               ");
                strSql.AppendLine("                     , R.CPFCNPJPES    CPF                                                                      ");
                strSql.AppendLine("                     , CP.VENCONPAG    VENCTO                                                                   ");
                strSql.AppendLine("                     , CP.DTPAGTO      PAGTO                                                                    ");
                strSql.AppendLine("                     , CP.VLRCONPAG    PRINCIPAL                                                                ");
                strSql.AppendLine("                     , CP.VLRCONPAGA   ATUALIZADO                                                               ");
                strSql.AppendLine("                     , S.NOMPES        ASSESSORIA                                                               ");
                strSql.AppendLine("                     , CP.INDSUCCONPAG SUCUMBENCIA                                                              ");
                strSql.AppendLine("                     , NULL            PRCP                                                                     ");
                strSql.AppendLine("                  FROM systemson10.ACAO A                                                                                   ");
                strSql.AppendLine("                  LEFT JOIN systemson10.PESSOA R  ON A.CODREQ = R.CODPES                                        ");
                strSql.AppendLine("                  LEFT JOIN systemson10.PESSOA S  ON A.CODASS = S.CODPES                                        ");
                strSql.AppendLine("                  LEFT JOIN systemson10.CONTAS_PAGAR CP  ON A.CODACA = CP.CODORIGEM AND CP.CODORICAD = 1        ");
                strSql.AppendLine("             ) CONTAS                                                                                           ");
                strSql.AppendLine("         WHERE 1 = 1                                                                                            ");

                strSql.AppendLine(pFiltro);

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

        public List<ResumoViewModel> PopulaGrid(ResumoViewModel pModel)
        {
            IEnumerable<ResumoViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {



                StringBuilder strSql = new StringBuilder();

                #region Antigo
                //strSql.AppendLine("SELECT CP.CODCONPAG AS Codigo        ");
                //strSql.AppendLine("     , CP.VENCONPAG AS Vencimento    ");
                //strSql.AppendLine("     , CP.DSCCONPAG AS Descricao ");
                //strSql.AppendLine("     , CP.VLRCONPAG AS Valor         ");
                //strSql.AppendLine("     , CP.CODPES    AS Favorecido    ");
                //strSql.AppendLine("     , P.NOMPES     AS FavorecidoNome");

                //strSql.AppendLine("     , CP.MESREF    AS MesReferencia");
                //strSql.AppendLine("     , CP.ANOREF    AS AnoReferencia");
                //strSql.AppendLine("     , CP.INDCONPAG AS Pago");

                //strSql.AppendLine("     , CASE                                                                                                        ");
                //strSql.AppendLine("       WHEN CP.CODORICAD = 1 THEN (SELECT P.NOMPES FROM ACAO AI INNER JOIN PESSOA P ON AI.CODREQ = P.CODPES WHERE AI.CODACA = CP.CODORIGEM)          ");

                //strSql.AppendLine("       ELSE null                                                                                                   ");
                //strSql.AppendLine("        END RequerenteNome                                                                                 ");


                //strSql.AppendLine("  FROM CONTAS_PAGAR AS CP ");
                //strSql.AppendLine("  LEFT JOIN PESSOA  AS P  ON CP.CODPES = P.CODPES");   //REQUERENTE

                //strSql.AppendLine(" where CP.STAREG <> 'D' ");

                //if (pModel.BuscaFavorecido != null)
                //    strSql.AppendLine(" AND P.NOMPES LIKE '%" + pModel.BuscaFavorecido + "%'");


                //if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte == null)
                //    strSql.AppendLine(" AND CP.VENCONPAG = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "'");

                //if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte != null)
                //    strSql.AppendLine(" AND CP.VENCONPAG BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoAte) + "'");



                //if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte == null)
                //    strSql.AppendLine(" AND CP.DTPAGTO = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoDe) + "'");

                //if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte != null)
                //    strSql.AppendLine(" AND CP.DTPAGTO BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoAte) + "'");


                //if (pModel.BuscaProcesso != null)
                //    strSql.AppendLine(" AND EXISTS (SELECT 1 FROM systemson10.ACAO AI WHERE AI.CODACA = CP.CODORIGEM AND CP.CODORICAD = 1 AND AI.NUMACA LIKE '%" + pModel.BuscaProcesso + "%' )");

                //if (pModel.BuscaProcessoCodigo != null)
                //    strSql.AppendLine(" AND EXISTS (SELECT 1 FROM systemson10.ACAO AI WHERE AI.CODACA = CP.CODORIGEM AND CP.CODORICAD = 1 AND AI.CODACA = " + pModel.BuscaProcessoCodigo + ")");

                //if (pModel.BuscaNegarAcaoProcesso != null)
                //    strSql.AppendLine(" AND NOT EXISTS (SELECT 1 FROM systemson10.ACAO AI WHERE CP.CODORICAD = 1)");


                //if (pModel.BuscaPagoSimNao != null && pModel.BuscaPagoSimNao.Equals("S"))
                //    strSql.AppendLine(" AND CP.INDCONPAG = 'S'");

                //if (pModel.BuscaPagoSimNao != null && !pModel.BuscaPagoSimNao.Equals("S"))
                //    strSql.AppendLine(" AND (CP.INDCONPAG = 'N' OR CP.INDCONPAG IS NULL)");

                //if (pModel.BuscaAcaoTipo != null)
                //{
                //    strSql.AppendLine(" AND EXISTS (SELECT 1 ");
                //    strSql.AppendLine("               FROM systemson10.ACAO AI ");
                //    strSql.AppendLine("              INNER JOIN systemson10.ACAO_TIPO AT ON AI.CODACATIP = AT.CODACATIP ");
                //    strSql.AppendLine("              WHERE CP.CODORICAD = 1 AND CP.CODORIGEM = AI.CODACA AND AT.DSCACATIP LIKE '%" + pModel.BuscaAcaoTipo + "%'");
                //    strSql.AppendLine("            ) ");
                //}


                //if (pModel.BuscaPorUsuarioPermitido != null)
                //{
                //    strSql.AppendLine(" AND EXISTS (SELECT 1 ");
                //    strSql.AppendLine("               FROM systemson10.ACAO AI ");
                //    strSql.AppendLine("              WHERE AI.CODACA = CP.CODORIGEM ");
                //    strSql.AppendLine("                AND CP.CODORICAD = 1 ");
                //    strSql.AppendLine("                AND EXISTS (SELECT 1 ");
                //    strSql.AppendLine("                              FROM systemson10.USUARIO_ASSESSORIA UA ");
                //    strSql.AppendLine("                             WHERE UA.CODASS = AI.CODASS ");
                //    strSql.AppendLine("                               AND UA.CODUSU = " + pModel.BuscaPorUsuarioPermitido);
                //    strSql.AppendLine("                           )   ");
                //    strSql.AppendLine("          )");
                //}

                //strSql.AppendLine("  order by CP.VENCONPAG DESC ");
                #endregion

                
                strSql.AppendLine("SELECT T.Numeracao                                                                ");
                strSql.AppendLine("     , T.AcaoData                                                                 ");
                strSql.AppendLine("     , T.CodigoAcao  ");
                strSql.AppendLine("     , T.DataRecebimento                                                          ");
                strSql.AppendLine("     , T.Cliente                                                                  ");
                strSql.AppendLine("     , IFNULL(T.SUCUMBENCIA,0) Sucumbencia                                        ");
                strSql.AppendLine("     , T.Pagamentos                                                               ");
                strSql.AppendLine("     , T.Assessoria                                                               ");
                strSql.AppendLine("     , T.Assessoria_Codigo    ");
                strSql.AppendLine("     , IFNULL(T.ASSESSORIA_PAGTOS,0) AssessoriaPagamento                          ");
                strSql.AppendLine("     , T.Recebimento                                                              ");
                strSql.AppendLine("     , T.Recebimento - IFNULL(T.Pagamentos,0) - IFNULL(T.ASSESSORIA_PAGTOS,0) Saldo         ");
                strSql.AppendLine("     , T.Principal                                                                ");
                strSql.AppendLine("     , T.EmpresaNome                                                             ");

                strSql.AppendLine("  FROM (                                                                          ");

                strSql.AppendLine("		SELECT CONTAS.NUMACA   NUMERACAO                                             ");
                strSql.AppendLine("          , CONTAS.DTACA    ACAODATA ");
                strSql.AppendLine("          , CONTAS.CODACA   CODIGOACAO");
                strSql.AppendLine("			 , R.NOMPES        CLIENTE                                               ");
                strSql.AppendLine("			 , R.CPFCNPJPES    CPF                                                   ");

                #region Campo Sucumbencia
                strSql.AppendLine("			 , (SELECT SUM(CP.VLRCONPAG)                                             ");
                strSql.AppendLine("				  FROM CONTAS_PAGAR CP                                               ");
                strSql.AppendLine("				 WHERE CONTAS.CODACA        = CP.CODORIGEM                           ");
                strSql.AppendLine("				   AND CP.CODORICAD    = 1                                           ");
                strSql.AppendLine("				   AND CP.INDSUCCONPAG = 'S'                                         ");
                strSql.AppendLine("				   AND CP.STAREG <> 'D'                                              ");

                if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte == null)
                    strSql.AppendLine(" AND CP.DTPAGTO = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoDe) + "'");

                if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte != null)
                    strSql.AppendLine(" AND CP.DTPAGTO BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoAte) + "'");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte == null)
                    strSql.AppendLine(" AND CP.VENCONPAG = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "'");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte != null)
                    strSql.AppendLine(" AND CP.VENCONPAG BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoAte) + "'");
                

                if (pModel.BuscaAcaoTipo != null)
                {
                    strSql.AppendLine(" AND EXISTS (SELECT 1 ");
                    strSql.AppendLine("               FROM ACAO AI ");
                    strSql.AppendLine("              INNER JOIN ACAO_TIPO AT ON AI.CODACATIP = AT.CODACATIP ");
                    strSql.AppendLine("              WHERE CP.CODORICAD = 1 AND CP.CODORIGEM = AI.CODACA AND AT.DSCACATIP LIKE '%" + pModel.BuscaAcaoTipo + "%'");
                    strSql.AppendLine("            ) ");
                }


                strSql.AppendLine("			   ) SUCUMBENCIA                                                         ");
                #endregion

                #region Campo Pagamentos
                strSql.AppendLine("			 , (SELECT SUM(CP.VLRCONPAG)                                             ");
                strSql.AppendLine("				  FROM CONTAS_PAGAR CP                                               ");
                strSql.AppendLine("				 INNER JOIN PESSOA         PA  ON CP.CODPES = PA.CODPES  ");
                strSql.AppendLine("				 WHERE CONTAS.CODACA   = CP.CODORIGEM                                ");
                strSql.AppendLine("				   AND CP.CODORICAD    = 1                                           ");
                strSql.AppendLine("				   AND IFNULL(CP.INDSUCCONPAG,'N') = 'N'                             ");
                strSql.AppendLine("				   AND CP.STAREG <> 'D'                                              ");
                strSql.AppendLine("				   AND IFNULL(PA.EHASSESSORIA,'N') = 'N'                             ");


                if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte == null)
                    strSql.AppendLine(" AND CP.DTPAGTO = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoDe) + "'");

                if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte != null)
                    strSql.AppendLine(" AND CP.DTPAGTO BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoAte) + "'");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte == null)
                    strSql.AppendLine(" AND CP.VENCONPAG = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "'");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte != null)
                    strSql.AppendLine(" AND CP.VENCONPAG BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoAte) + "'");
                
                if (pModel.BuscaAcaoTipo != null)
                {
                    strSql.AppendLine(" AND EXISTS (SELECT 1 ");
                    strSql.AppendLine("               FROM ACAO AI ");
                    strSql.AppendLine("              INNER JOIN ACAO_TIPO AT ON AI.CODACATIP = AT.CODACATIP ");
                    strSql.AppendLine("              WHERE CP.CODORICAD = 1 AND CP.CODORIGEM = AI.CODACA AND AT.DSCACATIP LIKE '%" + pModel.BuscaAcaoTipo + "%'");
                    strSql.AppendLine("            ) ");
                }

                strSql.AppendLine("			   ) PAGAMENTOS                                                          ");
                #endregion

                strSql.AppendLine("			 , S.CODPES ASSESSORIA_CODIGO                                             ");
                strSql.AppendLine("			 , S.NOMPES ASSESSORIA			                                         ");

                #region Campo Pagamentos para Assessoria
                strSql.AppendLine("			 , (SELECT SUM(CP.VLRCONPAG)                                             ");
                strSql.AppendLine("				  FROM CONTAS_PAGAR CP                                               ");
                strSql.AppendLine("				 INNER JOIN PESSOA         PA  ON CP.CODPES = PA.CODPES              ");
                strSql.AppendLine("				 WHERE CONTAS.CODACA   = CP.CODORIGEM                                ");
                strSql.AppendLine("				   AND CP.CODORICAD    = 1                                           ");
                strSql.AppendLine("				   AND CP.STAREG <> 'D'                                              ");
                strSql.AppendLine("				   AND PA.EHASSESSORIA = 'S'                                         ");

                if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte == null)
                    strSql.AppendLine(" AND CP.DTPAGTO = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoDe) + "'");

                if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte != null)
                    strSql.AppendLine(" AND CP.DTPAGTO BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoAte) + "'");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte == null)
                    strSql.AppendLine(" AND CP.VENCONPAG = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "'");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte != null)
                    strSql.AppendLine(" AND CP.VENCONPAG BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoAte) + "'");

                if (pModel.BuscaAcaoTipo != null)
                {
                    strSql.AppendLine(" AND EXISTS (SELECT 1 ");
                    strSql.AppendLine("               FROM ACAO AI ");
                    strSql.AppendLine("              INNER JOIN ACAO_TIPO AT ON AI.CODACATIP = AT.CODACATIP ");
                    strSql.AppendLine("              WHERE CP.CODORICAD = 1 AND CP.CODORIGEM = AI.CODACA AND AT.DSCACATIP LIKE '%" + pModel.BuscaAcaoTipo + "%'");
                    strSql.AppendLine("            ) ");
                }

                strSql.AppendLine("			   ) ASSESSORIA_PAGTOS			                                         ");
                #endregion

                #region Recebimento
                strSql.AppendLine("			 , (SELECT SUM(CR.VLRRECCONREC)                                          ");//devo pegar o valor recebido
                strSql.AppendLine("				  FROM CONTAS_RECEBER CR                                             ");
                strSql.AppendLine("				 WHERE CONTAS.CODACA = CR.CODORIGEM                                  ");
                strSql.AppendLine("				   AND CR.CODORICAD = 1                                              ");
                strSql.AppendLine("                AND (CR.INDVLRPRI = 'S' OR CR.INDSUCCONPAG = 'S')                                          ");
                strSql.AppendLine("				   AND CR.STAREG <> 'D'                                              ");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte == null)
                    strSql.AppendLine(" AND CR.VENCONREC = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "'");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte != null)
                    strSql.AppendLine(" AND CR.VENCONREC BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoAte) + "'");


                if (pModel.BuscaAcaoTipo != null)
                {
                    strSql.AppendLine(" AND EXISTS (SELECT 1 ");
                    strSql.AppendLine("               FROM ACAO AI ");
                    strSql.AppendLine("              INNER JOIN ACAO_TIPO AT ON AI.CODACATIP = AT.CODACATIP ");
                    strSql.AppendLine("              WHERE CR.CODORICAD = 1 AND CR.CODORIGEM = AI.CODACA AND AT.DSCACATIP LIKE '%" + pModel.BuscaAcaoTipo + "%'");
                    strSql.AppendLine("            ) ");
                }


                strSql.AppendLine("			   ) RECEBIMENTO			                                             ");
                #endregion

                #region Recebimento
                strSql.AppendLine("			 , (SELECT DISTINCT CASE WHEN DATE_FORMAT(CR.DTRECCONREC,'%m/%Y') IS NULL THEN  DATE_FORMAT(CR.VENCONREC,'%m/%Y') ELSE DATE_FORMAT(CR.DTRECCONREC,'%m/%Y') END                                     ");//devo pegar o valor recebido
                strSql.AppendLine("				  FROM CONTAS_RECEBER CR                                             ");
                strSql.AppendLine("				 WHERE CONTAS.CODACA = CR.CODORIGEM                                  ");
                strSql.AppendLine("				   AND CR.CODORICAD = 1                                              ");
                strSql.AppendLine("                AND (CR.INDVLRPRI = 'S' OR CR.INDSUCCONPAG = 'S')                 ");
                strSql.AppendLine("				   AND CR.STAREG <> 'D'                                              ");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte == null)
                    strSql.AppendLine(" AND CR.VENCONREC = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "'");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte != null)
                    strSql.AppendLine(" AND CR.VENCONREC BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoAte) + "'");


                if (pModel.BuscaAcaoTipo != null)
                {
                    strSql.AppendLine(" AND EXISTS (SELECT 1 ");
                    strSql.AppendLine("               FROM ACAO AI ");
                    strSql.AppendLine("              INNER JOIN ACAO_TIPO AT ON AI.CODACATIP = AT.CODACATIP ");
                    strSql.AppendLine("              WHERE CR.CODORICAD = 1 AND CR.CODORIGEM = AI.CODACA AND AT.DSCACATIP LIKE '%" + pModel.BuscaAcaoTipo + "%'");
                    strSql.AppendLine("            ) ");
                }


                strSql.AppendLine("			   ) DataRecebimento			                                             ");
                #endregion

                #region Principal
                strSql.AppendLine("			 , (SELECT SUM(CR.VLRCONREC)                                          ");//devo pegar o valor a receber
                strSql.AppendLine("				  FROM CONTAS_RECEBER CR                                             ");
                strSql.AppendLine("				 WHERE CONTAS.CODACA = CR.CODORIGEM                                  ");
                strSql.AppendLine("				   AND CR.CODORICAD = 1                                              ");
                strSql.AppendLine("                AND CR.INDVLRPRI = 'S'                                            ");
                strSql.AppendLine("				   AND CR.STAREG <> 'D'                                              ");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte == null)
                    strSql.AppendLine(" AND CR.VENCONREC = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "'");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte != null)
                    strSql.AppendLine(" AND CR.VENCONREC BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoAte) + "'");


                if (pModel.BuscaAcaoTipo != null)
                {
                    strSql.AppendLine(" AND EXISTS (SELECT 1 ");
                    strSql.AppendLine("               FROM ACAO AI ");
                    strSql.AppendLine("              INNER JOIN ACAO_TIPO AT ON AI.CODACATIP = AT.CODACATIP ");
                    strSql.AppendLine("              WHERE CR.CODORICAD = 1 AND CR.CODORIGEM = AI.CODACA AND AT.DSCACATIP LIKE '%" + pModel.BuscaAcaoTipo + "%'");
                    strSql.AppendLine("            ) ");
                }


                strSql.AppendLine("			   ) Principal			                                             ");
                strSql.AppendLine("            , CONTAS.CODEMP AS MatrizFilial ");
                strSql.AppendLine("            , E.NOMEMP      AS EmpresaNome  ");
                #endregion

                strSql.AppendLine("		  FROM ACAO CONTAS                                               ");
                strSql.AppendLine("		  LEFT JOIN PESSOA         R  ON CONTAS.CODREQ = R.CODPES        ");//REQUEERENTE
                strSql.AppendLine("		  LEFT JOIN PESSOA         S  ON CONTAS.CODASS = S.CODPES        ");//ASSESSORIA
                strSql.AppendLine("       LEFT JOIN EMPRESA        E  ON CONTAS.CODEMP = E.CODEMP        ");
                strSql.AppendLine("                                                                                  ");
                strSql.AppendLine(" ) T                                                                              ");

                strSql.AppendLine(" where 1 =1                                                                       ");

                strSql.AppendLine("   AND (IFNULL(T.SUCUMBENCIA,0) + IFNULL(T.Pagamentos,0) + IFNULL(T.ASSESSORIA_PAGTOS,0) + IFNULL(T.Recebimento,0)) > 0 ");


                if (pModel.BuscaMatrizFilial != null && pModel.BuscaMatrizFilial != "0")
                    strSql.AppendLine(" AND T.MATRIZFILIAL = " + pModel.BuscaMatrizFilial.Trim());

                if (pModel.BuscaFavorecido != null)
                    strSql.AppendLine(" AND T.CLIENTE LIKE '%" + pModel.BuscaFavorecido.Trim() + "%'");

                if (pModel.BuscaProcesso != null)
                    strSql.AppendLine(" AND T.Numeracao LIKE '%" + pModel.BuscaProcesso.Trim() + "%'");

                if (pModel.BuscaAssessoria != null)
                    strSql.AppendLine(" AND T.Assessoria LIKE '%" + pModel.BuscaAssessoria.Trim() + "%'");

                if (pModel.BuscaPorUsuarioPermitido != null)
                    strSql.AppendLine("AND EXISTS (SELECT 1 FROM USUARIO_ASSESSORIA UA WHERE UA.CODASS = T.Assessoria_Codigo AND UA.CODUSU = " + pModel.BuscaPorUsuarioPermitido + ")");



                model = conexao.Query<ResumoViewModel>(strSql.ToString());
            }

            return model.ToList();
        }

        public List<ResumoViewModel> PopulaGridDetalhado(ResumoViewModel pModel)
        {
            IEnumerable<ResumoViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                StringBuilder strSql = new StringBuilder();




                strSql.AppendLine("SELECT T.Numeracao, T.Pagamentos  ");
                strSql.AppendLine("     , T.AcaoData ");
                strSql.AppendLine("");
                strSql.AppendLine("     , T.Cliente");
                strSql.AppendLine("     , IFNULL(T.SUCUMBENCIA,0) Sucumbencia");
                strSql.AppendLine("     , T.Assessoria_Codigo           ");
                strSql.AppendLine("     , T.Assessoria ");
                strSql.AppendLine("     , IFNULL(T.ASSESSORIA_PAGTOS,0) AssessoriaPagamento           ");
                strSql.AppendLine("     , T.Recebimento                                                              ");
                strSql.AppendLine("     , T.Recebimento - T.Pagamentos - IFNULL(T.ASSESSORIA_PAGTOS,0) Saldo         ");
                strSql.AppendLine("     , T.EmpresaNome                                                             ");
                strSql.AppendLine("  FROM (                                                                          ");

                strSql.AppendLine("		SELECT CONTAS.NUMACA   NUMERACAO                                             ");
                strSql.AppendLine("          , CONTAS.DTACA    ACAODATA ");
                strSql.AppendLine("			 , R.NOMPES        CLIENTE                                               ");
                strSql.AppendLine("			 , R.CPFCNPJPES    CPF                                                   ");

                #region Campo Sucumbencia
                strSql.AppendLine("			 , (SELECT SUM(CP.VLRCONPAG)                                             ");
                strSql.AppendLine("				  FROM CONTAS_PAGAR CP                                   ");
                strSql.AppendLine("				 WHERE CONTAS.CODACA        = CP.CODORIGEM                           ");
                strSql.AppendLine("				   AND CP.CODORICAD    = 1                                           ");
                strSql.AppendLine("				   AND CP.INDSUCCONPAG = 'S'                                         ");
                strSql.AppendLine("				   AND CP.STAREG <> 'D'                                              ");

                if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte == null)
                    strSql.AppendLine(" AND CP.DTPAGTO = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoDe) + "'");

                if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte != null)
                    strSql.AppendLine(" AND CP.DTPAGTO BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoAte) + "'");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte == null)
                    strSql.AppendLine(" AND CP.VENCONPAG = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "'");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte != null)
                    strSql.AppendLine(" AND CP.VENCONPAG BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoAte) + "'");


                if (pModel.BuscaAcaoTipo != null)
                {
                    strSql.AppendLine(" AND EXISTS (SELECT 1 ");
                    strSql.AppendLine("               FROM ACAO AI ");
                    strSql.AppendLine("              INNER JOIN ACAO_TIPO AT ON AI.CODACATIP = AT.CODACATIP ");
                    strSql.AppendLine("              WHERE CP.CODORICAD = 1 AND CP.CODORIGEM = AI.CODACA AND AT.DSCACATIP LIKE '%" + pModel.BuscaAcaoTipo + "%'");
                    strSql.AppendLine("            ) ");
                }


                strSql.AppendLine("			   ) SUCUMBENCIA                                                         ");
                #endregion

                #region Campo Pagamentos
                strSql.AppendLine("			 , (SELECT SUM(CP.VLRCONPAG)                                             ");
                strSql.AppendLine("				  FROM CONTAS_PAGAR CP                                               ");
                strSql.AppendLine("				 INNER JOIN PESSOA         PA  ON CP.CODPES = PA.CODPES  ");
                strSql.AppendLine("				 WHERE CONTAS.CODACA   = CP.CODORIGEM                                ");
                strSql.AppendLine("				   AND CP.CODORICAD    = 1                                           ");
                strSql.AppendLine("				   AND IFNULL(CP.INDSUCCONPAG,'N') = 'N'                             ");
                strSql.AppendLine("				   AND CP.STAREG <> 'D'                                              ");
                strSql.AppendLine("				   AND IFNULL(PA.EHASSESSORIA,'N') = 'N'                             ");


                if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte == null)
                    strSql.AppendLine(" AND CP.DTPAGTO = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoDe) + "'");

                if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte != null)
                    strSql.AppendLine(" AND CP.DTPAGTO BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoAte) + "'");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte == null)
                    strSql.AppendLine(" AND CP.VENCONPAG = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "'");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte != null)
                    strSql.AppendLine(" AND CP.VENCONPAG BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoAte) + "'");

                if (pModel.BuscaAcaoTipo != null)
                {
                    strSql.AppendLine(" AND EXISTS (SELECT 1 ");
                    strSql.AppendLine("               FROM ACAO AI ");
                    strSql.AppendLine("              INNER JOIN ACAO_TIPO AT ON AI.CODACATIP = AT.CODACATIP ");
                    strSql.AppendLine("              WHERE CP.CODORICAD = 1 AND CP.CODORIGEM = AI.CODACA AND AT.DSCACATIP LIKE '%" + pModel.BuscaAcaoTipo + "%'");
                    strSql.AppendLine("            ) ");
                }

                strSql.AppendLine("			   ) PAGAMENTOS                                                          ");
                #endregion

                strSql.AppendLine("			 , S.CODPES ASSESSORIA_CODIGO                                             ");
                strSql.AppendLine("			 , S.NOMPES ASSESSORIA			                                         ");

                #region Campo Pagamentos para Assessoria
                strSql.AppendLine("			 , (SELECT SUM(CP.VLRCONPAG)                                             ");
                strSql.AppendLine("				  FROM CONTAS_PAGAR CP                                               ");
                strSql.AppendLine("				 INNER JOIN PESSOA         PA  ON CP.CODPES = PA.CODPES              ");
                strSql.AppendLine("				 WHERE CONTAS.CODACA   = CP.CODORIGEM                                ");
                strSql.AppendLine("				   AND CP.CODORICAD    = 1                                           ");
                strSql.AppendLine("				   AND CP.STAREG <> 'D'                                              ");
                strSql.AppendLine("				   AND PA.EHASSESSORIA = 'S'                                         ");

                if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte == null)
                    strSql.AppendLine(" AND CP.DTPAGTO = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoDe) + "'");

                if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte != null)
                    strSql.AppendLine(" AND CP.DTPAGTO BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoAte) + "'");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte == null)
                    strSql.AppendLine(" AND CP.VENCONPAG = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "'");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte != null)
                    strSql.AppendLine(" AND CP.VENCONPAG BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoAte) + "'");

                if (pModel.BuscaAcaoTipo != null)
                {
                    strSql.AppendLine(" AND EXISTS (SELECT 1 ");
                    strSql.AppendLine("               FROM ACAO AI ");
                    strSql.AppendLine("              INNER JOIN ACAO_TIPO AT ON AI.CODACATIP = AT.CODACATIP ");
                    strSql.AppendLine("              WHERE CP.CODORICAD = 1 AND CP.CODORIGEM = AI.CODACA AND AT.DSCACATIP LIKE '%" + pModel.BuscaAcaoTipo + "%'");
                    strSql.AppendLine("            ) ");
                }

                strSql.AppendLine("			   ) ASSESSORIA_PAGTOS			                                         ");
                #endregion

                strSql.AppendLine("			 , (SELECT SUM(CR.VLRRECCONREC)                                          ");
                strSql.AppendLine("				  FROM CONTAS_RECEBER CR                                             ");
                strSql.AppendLine("				 WHERE CONTAS.CODACA = CR.CODORIGEM                                  ");
                strSql.AppendLine("				   AND CR.CODORICAD = 1                                              ");
                strSql.AppendLine("				   AND CR.STAREG <> 'D'                                              ");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte == null)
                    strSql.AppendLine(" AND CR.VENCONREC = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "'");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte != null)
                    strSql.AppendLine(" AND CR.VENCONREC BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoAte) + "'");


                if (pModel.BuscaAcaoTipo != null)
                {
                    strSql.AppendLine(" AND EXISTS (SELECT 1 ");
                    strSql.AppendLine("               FROM ACAO AI ");
                    strSql.AppendLine("              INNER JOIN ACAO_TIPO AT ON AI.CODACATIP = AT.CODACATIP ");
                    strSql.AppendLine("              WHERE CR.CODORICAD = 1 AND CR.CODORIGEM = AI.CODACA AND AT.DSCACATIP LIKE '%" + pModel.BuscaAcaoTipo + "%'");
                    strSql.AppendLine("            ) ");
                }


                strSql.AppendLine("			   ) RECEBIMENTO			                                             ");
                strSql.AppendLine("            , CONTAS.CODEMP AS MatrizFilial ");
                strSql.AppendLine("            , E.NOMEMP      AS EmpresaNome  ");


                strSql.AppendLine("		  FROM ACAO CONTAS                                               ");
                strSql.AppendLine("		  LEFT JOIN PESSOA         R  ON CONTAS.CODREQ = R.CODPES        ");//REQUEERENTE
                strSql.AppendLine("		  LEFT JOIN PESSOA         S  ON CONTAS.CODASS = S.CODPES        ");//ASSESSORIA
                strSql.AppendLine("       LEFT JOIN EMPRESA        E  ON CONTAS.CODEMP = E.CODEMP        ");

                strSql.AppendLine("                                                                                  ");
                strSql.AppendLine(" ) T                                                                              ");

                strSql.AppendLine(" where 1 =1                                                                       ");

                strSql.AppendLine("   AND (IFNULL(T.SUCUMBENCIA,0) + IFNULL(T.Pagamentos,0) + IFNULL(T.ASSESSORIA_PAGTOS,0) + IFNULL(T.Recebimento,0)) > 0 ");

                if (pModel.BuscaMatrizFilial != null)
                    strSql.AppendLine(" AND T.MATRIZFILIAL = " + pModel.BuscaMatrizFilial.Trim());


                if (pModel.BuscaFavorecido != null)
                    strSql.AppendLine(" AND T.CLIENTE LIKE '%" + pModel.BuscaFavorecido.Trim() + "%'");

                if (pModel.BuscaProcesso != null)
                    strSql.AppendLine(" AND T.Numeracao LIKE '%" + pModel.BuscaProcesso.Trim() + "%'");

                if (pModel.BuscaAssessoria != null)
                    strSql.AppendLine(" AND T.Assessoria LIKE '%" + pModel.BuscaAssessoria.Trim() + "%'");

                model = conexao.Query<ResumoViewModel>(strSql.ToString());
            }

            return model.ToList();
        }

        public List<ResumoViewModel> PopulaGridDetalhadoII(ResumoViewModel pModel)
        {
            IEnumerable<ResumoViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                StringBuilder strSql = new StringBuilder();

                strSql.AppendLine(" SELECT T.* FROM ( ");

                strSql.AppendLine(" SELECT 'ACAO'      ORIGEM    ");
                strSql.AppendLine("      , A.NUMACA    Numeracao ");
                strSql.AppendLine("      , '01'        ORDEM  ");
                strSql.AppendLine("      , A.DTACA     DataVencimento  ");
                strSql.AppendLine("      , NULL        DataPagamento ");
                strSql.AppendLine("      , NULL        ValorAhPagarReceber ");
                strSql.AppendLine("      , NULL        ValorPagoRecebido ");
                strSql.AppendLine("      , A.CODACA    CODIGO ");
                strSql.AppendLine("      , P.NOMPES    Cliente ");
                                    
                strSql.AppendLine("   FROM ACAO A	");
                strSql.AppendLine("  INNER JOIN PESSOA P ON A.CODREQ = P.CODPES ");
                strSql.AppendLine("  WHERE 1 = 1");
                                
                strSql.AppendLine("  UNION ALL");

                strSql.AppendLine(" SELECT 'RECEBIMENTO'   ORIGEM");
                strSql.AppendLine("      , NULL ");
                strSql.AppendLine("      , '02'            ORDEM ");
                strSql.AppendLine("      , CR.VENCONREC    DataVencimento"); //DATA PAGTO/RECTO
                strSql.AppendLine("      , CR.DTRECCONREC  DataPagamento "); //DATA PAGTO/RECTO
                strSql.AppendLine("      , CR.VLRCONREC    ValorAhPagarReceber ");
                strSql.AppendLine("      , CR.VLRRECCONREC ValorPagoRecebido ");
                strSql.AppendLine("      , CR.CODCONREC    CODIGO ");
                strSql.AppendLine("      , P.NOMPES        Cliente ");
                strSql.AppendLine("   FROM CONTAS_RECEBER CR");
                strSql.AppendLine("  INNER JOIN ACAO      A ON CR.CODORIGEM = A.CODACA AND CR.CODORICAD = 1");
                strSql.AppendLine("  INNER JOIN PESSOA    P ON A.CODREQ = P.CODPES ");
                strSql.AppendLine("  WHERE 1 = 1");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte == null)
                    strSql.AppendLine(" AND CR.VENCONREC = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "'");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte != null)
                    strSql.AppendLine(" AND CR.VENCONREC BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoAte) + "'");


                if (pModel.BuscaAcaoTipo != null)
                {
                    strSql.AppendLine(" AND EXISTS (SELECT 1 ");
                    strSql.AppendLine("               FROM ACAO AI ");
                    strSql.AppendLine("              INNER JOIN ACAO_TIPO AT ON AI.CODACATIP = AT.CODACATIP ");
                    strSql.AppendLine("              WHERE CR.CODORICAD = 1 AND CR.CODORIGEM = AI.CODACA AND AT.DSCACATIP LIKE '%" + pModel.BuscaAcaoTipo + "%'");
                    strSql.AppendLine("            ) ");
                }


                strSql.AppendLine("  UNION ALL");

                strSql.AppendLine(" SELECT 'PAGAMENTO'          ORIGEM");
                strSql.AppendLine("      , NULL");
                strSql.AppendLine("      , '02'          ORDEM");
                strSql.AppendLine("      , CP.VENCONPAG  DataVencimento ");
                strSql.AppendLine("      , CP.DTPAGTO    DataPagamento ");
                strSql.AppendLine("      , (CP.VLRCONPAG  * (-1)) ValorAhPagarReceber ");
                strSql.AppendLine("      , (CP.VLRCONPAGA * (-1)) ValorPagoRecebido ");
                strSql.AppendLine("      , CP.CODCONPAG  CODIGO ");
                strSql.AppendLine("      , P.NOMPES            Cliente ");

                strSql.AppendLine("   FROM CONTAS_PAGAR CP");
                strSql.AppendLine("  INNER JOIN ACAO      A ON CP.CODORIGEM = A.CODACA AND CP.CODORICAD = 1");
                strSql.AppendLine("  INNER JOIN PESSOA    P ON A.CODREQ = P.CODPES ");

                strSql.AppendLine("  WHERE 1 = 1");

                if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte == null)
                    strSql.AppendLine(" AND CP.DTPAGTO = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoDe) + "'");

                if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte != null)
                    strSql.AppendLine(" AND CP.DTPAGTO BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoAte) + "'");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte == null)
                    strSql.AppendLine(" AND CP.VENCONPAG = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "'");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte != null)
                    strSql.AppendLine(" AND CP.VENCONPAG BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoAte) + "'");

                if (pModel.BuscaAcaoTipo != null)
                {
                    strSql.AppendLine(" AND EXISTS (SELECT 1 ");
                    strSql.AppendLine("               FROM ACAO AI ");
                    strSql.AppendLine("              INNER JOIN ACAO_TIPO AT ON AI.CODACATIP = AT.CODACATIP ");
                    strSql.AppendLine("              WHERE CP.CODORICAD = 1 AND CP.CODORIGEM = AI.CODACA AND AT.DSCACATIP LIKE '%" + pModel.BuscaAcaoTipo + "%'");
                    strSql.AppendLine("            ) ");
                }

                strSql.AppendLine("   ) T");
                strSql.AppendLine("  WHERE 1 = 1");


                if (pModel.BuscaFavorecido != null)
                    strSql.AppendLine(" AND T.CLIENTE LIKE '%" + pModel.BuscaFavorecido.Trim() + "%'");

                if (pModel.BuscaProcesso != null)
                    strSql.AppendLine(" AND T.Numeracao LIKE '%" + pModel.BuscaProcesso.Trim() + "%'");


                strSql.AppendLine(" ORDER BY T.DataVencimento, T.Origem");
                strSql.AppendLine("");



                model = conexao.Query<ResumoViewModel>(strSql.ToString());
            }

            return model.ToList();
        }


    }
}
