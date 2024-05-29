using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;
using Dapper;


using MODELS;
using BEAN;

namespace DaoMySql
{

    public class PessoaDaoMySql
    {
        FuncionalidadeDaoMySql Funcionalidade = new FuncionalidadeDaoMySql();

        #region Atribuir Valores
        void AtribuirValores(MySqlCommand cmd, PessoaViewModel pObjBean)
        {
            //Funcionalidade.AtribuiValorCampo(cmd, "@CODSISLEG", pObjBean.CodigoLegado);

            Funcionalidade.AtribuiValorCampo(cmd, "@CODEMP", pObjBean.MatrizFilial);
            Funcionalidade.AtribuiValorCampo(cmd, "@EHREQUERENTE", pObjBean.EhRequerente);
            Funcionalidade.AtribuiValorCampo(cmd, "@EHASSESSORIA", pObjBean.EhAssessoria);
            Funcionalidade.AtribuiValorCampo(cmd, "@EHFORNECEDOR", pObjBean.EhFornecedor);
            Funcionalidade.AtribuiValorCampo(cmd, "@EHFUNCIONARIO", pObjBean.EhFuncionario);

            Funcionalidade.AtribuiValorCampo(cmd, "@NOMPES", pObjBean.Nome);
            Funcionalidade.AtribuiValorCampo(cmd, "@CODUSU", pObjBean.CODUSU);

            Funcionalidade.AtribuiValorCampo(cmd, "@CPFCNPJPES", pObjBean.CpfCnpj);

            Funcionalidade.AtribuiValorCampo(cmd, "@RECEITAPECA", pObjBean.RECEITAPECA);
            Funcionalidade.AtribuiValorCampo(cmd, "@RECEITASERVICO", pObjBean.RECEITASERVICO);
            Funcionalidade.AtribuiValorCampo(cmd, "@RECEITAPNEU", pObjBean.RECEITAPNEU);
            Funcionalidade.AtribuiValorCampo(cmd, "@PORCENTAGEMPECA", pObjBean.PORCENTAGEMPECA);
            Funcionalidade.AtribuiValorCampo(cmd, "@PORCENTAGEMPSERVICO", pObjBean.PORCENTAGEMPSERVICO);
            Funcionalidade.AtribuiValorCampo(cmd, "@PORCENTAGEMPNEU", pObjBean.PORCENTAGEMPNEU);
            Funcionalidade.AtribuiValorCampo(cmd, "@CUSTOVARIAVELPECA", pObjBean.CUSTOVARIAVELPECA);
            Funcionalidade.AtribuiValorCampo(cmd, "@CUSTOVARIAVELPNEU", pObjBean.CUSTOVARIAVELPNEU);
            Funcionalidade.AtribuiValorCampo(cmd, "@INDICEVENDAS", pObjBean.INDICEVENDAS);
            Funcionalidade.AtribuiValorCampo(cmd, "@INDICETICKETMEDIO", pObjBean.INDICETICKETMEDIO);
            Funcionalidade.AtribuiValorCampo(cmd, "@TOTALFATPECA", pObjBean.TOTALFATPECA);
			Funcionalidade.AtribuiValorCampo(cmd, "@TOTALRECEITA", pObjBean.TOTALRECEITA);

			Funcionalidade.AtribuiValorCampo(cmd, "@TELPES", pObjBean.Telefone);
            //Funcionalidade.AtribuiValorCampo(cmd, "@CELPES", pObjBean.Celular);
            Funcionalidade.AtribuiValorCampo(cmd, "@EMAPES", pObjBean.Email);

            Funcionalidade.AtribuiValorCampo(cmd, "@CODPES", pObjBean.Codigo);

        }
		#endregion

		#region Atribuir Valores Pessoa Usuario
		void AtribuirValoresPessoaUsuario(MySqlCommand cmd, PessoaUsuarioViewModel pObjBean)
		{
			//Funcionalidade.AtribuiValorCampo(cmd, "@CODSISLEG", pObjBean.CodigoLegado);

			
			Funcionalidade.AtribuiValorCampo(cmd, "@CODPES", pObjBean.Pessoa);
			Funcionalidade.AtribuiValorCampo(cmd, "@CODUSU", pObjBean.Usuario);

			Funcionalidade.AtribuiValorCampo(cmd, "@CODPESUSU", pObjBean.Codigo);

		}
#endregion

