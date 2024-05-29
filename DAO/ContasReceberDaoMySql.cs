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
    public class ContasReceberDaoMySql
    {
        FuncionalidadeDaoMySql Funcionalidade = new FuncionalidadeDaoMySql();

        #region Atribuir Valores
        void AtribuirValores(MySqlCommand cmd, ContasReceberViewModel pObjBean)
        {

            Funcionalidade.AtribuiValorCampo(cmd, "@CODPES", pObjBean.Cliente);
            Funcionalidade.AtribuiValorCampo(cmd, "@DSCCONREC", pObjBean.Descricao);

            Funcionalidade.AtribuiValorCampo(cmd, "@PARCONRECDE", pObjBean.ParcelaX);
            Funcionalidade.AtribuiValorCampo(cmd, "@PARCONREC", pObjBean.ParcelaY);

            //Funcionalidade.AtribuiValorCampo(cmd, "@DSCNUMDOC", pObjBean.DscDocumento);
            //Funcionalidade.AtribuiValorCampo(cmd, "@DTDOCTO ", pObjBean.DataDocumento);

            Funcionalidade.AtribuiValorCampo(cmd, "@VENCONREC", pObjBean.Vencimento);
            Funcionalidade.AtribuiValorCampo(cmd, "@DTRECCONREC", pObjBean.DataPagamento);

            Funcionalidade.AtribuiValorCampo(cmd, "@VLRCONREC", pObjBean.Valor);
            Funcionalidade.AtribuiValorCampo(cmd, "@JURCONREC", pObjBean.Juros);
            Funcionalidade.AtribuiValorCampo(cmd, "@DESCONREC", pObjBean.Desconto);
            Funcionalidade.AtribuiValorCampo(cmd, "@VLRRECCONREC", pObjBean.ValorPago);

            //Funcionalidade.AtribuiValorCampo(cmd, "@CODTIPGRUPAGREC", pObjBean.TipoCobranca);
            //Funcionalidade.AtribuiValorCampo(cmd, "@CODCENCUS", pObjBean.CentroCusto);
            //Funcionalidade.AtribuiValorCampo(cmd, "@CODCONCON", pObjBean.ContaContabil);
            Funcionalidade.AtribuiValorCampo(cmd, "@CODCONBAN", pObjBean.ContaBancaria);

            Funcionalidade.AtribuiValorCampo(cmd, "@MESREF", pObjBean.MesReferencia);
            Funcionalidade.AtribuiValorCampo(cmd, "@ANOREF", pObjBean.AnoReferencia);

            Funcionalidade.AtribuiValorCampo(cmd, "@CODFORPAGREC", pObjBean.FormaPagamentoRecebimento);


            Funcionalidade.AtribuiValorCampo(cmd, "@OBSCONREC", pObjBean.Observacao);

            Funcionalidade.AtribuiValorCampo(cmd, "@INDCONREC", pObjBean.Pago);


            Funcionalidade.AtribuiValorCampo(cmd, "@CODORIGEM", pObjBean.CodigoOrigem);
            Funcionalidade.AtribuiValorCampo(cmd, "@CODORICAD", pObjBean.OrigemCadastro);


            //Funcionalidade.AtribuiValorCampo(cmd, "@CODCONRECSTA",pObjBean.Status);
            Funcionalidade.AtribuiValorCampo(cmd, "@CODEMP", pObjBean.MatrizFilial);
            //Funcionalidade.AtribuiValorCampo(cmd, "@CODCONPAGREC", pObjBean.Condicao);

            //Funcionalidade.AtribuiValorCampo(cmd, "@VLRMULTA", pObjBean.Multa);
            //Funcionalidade.AtribuiValorCampo(cmd, "@VLRJUROS", pObjBean.JurosBoleto); //PARA INFORMAR NAS INSTRUCOES DO BOLETO
            
            Funcionalidade.AtribuiValorCampo(cmd, "@INDVLRPRI", pObjBean.Principal);
            Funcionalidade.AtribuiValorCampo(cmd, "@CODTIPRECPAG", pObjBean.TipoRecebimentoPagamentoRec);

            Funcionalidade.AtribuiValorCampo(cmd, "@TELABOTAOQUECHAMOU", pObjBean.TelaBotaoQueChamou);

            Funcionalidade.AtribuiValorCampo(cmd, "@USUARIO", pObjBean.CodigoUsuario);            
            Funcionalidade.AtribuiValorCampo(cmd, "@CODIGO", pObjBean.Codigo);

        }
        #endregion

        #region Dml
        public string Gravar(ContasReceberViewModel pObjBean)
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
                        strSql.AppendLine("Insert Into CONTAS_RECEBER( ");

                        strSql.AppendLine("          CODPES");
                        strSql.AppendLine("        , CODCONRECSEQ ");
                        strSql.AppendLine("        , DSCCONREC");

                        strSql.AppendLine("        , PARCONRECDE ");
                        strSql.AppendLine("        , PARCONREC ");

                        strSql.AppendLine("        , VENCONREC ");
                        strSql.AppendLine("        , DTRECCONREC ");

                        strSql.AppendLine("        , VLRCONREC ");
                        strSql.AppendLine("        , JURCONREC ");
                        strSql.AppendLine("        , DESCONREC ");
                        strSql.AppendLine("        , VLRRECCONREC ");

                        //strSql.AppendLine("        , CODTIPGRUPAGREC ");
                        //strSql.AppendLine("        , CODCENCUS ");
                        //strSql.AppendLine("        , CODCONCON ");
                        strSql.AppendLine("        , CODCONBAN ");

                        strSql.AppendLine("        , MESREF");
                        strSql.AppendLine("        , ANOREF");

                        strSql.AppendLine("        , CODFORPAGREC ");

                        strSql.AppendLine("        , OBSCONREC ");



                        strSql.AppendLine("        , INDCONREC ");


                        strSql.AppendLine("        , CODORIGEM ");
                        strSql.AppendLine("        , CODORICAD ");

                        //strSql.AppendLine("        , CODCONRECSTA ");
                        strSql.AppendLine("        , CODEMP       ");
                        //strSql.AppendLine("        , CODCONPAGREC  ");// 29 09 2018

                        //strSql.AppendLine("        , VLRMULTA  ");
                        //strSql.AppendLine("        , VLRJUROS  "); //PARA INFORMAR NAS INSTRUCOES DO BOLETO

                        strSql.AppendLine("        , INDVLRPRI  "); //INDICA SE É VLR PRINCIPAL
                        strSql.AppendLine("        , CODTIPRECPAG");

                        strSql.AppendLine("        , TELABOTAOQUECHAMOU ");

                        strSql.AppendLine("        , CODUSUINC ");
                        strSql.AppendLine("        , STAREG ");
                        strSql.AppendLine("        , DTINC  ");
                        strSql.AppendLine("        , DTALT ");



                        strSql.Append("  ) ");

                        strSql.Append("  values (");

                        strSql.AppendLine("           @CODPES ");

                        strSql.AppendLine("        , NEXTVAL('SEQ_CONTAS_RECEBER" + pObjBean.MatrizFilial + "') ");
                        strSql.AppendLine("        , @DSCCONREC");

                        strSql.AppendLine("        ,  @PARCONRECDE");
                        strSql.AppendLine("        ,  @PARCONREC");

                        //strSql.AppendLine("        ,  @DSCNUMDOC");
                        //strSql.AppendLine("        ,  @DTDOCTO ");

                        strSql.AppendLine("        ,  @VENCONREC");
                        strSql.AppendLine("        ,  @DTRECCONREC");

                        strSql.AppendLine("        ,  @VLRCONREC ");
                        strSql.AppendLine("        ,  @JURCONREC");
                        strSql.AppendLine("        ,  @DESCONREC ");
                        strSql.AppendLine("        ,  @VLRRECCONREC");

                        //strSql.AppendLine("        ,  @CODTIPGRUPAGREC");
                        //strSql.AppendLine("        ,  @CODCENCUS");
                        //strSql.AppendLine("        ,  @CODCONCON");
                        strSql.AppendLine("        ,  @CODCONBAN");

                        strSql.AppendLine("        ,  @MESREF");
                        strSql.AppendLine("        ,  @ANOREF");

                        strSql.AppendLine("        ,  @CODFORPAGREC");
                        strSql.AppendLine("        ,  @OBSCONREC");


                        strSql.AppendLine("        ,  @INDCONREC");



                        strSql.AppendLine("        ,  @CODORIGEM");
                        strSql.AppendLine("        ,  @CODORICAD");

                        //strSql.AppendLine("        ,  @CODCONRECSTA ");
                        strSql.AppendLine("        ,  @CODEMP       ");
                        //strSql.AppendLine("        ,  @CODCONPAGREC  ");// 29 09 2018

                        //strSql.AppendLine("        ,  @VLRMULTA  ");
                        //strSql.AppendLine("        ,  @VLRJUROS  "); //PARA INFORMAR NAS INSTRUCOES DO BOLETO

                        strSql.AppendLine("        , @INDVLRPRI  "); //INDICA SE É VLR PRINCIPAL
                        strSql.AppendLine("                 , @CODTIPRECPAG");

                        strSql.AppendLine("        , @TELABOTAOQUECHAMOU ");

                        strSql.AppendLine("        ,  @USUARIO");

                        strSql.AppendLine("        , 'I' ");
                        strSql.AppendLine("        , NOW() ");
                        strSql.AppendLine("        , NOW() ");



                        strSql.AppendLine("        ) ");


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
                        strSql.AppendLine("UPDATE CONTAS_RECEBER SET ");

                        strSql.AppendLine("          CODPES    = @CODPES ");
                        strSql.AppendLine("        , DSCCONREC = @DSCCONREC");

                        strSql.AppendLine("        , PARCONRECDE = @PARCONRECDE");
                        strSql.AppendLine("        , PARCONREC   = @PARCONREC");

                        //strSql.AppendLine("        , DSCNUMDOC = @DSCNUMDOC");
                        //strSql.AppendLine("        , DTDOCTO = @DTDOCTO ");

                        strSql.AppendLine("        , VENCONREC = @VENCONREC");
                        strSql.AppendLine("        , DTRECCONREC = @DTRECCONREC ");
                        
                        strSql.AppendLine("        , VLRCONREC = @VLRCONREC ");
                        strSql.AppendLine("        , JURCONREC = @JURCONREC");
                        strSql.AppendLine("        , DESCONREC = @DESCONREC ");
                        strSql.AppendLine("        , VLRRECCONREC = @VLRRECCONREC");

                        //strSql.AppendLine("        , CODTIPGRUPAGREC = @CODTIPGRUPAGREC");
                        //strSql.AppendLine("        , CODCENCUS = @CODCENCUS");
                        //strSql.AppendLine("        , CODCONCON = @CODCONCON");
                        strSql.AppendLine("        , CODCONBAN = @CODCONBAN");

                        strSql.AppendLine("        , MESREF = @MESREF");
                        strSql.AppendLine("        , ANOREF = @ANOREF");

                        strSql.AppendLine("        , CODFORPAGREC = @CODFORPAGREC");

                        strSql.AppendLine("        , OBSCONREC = @OBSCONREC");
                        strSql.AppendLine("        , INDCONREC = @INDCONREC");

                        strSql.AppendLine("        , CODORIGEM = @CODORIGEM");
                        strSql.AppendLine("        , CODORICAD = @CODORICAD");

                        //strSql.AppendLine("        ,  VLRMULTA = @VLRMULTA  ");
                        //strSql.AppendLine("        ,  VLRJUROS = @VLRJUROS  "); //PARA INFORMAR NAS INSTRUCOES DO BOLETO

                        strSql.AppendLine("        , INDVLRPRI = @INDVLRPRI  "); //INDICA SE É VLR PRINCIPAL

                        strSql.AppendLine("         , CODTIPRECPAG = @CODTIPRECPAG");

                        strSql.AppendLine("        , CODEMP   = @CODEMP ");

                        strSql.AppendLine("        , CODUSUALT = @USUARIO");

                        strSql.AppendLine("        , STAREG = 'U' ");

                        strSql.AppendLine("        , DTALT  = NOW() ");


                        strSql.Append(" WHERE CODCONREC = @CODIGO ");

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
        public DataTable PopulaGrid(string pFiltro, string pOrderBy)
        {
            DataTable dtTabela = new DataTable();
            StringBuilder strSql = new StringBuilder();

            try
            {
                strSql.AppendLine("SELECT CONTA.CODCONREC   AS CODIGO ");

                strSql.AppendLine("     , CONTA.CODPES      AS COD_PESSOA");
                strSql.AppendLine("     , P.NOMPES          AS CLIENTE ");

                strSql.AppendLine("     , CONTA.DSCCONREC   AS DESCRICAO");
                strSql.AppendLine("     , CONTA.VENCONREC   AS VENCTO");
                strSql.AppendLine("     , CONTA.DTRECCONREC AS RECBTO");

                strSql.AppendLine("     , CONTA.VLRCONREC   AS RECEBER ");
                strSql.AppendLine("     , CONTA.JURCONREC   AS JUROS");
                strSql.AppendLine("     , CONTA.DESCONREC   AS DESCONTO");

                strSql.AppendLine("     , CONTA.VLRRECCONREC AS RECEBIDO ");

                strSql.AppendLine("     , CONTA.CODTIPGRUPAGREC    AS COD_TIPO");
                strSql.AppendLine("     , TCR.DSCTIPGRUPAGREC      AS TIPO ");

                strSql.AppendLine("     , CONTA.CODCENCUS    AS COD_CENTRO");
                strSql.AppendLine("     , CC.DSCCENCUS       AS CENTRO");

                strSql.AppendLine("     , CONTA.CODCONCON    AS COD_CONTA_CONTABIL");
                strSql.AppendLine("     , CT.DSCCONCON       AS CONTABIL");

                strSql.AppendLine("     , CONTA.CODFORPAGREC    AS COD_FORMA");
                strSql.AppendLine("     , FR.DSCFORPAGREC       AS FORMA ");
                strSql.AppendLine("     , FR.CODEXTFAZENDA      AS FORMA_FAZENDA");

                strSql.AppendLine("     , CONTA.INDCONREC    AS RECEBIDA ");

                //strSql.AppendLine("     , DateDiff('y', CONTA.DTRECCONREC , Now())  AS DIAS ");
                strSql.AppendLine("     , NULL  AS DIAS ");


                strSql.AppendLine("     , CONTA.DTINC AS INCLUSAO");
                strSql.AppendLine("     , CONTA.PARCONRECDE AS DE");
                strSql.AppendLine("     , CONTA.PARCONREC  AS ATE");

                strSql.AppendLine("     , CONTA.CODCONBAN AS COD_CONTA_BANCARIA");

                strSql.AppendLine("     , CONTA.MESREF AS MES ");
                strSql.AppendLine("     , CONTA.ANOREF AS ANO ");
                strSql.AppendLine("     , INDCONREC    AS QUITADO");

                strSql.AppendLine("     , CONTA.OBSCONREC AS OBS");

                strSql.AppendLine("     , CONTA.CODORIGEM AS COD_ORIGEM");
                strSql.AppendLine("     , CONTA.DSCORIGEM AS ORIGEM");

                strSql.AppendLine("  FROM CONTAS_RECEBER AS CONTA  ");

                strSql.AppendLine("  LEFT JOIN TIPO_GRUPO_PAGAMENTO_RECEBIMENTO AS TCR ON CONTA.CODTIPGRUPAGREC = TCR.CODTIPGRUPAGREC ");
                strSql.AppendLine("  LEFT JOIN CENTRO_CUSTO                AS CC ON CONTA.CODCENCUS = CC.CODCENCUS ");
                strSql.AppendLine("  LEFT JOIN CONTA_CONTABIL              AS CT ON CONTA.CODCONCON = CT.CODCONCON ");
                strSql.AppendLine("  LEFT JOIN PESSOA                      AS P  ON CONTA.CODPES = P.CODPES ");
                strSql.AppendLine("  LEFT JOIN FORMA_PAGAMENTO_RECEBIMENTO AS FR ON CONTA.CODFORPAGREC = FR.CODFORPAGREC ");

                strSql.AppendLine("  LEFT JOIN CONTA_BANCARIA CB ON CONTA.CODCONBAN = CB.CODCONBAN ");
                strSql.AppendLine("  LEFT JOIN BANCO          B  ON CB.CODBAN = B.CODBAN");
                strSql.AppendLine("  LEFT JOIN PESSOA         PB ON B.CODPES   = PB.CODPES"); //PESSOA BANCO


                strSql.AppendLine(" where CONTA.STAREG <> 'D' ");

                strSql.AppendLine(pFiltro);

                strSql.AppendLine(pOrderBy);

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

        public DataTable PopulaGridBoleto(string pFiltro)
        {
            StringBuilder strSql = new StringBuilder();

            try
            {
                DataTable tabela = new DataTable();

                strSql.Append("SELECT CR.CODCONREC    AS CODIGO ");
                strSql.Append("     , CR.VENCONREC    AS DATA_VENCIMENTO");

                strSql.Append("     , CR.PARCONRECDE  AS DE ");
                strSql.Append("     , CR.PARCONREC    AS ATE ");

                strSql.Append("     , E.CODEMP        AS COD_EMPRESA ");
                strSql.Append("     , E.NOMEMP        AS CEDENTE");
                strSql.Append("     , E.CNPJEMP       AS CEDENTE_CNPJ");
                strSql.Append("     , CB.CARCONBAN    AS CEDENTE_CARTEIRA");
                strSql.Append("     , B.NUMBAN        AS CEDENTE_NUMERO_BANCO");
                strSql.Append("     , CB.AGCONBAN     AS CEDENTE_AGENCIA");
                strSql.Append("     , CB.NUMCONBAN    AS CEDENTE_CONTA_BANCARIA");
                strSql.Append("     , CB.DVCONBAN     AS CEDENTE_CONTA_BANCARIA_DIGITO");
                strSql.Append("     , CB.CONVCONBAN   AS CEDENTE_CONVENIO");



                strSql.Append("     , CR.VLRCONREC    AS VALOR");
                strSql.Append("     , CR.VLRMULTA     AS MULTA_DIA");
                strSql.Append("     , CR.VLRJUROS     AS MULTA");

                strSql.Append("     , CR.CODCONREC AS NOSSO_NUMERO");
                strSql.Append("     , (SELECT NF.CODNFSEQ FROM orgcapenha.NOTA_FISCAL_PRODUTO NF WHERE NF.CODNFPROD = CR.CODORIGEM AND CR.DSCORIGEM = 'NOTA FISCAL PRODUTO') AS NUMERO_DOCUMENTO"); //jfazer subselect da nf com a sequencia

                strSql.Append("     , P.NOMPES     AS SACADO ");
                strSql.Append("     , P.CPFCNPJPES AS SACADO_CNPJ");
              


                strSql.Append("     , EN.ENDERECO             AS SACADO_ENDERCO");
                strSql.Append("     , EN.NUMERO               AS SACADO_NUMERO");
                strSql.Append("     , EN.COMPLEMENTO          AS SACADO_COMPLEMENTO");
                strSql.Append("     , EN.BAIRRO               AS SACADO_BAIRRO");
                strSql.Append("     , EC.NOMCID               AS SACADO_CIDADE");
                strSql.Append("     , EN.CEP                  AS SACADO_CEP");
                strSql.Append("     , ES.SIGEST               AS SACADO_ESTADO");

                strSql.Append("     , CR.DSCORIGEM            AS ORIGEM");
                strSql.Append("     , CR.CODORIGEM            AS CODORIGEM");
                                                             
                strSql.Append("     , CB.CODINST1             AS INSTRUCAO1");
                strSql.Append("     , CB.CODINST2             AS INSTRUCAO2");
                strSql.Append("     , CB.CODINST3             AS INSTRUCAO3");
                                                             
                strSql.Append("     , null                    AS ARQUIVO");
                                                             
                strSql.Append("     , CR.CODLOTREM            AS COD_LOTE_REMESSA");
                
                strSql.Append("     , CB.CODESPECIEDOCTO      AS ESPECIE_DOCTO_CODIGO");                


                strSql.Append("  from CONTAS_RECEBER        CR ");
                strSql.Append("  LEFT JOIN EMPRESA      AS  E ON CR.CODEMP = E.CODEMP ");
                strSql.Append("  LEFT JOIN PESSOA       AS  P ON CR.CODPES = P.CODPES ");               
                
                strSql.Append("  LEFT JOIN  ENDERECO        AS EN ON P.CODPES     = EN.CODPES "); // EM 20 08 2016
                strSql.Append("  LEFT JOIN  CIDADE          AS EC ON EN.CODCID    = EC.CODCID ");
                strSql.Append("  LEFT JOIN  ESTADO          AS ES ON EN.CODEST    = ES.CODEST ");

                strSql.Append("  INNER JOIN LOTE_REMESSA    AS LR ON CR.CODLOTREM = LR.CODLOTREM");
                strSql.Append("  INNER JOIN CONTA_BANCARIA  AS CB ON LR.CODCONBAN = CB.CODCONBAN ");
                strSql.Append("  INNER JOIN  BANCO          AS B  ON CB.CODBAN    = B.CODBAN ");

                strSql.Append(" where CR.stareg <> 'D' "); //
                strSql.Append("   AND LR.INDREMGER IS NULL ");// LOTE PARA PROCESSAR                

                if (pFiltro.Length > 0)
                    strSql.Append(pFiltro);

                strSql.Append("  order by CR.NUMDOC, CR.PARCONRECDE ");

                MySqlDataAdapter da = new MySqlDataAdapter(strSql.ToString(), ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN());

                da.Fill(tabela);

                return tabela;
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

        public void Excluir(ContasReceberViewModel pObjBean)
        {
            using (MySqlConnection con = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {
                #region Incluir
                MySqlCommand cmd = null;
                StringBuilder strSql = new StringBuilder();
                string strRetorno = string.Empty;


                try
                {
                    strSql.Append("UPDATE CONTAS_RECEBER  ");
                    strSql.Append("   SET STAREG = 'D'");

                    strSql.Append(" WHERE CODCONREC = @CODIGO");

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

        public List<ContasReceberViewModel> PopulaGrid(ContasReceberViewModel pModel)
        {
            IEnumerable<ContasReceberViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                StringBuilder strSql = new StringBuilder();

                strSql.AppendLine("SELECT CONTA.CODCONREC    AS Codigo        ");
                strSql.AppendLine("     , CONTA.VENCONREC    AS Vencimento    ");
                strSql.AppendLine("     , CONTA.DSCCONREC    AS Descricao     ");
                strSql.AppendLine("     , CONTA.VLRCONREC    AS Valor         ");
                strSql.AppendLine("     , CONTA.VLRRECCONREC AS ValorPago ");
                strSql.AppendLine("     , CONTA.CODPES       AS Cliente       ");
                strSql.AppendLine("     , P.NOMPES           AS ClienteNome   ");
                
                strSql.AppendLine("     , CONTA.MESREF       AS MesReferencia ");
                strSql.AppendLine("     , CONTA.ANOREF       AS AnoReferencia ");
                strSql.AppendLine("     , CONTA.INDCONREC    AS Pago");

                strSql.AppendLine("  FROM CONTAS_RECEBER      AS CONTA ");
                strSql.AppendLine("  LEFT JOIN PESSOA         AS P  ON CONTA.CODPES    = P.CODPES");
                strSql.AppendLine("  LEFT JOIN CONTA_BANCARIA AS B  ON CONTA.CODCONBAN = B.CODCONBAN ");


                strSql.AppendLine(" where CONTA.STAREG <> 'D' ");

                if (pModel.BuscaCliente != null)
                    strSql.AppendLine(" AND P.NOMPES LIKE '%" + pModel.BuscaCliente + "%'");

                if (pModel.BuscaDescricao != null)
                    strSql.AppendLine(" AND CONTA.DSCCONREC LIKE '%" + pModel.BuscaDescricao + "%'");
              

                if (pModel.BuscaAssessoria != null)
                {
                    strSql.AppendLine("  AND EXISTS (SELECT 1 FROM ACAO A LEFT JOIN PESSOA AS ASS ON A.CODASS = ASS.CODPES WHERE A.CODACA = CONTA.CODORIGEM AND CONTA.CODORICAD = 1 ");
                    strSql.AppendLine("  AND ASS.NOMPES LIKE '%" + pModel.BuscaAssessoria + "%' ) ");
                }


                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte == null)
                    strSql.AppendLine(" AND CONTA.VENCONREC = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "'");

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte != null)
                    strSql.AppendLine(" AND CONTA.VENCONREC BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoAte) + "'");



                if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte == null)
                    strSql.AppendLine(" AND CONTA.DTRECCONREC = '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoDe) + "'");

                if (pModel.BuscaDataPagamentoDe != null && pModel.BuscaDataPagamentoAte != null)
                    strSql.AppendLine(" AND CONTA.DTRECCONREC BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataPagamentoAte) + "'");

                if (pModel.BuscaProcesso != null)
                    strSql.AppendLine(" AND EXISTS (SELECT 1 FROM systemson10.ACAO AI WHERE AI.CODACA = CONTA.CODORIGEM AND CONTA.CODORICAD = 1 AND AI.NUMACA LIKE '%" + pModel.BuscaProcesso + "%' )");

                if (pModel.BuscaProcessoCodigo != null)
                    strSql.AppendLine(" AND EXISTS (SELECT 1 FROM systemson10.ACAO AI WHERE AI.CODACA = CONTA.CODORIGEM AND CONTA.CODORICAD = 1 AND AI.CODACA = " + pModel.BuscaProcessoCodigo + ")");


                if (pModel.BuscaAcaoTipo != null)
                {
                    strSql.AppendLine(" AND EXISTS (SELECT 1 ");
                    strSql.AppendLine("               FROM systemson10.ACAO AI ");
                    strSql.AppendLine("              INNER JOIN systemson10.ACAO_TIPO AT ON AI.CODACATIP = AT.CODACATIP ");
                    strSql.AppendLine("              WHERE CONTA.CODORICAD = 1 AND CONTA.CODORIGEM = AI.CODACA AND AT.DSCACATIP LIKE '%" + pModel.BuscaAcaoTipo + "%'");
                    strSql.AppendLine("            ) ");
                }

                if (pModel.BuscaPorUsuarioPermitido != null)
                {
                    strSql.AppendLine(" AND EXISTS (SELECT 1 ");
                    strSql.AppendLine("               FROM systemson10.ACAO AI ");
                    strSql.AppendLine("              WHERE AI.CODACA = CONTA.CODORIGEM ");
                    strSql.AppendLine("                AND CONTA.CODORICAD = 1 ");
                    strSql.AppendLine("                AND EXISTS (SELECT 1 ");
                    strSql.AppendLine("                              FROM systemson10.USUARIO_ASSESSORIA UA ");
                    strSql.AppendLine("                             WHERE UA.CODASS = AI.CODASS ");
                    strSql.AppendLine("                               AND UA.CODUSU = " + pModel.BuscaPorUsuarioPermitido);
                    strSql.AppendLine("                           )   ");
                    strSql.AppendLine("          )");
                }

                if (pModel.BuscaPagoSimNao != null && pModel.BuscaPagoSimNao.Equals("S"))
                    strSql.AppendLine(" AND CONTA.INDCONREC = 'S'");

                if (pModel.BuscaPagoSimNao != null && !pModel.BuscaPagoSimNao.Equals("S"))
                    strSql.AppendLine(" AND (CONTA.INDCONREC = 'N' OR CONTA.INDCONREC IS NULL)");

                if (pModel.BuscaContaBancaria != null)
                    strSql.AppendLine(" AND B.NOMCONBAN like '%" + pModel.BuscaContaBancaria + "%'");

                if (pModel.BuscaPrincipal != null && pModel.BuscaPrincipal.Equals("S"))
                    strSql.AppendLine(" AND CONTA.INDVLRPRI = 'S'");

                if(pModel.BuscaMatrizFilial != null && pModel.BuscaMatrizFilial != "0")
                    strSql.AppendLine(" AND CONTA.CODEMP = " + pModel.BuscaMatrizFilial);

                if (pModel.BuscaNegarAcaoProcesso != null)
                {
                    if (pModel.BuscaNegarAcaoProcesso.Equals("S"))
                        strSql.AppendLine(" AND NOT EXISTS (SELECT 1 FROM ACAO AI WHERE AI.CODACA = CONTA.CODORIGEM AND CONTA.CODORICAD = 1 )");
                    else
                        strSql.AppendLine(" AND     EXISTS (SELECT 1 FROM ACAO AI WHERE AI.CODACA = CONTA.CODORIGEM AND CONTA.CODORICAD = 1 )");
                }


                strSql.AppendLine("  order by CONTA.VENCONREC DESC ");


                model = conexao.Query<ContasReceberViewModel>(strSql.ToString());
            }

            return model.ToList();
        }

        public ContasReceberViewModel Registro(string pCodigo)
        {
            IEnumerable<ContasReceberViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                try
                {
                    StringBuilder strSql = new StringBuilder();


                    strSql.AppendLine("SELECT CONTA.CODCONREC    AS Codigo    ");
                    strSql.AppendLine("     , CONTA.CODCONRECSEQ AS Sequencia ");
                    strSql.AppendLine("     , CONTA.DSCCONREC    AS Descricao ");
                    //strSql.AppendLine("     , CONTA.DSCNUMDOC AS DOCUMENTO ");

                    strSql.AppendLine("     , CONTA.CODPES AS Cliente");
                    strSql.AppendLine("     , P.NOMPES     AS ClienteNome");

                    //strSql.AppendLine("     , CONTA.CODTIPGRUPAGREC AS COD_TIPO ");

                    //strSql.AppendLine("     , CONTA.CODCENCUS  AS COD_CENTRO");


                    //strSql.AppendLine("     , CONTA.CODCONCON  AS COD_CONTABIL");


                    strSql.AppendLine("     , CONTA.DTRECCONREC AS DataPagamento ");
                    strSql.AppendLine("     , CONTA.VENCONREC   AS Vencimento    ");

                    strSql.AppendLine("     , CONTA.VLRCONREC   AS Valor     ");

                    strSql.AppendLine("     , CONTA.DESCONREC    AS Desconto  ");
                    strSql.AppendLine("     , CONTA.JURCONREC    AS Juros     ");
                    strSql.AppendLine("     , CONTA.VLRRECCONREC AS ValorPago ");

                    strSql.AppendLine("     , CONTA.PARCONRECDE AS ParcelaX");
                    strSql.AppendLine("     , CONTA.PARCONREC   AS ParcelaY");

                    strSql.AppendLine("     , CONTA.CODCONBAN AS ContaBancaria");
                    strSql.AppendLine("     , CB.NOMCONBAN    AS ContaBancariaNome");

                    //strSql.AppendLine("     , CONTA.CODCONPAG AS COD_CONDICAO_PAGAMENTO");

                    strSql.AppendLine("     , CONTA.CODFORPAGREC AS FormaPagamentoRecebimento");

                    //strSql.AppendLine("     , CONTA.FIXCONPAG AS Fixa");
                    strSql.AppendLine("     , CONTA.INDCONREC AS Pago");

                    //strSql.AppendLine("     , CONTA.NUMCHQ    AS CHEQUE");
                    //strSql.AppendLine("     , CONTA.STACONPAG AS STATUS");

                    strSql.AppendLine("     , CONTA.MESREF AS MesReferencia");
                    strSql.AppendLine("     , CONTA.ANOREF AS AnoReferencia");

                    strSql.AppendLine("     , CONTA.CODORICAD AS OrigemCadastro");
                    strSql.AppendLine("     , CONTA.CODORIGEM AS CodigoOrigem");

                    strSql.AppendLine("     , CONTA.OBSCONREC    AS Observacao");

                    strSql.AppendLine("     , CASE                                                                                              ");
                    strSql.AppendLine("       WHEN CONTA.CODORICAD = 1 THEN (SELECT AI.CODACASEQ FROM ACAO AI WHERE AI.CODACA = CONTA.CODORIGEM) ");
                    //strSql.AppendLine("       WHEN CONTA.CODORICAD = 2 THEN (SELECT AI.CODACASEQ FROM TRANSFERENCIA AI WHERE A.CODACA = CONTA.CODORIGEM) ");
                    strSql.AppendLine("       ELSE null                                                                                         ");
                    strSql.AppendLine("        END CodigoOrigemSequencial                                                                       ");

                    strSql.AppendLine("     , CONTA.INDVLRPRI    AS Principal");
                    strSql.AppendLine("     , CONTA.CODEMP       AS MatrizFilial ");

                    strSql.AppendLine("     , CONTA.CODTIPRECPAG    AS  TipoRecebimentoPagamentoRec ");

                    strSql.AppendLine("  FROM  CONTAS_RECEBER AS CONTA  ");
                    //strSql.AppendLine("   LEFT JOIN TIPO_GRUPO_PAGAMENTO_RECEBIMENTO AS TCR ON CONTA.CODTIPGRUPAGREC = TCR.CODTIPGRUPAGREC");
                    //strSql.AppendLine("   LEFT JOIN CENTRO_CUSTO                     AS CC  ON CONTA.CODCENCUS = CC.CODCENCUS ");
                    //strSql.AppendLine("   LEFT JOIN CONTA_CONTABIL                   AS CT  ON CONTA.CODCONCON = CT.CODCONCON  ");
                    strSql.AppendLine("   LEFT JOIN PESSOA                           AS P   ON CONTA.CODPES = P.CODPES ");
                    strSql.AppendLine("   LEFT JOIN MES                              AS M   ON CONTA.MESREF = M.CODMES");
                    strSql.AppendLine("   LEFT JOIN CONTA_BANCARIA                   AS CB  ON CONTA.CODCONBAN = CB.CODCONBAN");
                    strSql.AppendLine("   LEFT JOIN FORMA_PAGAMENTO_RECEBIMENTO      AS FP  ON CONTA.CODCONBAN = CB.CODCONBAN");
                    strSql.AppendLine("   LEFT JOIN ORIGEM_CADASTRO                  AS OC  ON CONTA.CODORICAD = OC.CODORICAD");


                    strSql.Append(" where CONTA.STAREG <> 'D' AND CONTA.CODCONREC = " + pCodigo);


                    model = conexao.Query<ContasReceberViewModel>(strSql.ToString());
                }
                catch (Exception ex)
                {
                }

            }

            return model.FirstOrDefault();
        }

        #region Dml
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
                    strSql.AppendLine("UPDATE CONTAS_RECEBER SET ");
                              
                    strSql.AppendLine("          DTRECCONREC  = CASE WHEN DTRECCONREC  IS NULL THEN VENCONREC ELSE DTRECCONREC END ");
                    strSql.AppendLine("        , VLRRECCONREC = CASE WHEN VLRRECCONREC IS NULL THEN VLRCONREC ELSE VLRRECCONREC END ");
                    strSql.AppendLine("        , INDCONREC    = 'S'       ");
                    strSql.AppendLine("        , CODUSUALT = @USUARIO     ");
                    strSql.AppendLine("        , STAREG    = 'U' ");
                    strSql.AppendLine("        , DTALT     = NOW() ");

                    strSql.AppendLine("    WHERE CODCONREC = @CODIGO ");

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
        #endregion  

        public List<ContasReceberViewModel> ImprimirReceita(ContasReceberViewModel pModel)
        {
            IEnumerable<ContasReceberViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                StringBuilder strSql = new StringBuilder();

                strSql.AppendLine("SELECT ATT.DSCACATIP                      Descricao "); 
                strSql.AppendLine("     , DATE_FORMAT(CR.VENCONREC, '%Y/%m') Vencimento ");
                strSql.AppendLine("     , SUM(CR.VLRCONREC)                  Valor");
                strSql.AppendLine("  FROM CONTAS_RECEBER CR");
                strSql.AppendLine(" INNER JOIN ACAO A ON CR.CODORIGEM = A.CODACA AND CR.CODORICAD = 1");
                strSql.AppendLine(" INNER JOIN ACAO_TIPO ATT ON A.CODACATIP = ATT.CODACATIP");

                strSql.AppendLine(" WHERE 1 = 1"); //CR.VENCONREC BETWEEN '2020-08-01 00:00:00' AND '2020-8-31 23:00:00'

                if (pModel.BuscaDataVencimentoDe != null && pModel.BuscaDataVencimentoAte != null)
                    strSql.AppendLine(" AND CR.VENCONREC BETWEEN '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoDe) + "' AND '" + string.Format("{0:yyyy-MM-dd}", pModel.BuscaDataVencimentoAte) + "'");


                strSql.AppendLine(" GROUP BY ATT.DSCACATIP, ");
                strSql.AppendLine("          DATE_FORMAT(CR.VENCONREC, '%Y%m')");
                strSql.AppendLine(" ORDER BY DATE_FORMAT(CR.VENCONREC, '%Y%m'), ATT.DSCACATIP");

                model = conexao.Query<ContasReceberViewModel>(strSql.ToString());
            }

            return model.ToList();
        }
    }
}
