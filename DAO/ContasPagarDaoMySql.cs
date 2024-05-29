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
    public class ContaPagarDaoMySql
    {
        FuncionalidadeDaoMySql Funcionalidade = new FuncionalidadeDaoMySql();

        #region Atribuir Valores
        void AtribuirValores(MySqlCommand cmd, ContasPagarViewModel pObjBean)
        {            
            Funcionalidade.AtribuiValorCampo(cmd, "@CODPES", pObjBean.Favorecido);
            Funcionalidade.AtribuiValorCampo(cmd, "@CODEMP", pObjBean.MatrizFilial);
            Funcionalidade.AtribuiValorCampo(cmd, "@PARCONPAG", pObjBean.ParcelaY);  
            Funcionalidade.AtribuiValorCampo(cmd, "@PARCONPAGDE", pObjBean.ParcelaX);
            Funcionalidade.AtribuiValorCampo(cmd, "@DSCCONPAG", pObjBean.Descricao); 
            Funcionalidade.AtribuiValorCampo(cmd, "@VLRCONPAG", pObjBean.Valor);        
            Funcionalidade.AtribuiValorCampo(cmd, "@VLRCONPAGA", pObjBean.ValorPago);     
            Funcionalidade.AtribuiValorCampo(cmd, "@VLRJURCON", pObjBean.Juros);      
            Funcionalidade.AtribuiValorCampo(cmd, "@VLRDESCON", pObjBean.Desconto);  
            //Funcionalidade.AtribuiValorCampo(cmd, "@DSCNUMDOC", pObjBean.DscDocumento);  
            Funcionalidade.AtribuiValorCampo(cmd, "@DTPAGTO", string.Format("{0:yyyy-MM-dd}", pObjBean.DataPagamento));
            Funcionalidade.AtribuiValorCampo(cmd, "@FIXCONPAG", pObjBean.Fixa != null ? pObjBean.Fixa.ToUpper() : string.Empty);
            //Funcionalidade.AtribuiValorCampo(cmd, "@NUMCHQ", pObjBean.Cheque);

            Funcionalidade.AtribuiValorCampo(cmd, "@VENCONPAG", string.Format("{0:yyyy-MM-dd}", pObjBean.Vencimento));     

            Funcionalidade.AtribuiValorCampo(cmd, "@CODCONBAN", pObjBean.ContaBancaria);
            Funcionalidade.AtribuiValorCampo(cmd, "@CODFORPAGREC", pObjBean.FormaPagamentoRecebimento);

            Funcionalidade.AtribuiValorCampo(cmd, "@OBSCONPAG", pObjBean.Observacao);

            Funcionalidade.AtribuiValorCampo(cmd, "@CODCENCUS", pObjBean.CentroCusto);
            Funcionalidade.AtribuiValorCampo(cmd, "@CODCONCON", pObjBean.ContaContabil);
            Funcionalidade.AtribuiValorCampo(cmd, "@CODTIPGRUPAGREC", pObjBean.TipoCobrancaRecebimento);

            Funcionalidade.AtribuiValorCampo(cmd, "@MESREF", pObjBean.MesReferencia);        
            Funcionalidade.AtribuiValorCampo(cmd, "@ANOREF", pObjBean.AnoReferencia);    
   
            Funcionalidade.AtribuiValorCampo(cmd, "@CODORICAD", pObjBean.OrigemCadastro);
            Funcionalidade.AtribuiValorCampo(cmd, "@CODORIGEM", pObjBean.CodigoOrigem);

            Funcionalidade.AtribuiValorCampo(cmd, "@INDCONPAG", pObjBean.Pago != null ? pObjBean.Pago.ToUpper() : string.Empty);

            Funcionalidade.AtribuiValorCampo(cmd, "@INDSUCCONPAG", pObjBean.Sucumbencia);
            Funcionalidade.AtribuiValorCampo(cmd, "@CODTIPRECPAG", pObjBean.TipoRecebimentoPagamento);
            

            Funcionalidade.AtribuiValorCampo(cmd, "@TELABOTAOQUECHAMOU", pObjBean.TelaBotaoQueChamou);
            Funcionalidade.AtribuiValorCampo(cmd, "@USUARIO", pObjBean.CodigoUsuario);                
            Funcionalidade.AtribuiValorCampo(cmd, "@CODIGO", pObjBean.Codigo);
        
        }
        #endregion

        #region Dml
        public string Gravar(ContasPagarViewModel pObjBean)
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
                        #region Insert
                        strSql.AppendLine("Insert Into CONTAS_PAGAR ( ");

                        strSql.AppendLine("                   CODPES   ");
                        strSql.AppendLine("                 , CODEMP ");
                        strSql.AppendLine("                 , CODCONPAGSEQ ");

                        strSql.AppendLine("                 , PARCONPAG   ");
                        strSql.AppendLine("                 , PARCONPAGDE  ");

                        strSql.AppendLine("                 , DSCCONPAG");
                        strSql.AppendLine("                 , VLRCONPAG   ");
                        strSql.AppendLine("                 , VLRCONPAGA   ");
                        strSql.AppendLine("                 , VLRJURCON");
                        strSql.AppendLine("                 , VLRDESCON");

                        //strSql.AppendLine("                 , DSCNUMDOC");
                        strSql.AppendLine("                 , DTPAGTO");
                        strSql.AppendLine("                 , FIXCONPAG");
                        //strSql.AppendLine("                 , NUMCHQ");

                        strSql.AppendLine("                 , VENCONPAG    ");

                        strSql.AppendLine("                 , CODCONBAN");

                        strSql.AppendLine("                 , CODFORPAGREC");

                        strSql.AppendLine("                 , OBSCONPAG");

                        strSql.AppendLine("                 , CODCENCUS");
                        strSql.AppendLine("                 , CODCONCON");
                        strSql.AppendLine("                 , CODTIPGRUPAGREC"); // TIPO FORMA 

                        strSql.AppendLine("                 , MESREF");
                        strSql.AppendLine("                 , ANOREF");


                        
                        strSql.AppendLine("                 , CODORICAD");
                        strSql.AppendLine("                 , CODORIGEM");


                        strSql.AppendLine("         , INDCONPAG ");
                        strSql.AppendLine("         , INDSUCCONPAG ");
                        strSql.AppendLine("         , TELABOTAOQUECHAMOU ");

                        strSql.AppendLine("         , CODTIPRECPAG");


                        strSql.AppendLine("        , CODUSUINC");
                        strSql.AppendLine("        , STAREG ");                         
                        strSql.AppendLine("        , DTINC ");
                        strSql.AppendLine("        , DTALT ");
                       

                        strSql.Append("  ) ");

                        strSql.Append("  values (   ");

                        strSql.AppendLine("                   @CODPES   ");
                        strSql.AppendLine("                 , @CODEMP ");

                        strSql.AppendLine("                 , NEXTVAL('SEQ_CONTAS_PAGAR" + pObjBean.MatrizFilial + "') ");

                        strSql.AppendLine("                 , @PARCONPAG ");
                        strSql.AppendLine("                 , @PARCONPAGDE ");

                        strSql.AppendLine("                 , @DSCCONPAG ");
                        strSql.AppendLine("                 , @VLRCONPAG ");
                        strSql.AppendLine("                 , @VLRCONPAGA  ");
                        strSql.AppendLine("                 , @VLRJURCON ");
                        strSql.AppendLine("                 , @VLRDESCON ");

                        //strSql.AppendLine("                 , @DSCNUMDOC ");
                        strSql.AppendLine("                 , @DTPAGTO");
                        strSql.AppendLine("                 , @FIXCONPAG");
                        //strSql.AppendLine("                 , @NUMCHQ");

                        strSql.AppendLine("                 , @VENCONPAG   ");

                        strSql.AppendLine("                 , @CODCONBAN");

                        strSql.AppendLine("                 , @CODFORPAGREC");

                        strSql.AppendLine("                 , @OBSCONPAG");

                        strSql.AppendLine("                 , @CODCENCUS");
                        strSql.AppendLine("                 , @CODCONCON");
                        strSql.AppendLine("                 , @CODTIPGRUPAGREC");

                        strSql.AppendLine("                 , @MESREF");
                        strSql.AppendLine("                 , @ANOREF");

                        strSql.AppendLine("                 , @CODORICAD");
                        strSql.AppendLine("                 , @CODORIGEM");

                        strSql.AppendLine("                 , @INDCONPAG");
                        strSql.AppendLine("                 , @INDSUCCONPAG ");
                        strSql.AppendLine("                 , @TELABOTAOQUECHAMOU ");

                        strSql.AppendLine("                 , @CODTIPRECPAG");

                        strSql.AppendLine("                 , @USUARIO");
                        strSql.AppendLine("                 , 'I' ");

                        strSql.AppendLine("  , NOW()");
                        strSql.AppendLine("  , NOW()");

                        strSql.AppendLine("     ) ");


                        cmd = new MySqlCommand(strSql.ToString(), con);

                        AtribuirValores(cmd, pObjBean);

                        con.Open();

                        cmd.ExecuteNonQuery();

                        return Funcionalidade.Sequence(con);
                        #endregion
                    }
                    else
                    {
                        #region Update
                        strSql.AppendLine("UPDATE CONTAS_PAGAR SET ");

                        strSql.AppendLine("                   CODPES      = @CODPES   ");

                        strSql.AppendLine("                 , CODEMP      = @CODEMP ");

                        strSql.AppendLine("                 , PARCONPAG   = @PARCONPAG ");
                        strSql.AppendLine("                 , PARCONPAGDE = @PARCONPAGDE ");

                        strSql.AppendLine("                 , DSCCONPAG  = @DSCCONPAG ");
                        strSql.AppendLine("                 , VLRCONPAG  = @VLRCONPAG ");
                        strSql.AppendLine("                 , VLRCONPAGA = @VLRCONPAGA  ");
                        strSql.AppendLine("                 , VLRJURCON  = @VLRJURCON ");
                        strSql.AppendLine("                 , VLRDESCON  = @VLRDESCON ");

                        //strSql.AppendLine("                 , DSCNUMDOC  = @DSCNUMDOC ");
                        strSql.AppendLine("                 , DTPAGTO    = @DTPAGTO");
                        strSql.AppendLine("                 , FIXCONPAG  = @FIXCONPAG");
                        //strSql.AppendLine("                 , NUMCHQ     = @NUMCHQ");

                        strSql.AppendLine("                 , VENCONPAG  = @VENCONPAG   ");

                        strSql.AppendLine("                 , CODCONBAN  = @CODCONBAN");

                        strSql.AppendLine("                 , CODFORPAGREC  = @CODFORPAGREC");

                        strSql.AppendLine("                 , OBSCONPAG  = @OBSCONPAG");

                        strSql.AppendLine("                 , CODCENCUS  = @CODCENCUS");
                        strSql.AppendLine("                 , CODCONCON  = @CODCONCON");
                        strSql.AppendLine("                 , CODTIPGRUPAGREC  = @CODTIPGRUPAGREC");

                        strSql.AppendLine("                 , MESREF  = @MESREF");
                        strSql.AppendLine("                 , ANOREF  = @ANOREF");



                        strSql.AppendLine("                 , CODORICAD  = @CODORICAD");
                        strSql.AppendLine("                 , CODORIGEM  = @CODORIGEM");


                        strSql.AppendLine("        , INDCONPAG   = @INDCONPAG");

                        strSql.AppendLine("        , INDSUCCONPAG = @INDSUCCONPAG ");

                        strSql.AppendLine("         , CODTIPRECPAG = @CODTIPRECPAG");


                        strSql.AppendLine("        , CODUSUALT  = @USUARIO");
                        strSql.AppendLine("        , STAREG   = 'U'");
                        strSql.AppendLine("        , DTALT = NOW()  ");

                        strSql.AppendLine(" WHERE CODCONPAG = @CODIGO ");

                        cmd = new MySqlCommand(strSql.ToString(), con);

                        AtribuirValores(cmd, pObjBean);

                        con.Open();
                        cmd.ExecuteNonQuery();

                        return pObjBean.Codigo.ToString();
                        #endregion
                    }
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
        #endregion                

        #region Popula DataGrid
        public DataTable PopulaGrid(string pFiltro)
        {
            DataTable dtTabela = new DataTable();
            StringBuilder strSql = new StringBuilder();

            try
            {
                strSql.AppendLine("SELECT CONTA.CODCONPAG AS CODIGO ");
                strSql.AppendLine("     , CONTA.DSCCONPAG AS DESCRICAO ");
                strSql.AppendLine("     , CONTA.DSCNUMDOC AS DOCUMENTO ");

                strSql.AppendLine("     , CONTA.CODPES AS COD_FAVORECIDO");
                strSql.AppendLine("     , P.NOMPES AS FAVORECIDO");

                strSql.AppendLine("     , CONTA.CODTIPGRUPAGREC AS COD_TIPO ");
                //strSql.AppendLine("     , TCR.DSCTIPGRUPAGREC   AS TIPO ");

                strSql.AppendLine("     , CONTA.CODCENCUS  AS COD_CENTRO");
                //strSql.AppendLine("     , CC.DSCCENCUS     AS CENTRO");

                strSql.AppendLine("     , CONTA.CODCONCON  AS COD_CONTABIL");
                //strSql.AppendLine("     , CT.DSCCONCON  AS CONTABIL");

                strSql.AppendLine("     , CONTA.DTPAGTO   AS PAGTO ");
                strSql.AppendLine("     , CONTA.VENCONPAG AS VENCTO");

                strSql.AppendLine("     , CONTA.VLRCONPAG  AS PAGAR");
                strSql.AppendLine("     , CONTA.VLRDESCON  AS DESCONTO");
                strSql.AppendLine("     , CONTA.VLRJURCON  AS JUROS");
                strSql.AppendLine("     , CONTA.VLRCONPAGA AS PAGA");

                strSql.AppendLine("     , CONTA.PARCONPAGDE AS DE");
                strSql.AppendLine("     , CONTA.PARCONPAG   AS ATE");

                strSql.AppendLine("     , CONTA.CODCONBAN AS COD_CONTA_BANCARIA");

                strSql.AppendLine("     , CONTA.CODCONPAG AS COD_CONDICAO_PAGAMENTO");

                strSql.AppendLine("     , CONTA.CODFORPAGREC AS COD_FORMA");

                strSql.AppendLine("     , CONTA.FIXCONPAG AS FIXA");
                strSql.AppendLine("     , CONTA.INDCONPAG AS QUITADA");
                strSql.AppendLine("     , CONTA.NUMCHQ    AS CHEQUE");
                strSql.AppendLine("     , CONTA.STACONPAG AS STATUS");

                strSql.AppendLine("     , CONTA.MESREF AS MES");
                strSql.AppendLine("     , CONTA.ANOREF AS ANO");

                strSql.AppendLine("     , CONTA.CODORIGEM AS COD_ORIGEM");
                strSql.AppendLine("     , CONTA.DSCORIGEM AS ORIGEM");

                strSql.AppendLine("     , CONTA.OBSCONPAG    AS OBS");


                strSql.AppendLine("  FROM  CONTAS_PAGAR AS CONTA  ");
                //strSql.AppendLine("   LEFT JOIN TIPO_GRUPO_PAGAMENTO_RECEBIMENTO AS TCR ON CONTA.CODTIPGRUPAGREC = TCR.CODTIPGRUPAGREC");
                //strSql.AppendLine("   LEFT JOIN CENTRO_CUSTO                     AS CC  ON CONTA.CODCENCUS = CC.CODCENCUS ");
                //strSql.AppendLine("   LEFT JOIN CONTA_CONTABIL                   AS CT  ON CONTA.CODCONCON = CT.CODCONCON  ");
                strSql.AppendLine("   LEFT JOIN PESSOA                           AS P   ON CONTA.CODPES = P.CODPES ");               
                

                strSql.Append(" where CONTA.STAREG <> 'D' ");

                strSql.Append(pFiltro);

                strSql.Append("  order by CONTA.VENCONPAG ");

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

        public List<ContasPagarViewModel> PopulaGrid(ContasPagarViewModel pModel)
        {
            IEnumerable<ContasPagarViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {
                
                StringBuilder strSql = new StringBuilder();

                strSql.AppendLine("SELECT CP.CODCONPAG AS Codigo        ");
                strSql.AppendLine("     , CP.VENCONPAG AS Vencimento    ");
                strSql.AppendLine("     , CP.DSCCONPAG AS Descricao ");
                strSql.AppendLine("     , CP.VLRCONPAG AS Valor         ");
                strSql.AppendLine("     , CP.VLRCONPAGA AS ValorPago ");
                strSql.AppendLine("     , CP.CODPES    AS Favorecido    ");
                strSql.AppendLine("     , P.NOMPES     AS FavorecidoNome");

                strSql.AppendLine("     , CP.MESREF    AS MesReferencia");
                strSql.AppendLine("     , CP.ANOREF    AS AnoReferencia");
                strSql.AppendLine("     , CP.INDCONPAG AS Pago");

                strSql.AppendLine("     , CASE                                                                                                        ");
                strSql.AppendLine("       WHEN CP.CODORICAD = 1 THEN (SELECT P.NOMPES FROM ACAO AI INNER JOIN PESSOA P ON AI.CODREQ = P.CODPES WHERE AI.CODACA = CP.CODORIGEM)          ");
                
                strSql.AppendLine("       ELSE null                                                                                                   ");
                strSql.AppendLine("        END RequerenteNome                                                                                 ");
                strSql.AppendLine("     , CP.CODEMP   AS MatrizFilial");

                strSql.AppendLine("     , PARCONPAG    AS ParcelaY ");
                strSql.AppendLine("     , PARCONPAGDE  AS ParcelaX ");


                strSql.AppendLine("  FROM CONTAS_PAGAR AS CP ");
                strSql.AppendLine("  LEFT JOIN PESSOA         AS P  ON CP.CODPES    = P.CODPES");   //REQUERENTE
                strSql.AppendLine("  LEFT JOIN EMPRESA        AS E  ON CP.CODEMP    = E.CODEMP");
                strSql.AppendLine("  LEFT JOIN CONTA_BANCARIA AS B  ON CP.CODCONBAN = B.CODCONBAN ");

                strSql.AppendLine(" where CP.STAREG <> 'D' ");

                if (pModel.BuscaCodigoDe != null && pModel.BuscaCodigoAte == null)
                    strSql.AppendLine(" AND CP.CODCONPAG = " + pModel.BuscaCodigoDe);

                if (pModel.BuscaCodigoDe != null && pModel.BuscaCodigoAte != null)
                    strSql.AppendLine(" AND CP.CODCONPAG BETWEEN " + pModel.BuscaCodigoDe + " AND " + pModel.BuscaCodigoAte);

                if (pModel.BuscaFavorecido != null)
                     strSql.AppendLine(" AND P.NOMPES LIKE '%" + pModel.BuscaFavorecido + "%'");


                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte == null)
                    strSql.AppendLine(" AND CP.VENCONPAG = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "'");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte != null)
                    strSql.AppendLine(" AND CP.VENCONPAG BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoAte) + "'");



                if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte == null)
                    strSql.AppendLine(" AND CP.DTPAGTO = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoDe) + "'");

                if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte != null)
                    strSql.AppendLine(" AND CP.DTPAGTO BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoAte) + "'");


                if (pModel.BuscaProcesso != null)
                {
                    strSql.AppendLine(" AND  (          ");
                    strSql.AppendLine("         EXISTS (SELECT 1 FROM ACAO AI WHERE AI.CODACA = CP.CODORIGEM AND CP.CODORICAD = 1 AND AI.NUMACA LIKE '%" + pModel.BuscaProcesso + "%' )");
                    strSql.AppendLine("       OR ");

                    strSql.AppendLine("    (SELECT COUNT(*) ");
                    strSql.AppendLine("       FROM ACAO A");
                    strSql.AppendLine("      WHERE 1 = 1");
                    strSql.AppendLine("        AND A.CODACA = CP.CODORIGEM");
                    strSql.AppendLine("        AND CP.CODORICAD = 1");
                    strSql.AppendLine("        AND EXISTS (SELECT 1");
                    strSql.AppendLine("                      FROM PESSOA P");
                    strSql.AppendLine("                     WHERE P.CODPES = A.CODREQ");
                    strSql.AppendLine("                       AND P.NOMPES LIKE '%" + pModel.BuscaProcesso + "%')  ");
                    strSql.AppendLine("      ) > 0");

                    strSql.AppendLine(")");
                }

                if (pModel.BuscaAssessoria != null)
                {
                    strSql.AppendLine(" AND EXISTS           ");
                    strSql.AppendLine("            (SELECT 1 ");
                    strSql.AppendLine("               FROM ACAO A");
                    strSql.AppendLine("              WHERE 1 = 1");
                    strSql.AppendLine("                AND A.CODACA = CP.CODORIGEM");
                    strSql.AppendLine("                AND CP.CODORICAD = 1       ");
                    strSql.AppendLine("                AND EXISTS (SELECT 1       ");
                    strSql.AppendLine("                              FROM PESSOA P");
                    strSql.AppendLine("                             WHERE P.CODPES = A.CODASS");
                    strSql.AppendLine("                               AND P.NOMPES LIKE '%" + pModel.BuscaAssessoria + "%')  ");
                    strSql.AppendLine("              )");

                }

                if (pModel.BuscaProcessoCodigo != null)
                    strSql.AppendLine(" AND EXISTS (SELECT 1 FROM ACAO AI WHERE AI.CODACA = CP.CODORIGEM AND CP.CODORICAD = 1 AND AI.CODACA = " + pModel.BuscaProcessoCodigo + ")");

                if (pModel.BuscaNegarAcaoProcesso != null)
                {
                    if (pModel.BuscaNegarAcaoProcesso.Equals("S"))
                        strSql.AppendLine(" AND NOT EXISTS (SELECT 1 FROM ACAO AI WHERE CP.CODORICAD = 1)");
                    else
                        strSql.AppendLine(" AND EXISTS (SELECT 1 FROM ACAO AI WHERE CP.CODORICAD = 1)");
                }


                if (pModel.BuscaPagoSimNao != null && pModel.BuscaPagoSimNao.Equals("S"))
                    strSql.AppendLine(" AND CP.INDCONPAG = 'S'");

                if (pModel.BuscaPagoSimNao != null && pModel.BuscaPagoSimNao.Equals("C"))
                    strSql.AppendLine(" AND CP.INDCONPAG = 'C'");

                if (pModel.BuscaPagoSimNao != null && pModel.BuscaPagoSimNao.Equals("N"))
                    strSql.AppendLine(" AND (CP.INDCONPAG = 'N' OR CP.INDCONPAG IS NULL)");



                if (pModel.BuscaAcaoTipo != null)
                {
                    strSql.AppendLine(" AND EXISTS (SELECT 1 ");
                    strSql.AppendLine("               FROM systemson10.ACAO AI ");
                    strSql.AppendLine("              INNER JOIN systemson10.ACAO_TIPO AT ON AI.CODACATIP = AT.CODACATIP ");
                    strSql.AppendLine("              WHERE CP.CODORICAD = 1 AND CP.CODORIGEM = AI.CODACA AND AT.DSCACATIP LIKE '%" + pModel.BuscaAcaoTipo + "%'");
                    strSql.AppendLine("            ) ");
                }


                if (pModel.BuscaPorUsuarioPermitido != null)
                {
                    strSql.AppendLine(" AND EXISTS (SELECT 1 ");
                    strSql.AppendLine("               FROM systemson10.ACAO AI ");
                    strSql.AppendLine("              WHERE AI.CODACA = CP.CODORIGEM ");
                    strSql.AppendLine("                AND CP.CODORICAD = 1 ");                    
                    strSql.AppendLine("                AND EXISTS (SELECT 1 ");
                    strSql.AppendLine("                              FROM systemson10.USUARIO_ASSESSORIA UA ");
                    strSql.AppendLine("                             WHERE UA.CODASS = AI.CODASS ");
                    strSql.AppendLine("                               AND UA.CODUSU = " + pModel.BuscaPorUsuarioPermitido);
                    strSql.AppendLine("                           )   ");
                    strSql.AppendLine("          )");
                }


                if( pModel.BuscaDescricao != null)
                    strSql.AppendLine(" AND CP.DSCCONPAG like '%" + pModel.BuscaDescricao + "%'");

                if (pModel.BuscaEhSucumbencia != null) 
                    strSql.AppendLine(" AND CP.INDSUCCONPAG = 'S'");

                if (pModel.BuscaMatrizFilial != null  && pModel.BuscaMatrizFilial != "0")
                    strSql.AppendLine(" AND E.CODEMP = " + pModel.BuscaMatrizFilial);


                if (pModel.BuscaAnoVencimento != null)
                    strSql.AppendLine(" AND DATE_FORMAT(CP.VENCONPAG, '%Y') = '" + pModel.BuscaAnoVencimento);


                if (pModel.BuscaMesVencimento != null)
                    strSql.AppendLine(" AND DATE_FORMAT(CP.VENCONPAG, '%m') = '" + pModel.BuscaMesVencimento);

                if (pModel.BuscaContaBancaria != null)
                    strSql.AppendLine(" AND B.NOMCONBAN like '%" + pModel.BuscaContaBancaria + "%'" );


                if (pModel.BuscaFixa != null && pModel.BuscaFixa.Equals("S"))
                    strSql.AppendLine(" AND CP.FIXCONPAG = 'S'");

                if (pModel.BuscaFixa != null && pModel.BuscaFixa.Equals("N"))
                    strSql.AppendLine(" AND (CP.FIXCONPAG = 'N' OR CP.FIXCONPAG IS NULL)");

                strSql.AppendLine("  order by CP.VENCONPAG DESC ");


                model = conexao.Query<ContasPagarViewModel>(strSql.ToString());
            }

            return model.ToList();
        }

        public ContasPagarViewModel Registro(string pCodigo)
        {
            IEnumerable<ContasPagarViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                try
                {
                    StringBuilder strSql = new StringBuilder();


                    strSql.AppendLine("SELECT CONTA.CODCONPAG AS CODIGO ");
                    strSql.AppendLine("     , CONTA.CODCONPAGSEQ AS SEQUENCIA");
                    strSql.AppendLine("     , CONTA.DSCCONPAG AS DESCRICAO ");
                    //strSql.AppendLine("     , CONTA.DSCNUMDOC AS DOCUMENTO ");

                    strSql.AppendLine("     , CONTA.CODPES AS FAVORECIDO");
                    strSql.AppendLine("     , P.NOMPES AS FavorecidoNome");

                    strSql.AppendLine("     , CONTA.CODTIPGRUPAGREC AS TipoCobrancaRecebimento ");
                    strSql.AppendLine("     , CONTA.CODCENCUS       AS CentroCusto");
                    strSql.AppendLine("     , CONTA.CODCONCON       AS ContaContabil");

                    strSql.AppendLine("     , CONTA.DTPAGTO   AS DataPagamento ");
                    strSql.AppendLine("     , CONTA.VENCONPAG AS Vencimento    ");

                    strSql.AppendLine("     , CONTA.VLRCONPAG  AS Valor     ");

                    strSql.AppendLine("     , CONTA.VLRDESCON  AS Desconto  ");
                    strSql.AppendLine("     , CONTA.VLRJURCON  AS Juros     ");
                    strSql.AppendLine("     , CONTA.VLRCONPAGA AS ValorPago ");

                    //strSql.AppendLine("     , CONTA.PARCONPAGDE AS DE");
                    //strSql.AppendLine("     , CONTA.PARCONPAG   AS ATE");

                    strSql.AppendLine("     , CONTA.CODCONBAN AS ContaBancaria");
                    strSql.AppendLine("     , CB.NOMCONBAN    AS ContaBancariaNome");

                    //strSql.AppendLine("     , CONTA.CODCONPAG AS COD_CONDICAO_PAGAMENTO");

                    strSql.AppendLine("     , CONTA.CODFORPAGREC AS FormaPagamentoRecebimento");

                    strSql.AppendLine("     , CONTA.FIXCONPAG AS Fixa");
                    strSql.AppendLine("     , CONTA.INDCONPAG AS Pago");

                    //strSql.AppendLine("     , CONTA.NUMCHQ    AS CHEQUE");
                    //strSql.AppendLine("     , CONTA.STACONPAG AS STATUS");

                    strSql.AppendLine("     , CONTA.MESREF AS MesReferencia");
                    strSql.AppendLine("     , CONTA.ANOREF AS AnoReferencia");

                    strSql.AppendLine("     , CONTA.CODORICAD AS OrigemCadastro");
                    strSql.AppendLine("     , CONTA.CODORIGEM AS CodigoOrigem");

                    strSql.AppendLine("     , CASE                                                                                                        ");
                    strSql.AppendLine("       WHEN CONTA.CODORICAD = 1 THEN (SELECT AI.CODACASEQ FROM ACAO AI WHERE AI.CODACA = CONTA.CODORIGEM)          ");
                    
                    strSql.AppendLine("       ELSE null                                                                                                   ");
                    strSql.AppendLine("        END CodigoOrigemSequencial                                                                                 ");

                    strSql.AppendLine("     , PARCONPAG   AS ParcelaY ");
                    strSql.AppendLine("     , PARCONPAGDE AS ParcelaX ");


                    strSql.AppendLine("     , CONTA.OBSCONPAG    AS Observacao");

                    strSql.AppendLine("     , CONTA.INDSUCCONPAG    AS Sucumbencia");
                    strSql.AppendLine("     , CONTA.CODEMP          AS MatrizFilial");

                    strSql.AppendLine("     , CONTA.CODTIPRECPAG    AS  TipoRecebimentoPagamento ");


                    strSql.AppendLine("  FROM  CONTAS_PAGAR AS CONTA  ");
                    strSql.AppendLine("   LEFT JOIN TIPO_COBRANCA_RECEBIMENTO        AS TCR ON CONTA.CODTIPGRUPAGREC = TCR.CODTIPCOB");
                    strSql.AppendLine("   LEFT JOIN CENTRO_CUSTO                     AS CC  ON CONTA.CODCENCUS = CC.CODCENCUS ");
                    strSql.AppendLine("   LEFT JOIN CONTA_CONTABIL                   AS CT  ON CONTA.CODCONCON = CT.CODCONCON  ");
                    strSql.AppendLine("   LEFT JOIN PESSOA                           AS P   ON CONTA.CODPES = P.CODPES ");
                    strSql.AppendLine("   LEFT JOIN MES                              AS M   ON CONTA.MESREF = M.CODMES");
                    strSql.AppendLine("   LEFT JOIN CONTA_BANCARIA                   AS CB  ON CONTA.CODCONBAN = CB.CODCONBAN");
                    strSql.AppendLine("   LEFT JOIN FORMA_PAGAMENTO_RECEBIMENTO      AS FP  ON CONTA.CODCONBAN = CB.CODCONBAN");
                    


                    strSql.Append(" where CONTA.STAREG <> 'D' AND CONTA.CODCONPAG = " + pCodigo);                    
                    

                    model = conexao.Query<ContasPagarViewModel>(strSql.ToString());
                }
                catch (Exception ex)
                {
                }

            }

            return model.FirstOrDefault();
        }

        public void Excluir(string pCodigo)
        {
            using (MySqlConnection con = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {
                #region Incluir
                MySqlCommand cmd = null;
                StringBuilder strSql = new StringBuilder();
                string strRetorno = string.Empty;


                try
                {
                    strSql.Append("UPDATE CONTAS_PAGAR  ");
                    strSql.Append("   SET STAREG = 'D'");

                    strSql.Append(" WHERE CODCONPAG = @CODIGO");

                    cmd = new MySqlCommand(strSql.ToString(), con);

                    cmd.Parameters.Add(new MySqlParameter("@CODIGO", pCodigo));

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

        public string BaixaLote(string pCodigo, string pUsuario)
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
                    strSql.AppendLine("UPDATE CONTAS_PAGAR SET ");

                    strSql.AppendLine("          DTPAGTO    = VENCONPAG   ");
                    strSql.AppendLine("        , VLRCONPAGA = VLRCONPAG ");
                    strSql.AppendLine("        , INDCONPAG  = 'S'");

                    strSql.AppendLine("        , CODUSUALT = @USUARIO");
                    strSql.AppendLine("        , STAREG    = 'U' ");
                    strSql.AppendLine("        , DTALT     = NOW() ");

                    strSql.Append(" WHERE CODCONPAG = @CODIGO ");

                    cmd = new MySqlCommand(strSql.ToString(), con);

                    cmd.Parameters.Add(new MySqlParameter("@USUARIO", pUsuario));
                    cmd.Parameters.Add(new MySqlParameter("@CODIGO", pCodigo));

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

        public List<ContasPagarViewModel> ImprimirReceita(ContasPagarViewModel pModel)
        {
            IEnumerable<ContasPagarViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                StringBuilder strSql = new StringBuilder();

                strSql.AppendLine("SELECT E.NOMEMP      EmpresaNome ");
                strSql.AppendLine("     , ATT.DSCACATIP Descricao, ");
                strSql.AppendLine("       DATE_FORMAT(CP.VENCONPAG, '%Y/%m') Vencimento ");
                strSql.AppendLine("     , SUM(CP.VLRCONPAG) Valor ");
                strSql.AppendLine("  FROM CONTAS_PAGAR CP ");
                strSql.AppendLine(" INNER JOIN EMPRESA E ON CP.CODEMP = E.CODEMP ");
                strSql.AppendLine("  LEFT JOIN ACAO A ON CP.CODORIGEM = A.CODACA AND CP.CODORICAD = 1 ");
                strSql.AppendLine("  LEFT JOIN ACAO_TIPO ATT ON A.CODACATIP = ATT.CODACATIP ");

                //strSql.AppendLine(" WHERE CP.VENCONPAG BETWEEN '2020-06-01 00:00:00' AND '2020-8-31 23:00:00' ");
                                
                strSql.AppendLine(" where CP.STAREG <> 'D' ");

                //if (pModel.BuscaFavorecido != null)
                //    strSql.AppendLine(" AND P.NOMPES LIKE '%" + pModel.BuscaFavorecido + "%'");


                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte == null)
                    strSql.AppendLine(" AND CP.VENCONPAG = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "'");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte != null)
                    strSql.AppendLine(" AND CP.VENCONPAG BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoAte) + "'");



                if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte == null)
                    strSql.AppendLine(" AND CP.DTPAGTO = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoDe) + "'");

                if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte != null)
                    strSql.AppendLine(" AND CP.DTPAGTO BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoAte) + "'");


                


                if (pModel.BuscaAcaoTipo != null)
                {
                    strSql.AppendLine(" AND ATT.DSCACATIP LIKE '%" + pModel.BuscaAcaoTipo + "%'");
                }
                
                strSql.AppendLine(" GROUP BY E.NOMEMP ");
                strSql.AppendLine("     , ATT.DSCACATIP,  ");
                strSql.AppendLine("       DATE_FORMAT(CP.VENCONPAG, '%Y%m') ");
                strSql.AppendLine(" ORDER BY DATE_FORMAT(CP.VENCONPAG, '%Y%m'), ATT.DSCACATIP ");



                model = conexao.Query<ContasPagarViewModel>(strSql.ToString());
            }

            return model.ToList();
        }

    }
}