		#region Dml
		public string Gravar(PessoaViewModel pObjBean)
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
                        strSql.Append("Insert Into PESSOA (");

                        strSql.Append("    CODEMP");
                        //strSql.Append("  , CODPESSEQ ");

                        strSql.Append("  , EHREQUERENTE ");
                        strSql.Append("  , EHASSESSORIA ");
                        strSql.Append("  , EHFORNECEDOR ");
                        strSql.Append("  , EHFUNCIONARIO ");

                        strSql.Append("  , NOMPES");
                        //strSql.Append("  , FANPES  ");
                        strSql.Append("  , CPFCNPJPES ");


                        strSql.Append("  , RECEITAPECA ");
                        strSql.Append("  , RECEITASERVICO  ");
                        strSql.Append("  , RECEITAPNEU  ");
                        strSql.Append("  , PORCENTAGEMPECA  ");
                        strSql.Append("  , PORCENTAGEMPSERVICO  ");
                        strSql.Append("  , PORCENTAGEMPNEU  ");
                        strSql.Append("  , CUSTOVARIAVELPECA  ");
                        strSql.Append("  , CUSTOVARIAVELPNEU  ");
                        strSql.Append("  , INDICEVENDAS");
                        strSql.Append("  , INDICETICKETMEDIO");
                        strSql.Append("  , TOTALFATPECA");
						strSql.Append("  , TOTALRECEITA");


						strSql.Append("  , TELPES");
                        //strSql.Append("  , CELPES");
                        strSql.Append("  , EMAPES");

                        strSql.Append("  , STAREG");

                        strSql.Append("  , DTINC");
                        strSql.Append("  , DTALT");

                        strSql.Append(")  values (");

                        strSql.Append("    @CODEMP");
                        // strSql.Append("  , NEXTVAL('SEQ_PESSOA" + pObjBean.MatrizFilial + "') ");

                        strSql.Append("  , @EHREQUERENTE ");
                        strSql.Append("  , @EHASSESSORIA ");
                        strSql.Append("  , @EHFORNECEDOR ");
                        strSql.Append("  , @EHFUNCIONARIO ");

                        strSql.Append("  , @NOMPES");

						strSql.Append("  , @RECEITAPECA ");
						strSql.Append("  , @RECEITASERVICO  ");
						strSql.Append("  , @RECEITAPNEU  ");
						strSql.Append("  , @PORCENTAGEMPECA  ");
						strSql.Append("  , @PORCENTAGEMPSERVICO  ");
						strSql.Append("  , @PORCENTAGEMPNEU  ");
						strSql.Append("  , @CUSTOVARIAVELPECA  ");
						strSql.Append("  , @CUSTOVARIAVELPNEU  ");
						strSql.Append("  , @INDICEVENDAS");
                        strSql.Append("  , @INDICETICKETMEDIO");
                        strSql.Append("  , @TOTALFATPECA");
						strSql.Append("  , @TOTALRECEITA");


						//strSql.Append("  , @INDEHFORNECEDOR ");
						//strSql.Append("  , @INDEHFUNCIONARIO ");
						//strSql.Append("  , @INDEHFABRICANTE ");


						//strSql.Append("  , @FANPES");

						strSql.Append("  , @CPFCNPJPES ");
                        //strSql.Append("  , @RGIEPES ");

                        //strSql.Append("  , @ENDPES  ");
                        //strSql.Append("  , @NUMPES  ");
                        //strSql.Append("  , @COMPES  ");
                        //strSql.Append("  , @BAIPES  ");
                        //strSql.Append("  , @CODCID  ");
                        //strSql.Append("  , @CODEST  ");
                        //strSql.Append("  , @CEPPES  ");

                        //strSql.Append("  , @CODSISLEG");

                        //strSql.Append("  , @CODVEN");

                        strSql.Append("  , @TELPES");
                        //strSql.Append("  , @CELPES");
                        strSql.Append("  , @EMAPES");


                        strSql.Append("  , 'I'");
                        strSql.Append("  , NOW()");
                        strSql.Append("  , NOW()");

                        strSql.Append(")");

                        cmd = new MySqlCommand(strSql.ToString(), con);

                        AtribuirValores(cmd, pObjBean);

                        con.Open();

                        cmd.ExecuteNonQuery();

                        return Funcionalidade.Sequence(con);
                    }
                    else
                    {
                        strSql.Append("UPDATE PESSOA  ");
                        strSql.Append("   SET NOMPES = @NOMPES");

                        strSql.Append("     , EHREQUERENTE  = @EHREQUERENTE ");
                        strSql.Append("     , EHASSESSORIA  = @EHASSESSORIA");
                        strSql.Append("     , EHFORNECEDOR  = @EHFORNECEDOR ");
                        strSql.Append("     , EHFUNCIONARIO = @EHFUNCIONARIO ");

                        //strSql.Append("     , FANPES = @FANPES");

                        strSql.Append("     , CPFCNPJPES = @CPFCNPJPES");

						strSql.Append("  , RECEITAPECA = @RECEITAPECA ");
						strSql.Append("  , RECEITASERVICO = @RECEITASERVICO  ");
						strSql.Append("  , RECEITAPNEU = @RECEITAPNEU  ");
						strSql.Append("  , PORCENTAGEMPECA = @PORCENTAGEMPECA  ");
						strSql.Append("  , PORCENTAGEMPSERVICO = @PORCENTAGEMPSERVICO  ");
						strSql.Append("  , PORCENTAGEMPNEU = @PORCENTAGEMPNEU  ");
						strSql.Append("  , CUSTOVARIAVELPECA = @CUSTOVARIAVELPECA  ");
						strSql.Append("  , CUSTOVARIAVELPNEU = @CUSTOVARIAVELPNEU  ");
						strSql.Append("  , INDICEVENDAS = @INDICEVENDAS");
                        strSql.Append("  , INDICETICKETMEDIO = @INDICETICKETMEDIO");
                        strSql.Append("  , TOTALFATPECA = @TOTALFATPECA");
						strSql.Append("  , TOTALRECEITA = @TOTALRECEITA");

						//strSql.Append("     , CODSISLEG = @CODSISLEG");

						//strSql.Append("     , CODVEN = @CODVEN");

						strSql.Append("     , TELPES = @TELPES");
                        //strSql.Append("     , CELPES = @CELPES");
                        strSql.Append("     , EMAPES = @EMAPES");


                        strSql.Append("     , DTALT = NOW()");

                        strSql.Append("     , STAREG = 'U'");

                        strSql.Append(" WHERE CODPES = @CODPES");

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

		public string Gravarusuario(PessoaUsuarioViewModel pObjBean)
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
						strSql.Append("Insert Into PESSOA_USUARIO (");

						strSql.Append("    CODPES");
						strSql.Append("  , CODUSU ");
						
						strSql.Append(")  values (");

						strSql.Append("    @CODPES");
						strSql.Append("  , @CODUSU ");

						strSql.Append(")");

						cmd = new MySqlCommand(strSql.ToString(), con);

						AtribuirValoresPessoaUsuario(cmd, pObjBean);

						con.Open();

						cmd.ExecuteNonQuery();

						return Funcionalidade.Sequence(con);
					}
					else
					{
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

		public void Excluir(PessoaViewModel pObjBean)
        {
            using (MySqlConnection con = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {
                #region Incluir
                MySqlCommand cmd = null;
                StringBuilder strSql = new StringBuilder();
                string strRetorno = string.Empty;


                try
                {                    
                    strSql.Append("UPDATE PESSOA  ");
                    strSql.Append("   SET STAREG = 'D'");

                    strSql.Append(" WHERE CODPES = @CODIGO");

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
        #endregion        

        #region Popula DataGrid
        public DataTable PopulaGrid(string pFiltro)
        {
            DataTable dtTabela = new DataTable();
            StringBuilder strSql = new StringBuilder();

            try
            {

                strSql.Append("select P.CODPES     AS CODIGO ");
                strSql.Append("     , P.CODPESSEQ  AS SEQUENCIA");
                strSql.Append("     , P.NOMPES     AS NOME ");
                strSql.Append("     , P.FANPES     AS FANTASIA ");
                strSql.Append("     , P.CPFCNPJPES AS CNPJ");
                strSql.Append("     , P.RGIEPES    AS IE");


				strSql.Append("  , P.RECEITAPECA  AS RECEITAPECA");
				strSql.Append("  , P.RECEITASERVICO AS RECEITASERVICO  ");
				strSql.Append("  , P.RECEITAPNEU AS RECEITAPNEU  ");
				strSql.Append("  , P.PORCENTAGEMPECA AS PORCENTAGEMPECA ");
				strSql.Append("  , P.PORCENTAGEMPSERVICO AS PORCENTAGEMPSERVICO  ");
				strSql.Append("  , P.PORCENTAGEMPNEU AS PORCENTAGEMPNEU  ");
				strSql.Append("  , P.CUSTOVARIAVELPECA AS CUSTOVARIAVELPECA  ");
				strSql.Append("  , P.CUSTOVARIAVELPNEU AS CUSTOVARIAVELPNEU  ");
				strSql.Append("  , P.INDICEVENDAS AS INDICEVENDAS");
				strSql.Append("  , P.INDICETICKETMEDIO AS INDICETICKETMEDIO");

				//strSql.Append("     , P.ENDPES  AS ENDERECO");
				//strSql.Append("     , P.NUMPES  AS NUMERO ");
				//strSql.Append("     , P.COMPES  AS COMPLEMENTO");
				//strSql.Append("     , P.BAIPES  AS BAIRRO");
				//strSql.Append("     , P.CODCID  AS COD_CIDADE");
				//strSql.Append("     , P.CODEST  AS COD_ESTADO");
				//strSql.Append("     , P.CEPPES  AS CEP");

				strSql.Append("     , P.TELPES  AS TELEFONE");
                //strSql.Append("     , P.CELPES  AS CELULAR");
                strSql.Append("     , P.EMAPES  AS EMAIL");
                                   
                strSql.Append("     , P.STAREG  AS STAREG ");

                strSql.Append("  FROM PESSOA AS P  ");
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

        #region Pesquisa Cliente
        public DataTable PesquisaCliente(string pFiltro, string pEmpresa)
        {
            MySqlCommand cmd = null;
            DataTable dtTabela = new DataTable();
            MySqlConnection conn = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN();

            try
            {
                String strSql = "SELECT P.CODPES, P.NOMPES FROM PESSOA P WHERE P.CODEMP = @EMPRESA AND P.STAREG <> 'D' AND P.NOMPES LIKE CONCAT('%',@NOMEMP, '%');";
                conn.Open();
                cmd = new MySqlCommand(strSql.ToString(), conn);

                Funcionalidade.AtribuiValorCampo(cmd, "@EMPRESA", pEmpresa);
                Funcionalidade.AtribuiValorCampo(cmd, "@NOMEMP", pFiltro.ToUpper());

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
                String strSql = "SELECT P.* FROM PESSOA P WHERE P.CODPES = @CODIGO AND P.STAREG <> 'D' ";
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

        public void Excluir(string pCodigo)
        {
            using (MySqlConnection con = ConexaoDaoMySql.MySqlClientDSN.getBcoMySql().getConexaoMySqlDaoDSN())
            {
                try
                {
                    con.Open();
                    StringBuilder sql = new StringBuilder();

                    sql.Append("UPDATE PESSOA SET STAREG = 'D' ");
                    sql.Append(" WHERE CODPES = " + pCodigo);

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

                    strSql = "SELECT NOMPES FROM PESSOA WHERE CODPES = @CODIGO";

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

        public List<PessoaViewModel> PopulaCombo()
        {
            IEnumerable<PessoaViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                StringBuilder strSql = new StringBuilder();

                strSql.AppendLine("SELECT M.CODPES AS CODIGO, M.NOMPES AS NOME ");
                strSql.AppendLine("  FROM PESSOA M");
                strSql.AppendLine(" WHERE M.STAREG <> 'D' ");

                strSql.AppendLine(" ORDER BY M.NOMPES");

                model = conexao.Query<PessoaViewModel>(strSql.ToString());

            }

            return model.ToList();
        }

        public List<PessoaViewModel> PopulaGrid(PessoaViewModel pessoa)
        {
            IEnumerable<PessoaViewModel> model = null;

            using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
            {

                StringBuilder strSql = new StringBuilder();

                strSql.AppendLine("SELECT M.CODPES AS Codigo, M.NOMPES AS Nome, CONCAT(substr(M.CPFCNPJPES,1,3),'.',substr(M.CPFCNPJPES,4,3),'.',substr(M.CPFCNPJPES,7,3),'-',substr(M.CPFCNPJPES,10,2)) AS CpfCnpj, M.CODPESSEQ Sequencia ");
                strSql.AppendLine("     , M.EHREQUERENTE AS EhRequerente ");
                strSql.AppendLine("     , M.EHASSESSORIA AS EhAssessoria ");
                strSql.AppendLine("     , M.EHFORNECEDOR AS EhFornecedor");
                strSql.AppendLine("     , M.EHFUNCIONARIO AS EhFuncionario");
                strSql.AppendLine("     , M.EMAPES       AS Email ");
				strSql.AppendLine("     , M.TELPES       AS Telefone ");
				strSql.Append("  , M.RECEITAPECA  AS RECEITAPECA");
				strSql.Append("  , M.RECEITASERVICO AS RECEITASERVICO  ");
				strSql.Append("  , M.RECEITAPNEU AS RECEITAPNEU  ");
				strSql.Append("  , M.PORCENTAGEMPECA AS PORCENTAGEMPECA ");
				strSql.Append("  , M.PORCENTAGEMPSERVICO AS PORCENTAGEMPSERVICO  ");
				strSql.Append("  , M.PORCENTAGEMPNEU AS PORCENTAGEMPNEU  ");
				strSql.Append("  , M.CUSTOVARIAVELPECA AS CUSTOVARIAVELPECA  ");
				strSql.Append("  , M.CUSTOVARIAVELPNEU AS CUSTOVARIAVELPNEU  ");
				strSql.Append("  , M.INDICEVENDAS AS INDICEVENDAS");
				strSql.Append("  , M.INDICETICKETMEDIO AS INDICETICKETMEDIO");
				strSql.AppendLine("  FROM PESSOA M");
                strSql.AppendLine(" WHERE M.STAREG <> 'D' ");

                if (pessoa.BuscaNome != null)
                    strSql.AppendLine(" AND M.NOMPES LIKE '%" + pessoa.BuscaNome +"%'");

                if (pessoa.BuscaCpf != null)
                    strSql.AppendLine(" AND M.CPFCNPJPES LIKE '%" + pessoa.BuscaCpf + "%'");

                if (pessoa.BuscaEhAssessoria != null)
                    strSql.AppendLine(" AND M.EHASSESSORIA = 'S'");

                if (pessoa.BuscaEhRequerente != null)
                    strSql.AppendLine(" AND M.EHREQUERENTE = 'S' ");

                if (pessoa.BuscaEhFuncionario != null)
                    strSql.AppendLine(" AND M.EHFUNCIONARIO = 'S' ");

                strSql.AppendLine(" ORDER BY M.NOMPES");

                model = conexao.Query<PessoaViewModel>(strSql.ToString());

            }

            return model.ToList();
        }

		public List<PessoaViewModel> BuscaUsuarios(PessoaViewModel pessoa)
		{
			IEnumerable<PessoaViewModel> model = null;

			using (MySqlConnection conexao = new MySqlConnection(ConexaoDaoMySql.sqlServerDaoClientDSN.StringDeConexaoClient))
			{

				StringBuilder strSql = new StringBuilder();


                strSql.AppendLine("SELECT P.codpesusu codigo, P.codusu usuario, U.nomusu nome, U.emausu Email FROM PESSOA_USUARIO as P Left join usuario as U on U.codusu=P.codusu");
                strSql.AppendLine(" where P.codpes="+ pessoa.Codigo);


				model = conexao.Query<PessoaViewModel>(strSql.ToString());

			}
			return model.ToList();

		}
	}
}